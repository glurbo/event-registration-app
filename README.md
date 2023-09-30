~~~
dotnet tool update --global dotnet-ef
dotnet tool update --global dotnet-aspnet-codegenerator
~~~

creating migration and updating database
~~~
dotnet ef migrations add Initial --project DAL --startup-project webapi --context EventRegistrationDbContext
dotnet ef database update --project DAL --startup-project webapi --context EventRegistrationDbContext
~~~