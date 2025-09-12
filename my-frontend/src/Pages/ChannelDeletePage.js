import { useState, useEffect } from "react";
import { channelService } from "../Services/apiService";

export default function ChannelDeletePage() {
  const [userChannels, setUserChannels] = useState([]);
  const [selectedChannelId, setSelectedChannelId] = useState("");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchUserChannels();
  }, []);

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

  const handleSelectChange = (e) => {
    setSelectedChannelId(e.target.value); 
  };

  const handleDelete = async () => {
    if (!selectedChannelId) {
      alert("Lütfen silinecek bir kanal seçin.");
      return;
    }

    if (window.confirm("Bu kanalı silmek istediğinizden emin misiniz?")) {
      try {
        await channelService.deleteChannel(selectedChannelId); 
        alert("Kanal başarıyla silindi!");
        setUserChannels(userChannels.filter(channel => channel.channelId !== selectedChannelId));
        setSelectedChannelId(""); 
      } catch (err) {
        console.error("Kanal silinirken hata oluştu:", err);
        alert("Kanal silinirken bir hata oluştu.");
      }
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
      <h2>Kanal Sil</h2>
      {userChannels.length === 0 ? (
        <p>Henüz silinecek bir kanalınız bulunmamaktadır.</p>
      ) : (
        <>
          <select 
            name="channelSelector" 
            value={selectedChannelId} 
            onChange={handleSelectChange}
            style={{ marginRight: '10px' }}
          >
            {userChannels.map((channel) => (
              <option key={channel.channelId} value={channel.channelId}>
                {channel.channelName} ({channel.category})
              </option>
            ))}
          </select>
          
          <button 
            onClick={handleDelete}
            style={{ backgroundColor: 'red', color: 'white', border: 'none', padding: '5px 10px', cursor: 'pointer' }}
          >
            Seçili Kanalı Sil
          </button>
        </>
      )}
    </div>
  );
}