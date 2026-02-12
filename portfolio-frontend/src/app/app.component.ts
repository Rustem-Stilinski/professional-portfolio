import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <nav class="navbar">
      <div class="container nav-container">
        <a routerLink="/" class="logo">
          <h2>Rustem Aisariyev</h2>
        </a>
        <ul class="nav-links">
          <li><a routerLink="/" routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}">Home</a></li>
          <li><a routerLink="/projects" routerLinkActive="active">Projects</a></li>
          <li><a href="#contact">Contact</a></li>
          <li><a routerLink="/admin" routerLinkActive="active">Admin</a></li>
        </ul>
      </div>
    </nav>

    <main>
      <router-outlet></router-outlet>
    </main>

    <footer class="footer">
      <div class="container">
        <p>&copy; {{ currentYear }} Rustem Aisariyev. All rights reserved.</p>
      </div>
    </footer>
  `,
  styles: [`
    .navbar {
      background: white;
      box-shadow: 0 2px 4px rgba(0,0,0,0.1);
      position: sticky;
      top: 0;
      z-index: 100;
    }

    .nav-container {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 1rem 1.5rem;
    }

    .logo h2 {
      margin: 0;
      color: var(--primary-color);
    }

    .nav-links {
      display: flex;
      list-style: none;
      gap: 2rem;
      margin: 0;
    }

    .nav-links a {
      color: var(--text-color);
      font-weight: 500;
      transition: color 0.3s ease;
    }

    .nav-links a:hover,
    .nav-links a.active {
      color: var(--primary-color);
    }

    main {
      min-height: calc(100vh - 200px);
    }

    .footer {
      background: #1e293b;
      color: white;
      padding: 2rem 0;
      text-align: center;
      margin-top: 4rem;
    }

    .footer p {
      margin: 0;
    }

    @media (max-width: 768px) {
      .nav-links {
        gap: 1rem;
        font-size: 0.9rem;
      }
    }
  `]
})
export class AppComponent {
  currentYear = new Date().getFullYear();
}
