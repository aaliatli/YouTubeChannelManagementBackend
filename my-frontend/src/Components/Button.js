export default function Button({ text, onClick, type = "button" }) {
  return (
    <button
      type={type}
      onClick={onClick}
      style={{
        padding: "8px 16px",
        margin: "5px",
        border: "none",
        borderRadius: "5px",
        backgroundColor: "#007bff",
        color: "white",
        cursor: "pointer"
      }}
    >
      {text}
    </button>
  );
}
