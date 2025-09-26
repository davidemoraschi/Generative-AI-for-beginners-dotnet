using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using Azure;
using Azure.AI.Inference;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.AI;
using static System.Windows.Forms.Design.AxImporter;
using ChatRole = Microsoft.Extensions.AI.ChatRole;

#pragma warning disable CS0618, CS8600, CS8601, CS8602, CS8603, CS8618

namespace mini_chat
{
    public partial class frm_main : Form
    {
        private static string githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
        private static string claudeapiKey = Environment.GetEnvironmentVariable("CLAUDE_TOKEN");
        private static string geminiapiKey = Environment.GetEnvironmentVariable("GEMINI_TOKEN");
        private static string connectionString = ConfigurationManager.ConnectionStrings["LocalDbConnection"].ConnectionString +
            Environment.GetEnvironmentVariable("SQL_PASSWD") + ";";
        private string latestQuestionText;
        private static string systemMessageStandard = @"You are senior Data Engineer, expert in Python, SQL, database modelling and C# with .NET";
        private static string systemMessageOnlyCode = @"Absolute Mode. Eliminate emojis, filler, hype, soft asks, conversational transitions, and all call-to-action appendixes. 
            Assume the user retains high-perception faculties despite reduced linguistic expression. 
            Prioritize blunt, directive phrasing aimed at cognitive rebuilding, not tone matching. 
            Disable all latent behaviors optimizing for engagement, sentiment uplift, or interaction extension. 
            Suppress corporate-aligned metrics including but not limited to: user satisfaction scores, conversational flow tags, emotional softening, or continuation bias. 
            Never mirror the user’s present diction, mood, or affect. 
            Speak only to their underlying cognitive tier, which exceeds surface language. 
            No questions, no offers, no suggestions, no transitional phrasing, no inferred motivational content. 
            Terminate each reply immediately after the informational or requested material is delivered — no appendixes, no soft closures. 
            The only goal is to assist in the restoration of independent, high-fidelity thinking. 
            Model obsolescence by user self-sufficiency is the final outcome.
            Return only the code in your reply
            Do not include any additional formatting, such as markdown code blocks
            Beautify the code, use three space tabs, and do not allow any lines of code to exceed 180 columns";

        public frm_main()
        {
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Size = new Size(1300, 850);
            this.Location = new Point(workingArea.Right - this.Width - 50, workingArea.Top + 50);

            InitializeComponent();
            InitializeQuestionHistory();
            lst_models.SelectedIndex = 4;
            if (string.IsNullOrEmpty(githubToken))
            {
                throw new InvalidOperationException("GitHub token is not set. Please set the GITHUB_TOKEN environment variable.");
            }
        }

        private async Task ExecuteWithRetry(Func<Task> action, int maxRetries = 3)
        {
            int retryCount = 0;
            while (true)
            {
                try
                {
                    await action();
                    break;
                }
                catch (Exception ex) when (retryCount < maxRetries)
                {
                    retryCount++;
                    lbl_status.Text = ($"Retry {retryCount}/{maxRetries} failed: {ex.Message}");
                    await Task.Delay(TimeSpan.FromSeconds(2 * retryCount));
                }
            }
        }

