CREATE  DATABASE BookDb


USE BookDb


CREATE TABLE Books(
[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] NVARCHAR(40) NOT NULL,
[Authorname] NVARCHAR(40) NOT NULL,
[Price] MONEY NOT NULL
)


INSERT Books([Name],[Authorname],[Price])
VALUES 
('Essek ne bilir zeferan nedi','Zelimxan Yaqub',5.60),
('Sql','Elvin Camalzade',11.40),
('C#','Kamran Aliyev',9.70),
('Qadinlar marsdan kisiler veneradan','Zireddin Gulumcanli',8.90),
('Dont worry','John Gray',3.50),
('Cukur','Idris Kocovali',5.80),
('The War','Susana Dabera',6.80)


CREATE PROCEDURE SP_InsertBook
@Name AS NVARCHAR(40),
@Authorname AS NVARCHAR(40),
@Price AS MONEY
AS
BEGIN
INSERT INTO Books([Name],[Authorname],[Price])
VALUES (@Name,@Authorname,@Price)
END


EXEC SPInsertBooks 'Salam','Salam',4.68


CREATE PROCEDURE SP_DeleteBook
@Id AS INT 
AS
BEGIN
DELETE Books
WHERE Books.Id=@Id
END



CREATE PROCEDURE SP_UpdateBook
@Id AS INT,
@Name AS NVARCHAR(40),
@Authorname AS NVARCHAR(40),
@Price AS MONEY
AS
BEGIN 
UPDATE Books
SET Books.[Name]=@Name,Books.Authorname=@Authorname,Books.Price=@Price
WHERE Books.Id=@Id

END


EXEC SP_UpdateBook 7,'Sagol','Rusik',6.77


select*from Books




select*From Books





