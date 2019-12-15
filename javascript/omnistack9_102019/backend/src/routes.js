const express = require('express');
const SessionController = require('./controllers/SessionController');
const routes = express.Router();
// req.body = Acessar corpo da requisição (criação, edição)

routes.post('/sessions', SessionController.store);
module.exports = routes;