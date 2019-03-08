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
	Published date not null,
	[Description] ntext not null
)

truncate table Projects

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

/* bak file */

BACKUP DATABASE GeolabPortfolio TO DISK='D:\GeolabPortfolio.bak'

select * from Users;

select * from Authors;
select * from Tags
SELECT * FROM Projects
SELECT * FROM ProjectTags
select * from ProjectImages

truncate table Tags
truncate table ProjectTags
truncate table ProjectImages

select p.Id,p.Name,ProjectImages.ImageUrl as [Image],a.FirstName + ' ' + a.LastName as AuthorFullName,p.Published from Projects as p
join ProjectImages on p.Id = ProjectImages.ProjectId
join Authors as a on a.Id = p.AuthorId
where ProjectImages.IsMain = 1

select * FROM ProjectImages
select * FROM Projects
select * FROM ProjectTags

go
create Procedure GetProjectList
as
	select p.Id,p.Name,ProjectImages.ImageUrl as [Image],a.FirstName,a.LastName,p.Published 
	from Projects as p
	join ProjectImages on p.Id = ProjectImages.ProjectId
	join Authors as a on a.Id = p.AuthorId
	where ProjectImages.IsMain = 1
go

exec GetProjectList

select distinct(p.id),p.Name,p.Published,CAST(images.ImageUrl AS NVARCHAR(MAX)) as [Image],a.FirstName,a.LastName FROM Projects as p
join ProjectImages as images on p.id = images.ProjectId
join Authors as a on a.Id = p.AuthorId
join ProjectTags as t on t.ProjectId = p.Id
where images.IsMain = 1 and p.id in (30,31)

go
CREATE PROCEDURE FilterProjectByProjectIdAndTags
(
	@ids nvarchar(150),
	@tags nvarchar(150)
) 
AS   
	DECLARE @command nvarchar(max);
	set @command = 'select distinct(p.id),p.Name,p.Published,CAST(images.ImageUrl AS NVARCHAR(MAX)) as [Image],a.FirstName,a.LastName FROM Projects as p 
	join ProjectImages as images on p.id = images.ProjectId
	join Authors as a on a.Id = p.AuthorId
	join ProjectTags as t on t.ProjectId = p.Id
	where images.IsMain = 1 and p.Id in (' + @ids +') and t.TagId in (' + @tags + ')';
	execute (@command);
GO  

exec FilterProjectByTags '11,5'
exec FilterProjectByProjectId '31,30'
exec FilterProjectByProjectIdAndTags '12','2'
exec GetProjectList



select * FROM ProjectTags