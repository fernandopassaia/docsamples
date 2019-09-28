//o objetivo do filter é filtrar as informações como se fosse um banco de dados
const{ obterPessoas } = require('./service') //segunda maneira de importar: Eu digo que do Service, quero apenas obterPessoas (uau!)

//SEGUNDA MANEIRA: Usando meu próprio filtro (usado lá embaixo)
Array.prototype.meuReduce = function (callback, valorInicial) {
    let valorFinal = typeof valorInicial !== undefined ? valorInicial : this[0]
    for (let index = 0; index <= this.length - 1; index++) {
        valorFinal = callback(valorFinal, this[index], this)
    }
    return valorFinal
}


async function main() {
    try{

        //PRIMEIRA MANEIRA: Usando o Map conforme abaixo
        const { results } = await obterPessoas(`a`)
        //agora eu quero trazer todo o "peso" dessas pessoas, somar e verificar o peso de cada um
        const pesos = results.map(item => parseInt(item.height)) //força conversão por que vem string do webapi
        //resumo: agora eu tenho um array de pesos - tipo [ 172,202,150,178,165,183,182,188,180,228 ]
        //agora eu quero reduzir tudo a uma coisa só, ou seja, somar tudo: 20.2+30.3+40.5 = x
        const total = pesos.reduce((anterior, proximo) => {
            //função com 2 parametros, recebe o anterior, o próximo e soma
            return anterior + proximo
        })

        console.log('pesos: ', pesos)
        console.log('total: ', total)


        //SEGUNDA MANEIRA: Usando meu próprio filtro reduce - meu objetivo é concatenar e voltar uma lista só.        
        const minhaLista = [
            ['Erick', 'Wendel'],
            ['NodeBR', 'Nerdzão']
        ]
        const total2 = minhaLista.meuReduce((anterior, proximo) => {
                return anterior.concat(proximo)
            }, [])
            .join(', ')
        console.log('total', total2)

    }catch(error){
        console.error('DEU RUIM: ', error)
    }

}
main() //chamada do método principal acima