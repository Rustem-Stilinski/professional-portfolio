import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../../core/services/api.service';
import { AuthService } from '../../core/services/auth.service';
import { LoginRequest } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <section class="section">
      <div class="container">
        <div class="admin-container">
          <div *ngIf="!isAuthenticated" class="login-card card">
            <h2>Admin Login</h2>
            <p class="login-subtitle">Sign in to manage your portfolio</p>

            <form (ngSubmit)="login()" #loginForm="ngForm">
              <div class="form-group">
                <label for="username">Username</label>
                <input 
                  type="text" 
                  id="username" 
                  name="username" 
                  [(ngModel)]="credentials.username" 
                  required
                  class="form-control">
              </div>

              <div class="form-group">
                <label for="password">Password</label>
                <input 
                  type="password" 
                  id="password" 
                  name="password" 
                  [(ngModel)]="credentials.password" 
                  required
                  class="form-control">
              </div>

              <button 
                type="submit" 
                class="btn-primary btn-full" 
                [disabled]="!loginForm.valid || loading">
                {{ loading ? 'Signing in...' : 'Sign In' }}
              </button>

              <div *ngIf="error" class="error-message">
                {{ error }}
              </div>
            </form>
          </div>

          <div *ngIf="isAuthenticated" class="admin-dashboard card">
            <h2>Admin Dashboard</h2>
            <p>You are logged in as an administrator.</p>
            
            <div class="admin-actions">
              <p class="info-text">
                Use the Swagger UI to manage your portfolio content:
              </p>
              <a href="http://localhost:5000/swagger" target="_blank" class="btn-primary">
                Open API Documentation
              </a>
              
              <button (click)="logout()" class="btn-secondary" style="margin-top: 2rem;">
                Logout
              </button>
            </div>
          </div>
        </div>
      </div>
    </section>
  `,
  styles: [`
    .admin-container {
      max-width: 500px;
      margin: 4rem auto;
    }

    .login-card,
    .admin-dashboard {
      padding: 2.5rem;
    }

    .login-card h2,
    .admin-dashboard h2 {
      text-align: center;
      color: var(--primary-color);
      margin-bottom: 0.5rem;
    }

    .login-subtitle {
      text-align: center;
      color: var(--secondary-color);
      margin-bottom: 2rem;
    }

    .form-group {
      margin-bottom: 1.5rem;
    }

    .form-group label {
      display: block;
      margin-bottom: 0.5rem;
      font-weight: 500;
      color: var(--text-color);
    }

    .form-control {
      width: 100%;
      padding: 0.75rem;
      border: 1px solid var(--border-color);
      border-radius: 0.375rem;
      font-size: 1rem;
      transition: border-color 0.3s ease;
    }

    .form-control:focus {
      outline: none;
      border-color: var(--primary-color);
    }

    .btn-full {
      width: 100%;
      margin-top: 1rem;
    }

    .error-message {
      margin-top: 1rem;
      padding: 1rem;
      background-color: #fee2e2;
      color: #991b1b;
      border-radius: 0.375rem;
      text-align: center;
    }

    .admin-actions {
      margin-top: 2rem;
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .info-text {
      color: var(--secondary-color);
      text-align: center;
    }

    .admin-actions a,
    .admin-actions button {
      width: 100%;
    }
  `]
})
export class AdminComponent {
  private apiService = inject(ApiService);
  private authService = inject(AuthService);
  private router = inject(Router);

  credentials: LoginRequest = {
    username: '',
    password: ''
  };

  loading = false;
  error = '';

  get isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  login() {
    this.loading = true;
    this.error = '';

    this.apiService.login(this.credentials).subscribe({
      next: (response) => {
        this.authService.setToken(response.token);
        this.loading = false;
        // Stay on admin page to show dashboard
      },
      error: (err) => {
        this.loading = false;
        this.error = 'Invalid username or password';
        console.error('Login error:', err);
      }
    });
  }

  logout() {
    this.authService.clearToken();
    this.credentials = { username: '', password: '' };
  }
}
