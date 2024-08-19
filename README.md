# Leave Manager
NuGet Packages
```text
Id                                                Versions       ProjectName
--                                                --------       -----------
Microsoft.VisualStudio.Web.CodeGeneration.Design  {8.0.3}        Core       
MailKit                                           {4.7.1.1}      Core       
Microsoft.EntityFrameworkCore.SqlServer           {8.0.7}        Core       
Microsoft.AspNetCore.Identity.EntityFrameworkCore {8.0.7}        Core       
DotNetEnv                                         {3.1.0}        Core       
Microsoft.EntityFrameworkCore.Sqlite              {8.0.7}        Core       
Microsoft.AspNetCore.Identity.UI                  {8.0.7}        Core         
Microsoft.EntityFrameworkCore.Tools               {8.0.7}        Core       
Azure.AI.OpenAI                                   {2.0.0-beta.2} Core       
Microsoft.AspNetCore.Identity.UI                  {8.0.7}        Utils      
Microsoft.AspNetCore.Mvc.ViewFeatures             {2.2.0}        Models     
Microsoft.AspNet.Mvc                              {5.3.0}        Models     
Microsoft.AspNetCore.Mvc.Core                     {2.2.5}        Models     
MailKit                                           {4.7.1.1}      DataAccess 
Microsoft.EntityFrameworkCore                     {8.0.7}        DataAccess 
Microsoft.EntityFrameworkCore.SqlServer           {8.0.7}        DataAccess 
Microsoft.AspNetCore.Identity.EntityFrameworkCore {8.0.7}        DataAccess 
Microsoft.EntityFrameworkCore.Tools               {8.0.7}        DataAccess 
```

Expected `.env` variables
```txt
AZURE_OPENAI_ENDPOINT=<https://example.openai.azure.com/>
AZURE_OPENAI_KEY=<key>
AZURE_OPENAI_MODEL=<model name>
SYSTEM_PROMPT_FILE=<path>
IMAP_HOST=imap.gmail.com
IMAP_PORT=993
IMAP_USERNAME_Vacation=<example@gmail.com>
IMAP_PASSWORD_Vacation=<password>
IMAP_USERNAME_Remote=<example@gmail.com>
IMAP_PASSWORD_Remote=<password>
IMAP_SSL=true
ADMIN_EMAIL=<example@example.com>
ADMIN_PASSWORD=<password>
```