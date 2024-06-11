```
dotnet tool install --global dotnet-ef

dotnet ef dbcontext scaffold "Server=localhost:5432;Database=postgres;User Id=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -c UsersManagement
```


## MassTransit

```
dotnet ef migrations add AddMassTransit --context MassTransitContext

dotnet ef database update --context MassTransitContext  
```
