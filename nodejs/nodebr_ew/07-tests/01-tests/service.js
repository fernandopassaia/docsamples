const {
    get
} = require('axios')

const URL = 'https://swapi.co/api/people'

//quero obter as pessoas, recebo como parâmetro o nome
async function obterPessoas(nome){
    const url = `${URL}/?search=${nome}&format=json`
    const result = await get(url)
    
    console.log('Service.js - printando o resultado no console só pra testes:', result.data)
    console.log(' ')
    //o método stringfiy do Json irá mostrar os "arrays" de filmes e species que não são mostrados no result.data normal
    console.log('Service.js - printando o resultado no console só pra testes com JSON.stringify:', JSON.stringify( result.data))

    //return result.data // - se eu colocar assim retorno exatamente o formato do WebApi, mas, quero formatar o retorno
    return result.data.results.map(mapearPessoas) //ele vai entrar no mapearPessoas, e pra cada item do results vai retornar o nome e peso
}

//o webservice retorna alguns campos a mais, como massa, e eu quero retornar APENAS o nome e peso (name and height)
//além disso quero converter os nomes em inglês do webservice "name" e "height" para português, que será usado no tests.js
function mapearPessoas(item){
    return{
        nome: item.name,
        peso: item.height
    }
}

module.exports = {
    obterPessoas
}