Curso de JavaScript básico do Rodrigo Branas.

-----------------------------------------------------------------------------------------------------
História do JS:

Seu primeiro nome foi "Mocha" por sugestão do fundador do Netscap.
Foi lançado em 1995 com o Netscap Navigator 2.0 com o nome de LiveScript.
Em Dezembro em parceria com a SUN microsystems mudou para Javascript, com o objetivo de destruir a MS,
a SUN patenteou proibindo a MS de usar a linguagem. A MS fez uma "engenharia reversa" criando o JScript.
En 1997 a Netscape conseguiu padronizar a linguagem junto a ECMA, e então o nome ECMAScript (oficial).


A linguagem JavaScript herdou de 4 linguagens:
Java: Sintaxe e algumas convenções.
Scheme: Lambda, Closure e tipagem fraca.
Self: Herança baseada em protótipos, objetos dinâmicos.
Perl: Expressões regulares.

-----------------------------------------------------------------------------------------------------
Informações técnicas:
(1) O Javascript é interpretado pelo próprio Browser e não precisa de interpretador *.
(exceto que você queira rodar no backend, e ai você vai precisar de fato de um NodeJS ou outro).
(2) Possui O.O baseada em protótipos, não possuindo classes.
(3) Possui tipagem fraca e dinâmica (aff) (ruim por que dá muito liberdade, ruim compilador fraco).
(4) Possui funções de primeira classe (será falado no episódio só de funções). São funções que podem ser
atribuidas a variável e passadas por parametros e retornando de outras funções.
(5) Não oferece Multi-Thread.
(6) Atualmente está na versão 6.0 (vídeo de Mar2015).

Variáveis:
(1) Tipagem fraca (devem começar com var) e por uma letra, $ ou _ (evitar $ - usaremos pra frameworks e 
injeção de dependências) (evitar _ por que geralmente usamos pra variáveis privadas dentro de métodos).
(2) Após a letra podemos usar números.
(3) Pela convenção: usar camelCase (primeira menor outras maiores). Cuidado - JS também é case-sensitive.

Eu posso declarar variáveis das seguintes formas:

var name = "AgileCode";
var top10 = [1,2,3,4,5,6,7,8,9,0];
var $scope = {title: "Javascript", version: "6"};
var _person = "John"; //underline geralmente sinaliza uma variável privada interna do métodos
var numberCode= 876879876;
var a=10, b=9, c=8;
var language; //vai ficar undefined, mas também pode ser declarado assim.

Tipo de Dados:

** Number. Uso da API:
var nota = 10;
nota.toExponencial(2); //100
nota.toFixed(2); //10.00
nota.toPrecision(1); //1e+1
nota.toString(); //"10"
nota.valueOf(); //10
Math.cin(nota) //dá Math. e dois TABS que vai listar tudo
(Tem os prints na pasta usando Console com Node. Pode chamar também o "Math." e fazer operações matemáticas.

** String. Uso da API:
São imutaveis.
Podem ser declaradas com aspas simples ou duplas - tanto faz.

var nome = "AgileCode";
nome.charAt(2); //"i"
nome.chatCodeAt(0); //65
nome.concat("!") //"AgileCode!"
nome.IndexOf('e') //4
nome.replace('Code', '!'); //"Agile!"
nome.split('e') //['Agil', 'Cod', ''];

** Boolean
var isento = true;
var estudante = false;

isento.ToString(); //"true"
estudante.valueOf(); //false

Nota: ! no Javascript é "negação" (false). !! é a negação da negação - então é VERDADEIRO. (PQP).

** Undefined e null

O tipo "undefined" é retornado caso uma propriedade de um determinado objeto seja consultada e não exista.
O tipo "null" é a ausência - a propriedade está null, mas ela existe (o objeto, a variável, enfim).

** Objeto:
É uma coleção dinâmica de chaves e valores de qualquer tipo de dado. É possível adicionar ou remover propriedades
a qualquer momento. Não temos construtor, não temos classe, não temos métodos. É diferente de C# ou Java.

var pessoa = {}; //nesse caso ao abrir e fechar chaves crio um novo objeto, nesse caso vazio.
var pessoa = {
	nome:"João"
	};
	
var pessoa = {
	nome:"João",
	idade: 20,
	telefone: null,
	endereco: {
	logradouro: "anamao",
	numero: 70,
	bairro: "anow"
	}
};

-----------------------------------------------------------------------------------------------------

A linguagem JavaScript não tem:

* Classe
* Construtor
* Método
* Módulo

O poder da linguagem vem das FUNÇÕES. Uma função é um objeto que contém um bloco de código executável. Esse bloco é
isolado não sendo possível acessá-lo externamente. No JS as funções são de primeira classe: Aquelas que podem ser 
atribuidas a uma variável, passada como parâmetro ou ser retornada de uma outra função. Existem duas maneiras de 
declarar funções:

** Function declaration (forma 1):

function soma(a,b){
return a+b;
}

** Function expression (forma 2):
var soma = function(a,b){
return a+b;
}

(nota - também poderia fazer uma Named Function Expression):
var soma = function soma(a,b){
return a+b;
} //desse jeito fica melhor pois aparece no stack trace, listas de breakpoints e demais ferramentas de debbuging
//um segundo ponto positivo é que a NAMED é carregada antes da execução, ou seja, posso fazer a chamada dela (soma())
//lá em cima, antes da declaração dela... por isso é melhor

//nesse segundo caso ocorre a atribuição, então "soma" é o nome da função e também a variável soma.

DICA Importante: Dentro de qualquer função, se eu quiser ver os argumentos (parametros passados) eu posso imprimir:

console.log(arguments);

-----------------------------------------------------------------------------------------------------

** CHAMANDO FUNÇÕES, VOCÊ TEM 4 MANEIRAS. Veja o arquivo "funcoes.js" na pasta que está todo documentado.
Nota: tem mais exemplos no funcoes2.js, e depois o funcoes2Encapsulado é o 2 "re-escrito" com encapsulamento.

-----------------------------------------------------------------------------------------------------

** Aula 9 sobre Arrays: Não existem arrays no JS como em C# ou Java. O que existem são objetos especiais que oferecem
meios para acessar e manipular suas propriedades por meio de índices. Ver arquivo "arrays.js" na pasta.

Nota: Vários exemplos de Arrays, filtros, For, Foreach e tudo mais no arrays.js (exemplos Map, Reduce).

-----------------------------------------------------------------------------------------------------

** EXPRESSÕES REGULARES: São estruturas formadas por uma sequência de caracters que especificam um padrão formal.
RegExp é Muito útil para: Validação de campos, extração de dados, substituição de caracteres em textos.

Em Javascript usamos duas barras pra criar expressões regulares: var regExp = /<expressao regular>/;
Também é possível usar o new - então: var regExp = new RegExp("<expressao regular>");

RegExp API:
exec - executa a regexp, retornando os detalhes.
test - testa a regexp, retornando true ou false.

Agora iremos criar 12 passos: Primeiro reconheceremos um telefone, e depois evoluir para novos cenários mais complexos
para estimular utilização de grupos, metacaracteres, quantificadores e muito mais. Ver arquivo regexp.js

-----------------------------------------------------------------------------------------------------

** DATE: Apesar de parecer mais simples, a data não é representada dessa forma... Como assim?
A Data é representada pela quantidade de milissegundos desde o início da era Unix. Existem 4 maneiras de se criar uma
data, verificar classe 

-----------------------------------------------------------------------------------------------------

Sobre Operadores, Statements e Herança deixarei os exemplos nas classes. Olhar lá.

-----------------------------------------------------------------------------------------------------