# event-registration-app

> A registration app with Vue frontend and .NET backend

## Start frontend development server
```bash
# root folder
cd vueapp

# install dependencies
npm install

# serve at localhost:5002
npm run serve

# build for production
npm run build
```
updating node/npm helps if having issues.

## Start ASP.NET Core project
```bash
# root folder
cd webapi

# serve at localhost:8001 (https)
dotnet run --launch-profile https

#or

# serve at localhost:8000 (http)
dotnet run
# if running on port 8001 doesn't work, try this and edit baseUrl in vueapp/src/api.ts
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

credentials for admin account
```bash
admin@app.com
Foo.bar.1
```

## Work progress
Here I describe the work progress of this trial work. I started off with creating the project with Vue and ASP.NET Core template. I made domain entities and custom database context and created api controllers to test CRUD methods in Swagger. Created DTOs to avoid circular dependency. For authentication I used JWT tokens with long expire times without using refresh tokens to make implementation as simple as possible. Then I seeded some initial data and allowed Cors to enable frontend data fetching. Note that storing JWT token in appsettings.json is a bad idea, but in the context of this trial work I stored it there for easy access.

I moved on to frontend. First I added initial landing page and data fetching with axios. Following that I started abstracting logic and made dialog component for creating, deleting and editing events. Then I enabled registration for each event. Continued with overall UI improvements and polished all use cases. Finally I enabled authentication on the frontend, so only authenticated user can make changes to events and only un-authenticated ("clients") can register to events. Registrations can be seen with a popup dialog and when the maximum registration count is reached, the register button will disappear. JWT token in the frontend is saved into session storage on login and deleted on logout.

Some issues I had with the trial work:

* scaffolding controllers were bothersome, issue was because of a missing reference to my custom database context.
* axios had problems sending data to backend because of casing issues (camelCase to PascalCase), solved it by changing json serializer options in the backend to make models consistently have PascalCase names.
* Troubles with authentication implementation on the frontend. Little experience with authentication solutions on the frontend certainly added some hours to it. Settled with session storage, but certainly not the best solution. Implementing authentication without any third party solutions can be tricky.
* Issues with getting authorization with JWT tokens to work. Solution was to specify the authentication scheme in the authorize attribute on api controllers.
* Using bootstrap modals for dialog hindered progress for a bit, made it harder to do validation since submitting closes the dialog.