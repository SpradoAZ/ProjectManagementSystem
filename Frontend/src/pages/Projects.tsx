import { useEffect, useState } from "react";
import api from "../api/api";
import type { ProjectResponseDTO } from "../types";
import { Link } from "react-router-dom";
import "../styles/Projects.css";

const Projects = () => {
  const [projects, setProjects] = useState<ProjectResponseDTO[]>([]);

  useEffect(() => {
    const fetchProjects = async () => {
      try {
        const response = await api.get("/projects");
        setProjects(response.data);
      } catch (error) {
        console.error("Error fetching projects:", error);
      }
    };
    fetchProjects();
  }, []);

  return (
    <div className="projects-container">
      <div className="projects-header-row">
        <h1 className="projects-header">Mis Proyectos</h1>
        <Link to="/create-project" className="create-project-button">
          + Nuevo Proyecto
        </Link>
      </div>
      <div className="project-list">
        {projects.map((project) => (
          <Link
            to={`/projects/${project.id}`}
            key={project.id}
            className="project-card"
          >
            <div className="project-title">{project.name}</div>
          </Link>
        ))}
      </div>
    </div>
  );
};

export default Projects;