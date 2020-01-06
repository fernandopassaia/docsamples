To run this Meat App: 

(1) Enter the meat-app folder
(2) npm install (to install the dependencies)
(3) npm install -g json-server (this app run an automatic API under a json file with products...)
(4) json-server db.json (this will start the API)
(5) ng serve (in another terminal on same folder, to start Angular - or npm start)

Note: The source was wrote in English. The Front is Portuguese, but anyway, is easy to use, just select
a restaurant, add items, close the bag and well done. Below just info i've made during the DEV.

------------------------------------------------------------------------------------------------------------
Super Curso Completasso do Tarso Bessa (Udemy) sobre Angular 2 (e depois atualizado 4,5,6):
https://www.udemy.com/angular-pt/learn/v4/overview
https://github.com/cod3rcursos/meat-app-final


O Angular pode ser programado em 5 linguagens:
EcmaScript 5 - EcmaScript 2015 - TypeScript e Dart. Nesse curso usaremos TypeScript.
NPM - Node Package Manager, é a ferramenta de gerenciamento de dependências, inclusive do Angular.
Instalar o Angular como projeto Global: npm install -g @angular/cli
Para testar: ng -v (e imprime versão). Nota: comando mudou pra "ng version".
Instalar o TypeScript: npm install typescript -g (tsc -v testa)

Baixando Bibliotecas e Tipos:
npm init (name: typescript, version: 1.0.0) ou npm init -f (inicia com tudo padrão)
npm install --save lodash@4.14 (baixa uma versão específica - lodash é uma biblioteca JS de utilitários)
npm install --save-dev @types/lodash@4.14 (baixa a definição de tipos)
npm install -g json-server (cria servidores temporários de bancos de dados json)


Versões do Angular:
AngularJS se refere a primeira versão do Angular (2009). Depois tiraram o JS e vieram:
Angular 2.0 (Set2014), 4.0 (Dez2016), 5.0(Nov2017), 6.0 (Mai2018), 7.0 (Out2018) e 7.1 (Nov2018).

------------------------------------------------------------------------------------------------------------
TypeScript:

Começou do curso foi focado em TypeScript. Assuntos: Declarar variáveis, funções, classes e interfaces,
módulos e como importar uma biblioteca em JS. Typescript é uma linguagem que extende o Javascript e adiciona
 tipos como string, number e boolean, e também extende a linguagem permitindo o uso de classes e interfaces.
Foi criado pela Microsoft e Anders Hejlsberg, fundador do C#.

Features do Javascript:
Tipos estáticos - Organização de Códigos (encapsulamento com classes e módulos) - Suporta Código JS e 
estende as funções do EcmaScript 2015 (e ele também aceita código JS, se você escrever vai legal).

O Typescript não é entendido pelo navegador, então ele é "Transpilado" para Javascript. O Typescript pega
tudo e converte pra JavaScript (e isso não é lindo?).

Declaração de variáveis em TypeScript:

let name: string
name = "Fernando Passaia"
Ou: let name = "Fernando Passaia" (nesse caso ele não permitirá jogar um INT por exemplo).

Tipos: boolean, number e string. Existe o tipo "any" que foi criado pra compatibilidade com o Javascript, 
para permitir a adoção aos poucos (ou migração?) como se fosse um "var". Mas se uma atribuição errada for
feita, o compilador não detectará o problema.

Declaração Arrays:
let players: number[] = [1,2,3]
let players: Array<number> = [1,2,3]
let players = [1,2,3] //inferência de tipo
------------------------------------------------------------------------------------------------------------

TypeScript parte programação:
É necessário criar um tsconfig.json mesmo que ele fique vazio. E ai vou pedir pro meu compilador gerar o
arquivo automático com configurações padrões (como salvar o arquivo, ele regerar o .JS no mesmo diretório).

Instalar o TypeScript: npm install typescript -g (tsc -v testa)
Pra compilar um único arquivo: tsc app.ts
Para que ele fique monitorando uma pasta inteira: tsc -w (e ai ele fica monitorando a pasta)

Ou ainda, posso botar isso no tsconfig.json da seguinte forma:
{
    "compileOnSave": true,
    "compilerOptions": {
        "outDir": "dist"
    }
}

