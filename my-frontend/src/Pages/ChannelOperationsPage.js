import { useState } from "react";
import ChannelAddPage from "./ChannelAddPage";
import ChannelDeletePage from "./ChannelDeletePage";
import ChannelUpdatePage from "./ChannelUpdatePage";

export default function ChannelOperationsPage() {
  const [active, setActive] = useState("create");

  const renderContent = () => {
    switch (active) {
      case "create":
        return <ChannelAddPage />;
      case "delete":
        return <ChannelDeletePage />;
      case "update":
        return <ChannelUpdatePage />;
      default:
        return <h2>Bir işlem seçiniz</h2>;
    }
  };

  return (
    <div>
      <div style={{ display: "flex", height: "80vh", marginTop: "20px" }}>
        <div
          style={{
            width: "200px",
            border: "2px solid black",
            display: "flex",
            flexDirection: "column",
          }}
        >
          <button
            onClick={() => setActive("create")}
            style={{
              padding: "20px",
              borderBottom: "1px solid black",
              background: active === "create" ? "#ddd" : "white",
            }}
          >
            YouTube Kanal Oluştur
          </button>
          <button
            onClick={() => setActive("delete")}
            style={{
              padding: "20px",
              borderBottom: "1px solid black",
              background: active === "delete" ? "#ddd" : "white",
            }}
          >
            YouTube Kanalı Sil
          </button>
          <button
            onClick={() => setActive("update")}
            style={{
              padding: "20px",
              borderBottom: "1px solid black",
              background: active === "update" ? "#ddd" : "white",
            }}
          >
            YouTube Kanalı Güncelle
          </button>
        </div>

        <div
          style={{
            flex: 1,
            border: "2px solid black",
            background: "#ccc",
            padding: "20px",
          }}
        >
          {renderContent()}
        </div>
      </div>
    </div>
  );
}
