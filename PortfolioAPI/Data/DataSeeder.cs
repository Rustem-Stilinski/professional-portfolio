using PortfolioAPI.Data;
using PortfolioAPI.Models;

namespace PortfolioAPI.Data
{
    public static class DataSeeder
    {
        public static void SeedData(PortfolioDbContext context)
        {
            // Check if data already exists
            if (context.Projects.Any() || context.Skills.Any())
            {
                return; // Database already seeded
            }

            // Seed Projects
            var projects = new List<Project>
            {
                new Project
                {
                    Title = "GroceryPriceTracker",
                    Description = "Full-stack application to track and compare grocery prices across different stores",
                    DetailedDescription = "Built with ASP.NET Core Web API backend, Angular 19 frontend, and SQL Server database. Features include price history visualization using Chart.js, store-by-store comparisons, automated price alerts, and responsive design for mobile shopping.",
                    Technologies = new List<string> { "ASP.NET Core", "Angular 19", "SQL Server", "Entity Framework Core", "Chart.js", "Bootstrap" },
                    GithubUrl = "https://github.com/yourusername/grocery-tracker",
                    StartDate = DateTime.SpecifyKind(new DateTime(2024, 9, 1), DateTimeKind.Utc),
                    IsFeatured = true,
                    DisplayOrder = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Project
                {
                    Title = "ECTOL Canvas 3D Configurator",
                    Description = "Interactive 3D aircraft configuration tool with spatial computing",
                    DetailedDescription = "React Three.js application deployed on AWS EC2, featuring real-time 3D rendering, aircraft component customization, and performance optimizations for complex 3D models.",
                    Technologies = new List<string> { "React", "Three.js", "Node.js", "AWS EC2", "WebGL" },
                    StartDate = DateTime.SpecifyKind(new DateTime(2024, 5, 1), DateTimeKind.Utc),
                    EndDate = DateTime.SpecifyKind(new DateTime(2024, 8, 31), DateTimeKind.Utc),
                    IsFeatured = true,
                    DisplayOrder = 2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Project
                {
                    Title = "Tax Application Microservices",
                    Description = "Enterprise tax calculation and compliance system",
                    DetailedDescription = "Microservices architecture built at Amazon using ASP.NET Core, AWS Lambda, and DynamoDB. Implemented automated tax calculations, multi-jurisdiction compliance, and real-time validation APIs.",
                    Technologies = new List<string> { "ASP.NET Core", "AWS Lambda", "DynamoDB", "API Gateway", "CloudFormation" },
                    StartDate = DateTime.SpecifyKind(new DateTime(2023, 6, 1), DateTimeKind.Utc),
                    EndDate = DateTime.SpecifyKind(new DateTime(2025, 1, 31), DateTimeKind.Utc),
                    IsFeatured = true,
                    DisplayOrder = 3,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            // Seed Skills
            var skills = new List<Skill>
            {
                // Backend
                new Skill { Name = "ASP.NET Core", Category = "Backend", ProficiencyLevel = 90, DisplayOrder = 1 },
                new Skill { Name = "C#", Category = "Backend", ProficiencyLevel = 90, DisplayOrder = 2 },
                new Skill { Name = "Entity Framework Core", Category = "Backend", ProficiencyLevel = 85, DisplayOrder = 3 },
                new Skill { Name = "Node.js", Category = "Backend", ProficiencyLevel = 75, DisplayOrder = 4 },
                
                // Frontend
                new Skill { Name = "Angular", Category = "Frontend", ProficiencyLevel = 85, DisplayOrder = 5 },
                new Skill { Name = "React", Category = "Frontend", ProficiencyLevel = 80, DisplayOrder = 6 },
                new Skill { Name = "TypeScript", Category = "Frontend", ProficiencyLevel = 85, DisplayOrder = 7 },
                new Skill { Name = "JavaScript", Category = "Frontend", ProficiencyLevel = 90, DisplayOrder = 8 },
                
                // Database
                new Skill { Name = "PostgreSQL", Category = "Database", ProficiencyLevel = 85, DisplayOrder = 9 },
                new Skill { Name = "SQL Server", Category = "Database", ProficiencyLevel = 85, DisplayOrder = 10 },
                new Skill { Name = "MongoDB", Category = "Database", ProficiencyLevel = 75, DisplayOrder = 11 },
                
                // Cloud
                new Skill { Name = "AWS", Category = "Cloud", ProficiencyLevel = 80, DisplayOrder = 12 },
                new Skill { Name = "AWS Lambda", Category = "Cloud", ProficiencyLevel = 75, DisplayOrder = 13 },
                new Skill { Name = "Docker", Category = "Cloud", ProficiencyLevel = 70, DisplayOrder = 14 },
                
                // Other
                new Skill { Name = "Git", Category = "Tools", ProficiencyLevel = 90, DisplayOrder = 15 },
                new Skill { Name = "REST APIs", Category = "Backend", ProficiencyLevel = 90, DisplayOrder = 16 },
                new Skill { Name = "GraphQL", Category = "Backend", ProficiencyLevel = 70, DisplayOrder = 17 }
            };

            // Seed Experience
            var experiences = new List<Experience>
            {
                new Experience
                {
                    Company = "Amazon",
                    Position = "Software Development Engineer",
                    Location = "Seattle, WA",
                    Description = "Developed enterprise tax applications using modern full-stack technologies",
                    Responsibilities = new List<string>
                    {
                        "Designed and implemented RESTful APIs using ASP.NET Core for tax calculation services",
                        "Built responsive Angular frontends with state management and real-time updates",
                        "Deployed serverless functions using AWS Lambda and API Gateway",
                        "Optimized database queries reducing API response times by 40%",
                        "Collaborated with cross-functional teams in Agile environment"
                    },
                    Technologies = new List<string> { "ASP.NET Core", "Angular", "AWS Lambda", "DynamoDB", "PostgreSQL" },
                    StartDate = DateTime.SpecifyKind(new DateTime(2023, 6, 1), DateTimeKind.Utc),
                    EndDate = DateTime.SpecifyKind(new DateTime(2025, 1, 31), DateTimeKind.Utc),
                    IsCurrentRole = false,
                    DisplayOrder = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Experience
                {
                    Company = "Freddie Mac",
                    Position = "Software Developer",
                    Location = "McLean, VA",
                    Description = "Contributed to legacy-to-microservices transformation using business rule engines",
                    Responsibilities = new List<string>
                    {
                        "Developed dynamic business rule formulas using Drools rule engine",
                        "Participated in microservices migration from monolithic architecture",
                        "Implemented automated testing frameworks for rule validation",
                        "Collaborated with business analysts to translate requirements into code"
                    },
                    Technologies = new List<string> { "Java", "Drools", "Spring Boot", "Microservices", "Oracle DB" },
                    StartDate = DateTime.SpecifyKind(new DateTime(2022, 1, 1), DateTimeKind.Utc),
                    EndDate = DateTime.SpecifyKind(new DateTime(2023, 5, 31), DateTimeKind.Utc),
                    IsCurrentRole = false,
                    DisplayOrder = 2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            // Seed Education
            var education = new List<Education>
            {
                new Education
                {
                    Institution = "Boston University",
                    Degree = "Master of Science",
                    FieldOfStudy = "Computer Information Systems",
                    Location = "Boston, MA",
                    Description = "Graduate program focusing on advanced software engineering, database systems, and enterprise architecture",
                    Coursework = new List<string>
                    {
                        "CS669 Database Design and Implementation",
                        "CS602 Server-Side Web Development",
                        "CS673 Software Engineering",
                        "Network Analysis and Design"
                    },
                    StartDate = DateTime.SpecifyKind(new DateTime(2023, 9, 1), DateTimeKind.Utc),
                    IsCurrentlyEnrolled = true,
                    DisplayOrder = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            // Add to context
            context.Projects.AddRange(projects);
            context.Skills.AddRange(skills);
            context.Experiences.AddRange(experiences);
            context.Education.AddRange(education);

            // Save changes
            context.SaveChanges();
        }
    }
}