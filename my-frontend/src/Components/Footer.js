export default function Footer() {
  return (
    <footer
      style={{
        marginTop: "30px",
        padding: "10px",
        background: "#f1f1f1",
        textAlign: "center",
        fontSize: "14px"
      }}
    >
      <p>© {new Date().getFullYear()} YouTube Channel Management</p>
    </footer>
  );
}
