/*
0 Obter um usuário
1 Obter o número de telefone de um usuário a partir de seu Id
2 Obter o endereço do usuário pelo Id
*/

//Nota: Aqui comecei a aula "Introdução a resolução de Promises com Async/Await"
//Vou resolver toda aquela "gambiarra" das aulas anteriores com Async e Await + Promises.
//note que está havendo uma "simulação" de dados, e os dois segundos é pra simular que fosse a demora de um
//banco de dados ou mesmo uma webapi em devolver a informação


//TERCEIRO REFATORAMENTO MELHORADO: Importamos o módulo interno do node.js pra melhor refatorar e usar o Promises
const util = require('util') //primeira biblioteca que vou importar do node
const obterEnderecoAsync = util.promisify(obterEndereco) //o método obter endereço NÃO FOI modificado, continua do jeito
//antigo, porém essa biblioteca irá "promisifizar" (converter) o método automaticamente



//Quem chamar obterUsuario, como estou dando RETURN no promise, ele consegue recuperar as informações
//olhe a chamada o obterUsuario() lá embaixo que também ficou bem + simples
function obterUsuario(){
    //quando der algum problema -> chamamos o reject e passamos o erro pra dentro dele
    //quando tudo acontecer conforme esperado -> chamamos o resolve (É PRECISO LEMBRAR DESSA ASSINATURA)
    return new Promise(function resolvePromise(resolve,reject) {   
        setTimeout(function (){
            //se der tudo certo retornarei um resolve com o objeto e as informações do usuário            
            //return reject(new Error('DEU RUIM')) //simulação caso eu quisesse voltar um erro            
            return resolve({
                id: 1,
                nome: 'Steeeefaaaann',
                dataNascimento: new Date()
            })
        },1000) //aviso que irá esperar 1 segundo pra voltar
    })    
}

function obterTelefone(idUsuario)
{
    return new Promise(function resolverPromise(resolve,reject){ //recebe uma função, e o resolvePromise um resolve e um reject    
        setTimeout(() => {
            return resolve({
                telefone: '(11)98104-9080',
                ddd: 11            
            })
        },2000); //aviso que irá esperar 2 segundos pra voltar
    })
}


function obterEndereco(idUsuario, callback){
    
    setTimeout(() => {
        return callback(null,{ //aqui estamos seguindo o jeito antigo e a convenção do callback: Primeiro resultado é o erro, segundo o retorno certo.
            rua: 'Tó Utca',
            numero: 0
        })
    },2000);
}


//aqui começa a resolução usando Promises + Async/Await (main é pra simular o static void main)
//1o passo: adicionar a palavra async -> e automaticamente ela retornará uma promisse
main() //aqui eu chamo o método main (abaixo) pra ele executar
async function main() {
    try{
        console.time('medida-promise') //pra medir quanto tempo está demorando
        
        //PRIMEIRA FORMA: As 3 linhas abaixos é uma das maneiras de executar Promise por Promise, demora mais, levou 5008ms pra executar.
        //const usuario = await obterUsuario() //obterUsuario vai me retornar uma promisse
        //const telefone = await obterTelefone(usuario.id) //estou pegando o telefone, e mandando o id do usuário
        //const endereco = await obterEnderecoAsync(usuario.id) //estou pegando o endereço, mandando o id do usuário

        //SEGUNDA FORMA: Executando obterTelefone e obterEndereco em PARALELO! Mais de uma prommise por vez - levou 3008ms pra executar.
        //em resumo: Irei executar telefone e endereço ao MESMO tempo, pois ambos só dependem de usuário pra executar
        //não precisam ficar um esperando o final da execução do outro. Desse modo foi 2s mais rápido! (40% menos tempo).
        //Pra re-testar comente abaixo até o "const endereco = resultadoDos2[1]"" e descomente as 3 linhas acima (que são mais lentas)
        const usuario = await obterUsuario() //obterUsuario vai me retornar uma promisse        
        const resultadoDos2 = await Promise.all([
            obterTelefone(usuario.id), //estou pegando o telefone, e mandando o id do usuário
            obterEnderecoAsync(usuario.id) //estou pegando o endereço, mandando o id do usuário
        ])
        const telefone = resultadoDos2[0] //pego o resultado acima e separo em duas variaveis
        const endereco = resultadoDos2[1]
               

        console.log(`
             Nome: ${usuario.nome}
             Endereco: ${endereco.rua}, ${endereco.numero}
             Telefone: (${telefone.ddd}), ${telefone.telefone}
         `)
         console.timeEnd('medida-promise')
    }catch(error){
        console.error('DEU XABU: ', error)
    }
}
//Nota: Esse código todo acima (desde o main) executa exatamente a mesma coisa que esse código bem maior abaixo.








// //chamadas para os métodos acima! Todos .then ou .catch sempre irão retornar uma Promise
// //conceito de PIPE - ordem de execução: usuario -> telefone -> telefone
// const usuarioPromise = obterUsuario() //armazeno o retorno em uma variável
// //para manipular o sucesso usamos a função .then / para manipular os erros usamos o .catch
// usuarioPromise
//     .then(function (usuario) { //"usuário" ou "resultado" é o usuarioPromise, retorno do obterUsuario(). Essa conversão é mto away.
//         //vou resolver primeiro telefone, ai depois pegar um novo objeto para resolver usuário também
//         return obterTelefone(usuario.id) //esse RETURN retornará o resultado que irá parar lá no (resultado) do function embaixo
//             .then(function resolverTelefone(result){
//                 return {
//                     usuario: {
//                         nome: usuario.nome,
//                         id: usuario.id
//                     },
//                     telefone: result
//                 }
//             })
//     })
//     .then(function (resultado) {
//         //esse "resultado" é SEMPRE o resultado vindo do método anterior (pode chamar de qualquer coisa - Aff, veja jpg "resultado.jpg")
//         const endereco = obterEnderecoAsync(resultado.usuario.id) //lembrando que declarei isso lá em cima junto com o util
//         return endereco.then(function resolverEndereco(result){
//             return{
//                 usuario: resultado.usuario, //isso são objetos que já estão vindo lá de cima
//                 telefone: resultado.telefone,  //telefone está vindo lá de cima (telefone: result)
//                 endereco: result
//             }
//         })
//     })
//     .then(function (resultado) { //resultado vai vir sempre de CIMA e aqui poderia ter qualquer nome (resultado2, usuario, etc)
//         console.log('Print de maneira genérica jogando o result na tela:')
//         console.log('resultado: ', resultado) //DEIXEI AQUI apenas para mostrar como é um "print genérico". Abaixo dou o Print "estruturado" de maneira certa
//         console.log('')
//         console.log('Agora printando de maneira certa e estruturada:')
//         console.log(`
//             Nome: ${resultado.usuario.nome}
//             Endereco: ${resultado.endereco.rua}, ${resultado.endereco.numero}
//             Telefone: (${resultado.telefone.ddd}), ${resultado.telefone.telefone}
//         `)
//     })
//     .catch(function (error) { //catch quando der erro
//         console.error('DEU RUIM', error)
//     })