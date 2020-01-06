//o objetivo do filter é filtrar as informações como se fosse um banco de dados
const{ obterPessoas } = require('./service') //segunda maneira de importar: Eu digo que do Service, quero apenas obterPessoas (uau!)

/* explicação (sobre acima) - técnica chamada DESTRUCTING
(pode ser usado num Json, Array - conforme abaixo, ou classe/serviço conforme acima)
Explicação: Supondo que eu tenha um "Json" que retorne dois campos conforme abaixo:

const item = {
    nome: 'Erick',
    idade: 12
}

se eu fizesse conforme abaixo, conseguiria extrair apenas o nome:
const { nome } = item
console.log(nome) //imprimia somente o nome

Ou eu poderia fazer pra pegar 2 campos ou mais:
const { nome, idade } = item
console.log(nome, idade) //ai eu imprimia 2 variaveis, ou quntas eu quisesse

agora faremos filtros que permitem trabalhar como se estivessemos num banco de dados (order e tal)
*/

//SEGUNDA MANEIRA: Usando meu próprio filtro (usado lá embaixo)
Array.prototype.meuFilter = function (callback) {
    const lista = []
    for (index in this) {
        const item = this[index]
        const result = callback(item, index, this)
        // 0, "", null, undefined === false
        if (!result) continue;
        lista.push(item)
    }
    return lista;
}



async function main() {
    try{
        //agora vou pegar o results de dentro do nosso obterUsuario - só o results, sem count, paginação, nada
        const { results } = await obterPessoas(`a`)

        //PRIMEIRA MANEIRA: Usando o Map conforme abaixo: agora eu extraio somente o necessário de dentro de pessoas
        //(results) - vou pegar só o que é família Lars note que "filter" é um método do array interno, é tipo um 
        //lance de where (e indexOf é o "==")
        const familiaLars = results.filter(function (item){
            //por padrão precisa retornar um booleano para informar se deve manter ou remover da lista
            //false = remove da lista | true = mantem na lista
            //objetivo: não encontrou = -1 | encontrou = posicao no array
            const result = item.name.toLowerCase().indexOf(`lars`) !== -1 //isso é true, tire o "!" e ficará false
            return result
        })
        const names = familiaLars.map((pessoa) => pessoa.name) //pego o nome
        console.log(names)


        //SEGUNDA MANEIRA: Usando meu próprio filtro.
        const familiaLars2 = results.meuFilter((item, index, lista) => {
            console.log(`index: ${index}`, lista.length)
            return item.name.toLowerCase().indexOf('lars') !== -1 //isso é true, tire o "!" e ficará false
        })

        const names2 = familiaLars2.map((pessoa) => pessoa.name)
        console.log(names2)


    }catch(error){
        console.error('DEU RUIM: ', error)
    }

}
main() //chamada do método principal acima