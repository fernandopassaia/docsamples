const axios = require('axios')
const URL = `https://swapi.co/api/people` //api gratuita do Star Wars

//uso async por que quero manipular promises internas dessa função
async function obterPessoas (nome){
    //quando eu quero falar que to usando uma variavel, só usar a crase e dólar/crifrão
    const url = `${URL}/?search=${nome}&format=json`
    const response = await axios.get(url) //axios é uma promisse, então preciso passar await
    return response.data
}

//testando o resultado acima
obterPessoas('r2') //r2 é o robozinho do Star Wars
.then(function (resultado) {//quando chegar o resultado (conversão escrota automática)
    //quando chegar um resultado, você vai printar na tela esse resulado pra mim
    console.log('Resultado: ', resultado)
})
.catch(function (error) { //se der algum erro, eu jogo no error (conversão automática)
    console.error('DEU RUIM: ', error)
})