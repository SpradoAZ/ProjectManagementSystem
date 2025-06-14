// Tipos para Proyectos
export interface ProjectResponseDTO {
  id: string;
  name: string;
  description: string;
  createdAt: string; // o Date si prefieres convertirlo
}

// Tipos para Tareas
export interface TaskResponseDTO {
  id: string;
  title: string;
  description: string;
  projectId: string;
  dueDate: string;
  isCompleted: boolean;
  priority: number;
}

// Tipos para Usuarios
export interface UserResponseDTO {
  id: string;
  name: string;
  email: string;
}