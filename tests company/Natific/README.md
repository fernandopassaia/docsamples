Natific Task Made by Fernando Passaia:

How to **RUN:**<br />
(1) Right click on Natific Solution and select "Restore Nuget Package".<br />
(2) Right click on Solution and REBUILD Solution.<br />
(3) Right click on Solution Natific.Api and "Set as StartUp Project".<br />
(4) Now Open "Natific.Api > Web.Config" and check SQL Server Connection String.<br />
(5) Open Package Manager Console, project "Infra\Natific.Infra" and run on console:<br />
Update-DataBase -StartUpProjectName "Natific.Api" -ProjectName "Natific.Infra"<br />
<br />
Run API Project and Access Swagger: http://localhost:61380/swagger/ <br />
Run Natific.UI Project and use it: http://localhost:59841/ <br />

**Note:** This APP is Deployed on a TEMP-Server with some Test-Data made by me to Check It Online:<br />
API: http://www.futuradata.com.br/natificapi/swagger <br />
UI: www.futuradata.com.br/natificui <br />



The **SECOND PART** of the Exercise (Read the MNB WebService and Get Current Exchange) is separeted on
the Folder "src > Part2_HufToEur". Just check this APP.

Check **"Doc"** folder for some Print-screens of Api Working and other Stuffs. Note: For "Price" and
"Weight" use the Format 1,000 ~ 999,999. (HUF Format and Weight with 3 piles acceptance).



