//Não existem arrays no JS como em C# ou Java. O que existem são objetos especiais que oferecem
//meios para acessar e manipular suas propriedades por meio de índices. Ver arquivo "arrays.js" na pasta.

var carros = []; //essa é a forma de declarar um array
carros[0] = "Gol";
carros[1] = "Palio"; //e ai eu posso preencher ele assim, elemento por elemento em índice
carros[2] = "Peugeot";

var carros2 = ["Ka", "Corsa", "Honda"]; //também posso inicializar o array assim

console.log(carros);
console.log(carros2);

var carros3 = new Array(10); //ou também posso inicializar assim - tamanho inicial
carros3[4] = "Fuqueta";
carros3[7] = "Civic"; //note que consigo inserir em apenas algumas posições
console.log(carros3);


var carros4 = new Array("Escort", "Santana", "Fit", "Corsa", "Fusion", "BMW"); //também posso inicializar um array assim
carros4.push("Focus"); //insiro um item no final do array - ver a API completa no print na pasta
console.log(carros4);
var stringDeCarros = carros4.toString();
console.log(stringDeCarros);
carros4.pop(); //retira o último elemento - ver a API completa no print na pasta
console.log(carros4);
carros4.unshift("Uno"); //adiciono um elemento no começo, chamado uno
console.log(carros4);
carros4.shift(); //removo o primeiro elemento uno
console.log(carros4);

console.log(carros4.indexOf("Santana")); //imprimo o índice do Santana (1)

//agora usaremos o splice - canivete suiço, ele permite que remova elementos de posicao, troque, adicione elementos.
var pos = carros4.indexOf("Corsa"); //pego a posição do Corsa (3)
carros4.splice(pos,1); //removo a partir da posição 3 1 item (Removo o Corsa, não gosto dele)
console.log(carros4);

var pos = carros4.indexOf("Fit"); //agora eu pego a posição do FIT
carros4.splice(pos,1, "New Fit"); //e vou trocar a descrição por New Fit
console.log(carros4);

carros4.splice(pos,0, "New Civic"); //agora na mesma posição do New Fit eu adiciono outro carro (o New Civic, vai ficar antes dele)
console.log(carros4);

//agora usando um FOREACH no array
carros4.forEach(function (elemento) {
    console.log(elemento);
})

//agora usando um FOR no array
for(var i=0; i < carros4.length; i++){
    console.log(carros4[i]);
}


//agora usando um Array com mais de uma posição
var carrosMarcaModelo = [];
carrosMarcaModelo[0] = {marca: "Ford", modelo: "Escort"}
carrosMarcaModelo[1] = {marca: "Volks", modelo: "Santana"}
carrosMarcaModelo[2] = {marca: "Honda", modelo: "New Fit"}
carrosMarcaModelo[3] = {marca: "GM", modelo: "Corsa"}
carrosMarcaModelo[4] = {marca: "Ford", modelo: "Focus"}
carrosMarcaModelo[5] = {marca: "Honda", modelo: "New Civic"}
carrosMarcaModelo[6] = {marca: "Volksvagem", modelo: "Gol"}
carrosMarcaModelo[7] = {marca: "Fiat", modelo: "Uno"}

//primeira forma de filtrar
var carrosFord = [];
carrosMarcaModelo.filter(function(elemento){
    if(elemento.marca === "Ford"){
        carrosFord.push(elemento);
    }
});

console.log(carrosFord);

//segunda forma de filtrar
var carrosFordFilter = carrosMarcaModelo.filter(function (elemento){
    return elemento.marca === "Ford";
});

console.log(carrosFordFilter);

//o every vai testar se TODOS os elementos são da marca ford (nesse caso retorna false)
var carrosFordEvery = carrosMarcaModelo.every(function (elemento){
    return elemento.marca === "Ford";
});

console.log(carrosFordEvery); //false

//o some vai testar se tem ALGUM dos elementos são da marca ford (nesse caso retorna true)
var carrosFordSome = carrosMarcaModelo.some(function (elemento){
    return elemento.marca === "Ford";
});

console.log(carrosFordSome); //true



//AGORA VEM A FUNÇÃO MAP: Ela serve pra mapear elementos os "transformando" e derivando
//um novo array em que eu faça algum tipo de Analise. Por exemplo: Quero derivar um novo array
//de strings que me diga por exemplo quais marcas de carro eu tenho.
//O Map é indicado quando eu quero derivar um novo array transformado.

