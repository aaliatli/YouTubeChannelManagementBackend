import { useState } from "react";
import Input from "../Components/Input";
import Button from "../Components/Button";
import { userService } from "../Services/apiService";

export default function RegisterPage() {
  const [form, setForm] = useState({
    userName: "",
    userLastName: "",
    userMail: "",
    userPassword: ""
  });
  const [message, setMessage] = useState("");

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await userService.register(form); 
      setMessage("Kayıt başarılı! Artık giriş yapabilirsiniz.");
    } catch (err) {
      setMessage("Kayıt başarısız!");
    }
  };

  return (
    <div style={{ maxWidth: 350, margin: "20px auto" }}>
      <h2>Kayıt Ol</h2>
      <form onSubmit={handleSubmit}>
        <Input
          type="text"
          name="userName"
          placeholder="Ad"
          value={form.userName}
          onChange={handleChange}
        />
        <Input
          type="text"
          name="userLastName"
          placeholder="Soyad"
          value={form.userLastName}
          onChange={handleChange}
        />
        <Input
          type="email"
          name="userMail"
          placeholder="E-Posta"
          value={form.userMail}
          onChange={handleChange}
        />
        <Input
          type="password"
          name="userPassword"
          placeholder="Şifre"
          value={form.userPassword}
          onChange={handleChange}
        />
        <Button type="submit" text="Kayıt Ol" />
      </form>
      {message && <p>{message}</p>}
    </div>
  );
}
