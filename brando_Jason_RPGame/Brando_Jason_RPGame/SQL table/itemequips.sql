USE master;
GO

IF EXISTS(select * from sys.databases where name='RPG')
DROP DATABASE RPG;
GO

CREATE DATABASE RPG;
GO

USE RPG
GO

begin TRANSACTION

CREATE TABLE Usuable_Items
(
	item_id			int				identity(1, 1),
	name			varchar(20)		not null,
	price			int				not null,
	isPerm			bit				not null,
)	

CREATE TABLE Equipable_Items
(
	equip_id		int				identity(1, 1),
	name			varchar(20)		not null,
	price			int				not null,
	slotLocation	char(1)			not null,
	constraint pk_equip primary key (equip_id)
)	


CREATE TABLE Stat_Values
(
	stat_value_id	int				identity(1, 1),
	hp				int				not null,
	mp				int				not null,
	atk				int				not null,
	def				int				not null,
	matk			int				not null,
	mdef			int				not null,
	stam			int				not null,
	luck			int				not null,
	constraint pk_Stat_Values primary key (stat_value_id)
)	

insert into Stat_Values
values (10, 0, 0, 0, 0, 0, 0, 0)

CREATE TABLE Effects
(
	effects_id		int				identity(1, 1),
	name			varchar(20)		not null,
	stat_value_id	int				not null,
	constraint pk_effects primary key (effects_id),
	constraint fk_stat_value_effects foreign key (stat_value_id) references Stat_Values(stat_value_id)
)	

CREATE TABLE equip_effects
(
	equip_id		int				NOT NULL,
	effects_id		int				NOT NULL,

	constraint fk_equip_ee foreign key (equip_id) references Equipable_Items(equip_id),
	constraint fk_effect_ee foreign key (effects_id) references Effects(effects_id)

)

insert into Effects (name, stat_value_id)
values ('potion', 1)
	
CREATE TABLE Monsters
(
	monster_id		int			identity(1, 1),
	hp				int			not null,
	mp				int			not null,
	atk				int			not null,
	def				int			not null,
	matk			int			not null,
	mdef			int			not null,
	stam			int			not null,
	luck			int			not null,
	lvl				int			not null,
	[exp]           int			not null,
	gold			int			not null,
	name			varchar(50) not null,
	constraint pk_Monster primary key (monster_id),
	
)

create table Abilities
(
	ability_id		int				identity(1, 1),
	name			varchar(50)		not null,
	monster_id		int				not null,
	constraint pk_Abilities primary key (ability_id),
	constraint fk_Monster foreign key (monster_id) references Monsters(monster_id)
)
insert into Monsters
values (3, 0, 1, 0, 0, 0, 3, 1, 1, 1, 5, 'Slime')

insert into Abilities
values ('Wiggle', 1)

INSERT INTO Equipable_Items
VALUES ('Iron Sword', 10, 'W');

insert into Usuable_Items (name, price, isPerm)
values ('potion', 10, 0)

commit TRANSACTION

