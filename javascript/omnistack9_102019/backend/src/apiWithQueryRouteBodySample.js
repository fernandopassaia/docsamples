const express = require('express');

const app = express();
app.use(express.json()); //habilito o JSON no express

//Uma requisição sempre tem o que o usuário envia (req) e a resposta do servidor (res)
//arrow function é uma maneira simplificada de escrever uma função dentro da função
// app.post('/users', (req, res) => {
//     //return res.send('Hello World');
//     return res.json({ message: 'Hello World' });
// });

// req.query = Acessar query params (para filtros - get)
app.get('/users', (req, res) => {
    return res.json({ idade: req.query.idade });
});

// req.params = Acessar route params (para edição, delete)
app.put('/users/:id', (req, res) => {
    return res.json({ id: req.params.id });
});

// req.body = Acessar corpo da requisição (criação, edição)
app.post('/users', (req, res) => {
    return res.json(req.body);
});

app.listen(3333);