/*
0 Obter um usuário
1 Obter o número de telefone de um usuário a partir de seu Id
2 Obter o endereço do usuário pelo Id
*/

//note que está havendo uma "simulação" de dados, e os dois segundos é pra simular que fosse a demora de um
//banco de dados ou mesmo uma webapi em devolver a informação

//nota, agora melhoraremos o exemplo 02 (index.js) para usar Promise
//O Promise recebe um CallBack, o Promisse recebe uma função com dois parametrôs: Resolve e Reject






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
        return callback(null,{
            rua: 'Tó Utca',
            numero: 0
        })
    },2000);
}



//chamadas para os métodos acima! Todos .then ou .catch sempre irão retornar uma Promise
//conceito de PIPE - ordem de execução: usuario -> telefone -> telefone
const usuarioPromise = obterUsuario() //armazeno o retorno em uma variável
//para manipular o sucesso usamos a função .then / para manipular os erros usamos o .catch
usuarioPromise
    .then(function (usuario) { //"usuário" ou "resultado" é o usuarioPromise, retorno do obterUsuario(). Essa conversão é mto away.
        //vou resolver primeiro telefone, ai depois pegar um novo objeto para resolver usuário também
        return obterTelefone(usuario.id)
            .then(function resolverTelefone(result){
                return {
                    usuario: {
                        nome: usuario.nome,
                        id: usuario.id
                    },
                    telefone: result
                }
            })
    })
    .then(function (resultado) { //then quando der tudo certo vou passar um callback pra pegar - o resultado é o usuário em si
        console.log('resultado jogando retorno sem tratar: ', resultado) //vou imprimir o usuário de qualquer jeito no console kk
    })
    .catch(function (error) { //catch quando der erro
        console.error('DEU RUIM', error)
    })

//aqui é a chamada de tudo que chamará os métodos e causará a escrita no console
// obterUsuario(function resolverUsuario(error,usuario){
// // null || "" || 0 sempre será "false" no Javascript...
//     if(error)    {
//         console.error("DEU RUIM NO USUÁRIO", error)
//         return; //volto do método
//     }    
//     //se passou ai eu chamo o segundo método que é obterTelefone
//     //variavel se chama "error1" por que error já foi declarado, sempre lembrar que no "padrão" callback
//     //eu declaro o ERRO em primeiro lugar, e o acerto em segundo. "telefone" é o RETORNO, na frente do
//     //function eu passo o "usuario.Id" por que é parametro pedido lá no obter Telefone... ou seja:
//     //passa o usuário.Id pro método, e depois chama o "callback" (função) pra sincronizar
//     obterTelefone(usuario.id, function resolverTelefone(error1, telefone){
//         if(error1)    {
//             console.error("DEU RUIM NO TELEFONE", error1)
//             return; //volto do método
//         }    
    
//         obterEndereco(usuario.id, function resolverEndereco(error2, endereco){
//             if(error2)    {
//                 console.error("DEU RUIM NO ENDERECO", error2)
//                 return; //volto do método
//             }
            
//             //no javascript eu uso essa crase ` pra imprimir valor de variáveis
//             console.log(`
//                 Nome: ${usuario.nome},
//                 Endereco: ${endereco.rua},${endereco.numero}
//                 Telefone: ${telefone.ddd},${telefone.telefone}
//             `)
//         })
//     })
// })