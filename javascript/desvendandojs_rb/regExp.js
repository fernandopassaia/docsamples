//** EXPRESSÕES REGULARES: São estruturas formadas por uma sequência de caracters que especificam um padrão formal.
//RegExp é Muito útil para: Validação de campos, extração de dados, substituição de caracteres em textos.

//Em Javascript usamos duas barras pra criar expressões regulares: var regExp = /<expressao regular>/;
//Também é possível usar o new - então: var regExp = new RegExp("<expressao regular>");

//RegExp API:
//exec - executa a regexp, retornando os detalhes.
//test - testa a regexp, retornando true ou false.

//Agora iremos criar 12 passos: Primeiro reconheceremos um telefone, e depois evoluir para novos cenários mais complexos
//para estimular utilização de grupos, metacaracteres, quantificadores e muito mais. Ver arquivo regexp.js


//Passo 1 - reconhecendo um telefone
var regExpTelefone = /9999-9999/;
var telefone = "9999-9999";
console.log(regExpTelefone.exec(telefone)); //[ '9999-9999', index: 0, input: '9999-9999', groups: undefined ] diz que encontrou no index 0
console.log(regExpTelefone.test(telefone)); //deu true


//Passo 2 - reconhecendo um telefone com DDD
//Nota - eu preciso colocar uma "\" antes de caracteres especiais como o parenteses, para que eles se tornem literais.
var regExpTelefoneComChaves = /\(48\) 9999-9999/;
var telefoneComChaves = "(48) 9999-9999";
var telefoneComChavesETexto = "(48) 9999-9999 tratar com joao";
console.log(regExpTelefoneComChaves.exec(telefoneComChaves)); //[ '9999-9999', index: 0, input: '9999-9999', groups: undefined ] diz que encontrou no index 0
console.log(regExpTelefoneComChaves.test(telefoneComChaves)); //deu true
console.log(regExpTelefoneComChaves.test(telefoneComChavesETexto)); //deu true por que apesar do texto, contém o telefone


//Passo 3 - reconhecendo um telefone com DDD unicamente
//Isso existe pois se no exemplo acima eu escrever qualquer coisa junto com o telefone, como "(48) 9999-9999 tratar com João" ainda assim ele dará true.
//Nota: Iniciando e finalizando com um determinado caracter: ^ indica inicio com determinado caracter, $ finaliza com determinado caracter.
var regExpTelefoneComChaves = /^\(48\) 9999-9999$/;
var telefoneComChaves = "(48) 9999-9999";
var telefoneComChavesETexto = "(48) 9999-9999 tratar com joao";
console.log(regExpTelefoneComChaves.exec(telefoneComChaves)); //[ '9999-9999', index: 0, input: '9999-9999', groups: undefined ] diz que encontrou no index 0
console.log(regExpTelefoneComChaves.test(telefoneComChaves)); //deu true
console.log(regExpTelefoneComChaves.test(telefoneComChavesETexto)); //deu false por causa do ^ e $


//Passo 4 - Aceitar qualquer número de telefone, para isso vamos flexibilizar a expressão regular por meio de grupos
//Grupos:
// [abc] = aceita qualquer caractere dentro do grupo, nesse caso a, b e c
// [^abc] = não aceita qualquer caractere dentro do grupo, nesse caso a, b e c
// [0-9] = aceita qualquer caractere entre 0 e 9
// [^0-9] = não aceita qualquer caractere entre 0 e 9

var regExpTelefoneComChaves = /^\([0-9][0-9]\) [0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]$/; //essa gambi será melhorada
var telefoneComChaves = "(11) 2378-0944";
console.log(regExpTelefoneComChaves.exec(telefoneComChaves)); //blz
console.log(regExpTelefoneComChaves.test(telefoneComChaves)); //deu true


