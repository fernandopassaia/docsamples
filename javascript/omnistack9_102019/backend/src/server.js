const express = require('express');

const app = express();

//Uma requisição sempre tem o que o usuário envia (req) e a resposta do servidor (res)
//arrow function é uma maneira simplificada de escrever uma função dentro da função
app.post('/users', (req, res) => {
    //return res.send('Hello World');
    return res.json({ message: 'Hello World' });
});

app.listen(3333);