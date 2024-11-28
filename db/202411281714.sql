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