const express = require('express');
const routes = express.Router();

// req.body = Acessar corpo da requisição (criação, edição)
routes.post('/users', (req, res) => {
    return res.json(req.body);
});

module.exports = routes;