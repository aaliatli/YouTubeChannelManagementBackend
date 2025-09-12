import { useState } from "react";
import Input from "../Components/Input";
import Button from "../Components/Button";
import { userService } from "../Services/apiService";
import { useNavigate } from "react-router-dom";


export default function LoginPage() {
  const [form, setForm] = useState({
    userMail: "",
    userPassword: ""
  });
  const [message, setMessage] = useState("");
  const navigate = useNavigate();
  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const res = await userService.login(form); // POST api/user/login
      localStorage.setItem("token", res.data.token); // JWT token saklanıyor
      navigate("/channels")
    } catch (err) {
      setMessage("Giriş başarısız! Kullanıcı veya şifre hatalı.");
    }
  };

  return (
    <div style={{ maxWidth: 350, margin: "20px auto" }}>
      <h2>Giriş Yap</h2>
      <form onSubmit={handleSubmit}>
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
        <Button type="submit" text="Giriş Yap" />
      </form>
      {message && <p>{message}</p>}
    </div>
  );
}
