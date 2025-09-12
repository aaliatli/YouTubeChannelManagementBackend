import { useState, useEffect } from "react";
import { channelService } from "../Services/apiService";

export default function MainPage() {
  const [channels, setChannels] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchAllChannels();
  }, []);

  const fetchAllChannels = async () => {
    try {
      const res = await channelService.getAllChannels();
      setChannels(res.data);
      setLoading(false);
    } catch (err) {
      console.error("Kanallar çekilirken hata oluştu:", err);
      setError("Kanallar yüklenirken bir hata oluştu.");
      setLoading(false);
    }
  };

const handleSubscribe = async (channelId) => {
    try {
        await channelService.subscribeToChannel(channelId);
        alert("Kanala başarıyla abone olundu!");
        // Abone olunan kanalı görmek için listeyi yeniden çek
        await fetchAllChannels(); 
    } catch (err) {
        console.error("Abone olunurken hata:", err);
        alert("Abone olunurken hata oluştu.");
    }
};
  if (loading) return <div>Kanallar yükleniyor...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div>
      <h2>Tüm Kanallar</h2>
      {channels.length === 0 ? (
        <p>Henüz hiç kanal bulunmamaktadır.</p>
      ) : (
        <div>
          {channels.map((channel) => (
  <div
    key={channel.channelId}
    style={{
      border: "1px solid #ddd",
      padding: "15px",
      margin: "10px 0",
      borderRadius: "8px",
    }}
  >
    <h3>{channel.channelName}</h3>
    {/* kategori ve butonu aynı satıra aldık */}
    <div
      style={{
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
      }}
    >
      <p style={{ margin: 0 }}>Kategori: {channel.category}</p>
      <button onClick={() => handleSubscribe(channel.channelId)}>
        Abone Ol
      </button>
    </div>
  </div>
))}

        </div>
      )}
    </div>
  );
}
