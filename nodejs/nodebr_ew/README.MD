Curso do Erick Wendel - NodeBr:
Curso feito na Hungria já, 27/12/2018 - Bora!

------------------------------------------------------------------------------------------------------------

npm:
(1) É um gerenciador de dependências de Javascript.
(2) Identifica as Dependências a partir de arquivos package.json.
(nota: ao subir seu projeto pra outro PC ou Servidor, o restore
lerá o arquivo pra identificar as dependências e baixar o que precisar).
(3) Usado também para executar scripts bash a partir do seu projeto.
(4) Se eu tiver um projeto pronto que baixei do GIT (exemplo), mas não veio os pacotes,
e eu rodar um "npm install" (sem nada), ele baixará todos os pacotes que estiverem no
package.json de acordo com meu sistema operacional (tipo restore do nuget).


Package.json:
É possível adicionar a linha "dev" em scripts e informar o que ele deve
rodar em cada ambiente. Tipo: "dev": "node index.js". E ai rodo: npm run dev.

JavaScript:
Você pode declarar uma variável usando: var, let, const
Você passar o arrow function "=>" é o mesmo que declarar "function".

Nota sobre DESTRUCTOR em JavaScript. Se eu tiver algum resultado do tipo Array: pra pegar um resultado
específico dentro do array, eu poderia fazer o seguinte: const posicaoUm = resultado[0]
porém, eu posso fazer "const [resultado] = await". Quando eu coloco entre chavetas, ele entende que é o
resultado na posição 0. Se eu fizer const [resultado, posicao2, posicao3] ele irá entender e pegar os
próximos resultados... legal né?

Nota: Você pode concatenar variaveis e arrays em Javascript usando "..." (ver 08-cli database.js).

Cuidado com retornos: No Javascript quando não acha nada, ele sempre retorna -1 if(indice !== -1)

Comandos:
node -v (mostra versão Node) (atual 10.15.0)
npm --version (mostra versão npm - atual 6.4.1)
npm init (cria o arquivo package.json)
npm init -y (cria o arquivo package.json sem perguntar nada)
cat package.json (abre o arquivo "mostrando" ele dentro do cmd)
touch index.js (crio um arquivo index.js dentro da pasta)
code . (abro o VS Code na pasta onde estiver)
npm install axios (contém as ferramentas para ler uma webapi)
npm install commander (contém as ferramentas pra desenvolver console / linha comando).
Nota: "npm install axios" e "npm i axios" geram o mesmo resultado: "i" é abreviação.


Node e seus comandos no terminal:
node index.js (roda o arquivo JS)
node . (procura o main dentro do package.json e roda)
O arquivo main (principal) do node fica no package.json e por padrão é index.js


//ESQUELETO DO MÉTODO PRINCIPAL MAIN USADO EM PRATICAMENTE TUDO
async function main() {
    try{

    }catch(error){
        console.error('DEU RUIM: ', error)
    }

}
main() //chamada do método principal acima



------------------------------------------------------------------------------------------------------------
MÓDULO 1 do Curso: Sincronia de funções JavaScript (Promises e Eventos):

Promises:
É uma maneira de tratar melhor os CallBacks do JavaScript. A Aula 002 foi feita sem Promises,
Na Aula 03 o código será "refatorado" para uso de Promises e então ficar melhor escrito. Na aula
04 ele será refatorado pro uso de Promises Async/Await e ai sim ficará perfeito.

Ciclo de Vida do Promises:
Pending: Estado inicial, ainda não terminou ou ainda não foi rejeitado, não tem retorno, está pendente.
Fulfilled: Quando executou todas as operações com Sucesso.
Rejected: Quando algo deu errado e não funcionou.

Deixei um PRINT do ciclo de vida de Promise lá na pasta (jpg). Existem mais dois prints chamados
"resultado" e "resultadoProcessamentoExplicacao" que explicam bem a ordem e como é a execução das coisas.

Diferença entre PROMISES e EVENTOS: Promisses é pra executar só uma vez, eventos pra monitorar ações.


------------------------------------------------------------------------------------------------------------
MÓDULO 2 do Curso - Manipulação de Listas (For, Foreach, ForIn, Map, Filter, Reduce):

npm install axios
Olhar dentro das classes que eu fiz toda documentação sobre como fazer FOR, FOREACH e tudo mais, além,
também tem o uso de map, reduce e tudo mais.


