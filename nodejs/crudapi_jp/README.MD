Anotações do Curso de NodeJS com WebApi e Autenticação JWT do DOM JP 
(https://joaopaulors.wordpress.com/2018/05/10/introducao-a-criacao-de-api-com-nodejs/)

O que instalar:
Build Essential: apt-get install -y build-essential
Curl: apt-get install curl
Npm: apt-get install npm
Node Js: apt-get install nodejs

Para instalar o NPM na pasta do projeto, abra ela e digite: npm install

Para subir o Servidor do node, na basta digite: npm run dev
Nota Fernando: Se você baixar o projeto do repositório em outra máquina, ele não vai rodar, por que
determinados arquivos do node não sobem pro TFS. Então crie uma pasta nova, rode o npm install dentro (pra
isso precisa ter o package.json na pasta), ai copie node_modules criada e substitua na pasta desse projeto.
Precisa jogar o pacote inteiro na pasta onde está esse projeto, pra pasta ficar cheia - só substituir tudo.
Ai sim irá subir legal.


-- Postman (para inserir valores) (Modo Post, Body - Raw - JSON - importante selecionar JSON, senao ele
insere todo zoado) (http://localhost:8000/api/clientes)
{
	"nomeFantasia" : "JK",
	"razaoSocial" : "John e Kbelo",
	"cnpj": "000000000001"
}

{
	"nomeFantasia" : "Harada",
	"razaoSocial" : "Harada Auto Pecas",
	"cnpj": "000000000002"
}

{
	"nomeFantasia" : "Cahe",
	"razaoSocial" : "Cahe Fraldas",
	"cnpj": "000000000003"
}

-- Postman (para recuperar valores) (Modo Get)
http://localhost:8000/api/clientes (pode acessar tambem pelo Browser)

-- Modo Get passando o ID do cliente
http://localhost:8000/api/clientes/id/5ba8e90822a159274dd176b0

------------------------------------------------------------------------------------------------------------