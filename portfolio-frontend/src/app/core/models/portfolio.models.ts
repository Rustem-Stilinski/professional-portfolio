export interface Project {
  id: number;
  title: string;
  description: string;
  detailedDescription?: string;
  imageUrl?: string;
  liveUrl?: string;
  githubUrl?: string;
  technologies: string[];
  startDate: Date;
  endDate?: Date;
  isFeatured: boolean;
  displayOrder: number;
}

export interface Skill {
  id: number;
  name: string;
  category: string;
  proficiencyLevel: number;
  iconUrl?: string;
  displayOrder: number;
}

export interface Experience {
  id: number;
  company: string;
  position: string;
  location?: string;
  description: string;
  responsibilities: string[];
  technologies: string[];
  startDate: Date;
  endDate?: Date;
  isCurrentRole: boolean;
  displayOrder: number;
}

export interface Education {
  id: number;
  institution: string;
  degree: string;
  fieldOfStudy?: string;
  location?: string;
  description?: string;
  gpa?: number;
  coursework: string[];
  startDate: Date;
  endDate?: Date;
  isCurrentlyEnrolled: boolean;
  displayOrder: number;
}

export interface ContactMessage {
  name: string;
  email: string;
  subject?: string;
  message: string;
}

export interface AuthResponse {
  token: string;
  username: string;
  email: string;
  expiresAt: Date;
}

export interface LoginRequest {
  username: string;
  password: string;
}
