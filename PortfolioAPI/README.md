# Portfolio CMS API

A full-featured ASP.NET Core 9 Web API for managing portfolio content with JWT authentication.

## Features

- ✅ Full CRUD operations for Projects, Skills, Experience, Education
- ✅ Contact form submission handling
- ✅ JWT-based authentication for admin panel
- ✅ PostgreSQL database with Entity Framework Core
- ✅ Repository pattern + Unit of Work
- ✅ Swagger/OpenAPI documentation
- ✅ CORS configured for Angular frontend

## Tech Stack

- **Framework**: ASP.NET Core 9
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core 9
- **Authentication**: JWT Bearer tokens
- **Password Hashing**: BCrypt
- **API Documentation**: Swagger/OpenAPI

## Prerequisites

- .NET 9 SDK
- PostgreSQL 14+ (18 recommended)

## Getting Started

### 1. Install PostgreSQL

**macOS:**
- **Postgres.app** (Easiest): https://postgresapp.com/
- **Homebrew**: `brew install postgresql@18 && brew services start postgresql@18`

**Windows:**
- Download installer: https://www.postgresql.org/download/windows/

**Linux (Ubuntu/Debian):**
```bash
sudo apt-get update
sudo apt-get install postgresql postgresql-contrib
sudo systemctl start postgresql
```

**Create Database:**
```bash
# Connect to PostgreSQL
psql -U postgres

# Create database
CREATE DATABASE portfolio_db;

# Exit
\q
```

### 2. Configure Connection String

Update `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=portfolio_db;Username=postgres;Password=your_password"
  },
  "Jwt": {
    "Secret": "YourSuperSecretKeyMinimum32CharsLong!",
    "Issuer": "PortfolioAPI",
    "Audience": "PortfolioWebApp"
  }
}
```

**IMPORTANT**: Change the JWT Secret in production!

### 3. Install Dependencies

```bash
cd PortfolioAPI
dotnet restore
```

### 4. Run Migrations

```bash
# Create initial migration
dotnet ef migrations add InitialCreate

# Apply migration to database
dotnet ef database update
```

### 5. Run the API

```bash
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

## API Endpoints

### Authentication (Public)
```
POST   /api/auth/register     - Register new admin user
POST   /api/auth/login        - Login and get JWT token
```

### Projects
```
GET    /api/projects          - Get all projects (filter: ?featuredOnly=true)
GET    /api/projects/{id}     - Get project by ID
POST   /api/projects          - Create project [Auth Required]
PUT    /api/projects/{id}     - Update project [Auth Required]
DELETE /api/projects/{id}     - Delete project [Auth Required]
```

### Skills
```
GET    /api/skills            - Get all skills (filter: ?category=Backend)
GET    /api/skills/{id}       - Get skill by ID
POST   /api/skills            - Create skill [Auth Required]
PUT    /api/skills/{id}       - Update skill [Auth Required]
DELETE /api/skills/{id}       - Delete skill [Auth Required]
```

### Experience
```
GET    /api/experiences       - Get all experiences
GET    /api/experiences/{id}  - Get experience by ID
POST   /api/experiences       - Create experience [Auth Required]
PUT    /api/experiences/{id}  - Update experience [Auth Required]
DELETE /api/experiences/{id}  - Delete experience [Auth Required]
```

### Education
```
GET    /api/education         - Get all education records
GET    /api/education/{id}    - Get education by ID
POST   /api/education         - Create education [Auth Required]
PUT    /api/education/{id}    - Update education [Auth Required]
DELETE /api/education/{id}    - Delete education [Auth Required]
```

### Contact (Public POST, Auth Required for GET)
```
POST   /api/contact           - Submit contact message
GET    /api/contact           - Get all messages (filter: ?unreadOnly=true) [Auth Required]
PUT    /api/contact/{id}/read - Mark message as read [Auth Required]
```

## Authentication Flow

### 1. Register First User
```bash
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admin",
    "email": "admin@example.com",
    "password": "SecurePassword123!"
  }'
```

Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "admin",
  "email": "admin@example.com",
  "expiresAt": "2025-02-15T12:00:00Z"
}
```

### 2. Use Token for Protected Endpoints
```bash
curl -X POST https://localhost:5001/api/projects \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "GroceryPriceTracker",
    "description": "Track grocery prices across stores",
    "technologies": ["ASP.NET", "Angular", "PostgreSQL"],
    "startDate": "2024-09-01",
    "isFeatured": true,
    "displayOrder": 1
  }'
```