Usando a chave "NoEmitOnError: true" no Tsconfig, eu "forço" o Typescript/IDE a não aceitar modo de
compatibilidade, ou seja - se eu tentar botar uma variável decimal num string vai dar erro e não vai gerar
o JS novo correspondente.

** FUNÇÕES:

Permite tipos em parâmetros e retornos.
Arrow functions (funções de séries). Arrow functions é uma maneira de criar funções inchutas sem precisar 
citar a palavra function ou mesmo return, um exemplo:

function normal:
let tieFighters = ships.filter(function(ship){
	return ship.type == "TieFighter"
}

arrow function:
let ties = ships.filter(ship => ship.type === "TieFighter")

------------------------------------------------------------------------------------------------------------
** IMPORTANDO BIBLIOTECAS E DEFINIÇÕES DE TIPOS EM TYPESCRIPT:

Baixando Bibliotecas e Tipos:
npm init (name: typescript, version: 1.0.0) ou npm init -f (inicia com tudo padrão)
npm install --save lodash@4.14 (baixa uma versão específica - lodash é uma biblioteca JS de utilitários)
npm install --save-dev @types/lodash@4.14 (baixa a definição de tipos)

Definição de tipos: Nesse caso acima, lodash é uma biblioteca típica de Javascript que contém diversos 
utilitários. Eu baixo a definição de tipo para tornar esse código JavaScript compatível com Typescript, ou
seja, o Typescript passará a entender ele em tempo de desenvolvimento, dando "intellisense" e permitindo 
usar ele como se fosse typescript, entendendo os tipos de variáveis e tudo mais. Definição de tipo é usado
apenas no ambiente de DEV, e não precisa ser exportado para o Ambiente de Produção.

A Microsoft mantém um repositório de definição de tipos que pode ser baixado pelo NPM Registry, mas eles
também podem ser baixados num repositório GitHub Definitely Typed:
https://github.com/DefinitelyTyped/DefinitelyTyped

Isso irá gerar no Package.json as dependências e tipos:

packages.json
{
	"name": "typescript",
	"version": "1.0.0",
	"dependencies"{
		"lodash": "4.14.2"
	}
	"dev-dependencies"{
		"@types/lodash": "4.14.51"
	}
}

------------------------------------------------------------------------------------------------------------
Criando minha primeira aplicação: (Nota: Pasta doc img38 mostra um resumo do funcionamento em código/
estrutura). No terminal: ng new client-academy --prefix=cli

(Nota: O Prefixo serve pra separar nossos componentes de outros que estaremos usando, então na frente de
cada componente eu deverei usar "cli" e dessa forma sempre saberei quais componentes são meus).

Entro na pasta e digito: ng serve (inicia um servidor de dev). (subiu pasta localhost:4200).

Sobre os Arquivos Gerados, Estrutura e Como Personalizar minha aplicação:
* package.json: Arquivo da aplicação onde todas as dependências são listas.
* styles.css (dentro pasta src): aqui posso botar CSS que serão globais na aplicação.
* polyfills.ts (também dentro da src): arquivo de compatibilidade para browser antigos.

WebPack:
Existe uma ferramenta rodando por baixo dos panos chamada "WebPack". Ela é responsável por compilar todos
os arquivos em "pacotes" e subir o servidor, ao subir o servidor (ng serve) você verá no Terminal que ele
gerou alguns '.js' específicos (pacotes) e subiu o servidor. Além: o WebPack faz uma "injeção" de coisas:

Por exemplo, ao abrir o código fonte do "index.html" no browser, você verá uma série de Javascripts no
final da página que NÃO existem no arquivo index.html no VSCode. Esses JS são injetados "dinamicamente" no
momento da requisição pelo WebPack, e ele já passa direto pro Browser, de forma dinâmica. Interessante.
Salvei uma imagem (WebPack_Working.jpg) explicando como funciona.


** Configuração e Bootstrap (como roda por baixo dos panos - olhar ** ConfigBootAplicacao.jpg **):
Nota - Bootstrap refere-se ao boot do aplicativo (não ao framework css). O último arquivo inserido pelo 
WebPack é o "main.bundle.cs" que starta o Bootstrap e chamará o "main.ts". A linha que iniciará nossa
aplicação é a última:

platformBrowserDynamic().bootstrapModule(AppModule). Esse arquivo é definido acima, e ele aponta para a
pasta "./app/app.module". Ou seja, na pasta app haverá o "app.module.ts" que é um "módulo" em Angular. 
Nesse arquivo:

O Angular chama o módulo de Browser (que fará minha aplicação rodar no browser). Módulo FormsModule (para
desenhar forms). Módulo básico para trabalhar com formulários. O módulo HttpModule (para fazer requisições
remotas a um servidor para obter conteudo - módulo básico). O NgModule é um Decorator pra aplicar metadados
em uma Classe, Método, Atributo ou Argumento de Método. Se você for olhar ele já está sendo usado nessa
mesma classe - mostrando as declarações (componentes), fazendo o imports, providers e Boostrap (o
componente que irá iniciar nossa aplicação).

------------------------------------------------------------------------------------------------------------
Componentes:

Componentes são pequenas partes reusáveis. São Classes que tem um ciclo de vida, possuem um template que
define a aparência e um selector(TAG) para ser usado por outras partes na aplicação. São elementos 
personalizados. No AngularJS (Angular1) são Controllers e Scopes.

Para ver a estrutura de um componente coloquei a imagem (3) na pasta, e também a imagem 4 que mostra a 
estrutura via código. Também coloquei o @NgModule na imagem 5 de como ele seria importado.

Parar gerar um componente, na pasta do Aplicativo, usar o Terminal:

ng generate componente nomeComponente (OU - forma abreviada):
ng g c header --spec=false (spec = não vai gerar componentes de teste).
Nota: Para versões mais NOVAS do angular usar caminho a partir da raíz: ng g c order/delivery-costs 
--spec=false


Ele irá gerar 3 arquivos:
header.components.ts que é a classe do componente.
header.components.html que é o template do componente.
header.component.css que é o estilo do componente.


Nota Fer: Na pasta DOC existe um TEMPLATE do projeto que foi usado, basicamente vazio e não programado.
É interessante analisar como a página restaurantes (onde existem 6 restaurantes) foi componentizada, foi
transformada em 1 único componente Restaurante (que tem nome, nota, tempo, imagem) durante o curso, 
específicamente na aula 37. Veja a imagem 14 que eu salvei na pasta explicando.

------------------------------------------------------------------------------------------------------------
Property Binding:
Acontece quando você quer linkar uma propriedade a elementos Angular que podem variar pra um método ou até
uma expressão mais elaborada. Em resumo: É praticamente uma Session se formos olhar pro Asp.net. Olhar
imagem 6/7/8.

É uma maneira de você linkar uma propriedade/atributo de um componente pra uma propriedade do elemento do
DOM do template daquele elemento. Você pode fazer isso pra elementos HTMl ou mesmo seus componentes.

Nota: Criei a classe Student e vou seguir exatamente como ele está fazendo pra não confundir o exemplo.

Pra passar algo diferente de String, você precisa usar os [] (colchetes) que indicam que é um Property 
Binding. Toda vez que você ver um [] no HTML com algo dentro, indica que é um Property Bind.

------------------------------------------------------------------------------------------------------------
Diretivas:
Basicamente é aquilo que permite fazer IF, FOR, e CASE na interface (arquivo HTML). Tem o *ngIf, *ngFor e
por ai vai, fiz alguns exemplos lá no código na parte de Students.

Nota: No projeto "Student" (client-academy) tem vários exemplos de array, for, if e tudo mais. Show!

------------------------------------------------------------------------------------------------------------

Nota Importantíssima: IMAGEM 53 e 54 tem todo resumo de toda estrutura, observable, subscribe, como as
camadas se conversam, se comunicam, serviços, TUDO!


***************** INICIANDO O PROJETO MEAT:

*-*-*- NOTA IMPORTANTE: Na imagem 53 eu tenho toda "revisão" do meu último componente da Order (os Totais)
e exatamente a ordem de funcionamento das coisas nas telas e camadas. A Imagem 53 é uma super-revisão do 
modelo de Dev adotado. A imagem 54 é ÓTIMA: Uma verdadeira AULA sobre todas camadas e como usar o HttpPost 
e Observable com perfeição para Fechar a Compra!

Na pasta doc tem dois arquivos. O Meat-app-final refere-se ao projeto concluído, o código fonte do curso. 
O meat-app-starter é o "esqueleto" do projeto, o template utilizado durante o curso. Será baixado e 
utilizado bem no inicio do Desenvolvimento da aplicação MEAT (Sessão 5 Aula 32). O "meat-template" é o
template original sem alteração alguma que ele deixou lá no site.

Após isso abrir a pasta e rodar o "npm install" para que ele suba novamente as dependências.


** ROTAS:
Explicação sobre a declaração de rotas e o uso dela estão nas imagens de 10 até 13.

Nota Fernando: Na pasta DOC existe um TEMPLATE do projeto que foi usado, basicamente vazio e não 
programado. É interessante analisar como a página restaurantes (onde existem 6 restaurantes) foi
componentizada, foi transformada em 1 único componente Restaurante (que tem nome, nota,tempo, imagem)
durante o curso, específicamente na aula 37. Veja a imagem 14 que eu salvei na pasta explicando.


***** INJEÇÃO DE DEPENDÊNCIA: Injeção de dependência é um Padrão de Projeto. É um Framework onde a 
aplicação deixa de instanciar seus objetos e começa a depender do framework pra instanciar. O Framework
gerencia a instanciação dos objetos bem como suas dependencias e disponibiliza isso para os componentes
da aplicação. Veja imagem 15-18

***** SERVIÇOS: São classes comuns em Angular que você pode usar pra injetar em outros serviços ou 
componentes. Serviços são geralmente usados para encapsular o acesso para uma API no Backend. Serviços
pode ser Singletons: Isso é, podem ser usados para guardar dados de toda aplicação. Ver imagem 19-22.

Criando meu primeiro serviço: A partir da aula 40 eu irei criar um Serviço para acessar meus dados de
restaurante. É como se fosse o acesso da minha API.


***** Programação REATIVA: A ideia é simples, quando um evento acontece, os que estão interessados são
avisados e reagem a ele. É baseado num padrão bem comum da Web chamado "observer". Os objetos interessados
são chamados de "listeners", e eles se escrevem esperando uma mudança (evento). Quando o evento acontece,
o listener que é o interessado executa alguma ação - essa é a ideia base. Veja imagem 23 a 30. Na imagem
32 eu mostro na prática com anotações passo a passo de como isso tudo - observer e listener funcionam com
perfeição! VEJA!


***** API DE DADOS do arquivo db.json (um banco de dados que encontra-se na pasta):
Isso é bem louco, ele sobe uma API REST baseada no meu arquivo de banco de dados temporário JSON. É muita
xaropagem da cabeça mas funciona perfeitamente - veja a imagem 31 dentro da pasta doc.
 
npm install -g json-server (cria servidores temporários de bancos de dados json)
json-server db.json (starta o serviço).


***** Tratamento de Erros: No Angular você pode tratar erros de duas formas - criando uma classe para
tratamernto de erros ou fazendo o tradicional catch. Nós faremos uma classe de erro e lá no componente
restaurant.service.ts listener que obtem restaurantes) para usar caso a WebApi caia ou etc.
(app.error-handler.ts) (ver imagem 33).


