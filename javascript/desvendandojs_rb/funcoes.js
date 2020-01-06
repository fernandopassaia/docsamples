//** CHAMANDO FUNÇÕES, VOCÊ TEM 4 MANEIRAS (olhar o funcoes.js na pasta): Escopo Global, Objeto, Call e Apply, Operador New
//(1) Invocando diretamente no escopo global:
console.log('(1) Invocando função diretamente do escopo global')
var soma = function(a,b){
return a + b;
};

var result = soma(2,2); //4
console.log(result);
console.log('');

//Passando uma função como parâmetro:
console.log('Passando uma função como parametro')
var produto = {nome: 'Sapato', preco: 150};

var formulaImpostoA = function(preco){return preco*0.1;};
var formulaImpostoB = function(preco){return preco*0.2;};

var calcularPreco= function(produto,formulaImposto){
    return produto.preco + formulaImposto(produto.preco);
} //Note que eu passo uma função por parâmetro, isso é chamado de "LAMBDA".

var resultA = calcularPreco(produto, formulaImpostoA); //165 - Essa é a chamada, mando o produto e outra função
var resultB = calcularPreco(produto, formulaImpostoB); //180 - Essa é a chamada, mando o produto e outra função
console.log('Resultado A: ' + resultA + ' Resultado B: '+ resultB)
console.log('');

//Retornando uma Função de dentro de outra função
console.log('Retornando uma função dentro de outra função')
var helloWorld = function(){
return function(){
return "Hello world";
};
};

console.log(helloWorld()); //irá retornar [Function] - preciso invocar duas vezes por que são duas funções
console.log(helloWorld()()); //aqui como eu chamei 2x irá retornar o certo que é HelloWorld
console.log(''); //note que acima eu já imprimo direto no console chamando o método


//(2) Invocando uma função por meio de um Objeto (simular o funcionamento de um método)
console.log('(2) Invocando uma função por meio de um objeto (simulando funcionamento de um método)')
var pessoa = {
    nome: "João",
    idade:20,
    getIdade: function(){
        return this.idade;
    }
};

console.log(pessoa); //volto a função inteira
console.log(pessoa.getIdade); //volta dizendo que é uma função
console.log(pessoa.getIdade()); //20 (volta a idade, por que invoquei)
console.log(''); //note que acima eu já imprimo direto no console chamando o método


//(3) Invocando uma função com Call e Apply
//Toda função possui os métodos call() e apply(): eles são utilizados para indicar em qual escopo uma função
//deve ser utilizada. A diferença básica é de como é utilizada: 
//funcao.call(escopo,parametro1,parametro)
//funcao.apply(escopo,parametros)

console.log('(3) Invocando uma função com Call e Apply')

var getIdade = function(extra){
    console.log(arguments); //nota: eu sempre posso chamar isso dentro de uma function, ela irá mostrar todos argumentos (parametros) enviados
    return this.idade + extra;
}

var pessoa2 = {
    nome: "Hubinha",
    idade: 2,
    getIdade: getIdade
};

console.log(pessoa2.getIdade(2)); //chamada normal //4
console.log(getIdade.call(pessoa2,3)); //chamada call posso passar vários parametros 3,4,5 (note que chamo o método getIdade) //5
console.log(getIdade.apply(pessoa2, [4])); //chamada apply (obrigatório array - só 1) (note que chamo o método getIdade) //6
console.log(''); //note que acima eu já imprimo direto no console chamando o método


//(4) Invocando uma função por meio do operador new
//O Javascript não tem classe, mas o new consegue entre aspas "instanciar" funções.
//No Java nós temos formas interessantes de criar objetos: Podemos abrir e fechar chaves e definir dentro dele um novo objeto.
console.log('(4) Invocando uma função por meio do operador new')

//** OBJETOS em Java (exemplo 4 dentro da funcoes.js e o arquivo funcoes2.js que tem mais exemplos):
//Um objeto é uma coleção dinâmica de chaves e valores de qualquer tipo de dado. O Javascript não tem classe, mas o
//new consegue entre aspas "instanciar" funções. No Java nós temos formas interessantes de criar objetos: Podemos
//abrir e fechar chaves e definir dentro dele um novo objeto.

//var pessoa = {
//    nome: "João",
//    idade:20    
//};

//Essa é a melhor solução para apenas um objeto. Porém isso ficaria cansativo caso tivessemos vários objetos, teria que declarar
//isso umn a um. Para isso, temos as funções Construtoras e as funções Fábrica. 

//Primeiro vamos as Função Fábrica:

var criarPessoaFabrica = function(nome,idade){
return { //crio e devolvo um objeto
	nome: nome,
	idade: idade
};
};

console.log(criarPessoaFabrica("Pedro",20));
console.log(criarPessoaFabrica("Maria",30)); //veja como fica mais fácil, tenho uma fábrica, posso criar vários

//Agora vamos a Função Construtora (note, por convenção, Funções que usem "new" devem começar com letra maíuscula pra
//lembrar que ela é a instância de alguma coisa):

var PessoaConstrutora = function(nome,idade){
this.nome = nome;
this.idade = idade; //o this serve para referênciar que isso vem do enviado lá pela chamada (Pedro, Maria, 20,30)
};

console.log(new PessoaConstrutora("Pedro", 20));
console.log(new PessoaConstrutora("Maria", 30));

//Nota: eu ainda posso criar objetos vazios e mandar pro "PessoaConstrutora" como Parametro, e o método "call" irá 
//se encarregar de preencher esses objetos.

var pedro = {};
PessoaConstrutora.call(pedro, "Pedro",20);
var maria = {};
PessoaConstrutora.call(maria, "Maria",30);
console.log(pedro);
console.log(maria);

console.log(''); //note que acima eu já imprimo direto no console chamando o método

//E pra fechar, vamos aos Closures, que é uma função dentro de outra função.
//Note que eu irei criar uma função helloWorld que tem outra função dentro (inner function) que retorna uma mensagem.
//E lá embaixo eu irei chamar ela de duas maneiras: Primeiro chamado helloWorld duas vezes (helloWorld()()) dessa forma
//chamando a função e a sub-função. Depois irei criar um "Var" que recebe a primeira função, e irei chamar o var com ()
//que automáticamente irá chamar a segunda, sendo que "var" na verdade está armazenando a função helloWorld()
//veja documentação no message (variável) ali embaixo sobre o que é CLOSURE de verdade...


console.log('Exemplo de Closure e de chamar função dentro de função e armazenar função em var e chamar a função de dentro');
var helloWorld = function(){
    var message = "Hello World";
    return function()
    {
        return message; //closure quer dizer que essa "inner function" dentro da função manterá a "referência" da variável message acima
    }; //por isso conseguimos passar funções por PARAMETROS e continuar usando os recursos dentro dela (veja o exemplo de função por parametro lá em cima)
};

console.log(helloWorld()); //irá imprimir que é uma função, apenas, pois não chamei a segunda
console.log(helloWorld()()); //irá imprimir Hello World, pois chama a segunda função dentro
var fnHelloWorld = helloWorld(); //agora atributo a um var (note que "message" é mantida internamente no var, ao chamar abaixo - isso é o closure)
console.log(fnHelloWorld()); //agora imprime Hello World, pois chamei a função dentro da função que está no var (aff)

console.log(''); //note que acima eu já imprimo direto no console chamando o método
console.log('fim classe'); //note que acima eu já imprimo direto no console chamando o método