var soMarcas = carrosMarcaModelo.map(function (elemento){
    return elemento.marca;
});

console.log(soMarcas); //true



//AGORA VEM A FUNÇÃO REDUCE: Ele permite que eu faça um processamento e uma acumulação.
//Ele serve por exemplo pra eu pegar o array abaixo e saber o total de todos os preços.

var carrosMarcaModeloValor = [];
carrosMarcaModeloValor[0] = {marca: "Ford", modelo: "Escort", preco: 8000}
carrosMarcaModeloValor[1] = {marca: "Volks", modelo: "Santana", preco: 6000}
carrosMarcaModeloValor[2] = {marca: "Honda", modelo: "New Fit", preco: 32000}
carrosMarcaModeloValor[3] = {marca: "GM", modelo: "Corsa", preco: 5000}
carrosMarcaModeloValor[4] = {marca: "Ford", modelo: "Focus", preco: 16000}
carrosMarcaModeloValor[5] = {marca: "Honda", modelo: "New Civic", preco: 35000}
carrosMarcaModeloValor[6] = {marca: "Volksvagem", modelo: "Gol", preco: 12000}
carrosMarcaModeloValor[7] = {marca: "Fiat", modelo: "Uno", preco: 4000}

//primeiro farei no modo normal
var totalModoNormal = 0;
carrosMarcaModeloValor.forEach(function (elemento){
    totalModoNormal += elemento.preco;
});

console.log(totalModoNormal); //true

//agora usando o reduce - a função recebe o elemento anterior e o atual
var valorTotalReduce = carrosMarcaModeloValor.reduce(function (prev,cur){
    return prev + cur.preco;
},0); //aqui eu digo que começa em 0...

console.log(valorTotalReduce); //118000


//Agora o CONCAT que concatena dois arrays
var carrosFord = ["Focus","Escort","Fusion","Belina","Corcel","Mustang"];
var carrosHonda = ["New Fit","New Civic","CRV"];

var veiculos = carrosFord.concat(carrosHonda);
console.log(veiculos);


//Agora o SLICE, que pega a partir de um índice (0) quantos itens você quer (2)
var carroDoisTresQuatro = carrosFord.slice(0,2);
console.log(carroDoisTresQuatro); //comecou do zero, pegou 2 (Focus e Escort)


//agora o "REVERSE" que inverte um array
carrosFord.reverse();
console.log(carrosFord);


//e agora o Método SORT que tem dois cenários: se eu tiver um array de strings e fizer sort, ele vai ordenar

carrosFord.sort(); //ele vai ordenar mesmo sem passar nenhum parametro, por que é só string
console.log(carrosFord);


//eu também posso usar o SORT pra ordernar por preço... a lógica é meio complicada mas vou explicar:
//o método recebe A e B, ele vai comparar se o preço de A ou B é menor, e ai ele vai vir primeiro...
//ou seja: na primeira comparação 8000 e 6000, 6000 será menor, então ele virá primeiro... e assim vai
// ele vai comparar Santana e Escort, Santana vai passar pra primeiro, depois Escort com New Fit, ai
//escort vai subir, depois New Fit com Corsa, ai Corsa vai subir, depois New Fit com Focus, ai Focus vai
//subir, depois New Fit com Civic, como o Civic é mais caro, o Fit sobe, ai ele pega o Civic e compara com Gol...

var ordenaValorMenor = carrosMarcaModeloValor.sort(function(a,b){
    return a.preco - b.preco;
});

console.log(ordenaValorMenor);
console.log('');
//agora fazendo o efeito inverso
var ordenaValorMaior = carrosMarcaModeloValor.sort(function(a,b){
    return b.preco - a.preco;
});

console.log(ordenaValorMaior);


//agora o método JOIN que adiciona alguma coisa e separa os itens pelo valor
var carrosSeparadosPontoVirgula = carrosFord.join(";");
console.log(carrosSeparadosPontoVirgula); //nota, ele converteu o array pra string

//o método SPLIT faz exatamente o contrário, eu vou pegar o separado por ; e voltar ele pra array
var carrosVoltadosPraArray = carrosSeparadosPontoVirgula.split(";");
console.log(carrosVoltadosPraArray);