***** ROTAS com PARAMETROS: Aula 46 - Existem duas formas - ver imagem 34 a 38, explicado certinho lá.


***** Menú, carrinho de compras e avaliações: A partir da aula 46 o pau vai torar, iremos criar os 3
componentes que faltam pra aplicação: Menú, carrinho de compras e avaliações. Agora vai!

(NOTA IMPORTANTE: Pasta doc img38 mostra um resumo do funcionamento em código/estrutura).


***** PIPES: São filtros que permitem transformar conteúdo, em Angular são chamados de "Filtros". Pipes
são o toUpper(), toLower(), formatar números, moedas, datas, limitar um array, formatar um conteúdo json.


***** REVISÃO: Na Imagem 40 eu faço uma REVISÃO do funcionamento dos itens FILHOS sendo carregados pelo
PARENT, das chamadas, maneira como uma coisa conversa com a outra - fazendo uma revisão do curso até aqui.


----- RETA FINAL DO APP DE MEAT:
***** Carrinho de Compras: Para implementar o carrinho de compras, os itens selecionados pelo usuário serão
armazenados em memória até que o usuário decida FECHAR o pedido. O Pedido somente será PERSISTIDO no
momento que o usuário decidir fechar o pedido. A partir da imagem 40 terei o registro da parte do carrinho
no curso.

