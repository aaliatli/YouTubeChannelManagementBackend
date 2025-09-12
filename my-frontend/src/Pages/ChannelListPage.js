import { useState, useEffect } from "react";
import { channelService } from "../Services/apiService";

export default function ChannelsPage() {
  const [myChannels, setMyChannels] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchMyChannels();
  }, []);

  const fetchMyChannels = async () => {
    try {
      setLoading(true);
      const token = localStorage.getItem("token");
      
      if (!token) {
        setError("Token bulunamadı. Lütfen tekrar giriş yapın.");
        return;
      }
      const res = await channelService.getMyChannels();
      setMyChannels(res.data);
    } catch (err) {
      setError("Kanallar yüklenirken bir hata oluştu.");
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <div>Yükleniyor...</div>;
  if (error) return <div>Hata: {error}</div>;

  return (
    <div>
      <h2>Kanallarım</h2>
      
      {myChannels.length === 0 ? (
        <p>Henüz hiç kanalınız yok.</p>
      ) : (
        <div>
          {myChannels.map(channel => (
            <div key={channel.channelId} style={{ 
              border: "1px solid #ddd", 
              padding: "15px", 
              margin: "10px 0", 
              borderRadius: "8px" 
            }}>
              <h3>{channel.channelName}</h3>
              <p>Kategori: {channel.category}</p>
              <p>Abone Sayısı: {channel.subscribers}</p>
              <p>Oluşturulma Tarihi: {channel.creationDate}</p>
              <p>Aktif/Pasif: {channel.isActive === 1 || channel.isActive === true ? "Aktif" : "Pasif"}</p>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}