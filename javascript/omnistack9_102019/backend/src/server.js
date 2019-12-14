const express = require('express');
const routes = require('./routes');

const app = express();

// req.query = Acessar query params (para filtros - get)
// req.params = Acessar route params (para edição, delete)
// req.body = Acessar corpo da requisição (criação, edição)

app.use(express.json());
app.use(routes);

app.listen(3333);