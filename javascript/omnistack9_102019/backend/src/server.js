const express = require('express');
const mongoose = require('mongoose');
const routes = require('./routes');
const cors = require('cors');
const path = require('path');
const app = express();

mongoose.connect('mongodb+srv://omnistack:omnistack@cluster0-jhuln.mongodb.net/semana09?retryWrites=true&w=majority', {
    useNewUrlParser: true,
    useUnifiedTopology: true,
});

// req.query = Acessar query params (para filtros - get)
// req.params = Acessar route params (para edição, delete)
// req.body = Acessar corpo da requisição (criação, edição)

//app.use(cors({ origin: 'http://localhost:3333' }));
app.use(cors());
app.use(express.json());
//static = trabalhará com arquivos estáticos. Volto uma pasta e entro na uploads (será usado no Spot.js)
app.use('/files', express.static(path.resolve(__dirname, '..', 'uploads')));
app.use(routes);

app.listen(3333);