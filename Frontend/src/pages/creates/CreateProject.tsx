import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../api/api";
import "../../styles/CreateProject.css";

const CreateProject = () => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState(""); // <-- NUEVO
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      // Enviar ambos campos requeridos por el backend
      await api.post("/projects", {
        name,
        description,
      });

      navigate("/projects");
    } catch (error) {
      alert("Error al crear el proyecto");
      console.error(error); // ← útil para debug
    }
  };

  return (
    <div className="create-project-container">
      <h2 className="create-project-title">Crear Nuevo Proyecto</h2>
      <form onSubmit={handleSubmit} className="create-project-form">
        <input
          type="text"
          placeholder="Nombre del proyecto"
          value={name}
          onChange={(e) => setName(e.target.value)}
          className="create-project-input"
          required
        />
        <textarea
          placeholder="Descripción del proyecto"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          className="create-project-input"
          rows={4}
          required
        />
        <button type="submit" className="create-project-button">
          Crear
        </button>
      </form>
    </div>
  );
};

export default CreateProject;
