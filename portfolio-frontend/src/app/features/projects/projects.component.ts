import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../core/services/api.service';
import { Project } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [CommonModule],
  template: `
    <section class="section">
      <div class="container">
        <h1 class="page-title">All Projects</h1>
        <p class="page-subtitle">A collection of my work across different technologies and domains</p>

        <div *ngIf="loading" class="spinner"></div>

        <div *ngIf="!loading" class="grid grid-3">
          <div *ngFor="let project of projects" class="card project-card">
            <h3>{{ project.title }}</h3>
            <p>{{ project.description }}</p>
            
            <div class="project-details">
              <div class="technologies">
                <span *ngFor="let tech of project.technologies" class="badge">{{ tech }}</span>
              </div>
              
              <div class="project-footer">
                <div class="project-links">
                  <a *ngIf="project.githubUrl" [href]="project.githubUrl" target="_blank" class="link-button">
                    GitHub
                  </a>
                  <a *ngIf="project.liveUrl" [href]="project.liveUrl" target="_blank" class="link-button">
                    Live Demo
                  </a>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div *ngIf="!loading && projects.length === 0" class="empty-state">
          <p>No projects found. Check back soon!</p>
        </div>
      </div>
    </section>
  `,
  styles: [`
    .page-title {
      text-align: center;
      margin-bottom: 1rem;
      font-size: 3rem;
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      font-weight: 800;
      letter-spacing: -0.02em;
    }

    .page-subtitle {
      text-align: center;
      color: var(--text-secondary);
      margin-bottom: 4rem;
      font-size: 1.25rem;
      max-width: 600px;
      margin-left: auto;
      margin-right: auto;
      line-height: 1.6;
    }

    .project-card {
      display: flex;
      flex-direction: column;
      height: 100%;
      transition: all var(--transition-base);
    }

    .project-card h3 {
      color: var(--primary-color);
      margin-bottom: 1rem;
      font-size: 1.5rem;
    }

    .project-card p {
      flex-grow: 1;
      margin-bottom: 1.5rem;
      color: var(--text-secondary);
      line-height: 1.7;
    }

    .project-details {
      margin-top: auto;
    }

    .technologies {
      display: flex;
      flex-wrap: wrap;
      gap: 0.5rem;
      margin-bottom: 1.25rem;
      padding-bottom: 1.25rem;
      border-bottom: 1px solid var(--border-color);
    }

    .project-footer {
      padding-top: 0;
    }

    .project-links {
      display: flex;
      gap: 1rem;
    }

    .link-button {
      flex: 1;
      padding: 0.625rem 1.25rem;
      border: 2px solid var(--primary-color);
      color: var(--primary-color);
      border-radius: var(--radius-lg);
      font-weight: 600;
      transition: all var(--transition-base);
      text-align: center;
      font-size: 0.9375rem;
    }

    .link-button:hover {
      background: var(--gradient-primary);
      color: white;
      transform: translateY(-2px);
      box-shadow: var(--shadow-lg);
      border-color: transparent;
    }

    .empty-state {
      text-align: center;
      padding: 5rem 0;
      color: var(--text-secondary);
    }

    .empty-state p {
      font-size: 1.125rem;
    }

    @media (max-width: 768px) {
      .page-title {
        font-size: 2.25rem;
      }

      .page-subtitle {
        font-size: 1.125rem;
        margin-bottom: 3rem;
      }
    }

    @media (max-width: 480px) {
      .page-title {
        font-size: 1.875rem;
      }

      .page-subtitle {
        font-size: 1rem;
      }
    }
  `]
})
export class ProjectsComponent implements OnInit {
  private apiService = inject(ApiService);

  projects: Project[] = [];
  loading = true;

  ngOnInit() {
    this.loadProjects();
  }

  loadProjects() {
    this.apiService.getProjects().subscribe({
      next: (data) => {
        this.projects = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading projects:', err);
        this.loading = false;
      }
    });
  }
}
