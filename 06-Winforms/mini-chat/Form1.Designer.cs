namespace mini_chat
{
    partial class frm_main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_main));
            txt_question = new RichTextBox();
            txt_answer = new RichTextBox();
            btn_ask = new Button();
            lst_models = new ListBox();
            chk_onlycode = new CheckBox();
            lbl_status = new Label();
            SuspendLayout();
            // 
            // txt_question
            // 
            txt_question.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_question.Location = new Point(12, 12);
            txt_question.Name = "txt_question";
            txt_question.Size = new Size(1291, 216);
            txt_question.TabIndex = 0;
            txt_question.Text = "type a question here";
            // 
            // txt_answer
            // 
            txt_answer.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_answer.Location = new Point(12, 390);
            txt_answer.Name = "txt_answer";
            txt_answer.Size = new Size(1291, 501);
            txt_answer.TabIndex = 2;
            txt_answer.Text = "";
            // 
            // btn_ask
            // 
            btn_ask.Location = new Point(375, 276);
            btn_ask.Name = "btn_ask";
            btn_ask.Size = new Size(203, 94);
            btn_ask.TabIndex = 1;
            btn_ask.Text = "&Ask";
            btn_ask.UseVisualStyleBackColor = true;
            btn_ask.Click += btn_ask_Click;
            // 
            // lst_models
            // 
            lst_models.FormattingEnabled = true;
            lst_models.Items.AddRange(new object[] { "Phi-3.5-MoE-instruct", "Phi-4", "Phi-4-mini-instruct", "claude-3-7-sonnet-20250219", "gemini-2.5-pro", "gpt-4.1-mini", "gpt-4.1", "gpt-5", "gpt-4o-mini", "gpt-4o", "Mistral-large", "Meta-Llama-3.1-70B-Instruct", "DeepSeek-V3-0324", "DeepSeek-R1", "DeepSeek-R1-0528", "Codestral-2501", "ciccio" });
            lst_models.Location = new Point(12, 246);
            lst_models.Name = "lst_models";
            lst_models.Size = new Size(343, 124);
            lst_models.TabIndex = 3;
            // 
            // chk_onlycode
            // 
            chk_onlycode.AutoSize = true;
            chk_onlycode.Checked = true;
            chk_onlycode.CheckState = CheckState.Checked;
            chk_onlycode.Location = new Point(375, 246);
            chk_onlycode.Name = "chk_onlycode";
            chk_onlycode.Size = new Size(136, 24);
            chk_onlycode.TabIndex = 4;
            chk_onlycode.Text = "Show only code";
            chk_onlycode.UseVisualStyleBackColor = true;
            // 
            // lbl_status
            // 
            lbl_status.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_status.Location = new Point(12, 1019);
            lbl_status.Name = "lbl_status";
            lbl_status.Size = new Size(566, 29);
            lbl_status.TabIndex = 5;
            // 
            // frm_main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1315, 903);
            Controls.Add(lbl_status);
            Controls.Add(chk_onlycode);
            Controls.Add(lst_models);
            Controls.Add(btn_ask);
            Controls.Add(txt_answer);
            Controls.Add(txt_question);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frm_main";
            StartPosition = FormStartPosition.Manual;
            Text = "mini chat";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox txt_question;
        private RichTextBox txt_answer;
        private Button btn_ask;
        private ListBox lst_models;
        private CheckBox chk_onlycode;
        private Label lbl_status;
    }
}
