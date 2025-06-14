import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import api from "../api/api";
import type { ProjectResponseDTO, TaskResponseDTO } from "../types";
import "../styles/ProjectDetail.css";

const ProjectDetail = () => {
    const { projectId } = useParams<{ projectId: string }>();
    const [project, setProject] = useState<ProjectResponseDTO | null>(null);
    const [tasks, setTasks] = useState<TaskResponseDTO[]>([]);

    // Formulario
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [dueDate, setDueDate] = useState("");
    const [priority, setPriority] = useState("Medium");
    const [showModal, setShowModal] = useState(false);
    const [editingTask, setEditingTask] = useState<TaskResponseDTO | null>(null);

    // Filtros
    const [statusFilter, setStatusFilter] = useState("");
    const [priorityFilter, setPriorityFilter] = useState("");
    const [showOnlyOverdue, setShowOnlyOverdue] = useState(false);

    useEffect(() => {
        fetchProjectAndTasks();
    }, [projectId]);

    const fetchProjectAndTasks = async () => {
        try {
            const resProject = await api.get(`/projects/${projectId}`);
            setProject(resProject.data);

            const resTasks = await api.get(`/tasks/project/${projectId}`);
            setTasks(resTasks.data);
        } catch (error) {
            console.error("Error fetching project details:", error);
        }
    };

    const handleEditTask = (task: TaskResponseDTO) => {
        setEditingTask(task);
        setTitle(task.title);
        setDescription(task.description);
        setDueDate(task.dueDate.split("T")[0]);
        setPriority(task.priority === 3 ? "High" : task.priority === 2 ? "Medium" : "Low");
        setShowModal(true);
    };

    const toggleTaskStatus = async (task: TaskResponseDTO) => {
        try {
            await api.put(`/tasks/${task.id}`, {
                ...task,
                isCompleted: !task.isCompleted,
            });
            fetchProjectAndTasks();
        } catch (error) {
            alert("Error al cambiar el estado de la tarea");
        }
    };


    const handleDeleteTask = async (taskId: string) => {
        if (confirm("¿Estás seguro de que quieres eliminar esta tarea?")) {
            try {
                await api.delete(`/tasks/${taskId}`);
                fetchProjectAndTasks();
            } catch (err) {
                alert("Error al eliminar la tarea");
            }
        }
    };

    const handleSubmitTask = async (e: React.FormEvent) => {
        e.preventDefault();
        const priorityMap: { [key: string]: number } = { Low: 1, Medium: 2, High: 3 };

        try {
            if (editingTask) {
                await api.put(`/tasks/${editingTask.id}`, {
                    title,
                    description,
                    dueDate,
                    priority: priorityMap[priority],
                    isCompleted: editingTask.isCompleted,
                });
            } else {
                await api.post("/tasks", {
                    title,
                    description,
                    dueDate,
                    priority: priorityMap[priority],
                    projectId,
                    assignedUserId: "default-user-id",
                });
            }

            setTitle("");
            setDescription("");
            setDueDate("");
            setPriority("Medium");
            setEditingTask(null);
            setShowModal(false);
            fetchProjectAndTasks();
        } catch (error) {
            alert("Error al guardar la tarea");
        }
    };

    const handleCloseModal = () => {
        setShowModal(false);
        setEditingTask(null);
        setTitle("");
        setDescription("");
        setDueDate("");
        setPriority("Medium");
    };

    const filteredTasks = tasks.filter((task) => {
        const now = new Date();
        const dueDate = new Date(task.dueDate);

        const matchesStatus =
            statusFilter === "" ||
            (statusFilter === "done" && task.isCompleted) ||
            (statusFilter === "pending" && !task.isCompleted);

        const matchesPriority =
            priorityFilter === "" || task.priority.toString() === priorityFilter;

        const isOverdue =
            !showOnlyOverdue || (!task.isCompleted && dueDate < now);

        return matchesStatus && matchesPriority && isOverdue;
    });

    if (!project) return <p className="loading">Cargando proyecto...</p>;

    return (
        <div className="project-detail-container">
            <h1 className="project-title">{project.name}</h1>
            <p className="project-description">{project.description}</p>

            <h2 className="task-section-title">Tareas</h2>

            {/* Filtros */}
            <div className="task-filters">
                <select value={statusFilter} onChange={(e) => setStatusFilter(e.target.value)}>
                    <option value="">Todas</option>
                    <option value="pending">Pendientes</option>
                    <option value="done">Completadas</option>
                </select>

                <select value={priorityFilter} onChange={(e) => setPriorityFilter(e.target.value)}>
                    <option value="">Todas las prioridades</option>
                    <option value="1">Baja</option>
                    <option value="2">Media</option>
                    <option value="3">Alta</option>
                </select>

                <label>
                    <input
                        type="checkbox"
                        checked={showOnlyOverdue}
                        onChange={(e) => setShowOnlyOverdue(e.target.checked)}
                    />
                    Solo vencidas
                </label>
            </div>

            {/* Lista de tareas */}
            <ul className="task-list">
                {filteredTasks.map((task) => (
                    <li className="task-card" key={task.id}>
                        <div className="task-title">{task.title}</div>
                        <div className="task-meta">
                            <span className={`task-status ${task.isCompleted ? "done" : ""}`}>
                                {task.isCompleted ? "Completada" : "Pendiente"}
                            </span>
                            <span className="task-priority">Prioridad: {task.priority}</span>
                            <span className="task-due">
                                Vence: {new Date(task.dueDate).toLocaleDateString()}
                            </span>
                        </div>
                        <div className="task-actions">
                            <button onClick={() => handleEditTask(task)}>✏️ Editar</button>
                            <button onClick={() => toggleTaskStatus(task)}>
                                ✅ {task.isCompleted ? "Marcar como Pendiente" : "Marcar como Completada"}
                            </button>
                            <button onClick={() => handleDeleteTask(task.id)}>🗑️ Eliminar</button>

                        </div>
                    </li>
                ))}
            </ul>

            <hr />
            <div className="create-task-header">
                <button onClick={() => setShowModal(true)} className="btn-create-task">
                    + Nueva Tarea
                </button>
            </div>

            {showModal && (
                <div className="modal-overlay">
                    <div className="modal">
                        <h2>{editingTask ? "Editar Tarea" : "Crear Tarea"}</h2>
                        <form onSubmit={handleSubmitTask} className="modal-form">
                            <input
                                type="text"
                                placeholder="Título"
                                value={title}
                                onChange={(e) => setTitle(e.target.value)}
                                required
                            />
                            <textarea
                                placeholder="Descripción"
                                value={description}
                                onChange={(e) => setDescription(e.target.value)}
                                required
                            />
                            <input
                                type="date"
                                value={dueDate}
                                onChange={(e) => setDueDate(e.target.value)}
                            />
                            <select
                                value={priority}
                                onChange={(e) => setPriority(e.target.value)}
                            >
                                <option value="Low">Baja</option>
                                <option value="Medium">Media</option>
                                <option value="High">Alta</option>
                            </select>
                            <div className="modal-actions">
                                <button type="submit">{editingTask ? "Guardar Cambios" : "Crear"}</button>
                                <button type="button" onClick={handleCloseModal} className="cancel">
                                    Cancelar
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default ProjectDetail;
