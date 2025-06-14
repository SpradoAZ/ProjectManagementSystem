// src/components/Navbar.tsx
import { useNavigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";
import "../styles/Navbar.css";

const Navbar = () => {
  const navigate = useNavigate();
  const { logout } = useAuth();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  const handleGoBack = () => {
    navigate(-1); // Navega a la página anterior en el historial
  };

  return (
    <nav className="navbar">
      <button onClick={handleGoBack}>⬅ Volver</button>
      <h1>Gestión de Proyectos</h1>
      <button onClick={handleLogout}>Cerrar sesión</button>
    </nav>
  );
};

export default Navbar;
