import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ApiService } from '../../core/services/api.service';
import { Project } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-project-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <section class="section">
      <div class="container">
        <a routerLink="/projects" class="back-link">&larr; Back to Projects</a>

        <div *ngIf="loading" class="spinner"></div>

        <div *ngIf="!loading && project" class="project-detail">
          <h1>{{ project.title }}</h1>
          
          <div class="project-meta">
            <span>Started: {{ formatDate(project.startDate) }}</span>
            <span *ngIf="project.endDate">Completed: {{ formatDate(project.endDate) }}</span>
          </div>

          <p class="project-description">{{ project.description }}</p>

          <div *ngIf="project.detailedDescription" class="project-detailed">
            <h2>About This Project</h2>
            <p>{{ project.detailedDescription }}</p>
          </div>

          <div class="technologies-section">
            <h2>Technologies Used</h2>
            <div class="technologies">
              <span *ngFor="let tech of project.technologies" class="badge">{{ tech }}</span>
            </div>
          </div>

          <div class="project-links">
            <a *ngIf="project.githubUrl" [href]="project.githubUrl" target="_blank" class="btn-secondary">
              View on GitHub
            </a>
            <a *ngIf="project.liveUrl" [href]="project.liveUrl" target="_blank" class="btn-primary">
              View Live Demo
            </a>
          </div>
        </div>

        <div *ngIf="!loading && !project" class="empty-state">
          <p>Project not found.</p>
          <a routerLink="/projects" class="btn-primary">Back to Projects</a>
        </div>
      </div>
    </section>
  `,
  styles: [`
    .back-link {
      display: inline-block;
      margin-bottom: 2rem;
      color: var(--primary-color);
      font-weight: 500;
    }

    .back-link:hover {
      text-decoration: underline;
    }

    .project-detail h1 {
      color: var(--primary-color);
      margin-bottom: 1rem;
    }

    .project-meta {
      display: flex;
      gap: 2rem;
      color: var(--secondary-color);
      margin-bottom: 2rem;
      font-size: 0.9rem;
    }

    .project-description {
      font-size: 1.1rem;
      line-height: 1.8;
      margin-bottom: 2rem;
    }

    .project-detailed {
      margin: 3rem 0;
    }

    .project-detailed h2 {
      margin-bottom: 1rem;
    }

    .technologies-section {
      margin: 3rem 0;
    }

    .technologies-section h2 {
      margin-bottom: 1rem;
    }

    .technologies {
      display: flex;
      flex-wrap: wrap;
      gap: 0.5rem;
    }

    .project-links {
      display: flex;
      gap: 1rem;
      margin-top: 3rem;
      padding-top: 3rem;
      border-top: 1px solid var(--border-color);
    }

    .empty-state {
      text-align: center;
      padding: 4rem 0;
    }

    .empty-state p {
      color: var(--secondary-color);
      margin-bottom: 2rem;
    }
  `]
})
export class ProjectDetailComponent implements OnInit {
  private apiService = inject(ApiService);
  private route = inject(ActivatedRoute);
  
  project: Project | null = null;
  loading = true;

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadProject(+id);
    }
  }

  loadProject(id: number) {
    this.apiService.getProject(id).subscribe({
      next: (data) => {
        this.project = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading project:', err);
        this.loading = false;
      }
    });
  }

  formatDate(date: Date | undefined): string {
    if (!date) return '';
    return new Date(date).toLocaleDateString('en-US', { month: 'long', year: 'numeric' });
  }
}
