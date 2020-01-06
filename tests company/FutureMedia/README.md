Future Of Media Task.

**Note**: This is developed/tested using .NET Core Runtime 2.2.4 / SDK 2.2.106 .

How to RUN this APP:<br />
(1) Right-Click on Solution and then "Restore Nuget Packages". Then "Rebuild Solution".<br />
(2) Check your SQL Server connection string at "FutureOfMedia.Api > appsettings.json".<br />
(3) Set "FutureOfMedia.Api" as Startup, open Package-Manager Console and select FutureOfMedia.Api:<br />
(4) update-database (this will create the database based on migration)<br />
(5) Run the Api. Swagger will be available for Tests: /swagger<br />

On the "doc" folder there's some prints of the Tests made on PostMan!