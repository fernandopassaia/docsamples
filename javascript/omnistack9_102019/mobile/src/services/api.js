import axios from 'axios';

const api = axios.create({
    //baseURL: 'http://localhost:3333',
    baseURL: 'http://192.168.1.6:3333', //endereço do expo (pegar no EXPO!)
});

export default api;