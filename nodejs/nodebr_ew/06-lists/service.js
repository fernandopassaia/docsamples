const axios = require('axios')
const URL = `https://swapi.co/api/people` //api gratuita do Star Wars

//uso async por que quero manipular promises internas dessa função
async function obterPessoas (nome){
    //quando eu quero falar que to usando uma variavel, só usar a crase e dólar/crifrão
    const url = `${URL}/?search=${nome}&format=json`
    const response = await axios.get(url) //axios é uma promisse, então preciso passar await
    return response.data
}

//para ver como testa a função acima, rode/veja classe consumindoApi
module.exports = {
    obterPessoas //exporto o método (como se fosse uma DLL, ficará visível)
    //poderia declarar "obterPessoas: obterPessoas2" (caso eu quisesse mudar o nome na exportação)
}