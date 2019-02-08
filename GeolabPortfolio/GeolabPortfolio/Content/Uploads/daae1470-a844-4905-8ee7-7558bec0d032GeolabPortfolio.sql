CREATE DATABASE GeolabPortfolio;

use [GeolabPortfolio];

CREATE TABLE Users(
	Id int primary key identity(1,1),
	[Email] nvarchar(100) not null,
	[Password] nvarchar(100) not null
)


CREATE TABLE Authors(
	Id int primary key identity(1,1),
	FirstName nvarchar(100) not null,
	LastName nvarchar(100) not null,
	[Email] nvarchar(100) not null,
	[Image] ntext not null,
	[BehanceLink] ntext,
	[DribbleLink] ntext,
	[LinkedinLink] ntext,
	[GithubLink] ntext
)
INSERT INTO Authors(FirstName,LastName,[Image]) 
VALUES(N'სალომე',N'ყურაშვილი','salome.jpb');

select * from Authors


CREATE TABLE Tags(
	Id int primary key identity(1,1),
	[Name] nvarchar(max) not null
)

INSERT INTO Tags(Name)VALUES(N'გრაფიკული დიზაინი'),(N'ვებ დიზაინი'),(N'ფონტი');

select * FROM Tags

CREATE TABLE Projects(
	Id int primary key identity(1,1),
	AuthorId int foreign key references Authors(Id),
	Name nvarchar(100) not null,
	Published datetime not null,
	[Description] ntext not null
)

truncate table Projects

INSERT INTO Projects(AuthorId,Name,Published,[description])VALUES
(1,N'პროექტის სახელი','2018-06-18',N'აღწერა'),
(1,N'პროექტის სახელი 1','2018-06-18 ',N'აღწერა 3')

select * from Projects;
select * From Tags;

CREATE TAble ProjectTags(
	Id int primary key identity(1,1),
	ProjectId int foreign key references Projects(Id),
	TagId int foreign key references Tags(Id)
)

INSERT INTO ProjectTags(ProjectId,TagId) VALUES(1,1),(1,3),(2,2)

select * FROM ProjectTags;

CREATE TABLE ProjectImages(
	Id int primary key identity(1,1),
	ProjectId int foreign key references Projects(Id),
	ImageUrl ntext not null,
	IsMain int not null
)


INSERT INTO Users([Email],[Password]) VALUES ('sandro.mirianashvili@geolab.edu.ge','F7C3BC1D808E04732ADF679965CCC34CA7AE3441')

SELECT * FROM ProjectImages;

INSERT INTO ProjectImages(ProjectId,ImageUrl) VALUES(1,'image1.jpg'),(1,'image2.jpg'),(2,'image3.jpg'),(2,'image4.jpg');

/* bak file */

BACKUP DATABASE GeolabPortfolio TO DISK='D:\GeolabPortfolio.bak'

select * from Users;

select * from Authors;
SELECT * FROM Projects
SELECT * FROM ProjectTags

truncate table Projects