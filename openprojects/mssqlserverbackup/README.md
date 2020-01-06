C# WinForms APP to Monitor and Automatic-Backup SQL Server DataBase.
------------------------------------------------------------------------------------------------------------
You can install this application on computer with a MS-SQL Server instance, and program it to make automatic
backups of your database. Exemple: If you have a Software Developed with MS-SQL Server, you can install it 
on your clients to make the Data-Backup of your Software. Just inform the DataBase config (Server name, 
instance, name of Database) in database.xml (root of this program) and let it works.

This program will not use the "Query" Backup of SQL and .bkp files, BUT copy all Database files (.mdf and
.lfd(log)) to a folder. Then, will "ZIP" the both files to compress the final file.

You can configure a Pendrive or Removible Device (Like an External Hard Disk) to copy the final ".zip" backup
to a second disk, and make it more secure. You can program this Tool to Work on a specific time (like 2am)
when the server is not in use. The tool will stop the MS-SQL Service, copy the files, start the Service Again
(to up-again the Server) and do the backup-work.

It have options to "Automatic Backup" on specific time (like 02am, 04am, 06am, 18pm), days of backup (you 
can program for example, just one back per week, or every day), you have options to delete old backups (you
can inform the backuper, how many backups it should keep - just the last 6 for example), and you have options
to delete old files from the removable devices (to keep the PenDrive with enought space). You can also
program the Tool to ShutDown the machine after the work.

It also have some tools to connect, execute querys, get information about the Server and Open MS-SQL to
Network (open Windows Firewall). This Software was Tested on SQL-Server 2005-2014 and works good. The Source
Code is in Portuguese, but, i insert an option to translate all labels to ENGLISH (so the program will 
appears to user in ENG). It's a simple translate for a small program and little number of labels, so i don't
create a class or xml, json, but just translate labels direct on code.

Note: The program need 4 DLLS. They're included in the "Library" folder, "ionic.zip" and 
"WindowsFirewallHelper" can be found on Nuget. The other two was developed by me (CSOBRF_Criptografia and
CSOBRF_Validacoes). If you want to see the source, they're open in my repo. This program will compile/build
on C:\MsSqlServerBackup . This program can be "adapted" to backup other type of DataBases, or even other type
of files and etc. Don't forget to run always as 'administrator' because this program needs access to Windows
Services.

Note: This Software was developed by me too many years ago, a tool for backup a C# ERP WinForms application.
Then i decide to open the source and just adapt it to be an opensource. This source has too many years, so
maybe all the currently design patters are not nowdays/updated.

To run this program: Create a folder c:\MsSqlServerBackup and change the Output folder to it. Put inside the
folder, the 3 files in "Library" path on this solution. Configure your database in "database.xml". Run it,
configure on screen and enjoy. If your project don't compile because WindowsFirewallHelper, re-add the
Reference using the Library folder (dll is there).
________________________________________
C# Windows forms - aplicativo para monitorar e efetuar backups automáticos do SQL Server.
Você pode instalar esse aplicativo em um computador com uma instância do SQL Server, e programar ele para
fazer backups automáticos do seu banco. Exemplo: Se você tem um Software feito com SQL Server, você pode
instalar esse programa nos seus clientes para fazer backup do banco de dados do seu software. Apenas informe
a configuração do banco (Nome Servidor, instância, nome banco) no database.xml (na raiz desse programa) e
deixe que ele trabalhe. 

Esse programa não irá usar o “Query” backup do SQL e arquivos .bkp, mas copiar os arquivos do banco (.mdf e
.lfd(log)) para uma pasta. Então irá “zipar” e comprimir o arquivo final.

Você pode configurar um Pendrive ou Disco Removível (como um disco externo) para copiar o ".zip" final 
para um segundo disco, e tornar o backup mais seguro. Você pode programar essa ferramenta trabalhar em 
uma hora específica (como 2 da manhã) quando o servidor não estiver em uso. Essa ferramenta irá parar o 
serviço do SQL Server, copiar os arquivos, iniciar o Serviço Novamente (para liberar o servidor) e fazer 
o trabalho.

Tem opções de "Backup Automático" em alguma hora específica (como 02, 04, 06, 18h), dias do backup (você
pode programar backup só uma vez por semana, ou todo dia), você tem opções para deletar backups antigos
(você pode informar quantos backups deve manter, apenas os últimos 6 por exemplo), e tem opções para 
deletar arquivos antigos do disco removível (para manter o pendrive com espaço suficiente). Você também
pode programar a ferramenta para desligar a máquina após o trabalho.

Também existem algumas opções para conectar, executar querys, pegar informações sobre o servidor, abrir 
a Rede (liberar o Firewall). Esse Software foi testado sobre SQL-Server 2005-2014 e funcionou bem. O 
código fonte está em português, mas, eu inseri opções para traduzir todas labels para Inglês (então o
programa ficará em inglês). É uma tradução simples para um pequeno programa e algumas labels, então não
criei classes ou xml, json, mas apenas traduzi as lables direto no código.

Nota: O programa precisa de 3 DLLS. Elas estão incluidas na pasta "Library", "ionic.zip" e 
"WindowsFirewallHelper" podem ser encontrados no nuget. As outras duas foram desenvolvidas por mim
(CSOBRF_Criptografia and CSOBRF_Validacoes). Você pode olhar o fonte, elas são abertas no meu repositório.
Esse programa irá compilar em C:\MsSqlServerBackup . Esse programa pode ser "adaptado" para fazer backup
de outros tipos de banco de dados, ou mesmo outro tipos de arquivos e etc. Não esqueça de rodar como
'administrador' sempre, pois esse programa precisa de acesso a Serviços do Windows.

Nota: Esse software foi desenvolvido por mim a muitos anos atrás, uma ferramenta pra backup de um ERP
Windows Forms C#. Então eu decidi abrir o fonte e apenas adaptar pra opensource. Esse código tem 
muitos anos, então talvez não esteja de acordo com os design patterns atuais.

Para rodar esse programa: Crie uma pasta c:\MsSqlServerBackup (onde será compilado) e mude o "Output"
para essa pasta. Coloque dentro da pasta os 3 arquivos na pasta "Library" nessa solução. Configure seu
banco de dados no arquivo "database.xml". Rode, configure na tela e aproveite. Se seu projeto não rodar
por causa da referência WindowsFirewallHelper, re-adicione a Referência a dll usando a pasta Library
(a dll está lá).
