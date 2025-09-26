# GitHub Copilot Coding Agent Prompt

This prompt is designed to be used with **GitHub Copilot Coding Agent** in the repository:  
👉 [Generative-AI-for-beginners-dotnet](https://github.com/microsoft/Generative-AI-for-beginners-dotnet/tree/main/03-CoreGenerativeAITechniques)

---

## Goal

Create a new lesson named **Lesson-03.1-CoreGenerativeAITechniques-runapp**, which is a modernized copy of **Lesson 03**, using the new **`.NET 10 run <file>.cs`** feature.  
The new lesson will demonstrate the same concepts as Lesson 03 but with simplified **single-file C# demos**.

---

## Tasks

### 1. Create a new lesson folder
- Path: `03.1-CoreGenerativeAITechniques-runapp`
- Copy the structure of Lesson 03 (`/src` + Markdown).

### 2. Update the `/src` folder with single-file apps
- For each demo, create a `.cs` file runnable via `.NET run file.cs`.
- Suggested naming:
  - `01-llm-completion.cs`
  - `02-chat-flow.cs`
  - `03-functions-and-plugins.cs`
  - `04-retrieval-augmented-generation.cs`
  - Add any extra demos as needed (e.g., `05-structured-output.cs`, `06-multimodal.cs`).
- Remove unnecessary boilerplate — use top-level statements.

### 3. Update Markdown files
- Base them on Lesson 03’s Markdown.
- Replace old sample code with references to the new `.cs` files.
- Embed code snippets directly from the `.cs` files.
- Add instructions:

  ```bash
  dotnet run 01-llm-completion.cs
  ```

- Clearly mention that `.NET 10 SDK` is required.

### 4. Organize Lesson 03.x as sub-lessons (new TOC)
Create a `README.md` in `03.0-CoreGenerativeAITechniques-Overview` with a table of contents like this:

- **Lesson 03.0 – Core Generative AI Techniques (Overview)**  
  *High-level intro + TOC for all 3.x lessons.*
- **Lesson 03.1 – Text-based Techniques (with `.NET run app.cs`)**  
  - LLM Completions  
  - Chat Flows  
  - Prompt Engineering Patterns
- **Lesson 03.2 – Extending Models with Context**  
  - Functions and Plugins  
  - Retrieval Augmented Generation (RAG)  
  - Structured Outputs / JSON Mode
- **Lesson 03.3 – Beyond Text**  
  - Multi-modal Generative AI (text + vision)  
  - Optional: Speech-to-text-to-LLM
- **Lesson 03.4 – Intro to Agents**  
  - Basic agent loop (reasoning + actions)  
  - Teaser for Lesson 04

### 5. Polish
- Ensure code compiles with `.NET 10`.
- Ensure Markdown lessons reference the new samples correctly.
- Add a note at the top of Lesson 03.1:

  > 💡 *This lesson uses the new `.NET run <file>.cs` feature available in .NET 10. If you’re on an earlier version, please refer to Lesson 03.*

---

## ✅ Expected Output
- A new folder `03.1-CoreGenerativeAITechniques-runapp` with:
  - Single-file `.cs` demos runnable with `.NET run`.
  - Updated Markdown files embedding the new code.
  - Clear learner instructions.
- A reorganized **Lesson 03.x Table of Contents** for clarity and growth.
