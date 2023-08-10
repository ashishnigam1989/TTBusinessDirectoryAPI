Setup DatabaseService
#Install Entity Framework#
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.1.30
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.1.1
Install Microsoft.Extensions.DependencyInjection 3.1.30
Install-Package Microsoft.EntityFrameworkCore.Design -Version 3.1.1
Scaffold-DbContext "Server=185.182.184.243;Database=BusinessDirectoryDB;User Id=businessdir;Password=BusinessDir@123;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DbEntities-Force
