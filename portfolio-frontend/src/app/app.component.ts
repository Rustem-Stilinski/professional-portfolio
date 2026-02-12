import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <nav class="navbar" [class.scrolled]="isScrolled">
      <div class="container nav-container">
        <a routerLink="/" class="logo">
          <h2>Rustem Aisariyev</h2>
        </a>
        <ul class="nav-links">
          <li><a routerLink="/" routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}">Home</a></li>
          <li><a routerLink="/projects" routerLinkActive="active">Projects</a></li>
          <li><a href="#contact" (click)="scrollToSection($event, 'contact')">Contact</a></li>
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
      background: rgba(255, 255, 255, 0.95);
      backdrop-filter: blur(10px);
      -webkit-backdrop-filter: blur(10px);
      box-shadow: 0 1px 3px rgba(0, 0, 0, 0.05);
      position: sticky;
      top: 0;
      z-index: 100;
      transition: all 0.3s ease;
      border-bottom: 1px solid rgba(226, 232, 240, 0.6);
    }

    .navbar.scrolled {
      background: rgba(255, 255, 255, 0.98);
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
      border-bottom-color: rgba(226, 232, 240, 0.8);
    }

    .nav-container {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 1.25rem 1.5rem;
    }

    .logo {
      transition: transform 0.3s ease;
    }

    .logo:hover {
      transform: scale(1.02);
    }

    .logo h2 {
      margin: 0;
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      font-size: 1.5rem;
      font-weight: 800;
      letter-spacing: -0.02em;
    }

    .nav-links {
      display: flex;
      list-style: none;
      gap: 2.5rem;
      margin: 0;
      padding: 0;
    }

    .nav-links a {
      color: var(--text-color);
      font-weight: 600;
      font-size: 0.9375rem;
      transition: all 0.3s ease;
      position: relative;
      padding: 0.5rem 0;
    }

    .nav-links a::after {
      content: '';
      position: absolute;
      bottom: 0;
      left: 0;
      width: 0;
      height: 2px;
      background: linear-gradient(90deg, #667eea, #764ba2);
      transition: width 0.3s ease;
    }

    .nav-links a:hover::after,
    .nav-links a.active::after {
      width: 100%;
    }

    .nav-links a:hover,
    .nav-links a.active {
      color: var(--primary-color);
    }

    main {
      min-height: calc(100vh - 200px);
    }

    .footer {
      background: linear-gradient(135deg, #1e293b 0%, #334155 100%);
      color: white;
      padding: 2.5rem 0;
      text-align: center;
      margin-top: 5rem;
      position: relative;
    }

    .footer::before {
      content: '';
      position: absolute;
      top: 0;
      left: 0;
      right: 0;
      height: 3px;
      background: linear-gradient(90deg, #667eea, #764ba2, #06b6d4);
    }

    .footer p {
      margin: 0;
      opacity: 0.9;
      font-size: 0.9375rem;
    }

    @media (max-width: 768px) {
      .nav-container {
        padding: 1rem 1.25rem;
      }

      .logo h2 {
        font-size: 1.25rem;
      }

      .nav-links {
        gap: 1.5rem;
        font-size: 0.875rem;
      }
    }

    @media (max-width: 640px) {
      .nav-links {
        gap: 1rem;
      }

      .nav-links a {
        font-size: 0.8125rem;
      }
    }
  `]
})
export class AppComponent {
  currentYear = new Date().getFullYear();
  isScrolled = false;

  constructor() {
    if (typeof window !== 'undefined') {
      window.addEventListener('scroll', () => {
        this.isScrolled = window.scrollY > 20;
      });
    }
  }

  scrollToSection(event: Event, sectionId: string) {
    event.preventDefault();
    const element = document.getElementById(sectionId);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }
}
