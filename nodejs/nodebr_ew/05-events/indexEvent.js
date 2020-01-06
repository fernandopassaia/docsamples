const EventEmitter = require('events') //importo events do nodejs
class MeuEmissor extends EventEmitter { //como se eu "herdasse" 'events' pro meu emissor

}
const meuEmissor = new MeuEmissor() //inicializo a classe
const nomeEvento = 'usuario:click' 

//agora crio um evento que vai ficar "observando" qualquer coisa que ocorrer
//agora eu digo: quando ocorrer esse evento (nomeEvento), irei chamar essa função
meuEmissor.on(nomeEvento, function(click){
    console.log('um usuario clicou', click)
})



//DESCOMENTE AS LINHAS ABAIXO PARA RODAR A SIMULAÇÃO DE CHAMADA DE EVENTOS
// meuEmissor.emit(nomeEvento, 'na barra de rolagem') //como se forcasse o emissor acima
// meuEmissor.emit(nomeEvento, 'no ok') //como se forcasse o emissor acima

// //agora eu crio um "interval" (como se fosse a Thread no C#) pra ficar disparando
// let count = 0;
// setInterval(function () {
//     meuEmissor.emit(nomeEvento, 'tá OKé ' + count++)
// }, 1000)


//Agora simulando como se fosse uma aplicação console:
const stdin = process.openStdin()
stdin.addListener('data', function (value){
    console.log(`Você digitou: ${value.toString().trim()} `) //e ai toda vez que o usuário digitar alguma coisa, eu vou printar na tela
}) //falo que estou ouvindo o evento data (do próprio note) - depois minha função pegará o "value"