## Database Schema

### Projects
- Id, Title, Description, DetailedDescription
- ImageUrl, LiveUrl, GithubUrl
- Technologies (JSONB array)
- StartDate, EndDate
- IsFeatured, DisplayOrder
- CreatedAt, UpdatedAt

### Skills
- Id, Name, Category
- ProficiencyLevel (1-100)
- IconUrl, DisplayOrder
- CreatedAt, UpdatedAt

### Experience
- Id, Company, Position, Location
- Description
- Responsibilities (JSONB array)
- Technologies (JSONB array)
- StartDate, EndDate, IsCurrentRole
- DisplayOrder, CreatedAt, UpdatedAt

### Education
- Id, Institution, Degree, FieldOfStudy
- Location, Description, GPA
- Coursework (JSONB array)
- StartDate, EndDate, IsCurrentlyEnrolled
- DisplayOrder, CreatedAt, UpdatedAt

### ContactMessages
- Id, Name, Email, Subject, Message
- IsRead, CreatedAt

### Users
- Id, Username, Email, PasswordHash
- Role, CreatedAt, LastLoginAt

## EF Core Commands

```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Rollback to specific migration
dotnet ef database update MigrationName

# Remove last migration
dotnet ef migrations remove

# Drop database
dotnet ef database drop
```

## Project Structure

```
PortfolioAPI/
├── Controllers/          # API endpoints
│   ├── AuthController.cs
│   ├── ProjectsController.cs
│   ├── SkillsController.cs
│   └── OtherControllers.cs
├── Data/                 # DbContext
│   └── PortfolioDbContext.cs
├── DTOs/                 # Data Transfer Objects
│   └── PortfolioDtos.cs
├── Models/               # Entity models
│   ├── Project.cs
│   ├── Skill.cs
│   ├── Experience.cs
│   ├── Education.cs
│   ├── ContactMessage.cs
│   └── User.cs
├── Repositories/         # Data access layer
│   ├── Interfaces/
│   │   ├── IRepository.cs
│   │   └── IUnitOfWork.cs
│   ├── Repository.cs
│   └── UnitOfWork.cs
├── Services/             # Business logic
│   ├── AuthService.cs
│   └── JwtService.cs
├── Program.cs            # Application entry point
├── appsettings.json      # Configuration
└── PortfolioAPI.csproj   # Project file
```

## Development Tips

### Testing with Swagger
1. Navigate to `https://localhost:5001/swagger`
2. Click "Authorize" button
3. Enter: `Bearer YOUR_TOKEN_HERE`
4. Test endpoints interactively

### Seed Initial Data
Create a seeding script in `Program.cs`:
```csharp
// After app.Run();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    
    if (!context.Projects.Any())
    {
        context.Projects.Add(new Project
        {
            Title = "Sample Project",
            Description = "Description here",
            Technologies = new List<string> { "C#", "Angular" },
            StartDate = DateTime.UtcNow,
            IsFeatured = true,
            DisplayOrder = 1
        });
        
        context.SaveChanges();
    }
}
```

## Deployment

### Production Configuration

**appsettings.Production.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=your-db-host;Database=portfolio_db;Username=your-user;Password=your-password"
  },
  "Jwt": {
    "Secret": "Use-Environment-Variable-Or-Secrets-Manager",
    "Issuer": "PortfolioAPI",
    "Audience": "PortfolioWebApp"
  }
}
```

### Environment Variables
```bash
export ConnectionStrings__DefaultConnection="Host=db;Database=portfolio_db;..."
export Jwt__Secret="ProductionSecretKey"
```

### Cloud Deployment Options

**AWS Elastic Beanstalk:**
```bash
# Install EB CLI
pip install awsebcli

# Initialize and deploy
eb init
eb create portfolio-api
eb deploy
```

**Azure App Service:**
```bash
# Install Azure CLI
az webapp up --name portfolio-api --runtime "DOTNET|9.0"
```

**Google Cloud Run:**
```bash
gcloud run deploy portfolio-api --source .
```

## Next Steps

1. ✅ Backend API complete
2. 🔜 Build Angular frontend
3. 🔜 Add image upload functionality (AWS S3/Azure Blob)
4. 🔜 Add email notifications for contact form
5. 🔜 Deploy to AWS (ECS/Elastic Beanstalk)

## License

MIT
