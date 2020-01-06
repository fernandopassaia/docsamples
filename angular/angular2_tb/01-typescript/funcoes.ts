//função normal: note que eu consigo definir tipagem de parametros (numero) e de retorno (booleano)
let verificaNumero = function (valor: number): boolean{
    return valor < 12
}

let numeroAVerificar: number = 14
console.log(`O valor ${numeroAVerificar} é menor que o valor de 12? ${verificaNumero(numeroAVerificar) ? 'YES': 'NO' }`)

//agora arrow function - primeiro eu crio uma variável e ai declaro a minha função:
//do lado esquerdo declaração de parametros, do lado direito a implementação. Não preciso do nome
//"function" e também não preciso do keyword "return"

let chamar = (name: string) => console.log(`Você entendeu ${name}?`)
chamar('Fernando')

//agora parametros padrões em typescript
//eu tenho uma função que recebe dois parametros (note que o segundo é inicializado com 1) e retorna um number
function incrementa (speed: number, inc: number = 1) : number {
    return speed + inc
}

console.log(`incrementa (5,2) = ${incrementa(5,2)}`)
console.log(`incrementa (5) = ${incrementa(5)}`)