------------------------------------------------------------------------------------------------------------
MÓDULO 3 do Curso - Automatização de Testes em JavaScript com Mocha
Criei uma pasta pra testes, dentro dela: "npm init -y" (pra iniciar) e npm i axios (pra usar api starwars).

npm install -g mocha (instala o mocha global)
npm i --save-dev mocha (instalo ele como dependência de desenvolvimento - em produção nunca mandar testes)
npm install nock (nock é um pacote para simular requisições - dar o retorno sem ir na webapi, ver tests.js)

Para fazer o teste: mocha tests.js (ou o arquivo que eu quiser testar). Se o arquivo for "test.js": basta
digitar "mocha" no terminal e entenderá. Ou ainda, no "package.json" eu posso mudar o "test" pra "mocha"
e ai quando eu rodar npm test.js ele irá rodar o mocha. Ele também atende por "apelido": npm t.
Se no package.json eu botar "mocha -w" ele irá rodar no terminal do VSCode sozinho cada vez que eu salvar.

mocha --help (lista todos os comandos)
mocha --reporter nyan (mostra um resumo com um gatinho wtf)


------------------------------------------------------------------------------------------------------------
MÓDULO 4 do Curso - Criando uma ferramenta CLI (console) que fará todas operações de DataBase.

Primeiro eu criei o projeto 06, ai inicializei e instalei o mocka pois iniciamos com os testes:
npm init -y e npm i --save-dev mocha

Na parte final (index.js) rodei no terminal "npm install commander", o pacote node para ferramentas console.


------------------------------------------------------------------------------------------------------------
MÓDULO 5 do Curso - Projeto Multi-Banco de Dados com MongoDB e PostgresSQL rodando no Docker. Prints pasta
docker.

Docker:
docker ps (processes, pra ver o que está rodando)

*---------------------* Instalando Postgres no Docker:
(1) docker run -d -p 5432:5432 --name postgres -e POSTGRES_USER=fernandopassaia -e 
POSTGRES_PASSWORD=@1234Fd@ -e POSTGRES_DB=heroes postgres
Nota: Primeiro nome (postgres) é o nome do Repo, o último é o nome interno (poderia ser PostgresFer).
Primeira porta é externa, segunda interna, "heroes" é o nome do BD que to mandando ele criar. -d significa
que é pra rodar em segundo plano.

(2) docker run --name adminer -p 8080:8080 --link postgres:postgres -d adminer
Nome da Ferramenta é adminer, porta externa e interna 8080, no "link" eu digo que esssa ferramenta pode
acessar o Postgres,-d eu digo que rodará em segundo plano e o nome dele eu digo que é adminer. -d significa
que é pra rodar em segundo plano.

(3) docker exec -it postgres /bin/bash (entro no terminal do postgres, ls lista, é um linux).

*---------------------* Instalando MongoDB no Docker:
docker run --name mongo -d -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=admin -e 
MONGO_INITDB_ROOT_PASSWORD=@1234Fd@ mongo

Cliente Web para MongoDB:
docker run --name mongoclient -p 3000:3000 --link mongo:mongo -d mongoclient/mongoclient
(Ai abra o Browser localhost:3000, clique em add connection, host mongo, database name admin, na aba
autentication coloque scram-sha-1, username admin, senha @1234Fd@, autenthicationDB admin)

Criando e liberando o banco de dados na linha de comando:
docker exec -it mongo mongo --host localhost -u admin -p @1234Fd@ --authenticationDatabase admin --eval 
"db.getSiblingDB('herois').createUser({user: 'fernandopassaia', pwd: '@1234Fd@', roles: 
[{role: 'readWrite', db: 'herois'}]})"
(após criar o usuário acima, crie uma nova conexão no localhost:3000 com banco herois e usuário que passei
acima)

*---------------------* Padrões de Projeto:
Iremos trabalhar com o Strategy: A explicação do Wendel diz que por exemplo: Se eu quero assistir 
DragonBall eu preciso de uma estratégica: Assistir na Netflix, no DVD, ou na TV Globinho. A mesma coisa
serve pra isso: Se eu tenho que cadastrar um herói, eu posso fazer no MongoDB ou no PostgreSQL. Eu deixei
dois prints sobre o Padrão Strategy na pasta Doc.


------------------------------------------------------------------------------------------------------------

*---------------------* Banco de Dados PostGreSQL

Na pasta do projeto (10-multi-db-postgres) eu instalei o Sequelize (ORM): npm install sequelize.
Depois eu instalei os Drivers do Postgres: npm install pg-hstore pg