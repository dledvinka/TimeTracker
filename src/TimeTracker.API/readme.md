cd into the API project

Add migration:
dotnet ef migrations add {MigrationName}

Remove last migration
dotnet ef migrations remove

Apply migration
dotnet ef database update