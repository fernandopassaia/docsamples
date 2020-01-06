Aqui eu armazenarei cursos do Hebert Pereira que são bem completos, um dado no Canal DotNet, outros dados no
canal dele mesmo no Youtube. Hoje é dia 05-07-2019 e estou na FutureOfMedia, iniciando a parte pesada dos
estudos sobre IdentityServer. Bora lá!

https://identityserver.io/
https://github.com/IdentityServer
https://github.com/IdentityServer/IdentityServer4.Templates

Nota: Irei SALVAR os vídeos dele, tem um vídeo do Canal DotNet (Demo com o Groofe) e os outros vídeos
(numerados de 1 a 15) são do CANAL dele no youtube.

https://www.youtube.com/watch?v=G23-d9K7SKs
https://www.youtube.com/channel/UCRmWmrtzO3sLIa1aY9vNaJw

------------------------------------------------------------------------------------------------------------

Nota: Em 08/07/2019 eu criei uma nova pasta chamada "Practical" e dentro dela, eu criei um projeto "Beta"
de Client (MvcClient) e outro contendo o IdentityServer, que vai dar autenticação para esse projeto. Isso
será documentado a partir da imagem 10, e está no 00:58 minutos do vídeo 1 do CanalDotNet.

Nota 2: A partir da hora/min 01:33 já está tudo funcionando (img21). O cliente MVC Razor tem uma rota
chamada "Privacy" que eu protegi. Ao clicar nela, ele será direcionado para o meu IdentityServer (5000) e
irá se autenticar, recebendo um Token, e é mandado de VOLTA pro client MVC. Tudo está perfeito! Agora ele
irá criar um NOVO aplicativo API, um terceiro APP, e o aplicativo API irá usar esse MESMO token. Veja a
partir da imagem 22 esse processo.

Nota 3: A partir dos 01:58 ele irá criar OUTRO projeto, dessa vez Identity MVC (aquele projeto de contas)
de usuário do Identity. E ele irá integrar com o IdentityServer, muito bom! A partir img25. A partir desse
momento ele irá criar vários projetos (como AspNetMVCIdentity, AdminUi) para mostrar que existem várias
maneiras, templates, layouts, várias coisas sobre o Identity e maneiras de administrar ele.

Nota 4: O projeto criado aos 01:58 (Nota 3) estava ERRADO. Então aos 02:04 ele viu que era um projeto que
usava SQLite, e abordou, criando um novo projeto is4ef (a partir da imagem 29). Então eu exclui a pasta
antiga, mas manterei as imagens (da 25 a 29) pra mostrar que é possível usar IdentityServer com SQLite e
que ele cria exatamente a mesma estrutura de tabelas...

O novo projeto se chamará AspNetMvcIdentityEF mas agora terá o template "is4ef". A ideia desse projeto é
mostrar como o IdentityServer pode fazer o "storage" de suas coisas usando EF. Nesse template o EF está
usando SQLite, mas é só mudar pra UseSQLServer e configurar que vai rolar. A partir da imagem 29 irei 
documentar como seria usar EntityFramework para armazenar meus dados do IdentityServer.

------------------------------------------------------------------------------------------------------------

Nota 5: VEJA a partir da imagem 29 o Painel PAGO do IdentityServer. Existe um template específico chamado
"is4admin" que cria um PAINEL para o IdentityServer. Com ele é possível você cadastrar seus usuários, suas
roles, suas claims, seus aplicativos e tudo mais. Esse Painel é PAGO, porém é possível usar sua versão de
forma grátis para até 10 usuários e 2 aplicativos.

Ele tem todas as opções de gerencia, eu irei PRINTAR todas as telas e opções e explicações do Hebert, então
vai ficar bem documentado. É provável que um belo dia eu precise criar um painel desse, de administração,
para o meu identity server, criar minhas próprias telas... todavia, vale a pena ver o exemplo. Eu não 
consegui rodar o projeto na minha máquina, deu algum pau de banco de dados, ele está usando SQL Server...

Todavia depois tenta, vai ser legal... e é bom pra copiar a estrutura do banco de dados (ele cria um SQLite)
e replicar pro SQL Server, criando minha própria estrutura... essa APP usa Identity (MVC) pra criar usuários,
enfim, é algo assim que eu preciso, de FATO...


Nota 6: Achei uma opção de PAINEL gratuito na internet. Aqui está:
https://github.com/skoruba/IdentityServer4.Admin

Achei show demais. Projetinho porreta, vou salvar numa pasta chamada "SkorubaIdentityServer4Admin" o fonte
dele de hoje 10/07/2019. Mas parece que está sendo bem atualizado, ficar de olho...

Nota: Você pode SIM usar o Identity Server na mão, colocando todas configurações em memória, ou mesmo no
arquivo JSON. Mas você também pode criar um painel, usar um painel, enfim, fazer diversas outras coisas...
por tanto você pode iniciar direto, e depois ir melhorando, criando painel, e por ai vai...

------------------------------------------------------------------------------------------------------------

Nota de Pesquisa:
Eu não preciso necessariamente de um aplicativo MVC ou UI para obter um Token. Eu posso fazer isso de Server
pra Server, usando um fluxo chamado "ClientCredencialFlows" (chamada Server to Server) e ai usar um endpoint
\Token pra obter um Token.


