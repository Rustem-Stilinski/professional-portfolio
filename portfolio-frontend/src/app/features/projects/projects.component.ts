import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { ApiService } from '../../core/services/api.service';
import { Project } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
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
      font-size: 2.5rem;
    }

    .page-subtitle {
      text-align: center;
      color: var(--secondary-color);
      margin-bottom: 3rem;
      font-size: 1.1rem;
    }

    .project-card {
      display: flex;
      flex-direction: column;
      height: 100%;
    }

    .project-card h3 {
      color: var(--primary-color);
      margin-bottom: 1rem;
    }

    .project-card p {
      flex-grow: 1;
      margin-bottom: 1.5rem;
    }

    .project-details {
      margin-top: auto;
    }

    .technologies {
      display: flex;
      flex-wrap: wrap;
      gap: 0.5rem;
      margin-bottom: 1rem;
    }

    .project-footer {
      padding-top: 1rem;
      border-top: 1px solid var(--border-color);
    }

    .project-links {
      display: flex;
      gap: 1rem;
    }

    .link-button {
      padding: 0.5rem 1rem;
      border: 1px solid var(--primary-color);
      color: var(--primary-color);
      border-radius: 0.375rem;
      font-weight: 500;
      transition: all 0.3s ease;
    }

    .link-button:hover {
      background-color: var(--primary-color);
      color: white;
    }

    .empty-state {
      text-align: center;
      padding: 4rem 0;
      color: var(--secondary-color);
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
