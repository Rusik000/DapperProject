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






