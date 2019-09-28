const service = require('./service') //./ eu uso pra importar classes criadas por mim mesmo

//O map serve pra retornar um Array novo baseado no que o usuário pediu, então tipo:
//Se eu quiser que retorne apenas o nome, ou só o nome e o endereço... a "chamada" está lá em baixo (names4)
Array.prototype.meuMap = function (callback) {
    const novoArrayMapeado = []
    for (let indice = 0; indice <= this.length - 1; indice++) {
        const resultado = callback(this[indice], indice)
        novoArrayMapeado.push(resultado)
    }
    return novoArrayMapeado;
}

async function main(){
    try{
        
        //maneira um - com foreach
        const results = await service.obterPessoas('a')
        const names = []
        results.results.forEach(function (item){
            names.push(item.name)
        })


        //maneira 2 com maps
        const names2 = results.results.map(function (pessoa){
            return pessoa.name
        })

        //maneira 3 com maps melhorado
        const names3 = results.results.map((pessoa) => pessoa.name) //da pessoa que você pegou, quero só o name

        //maneira 4 - criando meu MAP personalizado (não entendi muito bem por que, mas...)
        const names4 = results.results.meuMap(function (pessoa, indice) {
            return `[${indice}]${pessoa.name}`
        })

        console.log('names com foreach: ', names)
        console.log('names com map: ', names2)
        console.log('names com map melhorado: ', names3)
        console.log('names com meu map personalizado: ', names4)
    }catch(error){
        console.error('DEU RUIM: ', error)
    }
}

main()