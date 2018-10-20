USE master;
GO

IF EXISTS(select * from sys.databases where name='RPG')
DROP DATABASE RPG;
GO

CREATE DATABASE RPG;
GO

USE RPG
GO

CREATE TABLE Items
(
	item_id			int				identity(1, 1),
	name			varchar(20)		


	
