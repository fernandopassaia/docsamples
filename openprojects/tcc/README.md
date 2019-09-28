Para Rodar esse Programa:
------------------------------------------------------------------------------------------------------------
(1) Existe um arquivo .RAR chamado "FuturaData.rar" na raiz do projeto, descompacte a pasta "FuturaData"
no C:\ (ficará c:\FuturaData). Essa pasta é necessária para que o projeto possa compilar e rodar. Dentro 
dessa pasta existem dois diretórios:

TCC (onde serão compilados os arquivos principais do projeto - aqui fica o programa principal)
DllsFuturaData (essas são Dlls adicionais, com funções úteis e gerais. Essa DLL atualmente se chama
"CSharpOpenBRFramework"). Você não precisa mexer nesse projeto, caso não queira, essas DLLS já estão
compiladas, é só usar.

Certifique-se de ter o .Net Framework 2 e 4.5 instalados, além disso o ReportViewer Redistributable 2015
da Microsoft (para relatórios).

(2) Dentro da raiz do projeto, existe um "banco.rar". É o banco de dados com informações de teste (SQL 
Server). Você precisa ter um Servidor de banco de dados SQL Server em sua máquina (2005-2014). Attache esse
banco em sua instância.

(3) Abra o projeto "FuturaDataTCC_Solution.sln" e compile seu projeto. Caso haja problema de referência de
alguma DLL, aponte para a pasta c:\FuturaData\TCC - todas DLLs estarão lá, caso você tenha seguido com 
sucesso o Passo (1). As DLLS também estão no diretório CodigoFonte>Retaguarda>DllsReferencia, basta jogar
em c:\FuturaData\TCC. Caso haja algum problema, entre na propriedads dos projetos (WindowsForms e DLLS) e
verifique se o Build está apontando pra c:\FuturaData\TCC.

(4) Rode o projeto Windows Forms "FuturaDataTCC" - ele irá pedir a conexão com o banco. Informe o nome da
sua máquina (servidor), o nome da instância, do banco (FDTCC) seu usuário sa e sua senha de usuário. Salve
e ele fechará. Basta rodar de novo e estará liberado. A conexão fica no "conexao.xml" dentro da pasta TCC,
apagando esse arquivo, o sistema passa a PERGUNTAR novamente qual o servidor.

O usuário e senha padrão do sistema são:
adm
1234fd

A documentação toda do projetos e todos diagramas estão na pasta Documentação. Alguns Diagramas estão no
Microsoft Visio, e alguns no Programa Astah 6.5 pro (procure na internet). Notas:

(1) Esse Software foi desenvolvido em 2013, apenas para TCC de nossa Universidade. Na época tivemos tempo
de validar para MVC o Cadastro de Clientes, Produtos e Vendas. O caixa ainda possui muito código que não
está adequado, como uso de DataTables e outras coisas, mas funciona perfeitamente.
(2) Foram feitos todos Diagramas e Mapas de acordo com o que a universidade solicitava na época.
(3) A Nota do TCC foi 9.5, pois havia um diagrama com erro, que corrigimos depois, antes de entregar o CD
final para os Gestores.
