# Email Parser
Expected NuGet Packages
```shell
# Core packages
dotnet add Core package Azure.AI.OpenAI --prerelease
dotnet add Core package DotNetEnv
dotnet add Core package MailKit

# DataAccess packages
dotnet add DataAccess package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add DataAccess package Microsoft.EntityFrameworkCore
dotnet add DataAccess package Microsoft.EntityFrameworkCore.SqlServer
dotnet add DataAccess package Microsoft.EntityFrameworkCore.Tools
```
Expected `.env` variables
```txt
AZURE_OPENAI_ENDPOINT=<https://example.openai.azure.com/>
AZURE_OPENAI_KEY=<key>
AZURE_OPENAI_MODEL=<model name>
SYSTEM_PROMPT_FILE=<path>
SMTP_HOST=imap.gmail.com
SMTP_PORT=993
SMTP_USERNAME=<example@gmail.com>
SMTP_PASSWORD=<password>
SMTP_SSL=true
```