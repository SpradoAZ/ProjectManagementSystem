import Navbar from "../components/Navbar";

const ProtectedLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <>
      <Navbar />
      <main className="protected-content">{children}</main>
    </>
  );
};

export default ProtectedLayout;
