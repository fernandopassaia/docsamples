To RUN this Project:

(1) Right Click on Solution then 'Restore Nuget Package' and then Rebuild ALL.
(2) Set your MS-SQLServer connection string in Bim.Repository > App.Config AND Bim.WebApi > Web.Config .
(3) Set Bim.Repository as STARTUP Project.
(4) In Package-Manager Console select Bim.Repository and: Update-Database
(5) Right-Click on Bim.WebApi and Debug > Run
(6) Right-Click on Bim.WebUI and Debug > Run

I left some "sample images" to test on Sample Images folder.
That's it. Look for "improvements.png" to know what i will do with more time. See ya!

Note: I let this Project running on My WebServer, so here it is (for tests):
API: http://www.futuradata.com.br/BimWebApi/swagger
WebUI: http://www.futuradata.com.br/BimWebUI