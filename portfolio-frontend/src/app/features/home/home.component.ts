import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/services/api.service';
import { Project, Skill, Experience, Education, ContactMessage } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <!-- Hero Section -->
    <section class="hero">
      <div class="container">
        <div class="hero-content">
          <h1 class="hero-title">Full Stack Software Engineer</h1>
          <p class="hero-subtitle">Building scalable web applications with ASP.NET Core, Angular, and cloud technologies</p>
          <div class="hero-buttons">
            <a href="#projects" class="btn-primary">View Projects</a>
            <a href="#contact" class="btn-secondary">Get in Touch</a>
          </div>
        </div>
      </div>
    </section>

    <!-- Featured Projects Section -->
    <section id="projects" class="section">
      <div class="container">
        <h2 class="section-title">Featured Projects</h2>
        
        <div *ngIf="loading.projects" class="spinner"></div>
        
        <div *ngIf="!loading.projects" class="grid grid-2">
          <div *ngFor="let project of featuredProjects" class="card project-card">
            <h3>{{ project.title }}</h3>
            <p>{{ project.description }}</p>
            <div class="technologies">
              <span *ngFor="let tech of project.technologies" class="badge">{{ tech }}</span>
            </div>
            <div class="project-links">
              <a *ngIf="project.githubUrl" [href]="project.githubUrl" target="_blank" class="btn-secondary">GitHub</a>
              <a *ngIf="project.liveUrl" [href]="project.liveUrl" target="_blank" class="btn-primary">Live Demo</a>
            </div>
          </div>
        </div>
        
        <div class="text-center" style="margin-top: 2rem;">
          <a routerLink="/projects" class="btn-primary">View All Projects</a>
        </div>
      </div>
    </section>

    <!-- Skills Section -->
    <section class="section bg-light">
      <div class="container">
        <h2 class="section-title">Technical Skills</h2>
        
        <div *ngIf="loading.skills" class="spinner"></div>
        
        <div *ngIf="!loading.skills" class="skills-container">
          <div *ngFor="let category of skillCategories" class="skill-category">
            <h3>{{ category }}</h3>
            <div class="skills-cloud">
              <span *ngFor="let skill of getSkillsByCategory(category)" 
                    class="skill-badge"
                    [attr.data-level]="getSkillLevel(skill.proficiencyLevel)">
                {{ skill.name }}
              </span>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Experience Section -->
    <section class="section">
      <div class="container">
        <h2 class="section-title">Work Experience</h2>
        
        <div *ngIf="loading.experiences" class="spinner"></div>
        
        <div *ngIf="!loading.experiences" class="timeline">
          <div *ngFor="let exp of experiences" class="timeline-item card">
            <div class="timeline-header">
              <h3>{{ exp.position }}</h3>
              <span class="company">{{ exp.company }}</span>
            </div>
            <div class="timeline-meta">
              <span>{{ exp.location }}</span>
              <span>{{ formatDate(exp.startDate) }} - {{ exp.isCurrentRole ? 'Present' : formatDate(exp.endDate) }}</span>
            </div>
            <p>{{ exp.description }}</p>
            <ul class="responsibilities">
              <li *ngFor="let resp of exp.responsibilities">{{ resp }}</li>
            </ul>
            <div class="technologies">
              <span *ngFor="let tech of exp.technologies" class="badge">{{ tech }}</span>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Education Section -->
    <section class="section bg-light">
      <div class="container">
        <h2 class="section-title">Education</h2>
        
        <div *ngIf="loading.education" class="spinner"></div>
        
        <div *ngIf="!loading.education" class="education-grid">
          <div *ngFor="let edu of education" class="education-card">
            <div class="education-header">
              <div class="education-icon">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 14l9-5-9-5-9 5 9 5z"/>
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 14l6.16-3.422a12.083 12.083 0 01.665 6.479A11.952 11.952 0 0012 20.055a11.952 11.952 0 00-6.824-2.998 12.078 12.078 0 01.665-6.479L12 14z"/>
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 14l9-5-9-5-9 5 9 5zm0 0l6.16-3.422a12.083 12.083 0 01.665 6.479A11.952 11.952 0 0012 20.055a11.952 11.952 0 00-6.824-2.998 12.078 12.078 0 01.665-6.479L12 14zm-4 6v-7.5l4-2.222"/>
                </svg>
              </div>
              <div class="education-info">
                <h3 class="education-degree">{{ edu.degree }}</h3>
                <h4 class="education-institution">{{ edu.institution }}</h4>
                <p class="education-field" *ngIf="edu.fieldOfStudy">{{ edu.fieldOfStudy }}</p>
              </div>
            </div>
            
            <div class="education-date">
              <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z"/>
              </svg>
              <span>{{ formatDate(edu.startDate) }} - {{ edu.isCurrentlyEnrolled ? 'Present' : formatDate(edu.endDate) }}</span>
            </div>
            
            <div *ngIf="edu.coursework.length > 0" class="education-coursework">
              <h5>Relevant Coursework</h5>
              <div class="coursework-tags">
                <span *ngFor="let course of edu.coursework" class="coursework-tag">
                  {{ course }}
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>


    <!-- Contact Section -->
    <section id="contact" class="section">
      <div class="container">
        <h2 class="section-title">Get in Touch</h2>
        
        <div class="contact-container">
          <div class="contact-info">
            <h3>Let's Connect</h3>
            <p>I'm always open to discussing new opportunities, projects, or just connecting with fellow developers.</p>
          </div>
          
          <form class="contact-form" (ngSubmit)="submitContactForm()" #contactForm="ngForm">
            <div class="form-group">
              <label for="name">Name</label>
              <input 
                type="text" 
                id="name" 
                name="name" 
                [(ngModel)]="contactMessage.name" 
                required
                class="form-control">
            </div>
            
            <div class="form-group">
              <label for="email">Email</label>
              <input 
                type="email" 
                id="email" 
                name="email" 
                [(ngModel)]="contactMessage.email" 
                required
                class="form-control">
            </div>
            
            <div class="form-group">
              <label for="subject">Subject</label>
              <input 
                type="text" 
                id="subject" 
                name="subject" 
                [(ngModel)]="contactMessage.subject"
                class="form-control">
            </div>
            
            <div class="form-group">
              <label for="message">Message</label>
              <textarea 
                id="message" 
                name="message" 
                [(ngModel)]="contactMessage.message" 
                rows="5" 
                required
                class="form-control"></textarea>
            </div>
            
            <button type="submit" class="btn-primary" [disabled]="!contactForm.valid || submitting">
              {{ submitting ? 'Sending...' : 'Send Message' }}
            </button>
            
            <div *ngIf="submitSuccess" class="success-message">
              Message sent successfully! I'll get back to you soon.
            </div>
            
            <div *ngIf="submitError" class="error-message">
              {{ submitError }}
            </div>
          </form>
        </div>
      </div>
    </section>
  `,
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  private apiService = inject(ApiService);

  featuredProjects: Project[] = [];
  skills: Skill[] = [];
  experiences: Experience[] = [];
  education: Education[] = [];

  loading = {
    projects: true,
    skills: true,
    experiences: true,
    education: true
  };

  skillCategories: string[] = [];

  contactMessage: ContactMessage = {
    name: '',
    email: '',
    subject: '',
    message: ''
  };

  submitting = false;
  submitSuccess = false;
  submitError = '';

  ngOnInit() {
    this.loadFeaturedProjects();
    this.loadSkills();
    this.loadExperiences();
    this.loadEducation();
  }

  loadFeaturedProjects() {
    this.apiService.getFeaturedProjects().subscribe({
      next: (data) => {
        this.featuredProjects = data;
        this.loading.projects = false;
      },
      error: (err) => {
        console.error('Error loading projects:', err);
        this.loading.projects = false;
      }
    });
  }

  loadSkills() {
    this.apiService.getSkills().subscribe({
      next: (data) => {
        this.skills = data;
        this.skillCategories = [...new Set(data.map(s => s.category))];
        this.loading.skills = false;
      },
      error: (err) => {
        console.error('Error loading skills:', err);
        this.loading.skills = false;
      }
    });
  }

  loadExperiences() {
    this.apiService.getExperiences().subscribe({
      next: (data) => {
        this.experiences = data;
        this.loading.experiences = false;
      },
      error: (err) => {
        console.error('Error loading experiences:', err);
        this.loading.experiences = false;
      }
    });
  }

  loadEducation() {
    this.apiService.getEducation().subscribe({
      next: (data) => {
        this.education = data;
        this.loading.education = false;
      },
      error: (err) => {
        console.error('Error loading education:', err);
        this.loading.education = false;
      }
    });
  }

  getSkillsByCategory(category: string): Skill[] {
    return this.skills.filter(s => s.category === category);
  }

  getSkillLevel(proficiency: number): string {
    if (proficiency >= 80) return 'expert';
    if (proficiency >= 60) return 'advanced';
    if (proficiency >= 40) return 'intermediate';
    return 'beginner';
  }

  formatDate(date: Date | undefined): string {
    if (!date) return '';
    return new Date(date).toLocaleDateString('en-US', { month: 'short', year: 'numeric' });
  }

  submitContactForm() {
    this.submitting = true;
    this.submitSuccess = false;
    this.submitError = '';

    this.apiService.sendContactMessage(this.contactMessage).subscribe({
      next: () => {
        this.submitting = false;
        this.submitSuccess = true;
        this.contactMessage = {
          name: '',
          email: '',
          subject: '',
          message: ''
        };
      },
      error: (err) => {
        this.submitting = false;
        this.submitError = 'Failed to send message. Please try again.';
        console.error('Error sending message:', err);
      }
    });
  }
}