//Passo 6 - Flexibilizo o telefone pra aceitar 4 ou 5 caracteres (celular)
var regExpTelefoneComChaves = /^\([0-9]{2}\) [0-9]{4,5}-[0-9]{4}$/; //essa gambi será melhorada
var telefoneComChaves = "(11) 2378-0944";
var celularComChaves = "(11) 92378-0944";
console.log(regExpTelefoneComChaves.exec(telefoneComChaves)); //blz
console.log(regExpTelefoneComChaves.test(telefoneComChaves)); //deu true
console.log(regExpTelefoneComChaves.test(celularComChaves)); //deu true


//QUANTIFICADORES PARTE 2. Os quantificadores podem ser aplicados a caracteres, grupos, conjuntos ou metacaracteres:
// ? - Zero ou um       * - Zero ou mais           + - Um ou mais


//EXEMPLO 7 - A interrogação torna algo "opcional", nesse caso tornarei o hífen opcional.
var regExpTelefoneComChaves = /^\([0-9]{2}\) [0-9]{4,5}-?[0-9]{4}$/; //essa gambi será melhorada
var telefoneComChaves = "(11) 23780944";
console.log(regExpTelefoneComChaves.exec(telefoneComChaves)); //blz
console.log(regExpTelefoneComChaves.test(telefoneComChaves)); //deu true


//EXEMPLO 8 - E se o telefone estiver numa estrutura de tabela, como fazer pra reconhecer cada linha?
var regExpTelefoneComChaves = /<table><tr>\([0-9]{2}\) [0-9]{4,5}-?[0-9]{4}<\/tr><\/table>/; //essa gambi será melhorada
var telefoneTabela = "<table>\
                        <tr>\
                        <td>(11) 2378-0944</td>\
                        <td>(11) 92378-0944</td>\
                        <td>(11) 82378-0944</td>\
                     </td>\
                     </table>";
regExpTelefoneComChaves.test(telefoneTabela);

//EXEMPLO 9 - Em muitos casos é possível substituir os números por metacaracteres: Utilizando Metacaracteres no lugar de numeros.
// . - representa um caractere qualquer
// \w - representa o conjunto [a-z A-Z 0-9]
// \W - representa o conjunto [^a-z A-Z 0-9]
// \d - representa o conjunto [0-9]
// \d - representa o conjunto [^0-9]
// \s - representa um espaço em branco
// \S - representa um não espaço em branco
// \n - representa uma quebra de linha
// \t - representa um tab

var regExpTelefoneComChaves = /<table><tr>(<td>\(\d{2}\)\s\d{4,5}-?\d{4}<\/td>)+<\/tr><\/table>/; //essa gambi será melhorada
regExpTelefoneComChaves.test(telefoneTabela);


//AGORA A API DE STRING:
//Match: Executa uma busca na String e retorna um array contendo dados encontrados ou null.
//Split: Divide a string com base em outra string fixa ou expressão regular.
//Replace: Substitui partes da String com base em outra string fixa ou expressão regular.

//EXEMPLO 10 - extraio o telefone de dentro, mas ele pega só o primeiro
var regExp = /\(\d{2}\)\s\d{4,5}-?\d{4}/;  
var telefone = "<table><tr><td>(80) 999778899</td><td>(90) 99897-8877</td><td>(70) 98767-9999</td></tr></table>";
console.log(telefone.match(regExp));

//agora eu vou usar o modificador /g para que ele percorra até o final. Vamos aos modificadores (que podem ser combinados):
// i = case-insensitive matching    g - global matching (percorra tudo, não pare, como acima)   m - multiline matching
//EXEMPLO 11 - vou usar o /g para que ele continue percorrendo e extraia todos os telefones (show)
var regExp = /\(\d{2}\)\s\d{4,5}-?\d{4}/g;  
var telefone = "<table><tr><td>(80) 999778899</td><td>(90) 99897-8877</td><td>(70) 98767-9999</td></tr></table>";
console.log(telefone.match(regExp));

//EXEMPLO 12 - vou dar replace dos telefones pela palavra "telefone"
var regExp = /\(\d{2}\)\s\d{4,5}-?\d{4}/g;  
var telefone = "<table><tr><td>(80) 999778899</td><td>(90) 99897-8877</td><td>(70) 98767-9999</td></tr></table>";
console.log(telefone.replace(regExp, "telefone"));