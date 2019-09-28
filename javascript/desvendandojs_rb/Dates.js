//** DATE: Apesar de parecer mais simples, a data não é representada dessa forma... Como assim? A Data é representada
//pela quantidade de milissegundos desde o início da era Unix. A Era Unix (Unix Epoch) ou Posix time, teve ínicio no dia
//1 de janeiro de 1970 ás 00:00:00 do tempo universal coordenado (mais conhecido como UTC), referência a partir de onde se
//calculam os fusos horários do mundo inteiro.

//Existem 4 maneiras de se criar uma data, verificar classe a única maneira de criar data é através da função construtora new
//é meio estranho mas: se você colocar var hoje = new Date(); e der um hoje.getTime(); terá um número gigantesco que é a contagem
//de segundos do inicio de 1/1/1970 00:00:00 até hoje. Away demais. O cálculo é feito a partir de UTC 00:00, também conhecido como
//Z, abreviação de Zulu Time. Veja a imagem "UTC" dentro da pasta que tem o UTC do mundo inteiro.
//Os formatos oficialmente aceitos são: RFC 2822 ou ISO 8601.


//Primeira forma de criar data
var hoje = new Date(); //cria a data de hoje
console.log(hoje.getTime()); //retorna os milisegundos de 1/1/1970 00:00:00 até hoje

//segunda forma - usando UTC
var natal2014 = new Date(1419465600000); //eu consigo criar uma data baseada lá numa data passada
console.log(natal2014); //imprimiu 25/12/2014 as 00:00, note que isso por que meu PC está no UTC da Hungria, se fosse no BR imprimiria 24/12/2018 22h

//terceira forma - usando string
var Data1 = new Date("2014/12/25");
var Data2 = new Date("12/25/2014");
var Data3 = new Date("25/12/2014"); //esse formato é o errado, vai dar bucha (Formato BR)

console.log(Data1); //2014-12-24T:23:00:00.000Z
console.log(Data2); //2014-12-24T:23:00:00.000Z
console.log(Data3); //Invalid Date

//agora eu vou converter pra milisegundos, e reconverter pra data de novo
var dataMilisegundos = Date.parse(Data1);
console.log(dataMilisegundos);
var dataVoltando = new Date(dataMilisegundos);
console.log(dataVoltando);

//nota - eu consigo usar traços e até um T pro tempo (string ISO 8601)
var dataComTracosETempo = new Date("2014-12-25T10:30:00");
console.log(dataComTracosETempo); //engraçado que imprime 09:30 na tela por causa dessa porra de UTC louco

//pra resolver o lance do UTC eu vou precisar colocar igual embaixo, descontando a 1h
var dataComTracosETempo = new Date("2014-12-25T10:30:00+01:00");
console.log(dataComTracosETempo); //engraçado que imprime 09:30 na tela por causa dessa porra de UTC louco

var criandoDataComInt = new Date(2018, 11, 25);
console.log(criandoDataComInt);

var criandoDataComInt = new Date(2018, 11, 25, 10, 30, 0);
console.log(criandoDataComInt);

//nota: documentacao completa na imagem DateAPI na pasta...