Um Serviço é muito BOM para guardar dados compartilhados, pois dependendo de onde for declarado, ela pode
ser acessada por todos os componentes da aplicação. Se o Serviço for declarado no módulo principal,
qualquer componente que pedir uma instância desse serviço, receberá o mesmo com os mesmos dados. Então
nesse caso um Serviço é o ideal pra guardar os dados.

Então eu criei um novo Serviço/Estrutura chamado "shopping-cart.service" e ele armazenará os dados em
memória.


***** Internacionalização: No arquivo "Polyfills.ts" eu importei o pacote 'intl' (de internacionalização,
que está instalado lá no package.json) e dentro do intl o PT-BR.js. Isso é necessário para colocar a moeda
no formato R$ 0,00. Após isso existem alterações no app.module.ts (documentei lá).


------------------------------------------------------------------------------------------------------------

***** TEMPLATE FORMS: É uma forma declarativa de declarar seus formulários no template do componente. Com
template forms se usa "ngModel" nos componentes. Mais doc na imagem 41 e 42. A partir dessa parte do curso
(Aula 56) será usado template forms e feito as páginas de compras e tudo mais - o componente será o ORDER
feito com Template Forms.


***************** Parte de FECHAR A COMPRA (Order) no Curso:
A partir da aula 57 o Curso fica louco por que ele fará 4 componentes para ORDER. Irei documentar muito bem
tudo a partir da Imagem 43 e também em todos os HTMLs e Componentes e tudo mais. Olhe bem a documentação da
Order e seus filhos. Abaixo irei documentar tudo que irei aprender a partir daqui:

