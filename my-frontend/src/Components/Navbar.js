import { Link, useNavigate } from "react-router-dom";

export default function Navbar() {
  const navigate = useNavigate();
  const token = localStorage.getItem("token");

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/login");
  };

  return (
    <div
      style={{
        backgroundColor: "#333",
        padding: "10px",
        display: "flex",
        gap: "20px",
        color: "white",
      }}
    >
      <Link to="/" style={{ color: "white", textDecoration: "none" }}>
        Ana Sayfa
      </Link>

      {token ? (
        <>
          <Link to="/channels" style={{ color: "white", textDecoration: "none" }}>
            Kanallarım
          </Link>
          <Link
            to="/channels/channel-operations"
            style={{ color: "white", textDecoration: "none" }}
          >
            Kanal İşlemleri
          </Link>
          <Link to="/subscriptions" style={{ color: "white", textDecoration: "none" }}>
            Aboneliklerim
          </Link>
          <span
            onClick={handleLogout}
            style={{
              cursor: "pointer",
              marginLeft: "auto",
            }}
          >
            Çıkış Yap
          </span>
        </>
      ) : (
        <>
          <Link to="/login" style={{ color: "white", textDecoration: "none" }}>
            Giriş Yap
          </Link>
          <Link to="/register" style={{ color: "white", textDecoration: "none" }}>
            Kayıt Ol
          </Link>
        </>
      )}
    </div>
  );
}
