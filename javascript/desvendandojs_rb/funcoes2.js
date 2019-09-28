var counter =0; //variável global
var add = function() {
    return ++counter; //método que incrementa
};

console.log(add()); //note que ao chamar, ele vai incrementando e mantém a variável global
console.log(add()); //2
console.log(add()); //3
console.log(add()); //4
console.log(add()); //4

//agora note que eu simplesmente construi outro método "add" diferente, ele vai "sobreescrever" e apagar o antigo da memória
var itens = [];
var add = function (item){
    itens.push(item);
    return itens;
};

console.log(add('A'));
console.log(add('B'));
console.log(add('C'));
//agora se eu tentar chamar como antigamente sem passar nada, vou adicionar "underfined" no array
console.log(add()); 
//* Cuidado com o Escopo Global - o JS não tem um linker, e por isso usa o escopo global para se comunicar.
//É preciso tomar cuidado pois pode poluir, já que ele faz uso do escopo global para estabelecer os vínculos
//entre diferentes partes de uma aplicação. O "escopo global" é um barramento compartilhado entre toda aplicação.

//E agora a solução: Como faz pra ENCAPSULAR? (e fugir disso)

//Técnica 1 - uso de um objeto:

var counter = {
    value: 0,
    add: function(){
        return ++this.value; //veja que o this se refere a algo que foi enviado
    }
};

console.log(counter.add());
console.log(counter.add());
console.log(counter.add());
console.log(counter.add());
console.log(counter.add());

//agora vamos ver como ao tentar adicionar as strings eu já não terei mais o problema de perder o anterior:

var itens = {
    value: [], //inicializo,
    add: function(item){
        this.value.push(item);
        return this.value;
    }
};

console.log(itens.add('A'));
console.log(itens.add('B'));
console.log(itens.add('C'));

//ou seja, o meu "counter" anterior continua funcionando perfeitamente...
console.log(counter.add());

//agora temos que pensar uma coisa: O Javascript NÃO TEM os modificadores "public" ou "private." Então é preciso tomar
//cuidado, eu posso modificar um valor de qualquer parte e quebrar o código, abaixo vou modificar e depois chamar o
//método de add e nós veremos um erro NAN (not a number).

counter.value = undefined; //agora eu simplesmente ferro o var "counter" lá de cima
console.log(counter.add());//como ferrei, terei um NAN. No "funções2Encapsulado.js" irei resolver esse problema.


//uma solução pra isso é fazer o encapsulamento por meio de funções... veja o arquivo funcoes3, irei re-escrever ele...
