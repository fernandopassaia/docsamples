//classe desenvolvida pra mostrar o uso de listas
//essa classe consome o service.js que consome uma webApi do StarWars

//quando eu importo módulos que EU MESMO criei, vou usar ./ (internos usa sem ./)
const service = require('./service')

//essa função irá manipular promises, então sempre coloco o async na frente
async function main(){
    try{
        const result = await service.obterPessoas('a')
        console.log('resultado: ', result) //printo o genérico

        const names = []
        
        //exemplo usando o FOR normal
         console.time('for')
         for(let i=0; i <= result.results.length -1; i++)
         {
             const pessoa = result.results[i]
             names.push(pessoa.name)            
         }
         console.timeEnd('for') //0.199ms - pior
        
        //exemplo usando o FORIN (parece o Foreach do C#)
         console.time('forin')
         for(let i in result.results){
             const pessoa = result.results[i]
             names.push(pessoa.name)         
         }
         console.timeEnd('forin') //0.041ms - melhor
        
        //exemplo usando o FOROF
        console.time('forof')
        for(pessoa of result.results){
            names.push(pessoa.name)
        }
        console.time('forof') //0.046ms

        console.log('')
        console.log('')
        console.log('Nomes: ', names)
    }
    catch(error){
        console.error('erro interno: ', error)
    }
}

main() //chamada para o método main acima