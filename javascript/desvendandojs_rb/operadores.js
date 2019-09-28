//O comportamento de Operadores pode produzir resultados inesperados por conta da coersão de tipos.
// 0 == '' (true)
// 0 == '0' (true)
// false == undefined (false)
// false == null (false)
// null == undefined (true)
// 1 == true (true)
// 0 == false (true)
// 0 == '\n' (true)

//se você fizer uma comparação de 10 == '10' o Javascript na verdade fará um "toNumber('10')" e dará true.
//Note que o operador == no Javascript é um nível abaixo do === - esse sim quer dizer "exatamente igual":

//recomendação é usar os operadores muito igual === e muito diferente !== (fica muito mais explicito!)
//se você usar o muito igual === na comparação acima (10 === '10') ai sim ele dará false.


//como fazer pra comparar dois OBJETOS em JavaScript:

var x = {};
var y = {};
x == y; //aqui dará false

//agora se eu fizer:
var x = x; //ai elas ficam iguais e se eu comparar dará true - locão


//mais uma coisa: Os operadores lógicos || e && também escondem segredos:

//nessa comparação ele dará TRUE, por que pra ele 10 é verdadeiro (aff) e 0 é falso por exemplo
if(10){
    console.log("OK");
}

//em resumo: se eu mandei avalidar uma situação booleana (se 10), ele vai converter isso pra truthy ou falsy
//por meio da operação abstrata ToBoolean por trás dos panos. São só 6 operações que nos levam pra 0:

// !!0
// !!NaN
// !!''
// !!false
// !!null
// !!undefined

// As 6 operações acima darão false como resultado. Todo restante dará true.
//A Documentação pra isso está no EcmaScript Language Specification: https://www.ecma-international.org/ecma-262/5.1/#sec-9.2

//agora vou tratar de inicialização de variáveis pra evitar que o sistema dê pau... primeiro método sem inicialização:

var generateSerial = function(max){
    return Math.floor(Math.random() * max);
};

console.log(generateSerial(1000));
console.log(generateSerial(100));
console.log(generateSerial(10));
console.log(generateSerial());  //aqui vai dar pau por que não passei o max

//primeira forma de inicializar:

var generateSerial = function(max){
    if(max == undefined || max == null || max ===0){
        max = 1000;
    }
    return Math.floor(Math.random() * max);
};

console.log(generateSerial(1000));
console.log(generateSerial(100));
console.log(generateSerial(10));
console.log(generateSerial()); //aqui já não tenho mais o pau por que inicializei o max dentro da função

//segunda forma = se "nãoMax", como ele está dando NaN, vai dar true e inicializar

var generateSerial = function(max){
    if(!max){
        max = 1000;
    }
    return Math.floor(Math.random() * max);
};

console.log(generateSerial(1000));
console.log(generateSerial(100));
console.log(generateSerial(10));
console.log(generateSerial()); //aqui já não tenho mais o pau por que inicializei o max dentro da função

//terceira forma = se max tiver valor recebe max, senão, 1000.

var generateSerial = function(max){
    max = max || 1000;
    return Math.floor(Math.random() * max);
};

console.log(generateSerial(1000));
console.log(generateSerial(100));
console.log(generateSerial(10));
console.log(generateSerial()); //aqui já não tenho mais o pau por que inicializei o max dentro da função



//Operadores de Atribuição e Operadores de comparação:
var x = 10;
x+=2; //aqui daria 12
//os operadores podem ser: +=, -=, *=, /= e %=


//Operadores Tenários: (expressão) ? true : false
var idade = 22;
var resultado = (idade >= 18) ? 'Maior de idade' : 'Menor de idade'; //aqui daria MAIOR de idade...
console.log(resultado);