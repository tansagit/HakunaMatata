create database HakunaMatata

use HakunaMatata

create table REAL_ESTATE_TYPE(
	Id int identity(1,1) not null primary key,
	RealEstateTypeName nvarchar(100) not null
)

create table CITY(
	Id int identity(1,1) not null primary key,
	CityName nvarchar(100) not null
)

create table DISTRICT(
	Id int identity(1,1) not null primary key,
	DistrictName nvarchar(100) not null,
	CityId int,
	Foreign key (CityId) references CITY(Id)
)

create table WARD(
	Id int identity(1,1) not null primary key,
	WardName nvarchar(100) not null,
	DistrictId int
	Foreign key (DistrictId) references DISTRICT(Id)
)

create table LEVEL(
	Id int identity(1,1) not null primary key,
	LevelName nvarchar(100) not null
)

create table AGENT(
	Id int identity(1,1) not null primary key,
	AgentName nvarchar(100),
	PhoneNumber varchar(12),
	Email varchar(200),
	Facebook nvarchar(100),
	Zalo nvarchar(100),
	LoginName varchar(50),
	[Password] varchar(300),
	ActiveKey varchar(300),
	ResetPasswordKey varchar(300),
	LastLogin datetime,
	IsActive bit not null,	
	LevelId int not null,
	Foreign key (LevelId) references LEVEL(Id)
)


create table REAL_ESTATE(
	Id int identity(1,1) not null primary key,		
	PostTime datetime not null,
	LastUpdate datetime,
	ExprireTime datetime,
	ReaEstateTypeId int Foreign key references REAL_ESTATE_TYPE(Id),
	AgentId int Foreign key references AGENT(Id),
	IsActive bit not null,
)

create table REAL_ESTATE_DETAIL(
	Id int identity(1,1) not null primary key,
	RealEstateId int unique foreign key references REAL_ESTATE(Id),
	Title nvarchar(300),
	Price decimal not null,
	Acreage int not null,
	RoomNumber int not null,
	[Description] ntext,
	HasPrivateWC bit not null,
	HasMezzanine bit not null,
	AllowCook bit not null,
	FreeTime bit not null,
	WaterPrice int,
	ElectronicPrice int,
	WifiPrice decimal	
)
drop table REAL_ESTATE_DETAIL
create table MAP(
	Id int identity(1,1) not null primary key,
	[Address] nvarchar(300) not null,
	Latitude decimal(9,6),
	Longtitude decimal (9,6),
	WardId int FOREIGN KEY REFERENCES WARD(Id),
	DistrictId int FOREIGN KEY REFERENCES DISTRICT(Id),
	CityId int FOREIGN KEY REFERENCES CITY(Id),
	RealEstateId int UNIQUE FOREIGN KEY REFERENCES REAL_ESTATE(Id)
)

create table PICTURE(
	Id int identity(1,1) not null primary key,
	PictureName varchar(100),
	RealEstateId int,
	URL varchar(300) not null,
	IsActive bit not null,
	Foreign key (RealEstateId) references REAL_ESTATE(Id),
)

create table RATING(
	Id int identity(1,1) not null primary key, 
	Rate int not null,
	RealEstateId int not null,
	Foreign key (RealEstateId) references REAL_ESTATE(Id),
)

drop table RATING
create table SOCIAL_LOGIN(
	ProviderKey nvarchar(300) primary key,
	UserId int not null,
	Provider varchar(100),
	Foreign key (UserId) references Agent(Id),
)

create table FAQ(
	Id int identity(1,1) not null primary key, 
	Question ntext,
	Answer ntext
)

create table POLICY(
	Id int identity(1,1) not null primary key,
	PolicyContent ntext
)

create table ABOUT_US(
	Id int identity(1,1) not null primary key,
	Content ntext
)


