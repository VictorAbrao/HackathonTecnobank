CREATE DATABASE Hackathon;

use dbo;

create table Detran
(
	Id int not null identity primary key,
	Codigo int not null,
	Nome varchar(100) not null
)

create table Publications
(
	Id int not null identity primary key,
	Detran int not null,
	LastReadPublications datetime null,
	CreatedAt datetime not null,
	UpdatedAt datetime null,
)


insert into Detran (Codigo,Nome) values (1,'SP');
insert into Detran  (Codigo,Nome) values (2,'MS');


create table Keywords
(
	Id int not null identity primary key,
	WordParentId int null,
	Detran int not null,
	Word varchar(100) not null,	
	SubWords varchar(max) null,	
	CreatedAt datetime not null,
	UpdatedAt datetime null,
)


create table Concierge
(
	Id int not null identity primary key,
	ExternalId varchar(255) not null,
	ExternalLink varchar(255) null,
	Detran int not null,
	Title varchar(255) not null,
	Status int not null,	
	[Body] varchar(max) not null,
	[Document] varchar(max) null,
	[Date] datetime not null,
	CreatedAt datetime not null,
	UpdatedAt datetime null
)
