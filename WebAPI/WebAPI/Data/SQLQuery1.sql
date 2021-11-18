DROP TABLE OrderLines
DROP TABLE Orders
DROP TABLE Products
DROP TABLE SubCategories
DROP TABLE Users
DROP TABLE Categories
DROP TABLE UserAdresses

GO

CREATE TABLE UserAdresses (
	Id int not null identity primary key,
	Adress nvarchar(100) not null,
	Zip char(7) not null,
	City nvarchar(50) not null
)

CREATE TABLE Categories (
	Id int not null identity primary key,
	Name nvarchar(50) not null
)

GO

CREATE TABLE Users (
	Id int not null identity primary key,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	Email varchar(100) not null unique,
	Password nvarchar(max) not null,
	Admin bit default 0,
	UserAdressesId int not null references UserAdresses(Id)
)

CREATE TABLE SubCategories (
	Id int not null identity primary key,
	Name nvarchar(50) not null,
	CategoriesId int not null references Categories(Id)
)

GO

CREATE TABLE Products (
	Id int not null identity primary key,
	Name nvarchar(100) not null,
	ShortDescription nvarchar(250) not null,
	LongDescription nvarchar(max),
	Price money not null,
	SubCategoriesId int not null references SubCategories(Id)
)

CREATE TABLE Orders (
	Id int not null identity primary key,
	UsersId int not null references Users(Id),
	OrderDate datetime not null,
	Status nvarchar(50) not null default 'Received',
	UserAdressesId int not null references UserAdresses(Id),
	TotalAmount money not null
)

GO

CREATE TABLE OrderLines (
	Id int not null identity primary key,
	OrdersId int not null references Orders(Id),
	ProductsId int not null references Products(Id),
	Quantity int not null default 1,
	UnitPrice money not null
)

GO