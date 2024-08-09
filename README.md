# Email Parser

Expected NuGet Packages
```.NET CLI
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
Expected environment variables
```powershell
AZURE_OPENAI_ENDPOINT=https://novahiringai.openai.azure.com/
AZURE_OPENAI_KEY=df98d1eebad2479cbd45a4e637c726fb
AZURE_OPENAI_MODEL=EmailParser
SYSTEM_PROMPT_FILE=Resources/systemprompt.txt
SMTP_HOST=imap.gmail.com
SMTP_PORT=993
SMTP_USERNAME=radepraksa@gmail.com
SMTP_PASSWORD=drsq hsez jncj mtnt
SMTP_SSL=true
```