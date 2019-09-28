/*
0 Obter um usuário
1 Obter o número de telefone de um usuário a partir de seu Id
2 Obter o endereço do usuário pelo Id
*/


//entendendo a Sincronia da coisa: Quando chamar o obterUsuario, eu passo uma função como parametro (callback)
//e ela vai ser chamada passando o resultado pra quem chamou, assim que ela for resolvida.
function obterUsuario(callback){
    setTimeout(function (){
        //o Return irá retornar depois de 1 segundo por causa da função setTimeOut (que manda esperar um tempo)
        return callback(null,{ //estou chamando o resolverUsuario, o primeiro parametro é o erro (null) e o segundo o sucesso (usuário)
            id: 1,
            nome: 'Steeeefaaaann',
            dataNascimento: new Date()
        })
    },1000) //aviso que irá esperar 1 segundo pra voltar
}

function obterTelefone(idUsuario, callback)//callback é sempre o último parametro
{
    setTimeout(() => {
        return callback(null,{
            telefone: '(11)98104-9080',
            ddd: 11            
        })
    },2000) //aviso que irá esperar 2 segundos pra voltar
}

//note que está havendo uma "simulação" de dados, e os dois segundos é pra simular que fosse a demora de um
//banco de dados ou mesmo uma webapi em devolver a informação
function obterEndereco(idUsuario, callback){
    setTimeout(() => {
        return callback(null,{  //null quer dizer que não deu erro no callback, to retornando erro vazio
            rua: 'Tó Utca',
            numero: 0
        })
    },2000);
}



//aqui é a chamada de tudo que chamará os métodos e causará a escrita no console
obterUsuario(function resolverUsuario(error,usuario){
// null || "" || 0 sempre será "false" no Javascript...
    if(error)    {
        console.error("DEU RUIM NO USUÁRIO", error)
        return; //volto do método
    }    
    //se passou ai eu chamo o segundo método que é obterTelefone
    //variavel se chama "error1" por que error já foi declarado, sempre lembrar que no "padrão" callback
    //eu declaro o ERRO em primeiro lugar, e o acerto em segundo. "telefone" é o RETORNO, na frente do
    //function eu passo o "usuario.Id" por que é parametro pedido lá no obter Telefone... ou seja:
    //passa o usuário.Id pro método, e depois chama o "callback" (função) pra sincronizar
    obterTelefone(usuario.id, function resolverTelefone(error1, telefone){
        if(error1)    {
            console.error("DEU RUIM NO TELEFONE", error1)
            return; //volto do método
        }    
    
        obterEndereco(usuario.id, function resolverEndereco(error2, endereco){
            if(error2)    {
                console.error("DEU RUIM NO ENDERECO", error2)
                return; //volto do método
            }
            
            //no javascript eu uso essa crase ` pra imprimir valor de variáveis
            console.log(`
                Nome: ${usuario.nome},
                Endereco: ${endereco.rua},${endereco.numero}
                Telefone: ${telefone.ddd},${telefone.telefone}
            `)
        })
    })
})
