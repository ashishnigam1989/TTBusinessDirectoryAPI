Setup DatabaseService
#Install Entity Framework#
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.1.30
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.1.1
Install Microsoft.Extensions.DependencyInjection 3.1.30
Install-Package Microsoft.EntityFrameworkCore.Design -Version 3.1.1
Scaffold-DbContext "Server=VICKY\MSSQL2019;Database=BusinessDirectoryDB;User Id=sa;Password=sql#2019;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DbCore -Force
