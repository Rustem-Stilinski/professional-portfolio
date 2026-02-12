# Portfolio Frontend - Angular 19

Modern, responsive portfolio website built with Angular 19 (standalone components) consuming the Portfolio API.

## Features

✅ **Home Page** with Hero, Projects, Skills, Experience, Education, Contact sections
✅ **Projects Page** showing all projects with filtering
✅ **Project Detail Page** for individual projects
✅ **Admin Login** with JWT authentication
✅ **Contact Form** integrated with API
✅ **Responsive Design** - works on all devices
✅ **Standalone Components** - using Angular 19 best practices
✅ **Type-safe** - Full TypeScript integration

## Tech Stack

- **Angular 19** - Latest version with standalone components
- **TypeScript 5.6** - Type safety
- **RxJS** - Reactive programming
- **SCSS** - Modern styling
- **HTTP Client** - API communication
- **Router** - Client-side routing

## Prerequisites

- Node.js 18+ 
- npm 9+
- Backend API running on http://localhost:5000

## Quick Start

### 1. Install Dependencies

```bash
npm install
```

### 2. Start Development Server

```bash
npm start
```

The app will run on **http://localhost:4200**

### 3. Make Sure Backend is Running

Before starting, ensure your Portfolio API is running:

```bash
cd ../PortfolioAPI
dotnet run
```

Backend should be available at: http://localhost:5000

## Available Scripts

```bash
# Start development server
npm start

# Build for production
npm run build

# Run tests
npm test

# Watch mode (auto-rebuild)
npm run watch
```

## Project Structure

```
src/
├── app/
│   ├── core/
│   │   ├── models/
│   │   │   └── portfolio.models.ts      # TypeScript interfaces
│   │   ├── services/
│   │   │   ├── api.service.ts           # API HTTP calls
│   │   │   └── auth.service.ts          # JWT token management
│   │   └── interceptors/
│   │       └── auth.interceptor.ts      # Add JWT to requests
│   │
│   ├── features/
│   │   ├── home/
│   │   │   ├── home.component.ts        # Main landing page
│   │   │   └── home.component.scss
│   │   ├── projects/
│   │   │   └── projects.component.ts    # All projects list
│   │   ├── project-detail/
│   │   │   └── project-detail.component.ts  # Single project
│   │   └── admin/
│   │       └── admin.component.ts       # Login & dashboard
│   │
│   ├── app.component.ts                 # Root component with nav
│   └── app.routes.ts                    # Routing configuration
│
├── styles.scss                          # Global styles
├── index.html                           # HTML entry point
└── main.ts                             # Application bootstrap
```

## Configuration

### API URL

The API URL is configured in `src/app/core/services/api.service.ts`:

```typescript
private apiUrl = 'http://localhost:5000/api';
```

For production, update this to your deployed API URL.

### Environment-Specific Configuration

Create environment files for different deployments:

**src/environments/environment.ts** (Development)
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

**src/environments/environment.prod.ts** (Production)
```typescript
export const environment = {
  production: true,
  apiUrl: 'https://your-api.azurewebsites.net/api'
};
```

## Features Overview

### 🏠 Home Page

- **Hero Section** - Eye-catching introduction
- **Featured Projects** - Showcase top 3 projects
- **Skills** - Technical skills grouped by category with proficiency bars
- **Experience Timeline** - Work history with details
- **Education** - Academic background
- **Contact Form** - Send messages directly to API

### 📂 Projects Page

- Grid layout of all projects
- Technology badges
- Links to GitHub and live demos
- Click through to project details

### 🔐 Admin Section

- Login with JWT authentication
- Token stored in localStorage
- Protected routes (can be extended)
- Quick access to Swagger API documentation

## Customization

### Colors

Edit CSS variables in `src/styles.scss`:

```scss
:root {
  --primary-color: #2563eb;    // Blue
  --secondary-color: #64748b;  // Gray
  --accent-color: #10b981;     // Green
  --bg-color: #ffffff;         // White
  --text-color: #1e293b;       // Dark gray
}
```

### Navigation

Update links in `src/app/app.component.ts`:

```typescript
<ul class="nav-links">
  <li><a routerLink="/">Home</a></li>
  <li><a routerLink="/projects">Projects</a></li>
  <li><a href="#contact">Contact</a></li>
  <li><a routerLink="/admin">Admin</a></li>
</ul>
```

## Build for Production

```bash
npm run build
```

Output will be in `dist/portfolio-frontend/`

### Deploy to Cloud

**Netlify / Vercel:**
```bash
# Install CLI
npm install -g netlify-cli
# or
npm install -g vercel

# Deploy
netlify deploy --prod
# or
vercel --prod
```

**Azure Static Web Apps:**
```bash
# Install CLI
npm install -g @azure/static-web-apps-cli

# Deploy
swa deploy
```

**AWS S3 + CloudFront:**
```bash
# Build
npm run build

# Upload to S3
aws s3 sync dist/portfolio-frontend/ s3://your-bucket-name
```

## CORS Configuration

Make sure your backend allows requests from your frontend:

In `PortfolioAPI/Program.cs`:

```csharp
app.UseCors(options => options
    .WithOrigins("http://localhost:4200")  // Development
    .AllowAnyMethod()
    .AllowAnyHeader());
```

For production, add your deployed frontend URL.

## Troubleshooting

### API Connection Failed

**Problem:** Cannot connect to http://localhost:5000

**Solution:**
1. Check backend is running: `dotnet run` in PortfolioAPI folder
2. Verify URL in `api.service.ts` matches your backend
3. Check CORS settings in backend

### 404 on Refresh

**Problem:** Page refreshes return 404

**Solution:** Configure your web server for SPA routing:

**Netlify:** Add `public/_redirects`:
```
/*    /index.html   200
```

**Azure:** Add `web.config` to dist folder

### Images Not Loading

**Problem:** Images return 404

**Solution:** Place images in `public/` folder, they'll be copied to dist automatically.

## Next Steps

1. ✅ Frontend complete and running
2. 🔜 Add more features:
   - Project filtering by technology
   - Search functionality
   - Dark mode toggle
   - Blog section
   - Resume download
3. 🔜 Deploy to production
4. 🔜 Add analytics (Google Analytics)
5. 🔜 SEO optimization

## License

MIT

---

**Happy coding! 🚀**
