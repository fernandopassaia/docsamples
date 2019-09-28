Modern WebApp FullStack - .NET Core Backend + Angular UI Web + Ionic Mobile APP. 
**Note**: This is developed/tested using .NET Core Runtime 2.2.4 / SDK 2.2.106 .

How to RUN this APP:<br />
(1) Right-Click on Solution and then "Restore Nuget Packages". Then "Rebuild Solution".<br />
(2) Check your SQL Server connection string at "ModernStore.Api > appsettings.json".<br />
(3) Set "ModernStore.Api" as Startup, open Package-Manager Console and select ModernStore.Api:<br />
(4) update-database (this will create the database based on migration)<br />
(5) Run the Api. Swagger will be available for Tests: /swagger<br />

On the "doc" folder there's some prints of the Tests made on PostMan!
------------------------------------------------------------------------------------------------------------

Note 30/04/2019 - This is UnderDev - not ready! Check out "SampleStudies>Company Tests" repo instead. Over
There you can check .Net Core Project (FutureMedia) or Classic Framework Project (Natfic).

------------------------------------------------------------------------------------------------------------
Note: Document of Customer is a "CPF" - the Brazilian Registration Card. It requires a Validation method to
ensure that the number is valid. There's a site who generate randoms VALID numbers - use it:
https://www.4devs.com.br/gerador_de_cpf (click "Gerar CPF" and just copy the random code).

Anyway, i'm a cool guy, so i will leave some samples for you to use it here:
25439617043 99139856097 83213537039 91869992067 61497144035 41968102094 04073835068 12474615059
57319857073 09765646020 48370753043 06713272088 57446920025 67103757062 52931810096 97247431016
41941308040 45402822042 62332179038 44761927003 22404619004 46571863022 48277438001 75318076025
98344239001 78007724036 38650498000 38803077090 36219496078 29330133045 30022632069 42384098098

Note: If you do a POST with an invalid (or already in use) Document, the data will not be saved to database.
Instead of that - i will get back a JSON with Validation info. Try it, that's cool.
------------------------------------------------------------------------------------------------------------

The Data-Access was Developed using EntityFramework Core + Dapper.
Dapper to Get Data (Selects) because it's more performatic.

------------------------------------------------------------------------------------------------------------

Autenthication: oAuth. Order of execution:

App => Request of Token.
Each Request => Send Token => API.
API Validate the Token => Return Result.

JWT => Json Web Token

There's to way of autenticate: (1) Roles (2) Policy and Claim.

To install: Microsoft.AspNetCore.Authentication.JwtBearer
(Note: If you want to build a Distributated-API and this API will generate auth to a lot of Systems, you can
use something like Think Tank Identity Server).

Note: I've updated my Code to .NET Core 2.2 (is different) but it's already there.

------------------------------------------------------------------------------------------------------------

FRONT END Part:

Angular Commands:

ng new testeApp (create the app)
ng generate component Teste (create a component)
ng generate service Teste (creates a service)
ng generate directive Teste (create a directive)
ng serve (run the app and rebuild when change something)
ng build (compile a "DIST" directory and leaves ready for deploy - to send to server)
ng test (run tests and open karma IDE)

Order:
ng generate component Headbar