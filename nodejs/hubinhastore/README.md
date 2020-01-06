NodeJS with Typescript API to Petshop - Agenda with CQRS, CRUD, Chat, Real Time, Reports, GraphQL,
WebSockets, Microservices, CD + Dual Database Object/Relational (MongoDB + MySQL) (Balta 7180).

Note: This app is under Dev 032019, so not all things are ready and maybe some problems could be happen.
When it's done i will make the Swagger with all DOC to see how it works.
Note 2: This samples if for my studies, models and notes, so it have a lot of comments and docs on classes.
------------------------------------------------------------------------------------------------------------

To run this App do a:
npm install (to restore packages)
npm run start:dev (to run the API)
http://localhost:3000/v1/customers 
(You can send a post passing on Body the JSON on the Folder Doc > ScriptsTest > PostACustomer)

The field "name", "document", "email" are validated by contract, so if you pass empty "" they will give
the return validations on API. Look for the prints on DOC folder).

------------------------------------------------------------------------------------------------------------

Objectives:
Structure of an API with Node/TS
Pattern and Optimize API
Use CQRS and advanced tecnhiniquies with Node/TS
Queries inside the API using GraphQL
RealTime communication with WebSockets
Concepts about Microservices
Publish the API in cloud (CD)

------------------------------------------------------------------------------------------------------------

First Step: NestJS is a Framework for Build efficient scalable NodeJS with modern JS, build with Typescript.
npm install -g @nestjs/cli

To create a project: nest new nameofProject (hubinhapetstore)
This makes a Pre-Set configured Project from where i will start develop my APP.

If i don't use NestJs, i will need to create anything with Gulp, Grunt or Webpack or some pack manager to
do it. NestJS use webpack behind the wall and makes anything ready for us. More than that: NestJS works 
easy with WebSockets, GraphQL, Microservices and etc. And the CLI give as commands to generate auto-items
like Services, Controllers and other useful commands.

------------------------------------------------------------------------------------------------------------

Then i Create my First Module: nest generate module backoffice and my first controller customer.

To Run the Application, i will use: npm run start:dev .
Note that "Start:dev" is inside package.json, and it uses nodemon. Nodemon will "monitore" my app folder
and if a file changes, nodemon will restart the server automatic. In package.json i have a lot of other
kinds of configurations and servers and etc. But, i will keep a "terminal" opened with nodemon, anyway.

------------------------------------------------------------------------------------------------------------

Design by Contract (Contracts): Is a concept to Validate Fields.
To do That i create a Folder "Contracts" and then put all the Code There. The Contract is just a interface
and it define what my Classes that will implement should have. So this class is like my "FluentValidator"
in C#. It will have an "Errors" (Array) and a Method Validate that receive a Model "N" (some model) and
will do all validations. This class is "Flunt" on Utils.

After that: i've created a "Customer Contract" (on Contract Folder).


Dependency Injection in TypeScript/Node:
(1) First i make my class injectable (done it on customer.contract.ts)
(2) Then in src/interceptors i've create validator.interceptor.ts (that need to be injetable too).
(3) On the WebApi (Controller) i have on Post() the Use of the Interceptor passing the contract. In the
case of validation error, the errors will be gave on JSON returns. Look for the Image 2 on Doc.

------------------------------------------------------------------------------------------------------------

Schemas:
My Model folders are not the same that will be inserted on DataBase. Because on DataBase i will have the
DataOfInsertion, Update and other fields like ID that're not on my Model, For that i will create schemas
on my backOffice folder that will represent my DataBase. First install Mongoose:

npm install --save @nestjs/mongoose mongoose
This will install 2 packages: Mongoose (the driver) for NodeJS works with Mongo, and then the Package for
the Nest work with Mongo too.

Then the "MAP" (yes, like in EF) in the Schema folder. Note that the Mongoose will create an ID automatic
and it`s based on GUID (yes, same guid like in C#) - Global Unique Identifier.

------------------------------------------------------------------------------------------------------------

DTOs - Data Transfer Object:
Let`s suppose that i have in my REGISTRATION FORM the email and 2 fields for Password - Pass and Pass
Confirmation (2 times). That will be sended to my API on body, and i don`t want that, because my model
will save PASS just once. So for this cases i will create DTOs.

In cases where my Model contains logs datas (like CreatedData, UserCreated, ID) that should be very useful.
And (3) reason: If i should create a USER for example, before create the Customer, i can have a DTO for both
Operation, even if my Customer and Users are differente Model. Because of that i don't have a Model in my
Controller (API) but the DTO.

In cases where to create I will use DTO just to Create my Customer, nothing else.

------------------------------------------------------------------------------------------------------------

MongoDB:

(1) I`ve imported Mongoose on app.module.ts (don`t forget to install the npm package)
(2) On BackOffice.Module i`ve imported my Schemas for Database.

Note about the Structure:
Read the image 10 to understand about the systema structure.

Note: Customer can have just one Address Billing and One Shipping Billing, and ONE User. But in the case of
pets, customer can have 2 dogs and one cat. Because of that, it's an N and if you check the Schema, you'll
see there's an ARRAY of pets [] inside customer. So because of that my methods on service will have the
"push" instead of "set". The Method will receite data as Pet (the Model) because i don't need a DTO, it
will transfer the PET and i don't need anymore field or hide some.

------------------------------------------------------------------------------------------------------------