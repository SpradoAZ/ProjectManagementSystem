import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import api from "../../api/api";

const CreateTask = () => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [dueDate, setDueDate] = useState("");
  const [priority, setPriority] = useState("Medium");

  const { id: projectId } = useParams();
  const navigate = useNavigate();

  const mapPriorityToNumber = (value: string) => {
    switch (value) {
      case "Low": return 1;
      case "Medium": return 2;
      case "High": return 3;
      default: return 2;
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await api.post("/tasks", {
        title,
        description,
        dueDate,
        priority: mapPriorityToNumber(priority),
        projectId,
        assignedUserId: "default-user-id"
      });
      navigate(`/projects/${projectId}`);
    } catch (err) {
      alert("Error al crear la tarea");
    }
  };

  return (
    <div className="create-task-container">
      <form onSubmit={handleSubmit} className="create-task-form">
        <h2 className="create-task-title">Crear Nueva Tarea</h2>
        
        <div className="form-group">
          <label htmlFor="title">Título</label>
          <input
            id="title"
            type="text"
            placeholder="Nombre de la tarea"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            required
            className="form-input"
          />
        </div>

        <div className="form-group">
          <label htmlFor="description">Descripción</label>
          <textarea
            id="description"
            placeholder="Detalles de la tarea"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
            className="form-textarea"
          />
        </div>

        <div className="form-row">
          <div className="form-group">
            <label htmlFor="dueDate">Fecha límite</label>
            <input
              id="dueDate"
              type="date"
              value={dueDate}
              onChange={(e) => setDueDate(e.target.value)}
              className="form-input"
            />
          </div>

          <div className="form-group">
            <label htmlFor="priority">Prioridad</label>
            <select
              id="priority"
              value={priority}
              onChange={(e) => setPriority(e.target.value)}
              className="form-select"
            >
              <option value="Low">Baja</option>
              <option value="Medium">Media</option>
              <option value="High">Alta</option>
            </select>
          </div>
        </div>

        <div className="form-actions">
          <button 
            type="button" 
            onClick={() => navigate(`/projects/${projectId}`)}
            className="cancel-button"
          >
            Cancelar
          </button>
          <button type="submit" className="submit-button">
            Crear Tarea
          </button>
        </div>
      </form>
    </div>
  );
};

export default CreateTask;