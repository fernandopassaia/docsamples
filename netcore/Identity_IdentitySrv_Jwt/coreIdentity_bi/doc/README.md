# identity
.Net Core+Identity (Customization, External Logins Facebook, Twitter, Linkedin, Google, MS) with EF/Dapper
------------------------------------------------------------------------------------------------------------

To Create and Run the App using Visual Studio Code + Terminal.

dotnet new sln -n OverViewIdentity -OverViewIdentity (second parameter is a folder)
cd OverViewIdentity
dotnet new mvc -n Id.Overview.Mvc -f netcoreapp2.0 -au Individual (au is identity, and individual type)
dotnet sln add .\Id.Overview.Mvc\Id.Overview.Mvc.csproj (add the project to the solution)
dotnet restore (restore all nugets)
dotnet build (to build the app)

To RUN the First app:

On appsettings.json set the connection string.
On Startup.cs alter "UseSqllite" to "UseSqlServer".
dotnet ef migrations add FirstBase
dotnet ef database update
dotnet run

------------------------------------------------------------------------------------------------------------

In Visual Studio 2017:
Select New > Project > .Net Web Application (with .Net Core) > Mvc and change Autenthication to Individual.

Note: If you choose .Net Core Framework 2.0 all controllers and Views will be created. If you choose 2.1 or
higher, the Framework will "pack" the components and then you'll need to create the Scaffolding to see them
back. Take a look at "CustomizandoApp.jpg" to see how to do that.

------------------------------------------------------------------------------------------------------------

Note: It's possible to install IDENTITY in a existing project. See image "3 Installing existing APP".

After That, just create the migration and update the database:

add-migration InitialBase
script-migration (this is a GOOD new, you can see the script SQL).
update-database

------------------------------------------------------------------------------------------------------------