import axios from 'axios'

const userApiClient = axios.create({
    baseURL: 'http://localhost:5100',
    headers: {'Content-Type' : 'application/json'}
});

const channelApiClient = axios.create({
    baseURL: 'http://localhost:5200',
    headers: {'Content-Type' : 'application/json'}
});

[userApiClient, channelApiClient].forEach((client) => {
  client.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  });
});

export const userService ={
    register: (userData) => userApiClient.post('/api/user/register-user', userData),
    login: (loginData) => userApiClient.post('/api/user/login', loginData),
    getAllUsers: () => userApiClient.get("/api/user/users"), // api prefix eklendi
    getUserById: (id) => userApiClient.get(`/api/user/${id}`), // api prefix eklendi
    deleteUser: (id) => userApiClient.delete(`/api/user/user-delete`, { data: { userId: id } }), // api prefix eklendi
    updateUser: (userData) => userApiClient.put("/api/user/update-user", userData), // api prefix eklendi
};

export const channelService ={
    createChannel: (channelData) => channelApiClient.post('/api/channel/create',channelData),
    getAllChannels: () => channelApiClient.get('/api/channel/get-channel'),
    deleteChannel: (id) => channelApiClient.delete(`/api/channel/${id}`),
    updateChannel: (channelData) => channelApiClient.put('/api/channel/update',channelData),
    getChannelById: (id) => channelApiClient.get(`/api/channel/${id}`), 
    subscribeToChannel: (channelId) => channelApiClient.post(`/api/channel/${channelId}/subscribe`), // Body parametresi kaldırıldı
    getMyChannels: () => channelApiClient.get('/api/channel/my-channels')
}