# event-registration-app

> A registration app with Vue frontend and .NET backend

## Start frontend development server
```bash
cd vueapp

# install dependencies
npm install

# serve at localhost:5002
npm run serve

# build for production
npm run build
```

## Start ASP.NET Core project
```bash
cd webapi

# serve at localhost:8000
dotnet run
```

update dotnet tools
```bash
dotnet tool update --global dotnet-ef
dotnet tool update --global dotnet-aspnet-codegenerator
```

creating migration and updating database
```bash
dotnet ef migrations add Initial --project DAL --startup-project webapi --context EventRegistrationDbContext
dotnet ef database update --project DAL --startup-project webapi --context EventRegistrationDbContext
```