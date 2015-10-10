﻿DROP TABLE Schedule_W;

CREATE TABLE Schedule_W
(
	Id						BIGINT			NOT NULL		PRIMARY KEY	IDENTITY,

	Year					INTEGER			NOT NULL,
	Month					INTEGER			NOT NULL,
	Day						INTEGER			NOT NULL,

	Time					NCHAR(5)		NULL,
	Play					NVARCHAR(1000)	NULL,
	Relay					NVARCHAR(1000)	NULL,
	BallPark				NCHAR(20)		NULL,
	Etc						NVARCHAR(1000)	NULL,
	InsertDateTime			DATETIME		DEFAULT			CURRENT_TIMESTAMP
);

DROP TABLE Schedule;
CREATE TABLE Schedule
(
	Id						BIGINT			NOT NULL		PRIMARY KEY	IDENTITY,

	Year					INTEGER			NOT NULL,
	Month					INTEGER			NOT NULL,
	Day						INTEGER			NOT NULL,

	Hour 					INTEGER			NULL,
	Minute 					INTEGER			NULL,

	BallPark				NVARCHAR(10)		NULL,

	HomeTeam				NVARCHAR(10)		NOT NULL,
	AwayTeam				NVARCHAR(10)		NOT NULL,

	HomeTeamScore			INTEGER			NULL,
	AwayTeamScore			INTEGER			NULL,

	Href					NVARCHAR(200)	NULL,
	GameId					NCHAR(13)		NULL,
	
	Etc						NVARCHAR(1000)	NULL,
	InsertDateTime			DATETIME		DEFAULT			CURRENT_TIMESTAMP
);


DROP TABLE Relay_W;
CREATE TABLE Relay_W
(
	Id						BIGINT			NOT NULL		PRIMARY KEY	IDENTITY,
	GameId					NCHAR(13)		NULL,
	Content					TEXT			NOT NULL,
	InsertDateTime			DATETIME		DEFAULT			CURRENT_TIMESTAMP
);

DROP TABLE Player_W;
CREATE TABLE Player_W
(
	Id						BIGINT			NOT NULL		PRIMARY KEY	IDENTITY,
	Team					NVARCHAR(10)	NOT NULL,
	Href					NVARCHAR(200)	NULL,
	InsertDateTime			DATETIME		DEFAULT			CURRENT_TIMESTAMP
);

DROP TABLE Player;
CREATE TABLE Player
(
	Id						BIGINT			NOT NULL		PRIMARY KEY	IDENTITY,
	PlayerId				INT			NOT NULL,
	Team					NVARCHAR(10)	NOT NULL,
	BackNumber				INT				NULL,
	Name					NVARCHAR(10)	NOT NULL,
	Height					INT				NOT NULL,
	Weight					INT				NOT NULL,
	Position				NVARCHAR(10)	NOT NULL,
	Hand					NVARCHAR(10)	NOT NULL,
	BirthDate				CHAR(8)			NOT NULL,
	Career					NVARCHAR(50)	NULL,
	Deposit					NVARCHAR(50)	NULL,
	Salary					NVARCHAR(20)	NULL,
	Rank					NVARCHAR(50)	NULL,
	JoinYear				NVARCHAR(20)	NULL,
	SCR						NVARCHAR(70)	NULL,
	InsertDateTime			DATETIME		DEFAULT			CURRENT_TIMESTAMP
);