* NGForm e Tags de Forms (template forms - ver imagem 41 e 42). 
* NGForm funcionando na prática (imagem 44).
* A partir da aula 58 faremos as VALIDAÇÕES no NGForm Template Forms (imagem 45 a 48 - validators, tipos e
etc).

Nota: Na Aula 60 (e print 49) eu fiz a validação de FORM direto no Form, funciona perfeito, mas ficou muito
verboso (cheio de repetições de códigos). Na aula 61 será feito um COMPONENTE pra validação de formulários
e mudará tudo. Deixei o exemplo salvo e explicado na imagem 49.

Em resumo: Farei um COMPONENTE de TextBox que permitirá desenhar vários textbox com seus validators, suas
cores para campo não preenchido, suas validações e tudo mais. E ai esse componente será compartilhado por
toda aplicação.


***************** Criando meus COMPONENTES Personalizados (Inputs - TextBox, RadioButtons e por ai vai):
Na aula 61 eu fiz um componente de INPUT (TextBox) personalizado, com validação, erro, classe, para ser
reutilizado pelo form inteiro. Muito bom, dá uma olhada na imagem 50 que explica como funciona!

** Criando o componente de Forma de Pagamento
A partir da aula 63 ele começará a criar o componente de Forma de Pagamento usando o ControlValueAccessor.
Será criado objetos segundo componente chamado de RADIO (que é pra fazer os buttons da seleção da forma de
pagamento).


** CONTROL VALUE ACCESSOR: Na Aula 64 eu vou implementar a Interface ControlValueAccessor. Essa interface
dará poderes ao meu componente (order) para conseguir acessar as diretivas de Formulário NgModel ou de
Reactive Forms. Isso irá permitir que o Parent ORDER acesse o RadioButton que está no FILHO de RadioButton
e outros mais. Os itens e frete serão implementados da mesma exata maneira. Ver img 51 e 52 (52 explica
como funciona a interação entre eles).


*-*-*- NOTA IMPORTANTE: Na imagem 53 eu tenho toda "revisão" do meu último componente da Order (os Totais)
e exatamente a ordem de funcionamento das coisas nas telas e camadas. A Imagem 53 é uma super-revisão do
modelo de Dev adotado. A imagem 54 é ÓTIMA: Uma verdadeira AULA sobre todas camadas e como usar o HttpPost
e Observable com perfeição para Fechar a Compra!


***** Fechando a Compra e enviando o HttpPost pro nosso BackEnd - Aula 68:
Documentei tudo na imagem 54 - uma verdadeira AULA sobre todas camadas e como usar o HttpPost e Observable
com perfeição!

------------------------------------------------------------------------------------------------------------