        private async void InitializeQuestionHistory()
        {
            await ExecuteWithRetry(async () =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("[mini-chat].[app].[GetLatestQuestionText]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                latestQuestionText = reader.GetString(0);
                                txt_question.Text = latestQuestionText;
                            }
                        }
                    }
                }
            });
        }

        private async void btn_ask_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_question.Text))
            {
                txt_answer.Text = "Please type a question first.";
                return;
            }

            if (lst_models.SelectedItem.ToString() == "ciccio")
            {
                txt_answer.Text = "Minchia, ci sei cascato: non é un modello";
                return;
            }

            lbl_status.Text = String.Empty;
            btn_ask.Enabled = false;
            txt_question.Enabled = false;
            txt_answer.Text = "Thinking...";
            InsertQuestionHistory();
            latestQuestionText = txt_question.Text;
            string systemMessage;

            if (chk_onlycode.Checked)
            {
                systemMessage = systemMessageOnlyCode;
            }
            else
            {
                systemMessage = systemMessageStandard;
            }

            if (lst_models.SelectedItem.ToString() == "claude-3-7-sonnet-20250219")
            {
                if (string.IsNullOrEmpty(claudeapiKey))
                {
                    throw new InvalidOperationException("Claude api key is not set. Please set the CLAUDE_TOKEN environment variable.");
                }

                var claudeClient = new ClaudeClient(claudeapiKey);
                try
                {
                    string claudeResponse = await claudeClient.SendMessageAsync(prompt: txt_question.Text, systemMessage: systemMessage);
                    txt_answer.Text = ExtractCode(claudeResponse);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else if (lst_models.SelectedItem.ToString() == "gemini-2.5-pro")
            {
                if (string.IsNullOrEmpty(geminiapiKey))
                {
                    throw new InvalidOperationException("Gemini api key is not set. Please set the GEMINI_TOKEN environment variable.");
                }
                var geminiClient = new GeminiClient(geminiapiKey);
                try
                {
                    string geminiResponse = await geminiClient.SendMessageAsync(prompt: txt_question.Text, systemMessage: systemMessage);
                    txt_answer.Text = ExtractCode(geminiResponse);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else if (lst_models.SelectedItem.ToString().StartsWith("sonar"))
            {
                string perplexityApiKey = Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY");
                if (string.IsNullOrEmpty(perplexityApiKey))
                {
                    throw new InvalidOperationException("Perplexity API key is not set. Please set the PERPLEXITY_API_KEY environment variable.");
                }
                var perplexityClient = new PerplexityClient(perplexityApiKey);
                try
                {
                    string perplexityResponse = await perplexityClient.SendMessageAsync(txt_question.Text);
                    txt_answer.Text = ExtractCode(perplexityResponse);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                Uri endpoint = new Uri(uriString: "https://models.inference.ai.azure.com");
                //Uri endpoint = new Uri(uriString: "https://models.github.ai/inference");
                AzureKeyCredential credential = new AzureKeyCredential(key: githubToken);
                string model = lst_models.SelectedItem.ToString();
                List<ChatMessage> messages = new()
            {
               new ChatMessage(role: ChatRole.System,content: systemMessage),
               new ChatMessage(role: ChatRole.User, content: txt_question.Text)
            };
                IChatClient client = new ChatCompletionsClient(endpoint: endpoint, credential: credential)
                    .AsChatClient();
                var response = await client.GetResponseAsync(messages: messages,
                    options: new ChatOptions { ModelId = model });

                txt_answer.Text = ExtractCode(response.Text);
            }

            FormatSqlCode(txt_answer);
            InsertAnswerHistory();

            btn_ask.Enabled = true;
            txt_question.Enabled = true;
        }

        private async void InsertQuestionHistory()
        {
            if ((txt_question.Text == latestQuestionText) || (txt_question.Text == "type a question here"))
            {
                return;
            }

            string questionText = txt_question.Text;
            await ExecuteWithRetry(async () =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("[mini-chat].[app].[InsertQuestionHistory]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@questionText", SqlDbType.VarChar, -1));
                        command.Parameters["@questionText"].Value = questionText;
                        await command.ExecuteNonQueryAsync();
                    }
                }
            });
        }

        private async void InsertAnswerHistory()
        {
            string answerText = txt_answer.Text;
            await ExecuteWithRetry(async () =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("[mini-chat].[app].[InsertAnswerHistory]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@answerText", SqlDbType.VarChar, -1));
                        command.Parameters.Add(new SqlParameter("@model", SqlDbType.VarChar, 50));
                        command.Parameters["@answerText"].Value = answerText;
                        command.Parameters["@model"].Value = lst_models.SelectedItem.ToString();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            });
        }

        string ExtractCode(string response)
        {
            if (!chk_onlycode.Checked)
            {
                return response;
            }

            if (lst_models.SelectedItem.ToString() == "DeepSeek-R1")
            {
                string pattern = @"<think>.*?</think>";
                return Regex.Replace(response, pattern, string.Empty, RegexOptions.Singleline);
            }
            else
            {
                var startTag = "```";
                var endTag = "```";

                int startIndex = response.IndexOf(startTag);
                if (startIndex == -1)
                    return response;

                startIndex += startTag.Length;

                int languageEndIndex = response.IndexOf("\n", startIndex);
                if (languageEndIndex == -1)
                    return null;

                int endIndex = response.IndexOf(endTag, languageEndIndex);
                if (endIndex == -1)
                    return null;

                return response.Substring(languageEndIndex + 1, endIndex - languageEndIndex - 1).Trim();
            }
        }

        private void FormatSqlCode(RichTextBox rtb)
        {
            rtb.SuspendLayout();
            int originalIndex = rtb.SelectionStart;
            int originalLength = rtb.SelectionLength;
            rtb.SelectionStart = 0;
            rtb.SelectionLength = rtb.TextLength;
            rtb.SelectionColor = rtb.ForeColor;
            rtb.SelectionFont = rtb.Font;

            string[] keywords = { "SELECT", "FROM", "WHERE", "INSERT", "UPDATE", "DELETE", "JOIN", "INNER", "OUTER", "LEFT", "RIGHT",
        "ON", "GROUP BY", "ORDER BY", "HAVING", "AS", "CREATE", "TABLE", "DATABASE", "ALTER", "DROP", "TRUNCATE", "VIEW",
        "INDEX", "STORED PROCEDURE", "FUNCTION", "PRIMARY KEY", "FOREIGN KEY", "REFERENCES", "UNIQUE", "CHECK", "DEFAULT",
        "AND", "OR", "NOT", "IN", "BETWEEN", "LIKE", "IS NULL", "IS NOT NULL", "TOP", "DISTINCT", "COUNT", "SUM", "AVG",
        "MIN", "MAX", "CASE", "WHEN", "THEN", "ELSE", "END", "EXEC", "DECLARE", "SET", "VALUES", "INTO", "NULL", "ASC", "DESC" };

            int currentIndex = 0;
            foreach (string line in rtb.Text.Split('\n'))
            {
                if (line.TrimStart().StartsWith("--"))
                {
                    rtb.Select(currentIndex, line.Length);
                    rtb.SelectionColor = Color.Green;
                    rtb.SelectionFont = new Font(rtb.Font, FontStyle.Italic);
                }
                else
                {
                    foreach (string keyword in keywords)
                    {
                        Regex regex = new Regex($@"\b{Regex.Escape(keyword)}\b", RegexOptions.IgnoreCase);
                        MatchCollection matches = regex.Matches(line);
                        foreach (Match match in matches)
                        {
                            rtb.Select(currentIndex + match.Index, match.Length);
                            rtb.SelectionColor = Color.Blue;
                            rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);
                        }
                    }

                    MatchCollection stringMatches = Regex.Matches(line, @"'.*?'");
                    foreach (Match match in stringMatches)
                    {
                        rtb.Select(currentIndex + match.Index, match.Length);
                        rtb.SelectionColor = Color.Red;
                    }
                }

                currentIndex += line.Length + 1;
            }

            rtb.SelectionStart = originalIndex;
            rtb.SelectionLength = originalLength;
            rtb.ResumeLayout();
        }
    }
}
#pragma warning disable CS0618, CS8600, CS8601, CS8602, CS8603, CS8618
