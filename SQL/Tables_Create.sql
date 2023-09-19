IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Users]'))
BEGIN
	CREATE TABLE Users (
		userId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		firstName VARCHAR(30) NOT NULL,
		lastName VARCHAR (30) NOT NULL,
		CNIC VARCHAR(20) UNIQUE NOT NULL,
		phoneNo VARCHAR(20) NOT NULL,
		userMail VARCHAR (100) NOT NULL,
		password VARCHAR(150) NOT NULL,
		isAdmin BIT NOT NULL,
		isActive BIT NOT NULL,
		userCreateTime DATETIME DEFAULT Current_timestamp,
		updatetime DATETIME DEFAULT Current_timestamp,
		createdby int,
		updatedby int)
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Floors]'))
BEGIN
	CREATE TABLE Floors (
		floorId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		floorTitle VARCHAR(30) NOT NULL,
		isActive BIT NOT NULL,
		updatetime DATETIME DEFAULT Current_timestamp,
		createtime DATETIME DEFAULT Current_timestamp,
		createdby int,
		updatedby int)
END


IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[UserToFloor]'))
BEGIN
	CREATE TABLE UserToFloor (
		userId int NOT NULL,
		floorId int NOT NULL,
		FOREIGN KEY	(userId) REFERENCES Users(userId),
		FOREIGN KEY	(floorId) REFERENCES Floors(floorId),
		floorAssignTime DATETIME DEFAULT Current_timestamp
		)
END


IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Tables]'))
BEGIN
	CREATE TABLE [Tables] (
		tableId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		floorId INT NOT NULL
		FOREIGN KEY	(floorId) REFERENCES Floors(floorId),
		isOccupied BIT NOT NULL,
		isActive BIT NOT NULL,
		updatetime DATETIME DEFAULT Current_timestamp,
		createtime DATETIME DEFAULT Current_timestamp,
		createdby int,
		updatedby int)
END


IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Categories]'))
BEGIN
	CREATE TABLE Categories (
		catId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		catName VARCHAR(30) NOT NULL,
		isActive BIT NOT NULL,
		updatetime DATETIME DEFAULT Current_timestamp,
		createtime DATETIME DEFAULT Current_timestamp,
		createdby int,
		updatedby int)
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Subcategories]'))
BEGIN
	CREATE TABLE Subcategories (
		subcatId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		catId INT NOT NULL
		FOREIGN KEY	(catId) REFERENCES Categories(catId),
		subcatName VARCHAR(30),
		isActive BIT NOT NULL,
		updatetime DATETIME DEFAULT Current_timestamp,
		createtime DATETIME DEFAULT Current_timestamp,
		createdby int,
		updatedby int
		)
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Items]'))
BEGIN
	CREATE TABLE Items (
		itemId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		subcatId INT,
		FOREIGN KEY	(subcatId) REFERENCES Subcategories(subcatId),
		catId INT,
		FOREIGN KEY	(catId) REFERENCES Categories(catId),
		itemName VARCHAR(30) NOT NULL,
		itemPrice INT NOT NULL,
		isGstAdded BIT NOT NULL,
		afterGST INT NOT NULL,
		stockQuantity INT NOT NULL,
		isActive BIT NOT NULL,
		updatetime DATETIME DEFAULT Current_timestamp,
		createtime DATETIME DEFAULT Current_timestamp,
		createdby int,
		updatedby int)
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Orders]'))
BEGIN
	CREATE TABLE Orders (
		orderId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		tableId INT NOT NULL,
		FOREIGN KEY (tableId) REFERENCES Tables(tableId),
		costumerMail VARCHAR(100) NOT NULL,
		totalBill INT NOT NULL,
		isDiscounted BIT NOT NULL,
		discountAmount INT,
		userId INT NOT NULL,
		FOREIGN KEY (userId) REFERENCES Users(userId),
		isCompleted BIT NOT NULL,
		orderDate DATETIME default current_timestamp,
		ordercompletetime DATETIME default current_timestamp
		)
END


IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[ItemsToOrders]'))
BEGIN
	CREATE TABLE ItemsToOrders (
		orderId INT  NOT NULL,
		itemId INT NOT NULL,
		FOREIGN KEY (orderId) REFERENCES Orders(orderId),
		FOREIGN KEY (itemId) REFERENCES Items(itemId),
		itemQuantity INT NOT NULL,
		isConfirmed BIT NOT NULL,
		confirmtime datetime default current_timestamp
		)
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[GST]'))

BEGIN
	CREATE TABLE GST(
	GST int NOT NULL,
	updatetime datetime default current_timestamp,
	updatedby int
	)
END