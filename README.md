# DB
The data will store in local
C:\Users\<YourUser>\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB
```bash
    "DefaultConnection": "
    Server=(localdb)\\mssqllocaldb;Database=DotnetTestDb;Trusted_Connection=True;MultipleActiveResultSets=true"
```

# LjhBackendApi
## Build
Run `dotnet build -tl` to build the solution.

## Run
To run the web application:
```bash
cd .\src\Web\
dotnet watch run
```
Navigate to https://localhost:5001. 
The application will open swagger UI at https://localhost:5001/swagger/index.html.

## Test
To run the tests:
```bash
dotnet test
```

# LjhFrontendApi

## Build
Run `npm install` to install depnedency
Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Run
To run the web application:
Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. 
