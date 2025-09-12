import { useState } from "react";
import { channelService } from "../Services/apiService";

export default function ChannelAddPage() {
  const [form, setForm] = useState({ channelName: "", category: "" });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    await channelService.createChannel(form);
    alert("Kanal eklendi!");
  };

  return (
    <div>
        <h2>Kanal Ekle</h2>
        <form onSubmit={handleSubmit}>
        <input
            name="channelName"
            placeholder="Kanal Adı"
            onChange={handleChange}
        />
        <input
            name="category"
            placeholder="Kategori"
            onChange={handleChange}
        />
        <button type="submit">Ekle</button>
        </form>
    </div>
   
  );
}
