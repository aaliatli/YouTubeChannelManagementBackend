import { useState, useEffect } from "react";
import { channelService } from "../Services/apiService";
import Input from "../Components/Input"; 
import Button from "../Components/Button"; 

export default function ChannelUpdatePage() {
  const [userChannels, setUserChannels] = useState([]);
  const [selectedChannelId, setSelectedChannelId] = useState("");
  const [form, setForm] = useState({ channelName: "", category: "" }); 
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchUserChannels();
  }, []);

  useEffect(() => {
    if (selectedChannelId) {
      const channelToUpdate = userChannels.find(ch => ch.channelId === selectedChannelId);
      if (channelToUpdate) {
        setForm({
          channelName: channelToUpdate.channelName,
          category: channelToUpdate.category
        });
      }
    }
  }, [selectedChannelId, userChannels]);

  const fetchUserChannels = async () => {
    try {
      const res = await channelService.getMyChannels();
      setUserChannels(res.data);
      if (res.data.length > 0) {
        setSelectedChannelId(res.data[0].channelId);
      }
      setLoading(false);
    } catch (err) {
      console.error("Kullanıcı kanalları çekilirken hata oluştu:", err);
      setError("Kanallar yüklenirken bir hata oluştu.");
      setLoading(false);
    }
  };

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };
  
  const handleSelectChange = (e) => {
    setSelectedChannelId(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!selectedChannelId) {
      alert("Lütfen güncellenecek bir kanal seçin.");
      return;
    }

    try {
      await channelService.updateChannel({ channelId: selectedChannelId, ...form });
      alert("Kanal başarıyla güncellendi!");
      fetchUserChannels(); 
    } catch (err) {
      console.error("Kanal güncellenirken hata oluştu:", err);
      alert("Kanal güncellenirken bir hata oluştu.");
    }
  };

  if (loading) {
    return <div>Kanallar yükleniyor...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <div>
      <h2>Kanal Güncelle</h2>
      {userChannels.length === 0 ? (
        <p>Henüz güncellenecek bir kanalınız bulunmamaktadır.</p>
      ) : (
        <form onSubmit={handleSubmit}>
          <select
            name="channelSelector"
            value={selectedChannelId}
            onChange={handleSelectChange}
            style={{ marginBottom: '20px' }}
          >
            {userChannels.map((channel) => (
              <option key={channel.channelId} value={channel.channelId}>
                {channel.channelName}
              </option>
            ))}
          </select>

          <input
            name="channelName"
            placeholder="Kanal Adı"
            value={form.channelName}
            onChange={handleChange}
          />
          <input
            name="category"
            placeholder="Kategori"
            value={form.category}
            onChange={handleChange}
          />
          <button type="submit">Güncelle</button>
        </form>
      )}
    </div>
  );
}