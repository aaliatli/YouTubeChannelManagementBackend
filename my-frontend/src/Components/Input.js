export default function Input({ type = "text", placeholder, value, onChange, ...rest }) {
  return (
    <input
      type={type}
      placeholder={placeholder}
      value={value}
      onChange={onChange}
      {...rest}   // name vs. buradan aktarılır
      style={{
        padding: "8px",
        margin: "5px 0",
        border: "1px solid #ccc",
        borderRadius: "5px",
        width: "100%"
      }}
    />
  );
}
