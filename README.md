# Project Name
Job Candidate Hub

## How to Run the Project
### Prerequisites
1. **.NET 8.0 SDK**: [Download](https://dotnet.microsoft.com/download)
2. **IDE**: Visual Studio 2022 or Visual Studio Code (with C# extension)
3. **Microso SQL Server**: Ensure it's set up and accessible.

### Setup Instructions
1. **Clone the Repository**: 
  -Command: git clone https://github.com/manojphuyal/JobCandidateHub.git

2. **Restore Dependencies**: 
  -Command: dotnet restore

3. **Database Setup**: 
  Update connection string in appsettings.json/appsettings.Development.json and run migrations: Run in package manager console selecting JobCandidateHub.Database as Default project.
  -PM Command: Update-Database

4. **Build the Project (no need if run manually)**: 
  -Command: dotnet build

5. **Run the Project (no need if run manually)**: 
  -Command: cd .\JobCandidateHub.API (run this if you are not in specific path)
  -Command: dotnet run

6. **Access API**: Visit in your browser. Swagger UI will be available at https://localhost:YourPort/swagger
  URL Endpoint: /api/Candidate/Save
  Body:
  {
    "email": "test@email.com",
    "firstName": "Manoj",
    "lastName": "Phuyal",
    "phoneNumber": "+9779845670262",
    "preferredCallTime": "",
    "linkedInProfile": "",
    "gitHubProfile": "",
    "comments": "no comment"
  }

## Ways for Improvement
1. **Code Refactoring**: Split large methods into smaller ones.
2. **Performance Optimization**: Implement caching and asynchronous DB operations.
3. **Security Enhancements**: Add authentication, input validation, and role-based authorization.
4. **Testing**: Add more edge cases in unit tests, use mocking frameworks.
5. **Error Handling**: Improve centralized error handling using middleware.

## Assumptions
1. Database is already set up (SQL Server).
2. The application is used within a single organization (no multi-tenancy).
3. Authentication is not implemented yet.
4. Database schema is already created.
5. No external services are integrated.

## Total Time Spent
- Time spent on coding: 3 hours
- Time spent on unit test: 1 hours
- Time spent on testing: 0.5 hours
- Time spent on documentation: 0.5 hours
- **Total time spent**: 5 hours