***** REACTIVE FORMS: No módulo 7 aula 72 o projeto foi todo REFATORADO de Template Forms para Reactive
Forms. Antes do Refatoramento, peguei todo código, tirei o node_modules e salvei na pasta
"meat-app_antesRefatorarReactiveForms.rar".

Reactive Forms é a nova maneira do Angular de implementar formulários: NgForm como foi feito é compatível
com AngularJS, a versão antiga. Na nova versão (2) eles criaram o Reactive Forms, que é uma nova maneira de
criar Forms de forma Programática, ou seja, a responsabilidade está mais no Back (Component.ts) do que no
Front. Nós podemos criar validações e agrupar nossos componentes de forma um pouco + sofisticada.

Dessa forma nós iremos refatorar todo componente de compras e tirar ngModel, e trocar tudo pra FormGroup e
FormControlName. Por causa disso teremos que adaptar nossos Inputs pra não depender mais de ngModel mas sim
de outra diretiva FormControlName.

Nos Forms importarei as diretivas FormGroup e FormBuilder para poder fazer isso. Nosso código do component
(TS) ficará um pouco mais verboso (cheio), mas obviamente nosso Template (HTML) ficará mais limpo. As 
validações e diversas outras coisas irão parar no Code-Behind também.

Vamos editar nosso componente de Compras e criar uma representação programática de Formulário. Irei doc a
partir da img56.

Aula 74: Irei criar um Validador Personalizado para verificar se o EMAIL e o EMAILCONFIRMACAO são iguais,
senão invalidar formulários.

------------------------------------------------------------------------------------------------------------

***** MÓDULOS ANGULAR: A partir da aula 75 ele falará sobre MÓDULOS no Angular. Módulo serve para Organizar
sua aplicação, para decidir e definir o que será Exportado para fora de sua aplicação, e além disso: Como
a injeção de dependência será configurada.

Além disso: Supondo que você tenha uma aplicação muito grande, com milhares de usuários em uma empresa em
vários setores diferentes, não é interessante que você carregue toda aplicação pra todos eles. Uma parte
estará usando apenas um PEDAÇO. Com módulos conseguimos definir partes independentes, e definir que elas 
não devem ser carregadas de imediato, apenas quando requisitadas. Isso garante que o Startup seja feito
mais rápido e é chamado de "Lazy Loading" (carregamento tardio). 

Lazy Loading é a capacidade de carregar uma parte de nossa aplicação apenas sob demanda, apenas quando ela
é necessária. No momento atual todas nossas dependências estão no módulo raíz, ou seja, serão sempre 
carregadas no boot da aplicação. Quem carrega o Lazy Loading (ou seja: quem carrega o módulo quando o 
usuário requisitar alguma coisa que não está carregada) é o ROTAS, ele que carregará o módulo no momento
necessário.

Os tipos de Módulos da Aplicação podem ser:
* Root (módulos raíz, que serão usados pela aplicação inteira - o bootstrap da aplicação). Em aplicações
pequenas é suficiente ter tudo no root, mas em aplicações grandes é interessante organizar tudo em módulos.
* Shared & Core Modules: Contém componentes (Shared) e serviços (Core) que podem ser compartilhados com
outros módulos. O HTTP é um exemplo.
* Feature: É onde encontram-se as implementações, os módulos, e é nisso que podem ser separados e 
organizados, e carregados em partes.

Deixei como exemplo a imagem 56 e 57 com uma separação da aplicação em Módulos. Criei um backup rar 
"AntesModularizar".


Core Modules: Pelo que entendi são uma maneira de organizar serviços que podem ser usados por toda 
aplicação. Nesse caso específico, ele criou um diretório chamado CORE, e dentro dele um CoreMódule.
Serviços (OrderService, ShoppingCartService, RestaurantsServices) ficarão nesse core.

Nota: Existe também uma opção chamada "Pre Loading Strategy" que permite que eu carregue a aplicação pro
usuário, liberando ele pro uso, e após isso, eu comece a carregar meus componentes "secundários" no 
BackGround. Isso também evitará que lá na frente quando o usuário tenta carregar uma funcionalidade que
ainda não está, ela já esteja disponível. Eu farei isso lá no app.module.ts no RouterModule 
(preloadingStrategy).

------------------------------------------------------------------------------------------------------------