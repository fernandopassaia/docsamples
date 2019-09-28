//um objet é uma coleção dinamica de chaves e valores, que podem ser de qualquer tipo, e um protipo que pode ser um objeto ou null.
//no exemplo dele eu farei João e Pedro herdar de homem onde tem sexo... a propriedade _proto_ é uma referência para o protótipo do objeto.

//Forma 1 - Proto nos dá a referência para o protótipo (não é a mais recomendada, mais funciona)

var homem = {
    sexo: "masculino"
}; //essa é a "classe" homem principal que irei herdar de 3 maneiras diferentes abaixo

var joao = {
    nome: "João",
    idade: 20,
    _proto_: homem
};

var pedro = {
    nome: "Pedro",
    idade: 25,
    _proto_: homem
};

console.log(joao);
//console.log(joao.sexo);
console.log(pedro);
//console.log(pedro.sexo);

//Forma 2 - Prefira a utilização de Object.getPrototypeOf e Object.setPrototypeOf para interagir com o protótipo do objeto.

var fer = {
    nome: "Fernando",
    idade: 30    
};

var hubinha = {
    nome: "Hubinha",
    idade: 2    
};

Object.setPrototypeOf(fer, homem);
Object.setPrototypeOf(hubinha, homem);
console.log(fer);
console.log(hubinha);
console.log(fer.sexo);
console.log(hubinha.sexo);

//Forma 3 - também é possível utilizar object.create para determinar o protótipo de um objeto.

var steeeeéééfffaaaann = Object.create(homem); //pronto, aqui herdei
steeeeéééfffaaaann.nome = "Akos";
steeeeéééfffaaaann.idade = 35;
console.log(steeeeéééfffaaaann);
console.log(steeeeéééfffaaaann.sexo);

//Shadowing - conceito de sombra.
//quando eu chamo .sexo ele vai buscar no "stefan", não tem - no homem, e não tem no prototype do js... ele busca em escadinha
//ou seja: se eu chumbar "feminino" direto no objeto stefan ele vai pegar esse e ignorar o homem lá acima

var steeeeéééfffaaaann = Object.create(homem); //pronto, aqui herdei
steeeeéééfffaaaann.nome = "Escravo";
steeeeéééfffaaaann.idade = 35;
steeeeéééfffaaaann.sexo = "feminino"
console.log(steeeeéééfffaaaann);
console.log(steeeeéééfffaaaann.sexo);

//object.keys irá me mostrar as propriedades do objeto
console.log('');
console.log(Object.keys(steeeeéééfffaaaann));

delete steeeeéééfffaaaann.sexo; //consigo remover
console.log(steeeeéééfffaaaann);
steeeeéééfffaaaann.sexo = "bichinha"; //reinsiro
console.log(steeeeéééfffaaaann);


//percorrer propriedades de steeefaaan
for(var propriedade in steeeeéééfffaaaann){
    console.log(propriedade);
}


//Forma 4 - criando objetos com uma função fábrica:

var criarHomem = function(nome,idade){
    return {
        nome: nome,
        idade: idade,
        sexo: "masculino"
    };
};

var joao = new criarHomem("João", 20);
console.log(joao);

//nota, eu também posso chamar a função por call (ou apply, o que muda são os parametros) e fazer a função assim:
var Homem = function(nome,idade){    
        this.nome = nome,
        this.idade = idade,
        this.sexo = "masculino"    
};

var pedro = {}; //usando call
Homem.call(pedro, "Pedro", 30);
console.log(pedro);

var henrique = {}; //usando apply
Homem.apply(henrique, ["Henrique", 18]);
console.log(henrique);


//Forma 5 - usando prototype
var Homem = function(nome,idade){    
    this.nome = nome,
    this.idade = idade    
};

Homem.prototype.sexo = "masculino";

var pedro = new Homem("Pedro", 30);
console.log(pedro);
console.log(pedro.sexo);

var henrique = {}; //usando apply
henrique._proto_ = Homem.prototype; //acesso o proto de pedro e jogo
Homem.apply(henrique, ["Henrique", 18]);
console.log(henrique);


//Forma 6 pra FINALIZAR - usando função new e prototype deixando o new mais genérico
var _new = function (f){
    var obj = {};
    obj._proto_ = f.prototype;
    f.apply(obj, Array.prototype.slice.call(arguments,1)); //arguments pega o que está vindo no f, posso printar no console
    return obj;
};

var Homem = function(nome,idade){    
    this.nome = nome,
    this.idade = idade    
};

Homem.prototype.sexo = "masculino";

var ze = _new(Homem, "Zé", 40);
console.log(ze);
var albardao = _new(Homem, "Albardão", 50);
console.log(albardao);