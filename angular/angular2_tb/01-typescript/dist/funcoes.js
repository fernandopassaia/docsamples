//função normal: note que eu consigo definir tipagem de parametros (numero) e de retorno (booleano)
var verificaNumero = function (valor) {
    return valor < 12;
};
var numeroAVerificar = 14;
console.log("O valor " + numeroAVerificar + " \u00E9 menor que o valor de 12? " + (verificaNumero(numeroAVerificar) ? 'YES' : 'NO'));
//agora arrow function - primeiro eu crio uma variável e ai declaro a minha função:
//do lado esquerdo declaração de parametros, do lado direito a implementação. Não preciso do nome
//"function" e também não preciso do keyword "return"
var chamar = function (name) { return console.log("Voc\u00EA entendeu " + name + "?"); };
chamar('Fernando');
//agora parametros padrões em typescript
//eu tenho uma função que recebe dois parametros (note que o segundo é inicializado com 1) e retorna um number
function incrementa(speed, inc) {
    if (inc === void 0) { inc = 1; }
    return speed + inc;
}
console.log("incrementa (5,2) = " + incrementa(5, 2));
console.log("incrementa (5) = " + incrementa(5));
