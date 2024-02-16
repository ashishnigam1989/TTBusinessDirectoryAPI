Visual Studio 2022
SQL Server 2019
.Net Core 3.1 framework


Setup DatabaseService
#Install Entity Framework#
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.1.30
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.1.1
Install Microsoft.Extensions.DependencyInjection 3.1.30
Install-Package Microsoft.EntityFrameworkCore.Design -Version 3.1.1
Scaffold-DbContext "Server=185.182.184.243;Database=BusinessDirectoryDB;User Id=businessdir;Password=BusinessDir@123;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DbEntities -Force

Setup Common Service
Logger (Install-Package NLog.Extensions.Logging)

Install-Package Swashbuckle.AspNetCore

User - admin@yopmail.com, Pwd - 1