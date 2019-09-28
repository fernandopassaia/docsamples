CREATE VIEW GetCustomerInfoView AS
Select Name = Cus.FirstName + ' ' + Cus.LastName, Cus.DocumentNumber, Cus.EmailAddress,
Usr.Username, Usr.Password, Usr.Active from Customer Cus Inner Join
[User] Usr ON Cus.UserId = Usr.UserId
GO

INSERT INTO Product values(GetDate(),0,GetDate(),0,'Teclado Ryzer', '100', 'teclado.jpg', 50);
INSERT INTO Product values(GetDate(),0,GetDate(),0,'Mouse Ryzer', '30', 'mouse.jpg', 150);
INSERT INTO Product values(GetDate(),0,GetDate(),0,'Monitor 17', '20', 'monitor17.jpg', 450);
INSERT INTO Product values(GetDate(),0,GetDate(),0,'Monitor 22', '10', 'monitor22.jpg', 799);

INSERT INTO [User] values(GetDate(),0,GetDate(),0,'fernandopassaia', '@1234Fd', 1);
INSERT INTO [Customer] values(GetDate(),0,GetDate(),0,'Fernando', 'Passaia', GetDate(), 'fernandopassaia@futuradata.com.br', '32533832888', 1);