
## Copilot Coding Instructions for Generative AI for Beginners .NET

This is a C#/.NET and AI-focused repository. It contains practical lessons, code samples, and integrations for building Generative AI applications in .NET, including Azure OpenAI, local models, and more. Please follow these guidelines when contributing:

## Code Standards

### Required Before Each Commit
- Run `dotnet format` before committing any changes to ensure proper C# code formatting and style
- Ensure all code builds and passes tests locally with `dotnet build` and `dotnet test`

### Development Flow
- Build: `dotnet build`
- Test: `dotnet test`
- Format: `dotnet format`
- Lint (if enabled): `dotnet format --verify-no-changes`
- For samples using other tools (e.g., Python, Docker), follow instructions in the relevant lesson's `readme.md`

## Repository Structure
- `01-IntroToGenAI/`, `02-SetupDevEnvironment/`, etc.: Lesson folders with code, images, and documentation
- `src/`: Source code for practical samples and lesson demos
- `images/`: Course and lesson images
- `docs/`: Additional documentation and guides
- `translations/`: Translated course materials

## Key Guidelines
1. Follow C# and .NET best practices (naming, async/await, nullability, etc.)
2. Use modern .NET features (e.g., top-level statements, nullable reference types) where appropriate
3. Maintain existing code structure and organization—place new samples in the correct lesson folder
4. Write unit tests for new functionality when possible (use xUnit or MSTest)
5. Document public APIs, sample usage, and complex logic. Update lesson `readme.md` files as needed
6. For AI integrations (Azure OpenAI, Ollama, etc.), follow official SDK and security guidelines
7. Prefer dependency injection and configuration via `appsettings.json` for new .NET projects
8. Keep code and documentation accessible for beginners—add comments and links to docs where helpful
