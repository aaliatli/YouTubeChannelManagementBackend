import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "./Components/Navbar";
import Footer from "./Components/Footer";
import LoginPage from "./Pages/LoginPage";
import RegisterPage from "./Pages/RegisterPage";
import ChannelListPage from "./Pages/ChannelListPage";
import ChannelAddPage from "./Pages/ChannelAddPage";
import ChannelUpdatePage from "./Pages/ChannelUpdatePage";
import ChannelDeletePage from "./Pages/ChannelDeletePage";
import ChannelOperationsPage from "./Pages/ChannelOperationsPage";
import MainPage from "./Pages/MainPage";
import SubscriptionsPage from "./Pages/SubscriptionsPage";

function App() {
  return (
    <BrowserRouter>
      <div className="app-container">
        <Navbar />
        <div className="app-content" style={{ padding: "20px" }}>
          <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/channels" element={<ChannelListPage />} />
            <Route path="/channels/channel-add" element={<ChannelAddPage/>}/>
            <Route path="/channels/channel-update" element={<ChannelUpdatePage/>}/>
            <Route path="/channels/channel-delete" element={<ChannelDeletePage/>}/>
            <Route path="/channels/subscriptions" element={<SubscriptionsPage/>}/>
            <Route path="/channels/channel-operations" element={<ChannelOperationsPage />} />
            <Route path="/" element={<MainPage/>} />
          </Routes>
        </div>
        <Footer />
      </div>
    </BrowserRouter>
  );
}

export default App;
