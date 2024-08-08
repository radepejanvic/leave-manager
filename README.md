# Email Parser

Expected NuGet Packages
```.NET CLI
dotnet add Core package Azure.AI.OpenAI --prerelease
dotnet add Core package DotNetEnv
```
Expected environment variables
```powershell
AZURE_OPENAI_ENDPOINT=https://novahiringai.openai.azure.com/
AZURE_OPENAI_KEY=df98d1eebad2479cbd45a4e637c726fb
AZURE_OPENAI_MODEL=EmailParser
SYSTEM_PROMPT_FILE=Resources/systemprompt.txt
```