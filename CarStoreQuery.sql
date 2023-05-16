CREATE DATABASE TRMBcar
USE TRMBcar


CREATE TABLE EMPLOYEE(  
	ID INT, 
	Emp_Username VARCHAR(100) unique,
	Fname NVARCHAR(50),   
	Mname NVARCHAR(50),   
	Lname NVARCHAR(50),  
	pass varchar(30),
	SSN INT UNIQUE NOT NULL,   
	BirthDate DATE,   
	Gender CHAR(1),  
	Salary INT, 
	SuperID INT,
	UserType INT , -- maybe 1 is admin
	PRIMARY KEY(ID), 
	FOREIGN KEY (SuperID) REFERENCES EMPLOYEE(ID)
);
select * from CLIENT

CREATE TABLE BRANCH(
	BranchID INT,
	MgrID INT,
	Bname NVARCHAR(50),
	City NVARCHAR(50),
	Street NVARCHAR(100),
	BuildingNo NVARCHAR(20),
	PRIMARY KEY(BranchID),
	FOREIGN KEY(MgrID) REFERENCES EMPLOYEE(ID)
	ON DELETE SET NULL
	ON UPDATE CASCADE
);

CREATE TABLE PRODUCT(
	ProductID INT PRIMARY KEY ,
	CATEGORY VARCHAR(100) NOT NULL,
	BranchNo INT,
	Quantity INT NOT NULL,
	brand VARCHAR(100),
	Price INT NOT NULL,
	iimage varbinary(MAX),
	Statuss INT, --PRODUCT AVALABILE OR NOT
	FOREIGN KEY(BranchNo) REFERENCES BRANCH(BranchID)
	ON DELETE SET NULL
	ON UPDATE cascade --added

);


CREATE TABLE SUPPLIER(
	Supplier_Name VARCHAR(100),
	BranchNo int,
	Supply_Type VARCHAR(100),
	PRIMARY KEY (Supplier_Name,BranchNo, Supply_Type),
	FOREIGN KEY (BranchNo) REFERENCES BRANCH(BranchID)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE DEPARTMENT(
	DeparmentID INT ,
	Department_Name VARCHAR(100),
	BranchNo INT,
	PRIMARY KEY(DeparmentID, BranchNo),
	FOREIGN KEY (BranchNo) REFERENCES BRANCH(BranchID)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
);

CREATE TABLE DEPMGR(
	DepID INT,
	Mgr_ID INT,
	Branch_No INT,
	PRIMARY KEY(DepID, Branch_No),
	FOREIGN KEY (DepID, Branch_No) REFERENCES Department(DeparmentID, BranchNo)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
);


CREATE TABLE VEHICLE(
	Vehicle_No INT PRIMARY KEY IDENTITY(1,1),
	name varchar(30),
	Car_Status VARCHAR(20) NOT NULL,
	SHOWROOM VARCHAR(100),
	Brand VARCHAR(50) NOT NULL,
	CC_Rnage INT NOT NULL,
	Color VARCHAR(20) NOT NULL,
	Year_Model INT NOT NULL,
	Gearing VARCHAR(20) NOT NULL,
	Body_Style VARCHAR(20),
	Engine_Capacity int,
	hourse_power int,
	maximum_speed int,
	Warranty_years int,
	Warranty_Kilometers int,
	acceleration int,
	speeds int,
	Fuel varchar(20),
	Liter_per_100KM varchar(6),
	width int,
	height int,
	Trunk_Size int,
	Seats int,
	Traction_Type varchar(30),
	Cylinders int,
	Tank_Capacity int,
	iimage varbinary(max),
	image2 varbinary(max),
	image3 varbinary(max)
);
insert into VEHICLE('new', 'ezaby', )

CREATE TABLE NEW_VEHICLE(
	Vehicle_ID INT PRIMARY KEY,
	NUMBER_OF_CLASSES INT NOT NULL,
	Start_Price INT NOT NULL,
	End_Price INT NOT NULL
	CONSTRAINT NEW_VEHICLE_TO_VEHICLE FOREIGN KEY(Vehicle_ID) REFERENCES VEHICLE(Vehicle_No)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);

CREATE TABLE USED_VEHICLE(
	Vehicle_ID INT PRIMARY KEY,
	Kilometers INT NOT NULL,
	Price INT NOT NULL,
	Posting_Date DATE NOT NULL,
	Class INT NOT NULL,
	CONSTRAINT USED_VEHICLE_TO_VEHICLE FOREIGN KEY(Vehicle_ID) REFERENCES VEHICLE(Vehicle_No)
	ON UPDATE CASCADE
	ON DELETE CASCADE

);

CREATE TABLE CAR_CLASSES(
	Vehicle_ID INT,
	Class INT,
	Price INT NOT NULL,
	Deposit INT NOT NULL,
	Minimum_Installment INT
	PRIMARY KEY(Vehicle_ID,Class),
	CONSTRAINT NEW_VEHICLE_CLASS_TO_NEW_VEHICLE FOREIGN KEY(Vehicle_ID) REFERENCES NEW_VEHICLE(Vehicle_ID)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);

CREATE TABLE SHOWROOM(
	Show_Room_Name VARCHAR(100) PRIMARY KEY,
	City VARCHAR(50),
	STreet VARCHAR(100),
	phoneNumber varchar(20)
);


CREATE TABLE SERVICES_CENTER(
	ID INT Primary Key,
	Name VARCHAR(100) Unique NOT NULL,
	Address VARCHAR(200) NOT NULL,
	Services VARCHAR(300),
	latitude DECIMAL(17, 14),
	longitude DECIMAL(17, 14),
	Stars INT,
);

CREATE TABLE SERVICES_CENTER_PHONENUMBER(
	Service_Center_ID INT,
	PhoneNumber INT,
	PRIMARY KEY(Service_Center_ID,PhoneNumber),
	FOREIGN KEY (Service_Center_ID) REFERENCES SERVICES_CENTER(ID)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE SERVICES_CENTER_BRANDS(
	Service_Center_ID INT,
	CarBrand VARCHAR(50),
	PRIMARY KEY(Service_Center_ID,CarBrand),
	FOREIGN KEY (Service_Center_ID) REFERENCES SERVICES_CENTER(ID)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
);

CREATE TABLE ORDERS(
	Order_No INT PRIMARY KEY IDENTITY(1,1),
	order_date DATE NOT NULL,
	order_time TIME,
	order_status VARCHAR(20),
	City VARCHAR(50),
	Street VARCHAR(100),
	Building VARCHAR(20),
	HouseNo INT,
	Branch_No INT,
	FOREIGN KEY (Branch_No) REFERENCES BRANCH
	ON UPDATE CASCADE
);

CREATE TABLE CLIENT(
	ClientID INT Primary Key IDENTITY(1,1),
	Client_Username VARCHAR(100) Unique,
	Client_FName VARCHAR(20),
	Client_LName VARCHAR(20),
	Client_image varbinary(MAX),
	Client_phone varchar(20),
	pass varchar(30),
	bdate Date,
	Mail VARCHAR(100),
	UserType INT Default 0,
);

select * from CLIENT
CREATE TABLE CLIENT_PHONENUMBER(
	ClientID INT,
	PhoneNumber varchar(20),
	PRIMARY KEY (ClientID,PhoneNumber),
	FOREIGN KEY (ClientID) REFERENCES CLIENT(ClientID)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE CLIENT_ORDER(
	Client_ID INT,
	OrderID INT,
	PRIMARY KEY(Client_ID,OrderID),
	FOREIGN KEY (Client_ID) REFERENCES CLIENT(ClientID)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	FOREIGN KEY (OrderID) REFERENCES ORDERS(Order_No)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE works_in_branchdep(
	Emp_ID INT,
	Branch_ID INT,
	Dep_ID INT,
	PRIMARY KEY(Emp_ID),
	FOREIGN KEY(Branch_ID) REFERENCES Branch(BranchID)
	ON DELETE SET NULL
	ON UPDATE CASCADE
);

CREATE TABLE wishlist_products(
	Customer_ID int,
	Product_ID int
	primary key (Customer_ID, Product_ID)
	Foreign key (Customer_ID) references CLIENT(ClientID),
	Foreign key (Product_ID) references PRODUCT(ProductID)
);

CREATE TABLE wishlist_vehicle(
	Customer_ID int,
	vehichle_ID int
	primary key (Customer_ID, vehichle_ID)
	Foreign key (Customer_ID) references CLIENT(ClientID),
	Foreign key (Vehichle_ID) references vehicle(Vehicle_No)
);

CREATE TABLE Cart_products(
	Customer_ID int,
	Product_ID int
	primary key (Customer_ID, Product_ID)
	Foreign key (Customer_ID) references CLIENT(ClientID),
	Foreign key (Product_ID) references PRODUCT(ProductID)
);

CREATE TABLE Cart_vehicle(
	Customer_ID int,
	vehichle_ID int
	primary key (Customer_ID, vehichle_ID)
	Foreign key (Customer_ID) references CLIENT(ClientID),
	Foreign key (Vehichle_ID) references vehicle(Vehicle_No)
);



ALTER TABLE VEHICLE
ADD CONSTRAINT SHOW_ROOM_INVEHICLE_TO_SHOWROOM FOREIGN KEY(SHOWROOM) REFERENCES SHOWROOM(Show_Room_Name)
ON DELETE SET NULL
ON UPDATE CASCADE

-- Till Here--


-- Filling Data Fake

-- EMPLOYEE table
INSERT INTO EMPLOYEE (ID, Emp_Username, Fname, Mname, Lname, pass, SSN, BirthDate, Gender, Salary, SuperID, UserType) VALUES
(1, 'mohamedali', 'Mohamed', 'F', 'Ali', 'password6', 234567890, '1988-08-08', 'M', 55000, 1, 0),
(2, 'beso', 'Ahmed', 'Bassam', 'Abdelaal', '123', 214567890, '2003-07-06', 'M', 100000, NULL, 1),
(3, 'tarekteto', 'Tarek', 'Shalaby', 'Mohammed', 'password6', 232567890, '1988-08-08', 'M', 55000, NULL, 1),
(4, 'sieka', 'Mohamed', 'Elsayed', 'Mohammed', 'password6', 244567890, '1988-08-08', 'M', 55000, NULL, 1),
(5, 'body', 'Abdel Rahman', 'Rashad', 'Mohammed', 'password6', 234967890, '1988-08-08', 'M', 55000, NULL, 1),
(6, 'soso', 'Salwa', 'Ragab', 'Sharnoby', 'password6', 234567110, '1988-08-08', 'M', 55000, 2, 0),
(7, 'fatmahassan', 'Fatma', 'G', 'Hassan', 'password7', 345672201, '1995-03-12', 'F', 45000, 2, 0),
(8, 'ahmedkamal', 'Ahmed', 'H', 'Kamal', 'password8', 456789033, '1978-11-05', 'M', 80000, NULL, 0),
(9, 'nourhanhamed', 'Nourhan', 'I', 'Hamed', 'password9', 567844123, '1992-12-25', 'F', 65000, 2, 0),
(10, 'amrsayed', 'Amr', 'J', 'Sayed', 'password10', 678901255, '1987-06-17', 'M', 45000, 2, 0),
(11, 'sarahmohamed', 'Sarah', 'K', 'Mohamed', 'password11', 789012366, '1998-07-30', 'F', 55000, NULL, 0),
(12, 'omarelsayed', 'Omar', 'L', 'El Sayed', 'password12', 890123477, '1991-03-21', 'M', 70000, NULL, 0),
(13, 'rehamhassan', 'Reham', 'M', 'Hassan', 'password13', 901234588, '1983-04-02', 'F', 60000, 2, 0),
(14, 'mohamedahmed', 'Mohamed', 'N', 'Ahmed', 'password14', 123489954, '1993-09-15', 'M', 55000, 2, 0),
(15, 'sarahamr', 'Sarah', 'O', 'Amr', 'password15', 234598764, '1989-12-31', 'F', 45000, 2, 0),
(16, 'omarhassan', 'Omar', 'P', 'Hassan', 'password16', 345610876, '1997-05-27', 'M', 60000, 2, 0),
(17, 'amrelnagar', 'Amr', 'Q', 'El Naggar', 'password17', 456712235, '1982-08-23', 'M', 80000, NULL, 0),
(18, 'mariamahmed', 'Maria', 'R', 'Ahmed', 'password18', 567815449, '1994-10-18', 'F', 55000, 2, 0),
(19, 'ahmedhassan', 'Ahmed', 'S', 'Hassan', 'password19', 678948458, '1986-02-05', 'M', 70000, 2, 0),
(20, 'sarahmohammed', 'Sarah', 'T', 'Mohammed', 'password20', 798034567, '1999-01-28', 'F', 45000, 3, 0),
(21, 'omarmohamed', 'Omar', 'U', 'Mohamed', 'password21', 890484578, '1990-07-13', 'M', 75000, NULL, 0),
(22, 'mohamedhassan', 'Mohamed', 'V', 'Hassan', 'password22', 904856789, '1985-04-20', 'M', 65000, 3, 0),
(23, 'mariamhassan', 'Maria', 'W', 'Hassan', 'password23', 234564890, '1996-03-05', 'F', 55000, 3, 0),
(24, 'ahmedmohamed', 'Ahmed', 'X', 'Mohamed', 'password24', 345675901, '1981-12-10', 'M', 85000, NULL, 0),
(25, 'nadiamohamed', 'Nadia', 'Y', 'Mohamed', 'password25', 456781212, '1993-11-25', 'F', 50000, 3, 0),
(26, 'mohamedhamed', 'Mohamed', 'Z', 'Hamed', 'password26', 567891823, '1990-06-21', 'M', 70000, 3, 0),
(27, 'sarahebrahim', 'Sarah', 'Aa', 'Ebrahim', 'password27', 678984234, '1998-09-30', 'F', 45000, 3, 0),
(28, 'omarhamed', 'Omar', 'Bb', 'Hamed', 'password28', 789012615, '1987-02-17', 'M', 60000, 3, 0),
(29, 'ahmedhamed', 'Ahmed', 'Cc', 'Hamed', 'password29', 890184456, '1992-04-09', 'M', 65000, NULL, 0),
(30, 'nourelhoda', 'Nour', 'Dd', 'El Hoda', 'password30', 456781282, '1994-05-30', 'F', 55000, 4, 0),
(31, 'user', 'Nouwwr', 'Dwd', 'El wHodqa', 'user', 456721212, '1994-05-30', 'F', 55000, 4, 0),
(32, 'admin', 'Nouwr', 'Ddw', 'Elw Hodqa', 'admin', 416781212, '1994-05-30', 'F', 55000, 4, 1);

select * from employee
-- BRANCH table
INSERT INTO BRANCH (BranchID, MgrID, Bname, City, Street, BuildingNo) VALUES
(1, 2, 'Main Branch', 'Giza', 'Zewail', '123'),
(2, 3, 'Masara Branch', 'Cairo', 'Shaheed Ghaly', '25'),
(3, 2, 'Tarek Branch', 'Giza', 'Hadayek El Ahram', '789'),
(4, 4, 'Elsayed Branch', 'Giza', 'Elhosary', '1011'),
(5, 5, 'West Branch', 'Alexandria', 'Abo Kir Street', '1213'),
(6, 2, 'East Branch', 'Cairo', 'Boylston Street', '1415'),
(7, 3, 'South Branch', 'Alexandria', 'Main Street', '1617'),
(8, 2, 'North Branch', 'Luxor', 'Hennepin Avenue', '1819'),
(9, 4, 'Central Branch', 'Aswan', '16th Street', '2021'),
(10, 5, 'Pacific Branch', 'Giza', 'Waikiki Beach', '2223'),
(11, 2, 'Atlantic Branch', 'Cairo', 'Ocean Drive', '2425'),
(12, 3, 'Gulf Branch', 'Alexandria', 'Bourbon Street', '2627'),
(13, 2, 'Mountain Branch', 'Luxor', 'Temple Square', '2829'),
(14, 4, 'Prairie Branch', 'Aswan', 'Country Club Plaza', '3031'),
(15, 5, 'Sunrise Branch', 'Giza', 'International Drive', '3233'),
(16, 2, 'Sunset Branch', 'Cairo', 'The Strip', '3435'),
(17, 3, 'Lake Branch', 'Alexandria', 'Belle Isle', '3637'),
(18, 2, 'River Branch', 'Luxor', 'The Arch', '3839'),
(19, 4, 'Valley Branch', 'Aswan', 'Camelback Road', '4041'),
(20, 5, 'Desert Branch', 'Giza', 'Saguaro National Park', '4243'),
(21, 2, 'Coastal Branch', 'Cairo', 'La Jolla Cove', '4445'),
(22, 3, 'Island Branch', 'Alexandria', 'Prince William Sound', '4647'),
(23, 2, 'Bay Branch', 'Luxor', 'Fisherman''s Wharf', '4849'),
(24, 4, 'Harbor Branch', 'Aswan', 'Puget Sound', '5051'),
(25, 5, 'Ocean Branch', 'Giza', 'Cannon Beach', '5253'),
(26, 2, 'Riverfront Branch', 'Cairo', 'Ohio River', '5455'),
(27, 3, 'Lakefront Branch', 'Alexandria', 'Lake Michigan', '5657'),
(28, 2, 'City Center Branch', 'Luxor', 'Monument Circle', '5859'),
(29, 4, 'Park Branch', 'Aswan', 'Fairmount Park', '6061'),
(30, 5, 'University Branch', 'Giza', 'Harvard Square', '6263');

-- SUPPLIER table
INSERT INTO SUPPLIER (Supplier_Name, BranchNo, Supply_Type) VALUES
('AutoZone Egypt', 6, 'Car Parts'),
('Egyptian Tire Company', 7, 'Tires'),
('Car Care Egypt', 8, 'Auto Detailing'),
('Egyptian Car Paints', 9, 'Car Paints'),
('Egyptian Car Keys', 10, 'Car Keys'),
('Egyptian Car Covers', 11, 'Car Covers'),
('Car Wash Egypt', 12, 'Car Wash'),
('Egyptian Car Rentals', 13, 'Car Rentals'),
('Car Audio Egypt', 14, 'Car Audio'),
('Egyptian Car Seats', 15, 'Car Seats'),
('Car Gadgets Egypt', 16, 'Car Gadgets'),
('Egyptian Car Accessories', 17, 'Car Accessories'),
('Egyptian Car Mats', 18, 'Car Mats'),
('Egyptian Car Air Conditioning', 19, 'Car Air Conditioning'),
('Egyptian Car Batteries', 20, 'Car Batteries'),
('Egyptian Car Alarms', 21, 'Car Alarms'),
('Egyptian Car Suspension', 22, 'Car Suspension'),
('Egyptian Car Filters', 23, 'Car Filters'),
('Egyptian Car Brakes', 24, 'Car Brakes'),
('Car Dealers Egypt', 25, 'Car Dealership');

-- PRODUCT
INSERT INTO PRODUCT (ProductID, CATEGORY, BranchNo, Quantity, brand, Price, iimage, Statuss)
VALUES (11, 'Car Accessories', 1, 15, 'Pioneer', 500, (SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\pioneer.png', SINGLE_BLOB) as Image), 1);

INSERT INTO PRODUCT (ProductID, CATEGORY, BranchNo, Quantity, brand, Price, iimage, Statuss)
VALUES (12, 'Car Accessories', 2, 22, 'Sony', 400, (SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\sony.jpg', SINGLE_BLOB) as Image), 1);

INSERT INTO PRODUCT (ProductID, CATEGORY, BranchNo, Quantity, brand, Price, iimage, Statuss)
VALUES (13, 'Car Accessories', 3, 18, 'Garmin', 300, (SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\garmin.jpg', SINGLE_BLOB) as Image), 1);

INSERT INTO PRODUCT (ProductID, CATEGORY, BranchNo, Quantity, brand, Price, iimage, Statuss)
VALUES (14, 'Car Accessories', 4, 9, 'JBL', 200, (SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\JBL.png', SINGLE_BLOB) as Image), 1);

INSERT INTO PRODUCT (ProductID, CATEGORY, BranchNo, Quantity, brand, Price, iimage, Statuss)
VALUES (15, 'Car Accessories', 5, 26, 'Philips', 150, (SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\Philips.png', SINGLE_BLOB) as Image), 1);

INSERT INTO PRODUCT (ProductID, CATEGORY, BranchNo, Quantity, brand, Price, iimage, Statuss)
VALUES (17, 'Car Accessories', 7, 7, 'TomTom', 250, (SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\tablet_holder.jpg', SINGLE_BLOB) as Image), 1);

-- SUPPLIER

INSERT INTO SUPPLIER (Supplier_Name, BranchNo, Supply_Type) VALUES
('Auto Parts Co.', 1, 'Car Parts'),
('Auto Parts Co.', 2, 'Car Parts'),
('Auto Parts Co.', 3, 'Car Parts'),
('Auto Parts Co.', 4, 'Car Parts'),
('Auto Parts Co.', 5, 'Car Parts'),
('Car Manufacturers Inc.', 1, 'Cars'),
('Car Manufacturers Inc.', 2, 'Cars'),
('Car Manufacturers Inc.', 3, 'Cars'),
('Car Manufacturers Inc.', 4, 'Cars'),
('Car Manufacturers Inc.', 5, 'Cars'),
('Oil Supplier Corp.', 1, 'Oil'),
('Oil Supplier Corp.', 2, 'Oil'),
('Oil Supplier Corp.', 3, 'Oil'),
('Oil Supplier Corp.', 4, 'Oil'),
('Oil Supplier Corp.', 5, 'Oil');

-- Department

INSERT INTO DEPARTMENT (DeparmentID, Department_Name, BranchNo) VALUES
(1, 'Sales', 1),
(2, 'Service', 1),
(3, 'Parts', 1),
(4, 'Sales', 2),
(5, 'Service', 2),
(6, 'Parts', 2),
(7, 'Sales', 3),
(8, 'Service', 3),
(9, 'Parts', 3),
(10, 'Sales', 4),
(11, 'Service', 4),
(12, 'Parts', 4),
(13, 'Sales', 5),
(14, 'Service', 5),
(15, 'Parts', 5);

-- DEPMGR
INSERT INTO DEPMGR (DepID, Mgr_ID, Branch_No) VALUES
(1, 1001, 1),
(2, 1002, 1),
(3, 1003, 1),
(4, 1004, 2),
(5, 1005, 2),
(6, 1006, 2),
(7, 1007, 3),
(8, 1008, 3),
(9, 1009, 3),
(10, 1010, 4),
(11, 1011, 4),
(12, 1012, 4),
(13, 1013, 5),
(14, 1014, 5),
(15, 1015, 5);

-- SHOWROOM
INSERT INTO SHOWROOM (Show_Room_Name, City, Street, phoneNumber) VALUES
('Main Showroom', 'Alexandria', 'El-Raml Station', '+20-3-1234567'),
('Cairo Showroom', 'Cairo', 'El-Nasr Road', '+20-2-2345678'),
('Giza Showroom', 'Giza', 'Pyramids Street', '+20-2-3456789'),
('Luxor Showroom', 'Luxor', 'Corniche El-Nil', '+20-95-4567890'),
('Aswan Showroom', 'Aswan', 'Corniche El-Nil', '+20-97-5678901'),
('Sharm El-Sheikh Showroom', 'South Sinai', 'El-Salam Road', '+20-69-6789012'),
('Hurghada Showroom', 'Red Sea', 'El-Mamsha Street', '+20-65-7890123'),
('Port Said Showroom', 'Port Said', 'El-Ganayen Street', '+20-66-8901234'),
('Suez Showroom', 'Suez', 'El-Mahatta Street', '+20-62-9012345'),
('Ismailia Showroom', 'Ismailia', 'El-Tal El-Kabeer Street', '+20-64-0123456'),
('Tanta Showroom', 'Gharbia', 'El-Galaa Street', '+20-40-1234567'),
('Mansoura Showroom', 'Dakahlia', 'El-Salam Street', '+20-50-2345678'),
('Zagazig Showroom', 'Sharqia', 'El-Gomhouria Street', '+20-55-3456789'),
('Beni Suef Showroom', 'Beni Suef', 'El-Mahatta Street', '+20-82-4567890'),
('Fayoum Showroom', 'Fayoum', 'El-Kawmeya Street', '+20-84-5678901'),
('Minya Showroom', 'Minya', 'El-Midane Street', '+20-86-6789012'),
('Sohag Showroom', 'Sohag', 'El-Gomhouria Street', '+20-93-7890123'),
('Qena Showroom', 'Qena', 'El-Ganayen Street', '+20-96-8901234'),
('Damanhour Showroom', 'Beheira', 'El-Thawra Street', '+20-45-9012345'),
('Kafr El-Sheikh Showroom', 'Kafr El-Sheikh', 'El-Galaa Street', '+20-47-0123456');


-- Vehicle

INSERT INTO VEHICLE (Car_Status, SHOWROOM, Brand, CC_Rnage, Color, Year_Model, Gearing, Body_Style, Engine_Capacity, hourse_power, maximum_speed, Warranty_years, Warranty_Kilometers, acceleration, speeds, Fuel, Liter_per_100KM, width, height, Trunk_Size, Seats, Traction_Type, Cylinders, Tank_Capacity, iimage)
VALUES ('new', 'Main Showroom', 'Toyota', 2000, 'Red', 2022, 'Automatic',
'Sedan', 2000, 150, 200, 3, 50000, 8, 6, 'Gasoline', 8.5, 1800, 1400, 500,
5, '2WD', 4, 60, (SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\toyota1.jpg',SINGLE_BLOB) as Image));

INSERT INTO VEHICLE (Car_Status, SHOWROOM, Brand, CC_Rnage, Color, Year_Model, Gearing, Body_Style, Engine_Capacity,
hourse_power, maximum_speed, Warranty_years, Warranty_Kilometers, acceleration, speeds, Fuel, Liter_per_100KM, width,
height, Trunk_Size, Seats, Traction_Type, Cylinders, Tank_Capacity, iimage)
VALUES ('new', 'Main Showroom', 'Honda', 1800, 'White', 2022, 'Automatic',
'Sedan', 1800, 130, 190, 3, 50000, 7, 6, 'Gasoline', 7.5, 1800, 1400, 450,
5, '2WD', 4, 55, (SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\honda1.jpg', SINGLE_BLOB) as Image));

INSERT INTO VEHICLE (Car_Status, SHOWROOM, Brand, CC_Rnage, Color,
Year_Model, Gearing, Body_Style, Engine_Capacity, hourse_power,
maximum_speed, Warranty_years, Warranty_Kilometers, acceleration,
speeds, Fuel, Liter_per_100KM, width, height, Trunk_Size, Seats,
Traction_Type, Cylinders, Tank_Capacity, iimage)
VALUES ('new', 'Main Showroom', 'Ford', 2500, 'Black', 2022,
'Automatic', 'SUV', 2500, 180, 220, 3, 50000, 10, 6, 'Diesel',
10.5, 2000, 1600, 700, 7, '4WD', 6, 80, (SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\ford1.jpg', SINGLE_BLOB) as Image));

INSERT INTO VEHICLE (Car_Status, SHOWROOM, Brand, CC_Rnage, Color,
Year_Model, Gearing, Body_Style, Engine_Capacity, hourse_power,
maximum_speed, Warranty_years, Warranty_Kilometers, acceleration,
speeds, Fuel, Liter_per_100KM, width, height, Trunk_Size, Seats,
Traction_Type, Cylinders, Tank_Capacity, iimage)
VALUES ('new', 'Main Showroom', 'Nissan', 1500, 'Blue', 2022,
'Manual', 'Hatchback', 1500, 100, 180, 3, 50000, 8, 5,
'Gasoline', 7.0, 1600, 1300, 350, 5, '2WD', 4, 50,
(SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\nissan1.jpg', SINGLE_BLOB) as Image));

INSERT INTO VEHICLE (Car_Status, SHOWROOM, Brand, CC_Rnage, Color,
Year_Model, Gearing, Body_Style, Engine_Capacity, hourse_power,
maximum_speed, Warranty_years, Warranty_Kilometers, acceleration,
speeds, Fuel, Liter_per_100KM, width, height, Trunk_Size, Seats,
Traction_Type, Cylinders, Tank_Capacity, iimage)
VALUES ('used', 'Cairo Showroom', 'BMW', 2000, 'Black', 2019,
'Automatic', 'Sedan', 2000, 150, 240, 2, 20000, 7, 6,
'Petrol', 9.0, 1700, 1400, 400, 5, 'RWD', 6, 60,
(SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\bmw1.jpg', SINGLE_BLOB) as Image));

INSERT INTO VEHICLE (Car_Status, SHOWROOM, Brand, CC_Rnage, Color,
Year_Model, Gearing, Body_Style, Engine_Capacity, hourse_power,
maximum_speed, Warranty_years, Warranty_Kilometers, acceleration,
speeds, Fuel, Liter_per_100KM, width, height, Trunk_Size, Seats,
Traction_Type, Cylinders, Tank_Capacity, iimage)
VALUES ('new', 'Cairo Showroom', 'Mercedes', 3000, 'White', 2022,
'Automatic', 'SUV', 3000, 250, 260, 5, 100000, 6, 7,
'Diesel', 10.0, 1900, 1600, 600, 7, '4WD', 8, 80,
(SELECT BulkColumn FROM OPENROWSET(BULK N'D:\Academic\Second Year\Spring\DataBase\Project\Car_Store\wwwroot\images\DataBase\mercedes1.jpg', SINGLE_BLOB) as Image));


-- New_Vehicle
select * from VEHICLE

INSERT INTO NEW_VEHICLE (Vehicle_ID, NUMBER_OF_CLASSES, Start_Price, End_Price) VALUES
(2, 2, 40000, 60000),
(3, 4, 60000, 90000),
(4, 1, 30000, 35000),
(5, 5, 70000, 120000);

-- USED_VEHICLE
INSERT INTO USED_VEHICLE (Vehicle_ID, Kilometers, Price, Posting_Date, Class) VALUES
(2, 80000, 25000, '2022-02-10', 1),
(3, 60000, 45000, '2022-03-05', 3),
(4, 90000, 20000, '2022-04-20', 1),
(5, 70000, 55000, '2022-05-30', 4);

-- CAR_CLASSES
INSERT INTO CAR_CLASSES (Vehicle_ID, Class, Price, Deposit, Minimum_Installment) VALUES
(2, 1, 40000, 8000, 1600),
(2, 2, 50000, 10000, 2000),
(3, 1, 60000, 12000, 2400),
(3, 2, 70000, 14000, 2800),
(3, 3, 80000, 16000, 3200),
(3, 4, 90000, 18000, 3600),
(4, 1, 30000, 6000, 1200),
(5, 1, 70000, 14000, 2800),
(5, 2, 80000, 16000, 3200),
(5, 3, 90000, 18000, 3600),
(5, 4, 100000, 20000, 4000),
(5, 5, 120000, 24000, 4800);



-- Service_Centers

--To change
INSERT INTO SERVICES_CENTER (Service_Center_Name, City, Street, BuildingNo) VALUES
('ABC Service Center', 'Cairo', 'El-Nasr Road', '123'),
('XYZ Service Center', 'Alexandria', 'El-Raml Station', '456'),
('DEF Service Center', 'Giza', 'Pyramids Street', '789'),
('GHI Service Center', 'Luxor', 'Corniche El-Nile', '234'),
('JKL Service Center', 'Aswan', 'El-Souq Street', '567'),
('MNO Service Center', 'Sharm El-Sheikh', 'El-Salam Road', '890'),
('PQR Service Center', 'Hurghada', 'El-Mamsha Street', '123'),
('STU Service Center', 'Port Said', 'El-Ganayen Street', '456'),
('VWX Service Center', 'Suez', 'El-Mahatta Street', '789'),
('YZA Service Center', 'Ismailia', 'El-Tal El-Kabeer Street', '234'),
('BCD Service Center', 'Tanta', 'El-Galaa Street', '567'),
('EFG Service Center', 'Mansoura', 'El-Salam Street', '890'),
('HIJ Service Center', 'Zagazig', 'El-Gomhouria Street', '123'),
('KLM Service Center', 'Beni Suef', 'El-Mahatta Street', '456'),
('NOP Service Center', 'Fayoum', 'El-Kawmeya Street', '789'),
('QRS Service Center', 'Minya', 'El-Midane Street', '234'),
('TUV Service Center', 'Sohag', 'El-Gomhouria Street', '567'),
('WXY Service Center', 'Qena', 'El-Ganayen Street', '890'),
('ZAB Service Center', 'Damanhour', 'El-Thawra Street', '123'),
('CDE Service Center', 'Kafr El-Sheikh', 'El-Galaa Street', '456');

--SERVICES_CENTER_PHONENUMBER

INSERT INTO SERVICES_CENTER_PHONENUMBER (Service_Center_Name, PhoneNumber) VALUES
('ABC Service Center',201234569),
('XYZ Service Center',201234567),
('DEF Service Center', 203456789),
('GHI Service Center', 203456789),
('JKL Service Center', 202345678),
('MNO Service Center', 209567890),
('PQR Service Center', 975678901),
('STU Service Center', 975678901),
('VWX Service Center', 692345678),
('YZA Service Center', 692345678),
('BCD Service Center', 656789012),
('EFG Service Center', 662345678),
('HIJ Service Center', 620123456),
('KLM Service Center', 640123456),
('NOP Service Center', 404567890),
('QRS Service Center', 502345678),
('TUV Service Center', 555789012),
('WXY Service Center', 555789012),
('ZAB Service Center', 511189012),
('CDE Service Center',552229012);

-- SERVICES_CENTER_BRANDS
INSERT INTO SERVICES_CENTER_BRANDS (Service_Center_Name, CarBrand) VALUES
('GHI Service Center', 'Toyota'),
('GHI Service Center', 'Nissan'),
('JKL Service Center', 'Chevrolet'),
('JKL Service Center', 'Ford'),
('MNO Service Center', 'Mercedes-Benz'),
('MNO Service Center', 'BMW'),
('PQR Service Center', 'Hyundai'),
('STU Service Center', 'Kia'),
('VWX Service Center', 'Renault'),
('YZA Service Center', 'Mitsubishi'),
('BCD Service Center', 'Fiat'),
('EFG Service Center', 'Peugeot'),
('HIJ Service Center', 'Volkswagen'),
('KLM Service Center', 'Honda'),
('NOP Service Center', 'Mazda'),
('QRS Service Center', 'Audi'),
('TUV Service Center', 'Lexus');

-- CLIENT
INSERT INTO CLIENT (Client_Username, Client_FName, Client_LName, pass, bdate, Mail) VALUES
('johnsmith', 'John', 'Smith', 'password123', '1990-05-10', 'johnsmith@example.com'),
('ahmedali', 'Ahmed', 'Ali', 'mypassword', '1995-02-21', 'ahmedali@example.com'),
('emilyjones', 'Emily', 'Jones', 'emilypassword', '1987-09-04', 'emilyjones@example.com'),
('mohammedmustafa', 'Mohammed', 'Mustafa', 'mypassword', '1993-07-15', 'mohammedmustafa@example.com'),
('jessicawilliams', 'Jessica', 'Williams', 'mypassword', '1992-12-28', 'jessicawilliams@example.com'),
('omarsuliman', 'Omar', 'Suliman', 'mypassword', '1991-04-02', 'omarsuliman@example.com'),
('michaelsmith', 'Michael', 'Smith', 'mypassword', '1988-11-13', 'michaelsmith@example.com'),
('laylakhaleed', 'Layla', 'Khaled', 'mypassword', '1996-03-25', 'laylakhaleed@example.com'),
('sarahjohnson', 'Sarah', 'Johnson', 'mypassword', '1989-06-17', 'sarahjohnson@example.com'),
('abdulrahmanali', 'Abdulrahman', 'Ali', 'mypassword', '1994-08-29', 'abdulrahmanali@example.com');

INSERT INTO CLIENT (Client_Username, Client_FName, Client_LName, pass, bdate, Mail) VALUES
('johndoe', 'John', 'Doe', 'password123', '1991-02-14', 'johndoe@example.com'),
('sarahsmith', 'Sarah', 'Smith', 'mypassword', '1990-06-23', 'sarahsmith@example.com'),
('aliyousef', 'Ali', 'Yousef', 'mypassword', '1985-12-05', 'aliyousef@example.com'),
('laurajackson', 'Laura', 'Jackson', 'mypassword', '1994-03-18', 'laurajackson@example.com'),
('samuellee', 'Samuel', 'Lee', 'mypassword', '1986-08-27', 'samuellee@example.com'),
('fatmahassan', 'Fatma', 'Hassan', 'mypassword', '1992-05-08', 'fatmahassan@example.com'),
('andrewjohnson', 'Andrew', 'Johnson', 'mypassword', '1993-11-29', 'andrewjohnson@example.com'),
('reemalqasim', 'Reem', 'Alqasim', 'mypassword', '1990-09-21', 'reemalqasim@example.com'),
('markdavis', 'Mark', 'Davis', 'mypassword', '1988-02-12', 'markdavis@example.com'),
('jenniferlopez', 'Jennifer', 'Lopez', 'mypassword', '1997-01-03', 'jenniferlopez@example.com'),
('ahmedmohammed', 'Ahmed', 'Mohammed', 'mypassword', '1989-04-26', 'ahmedmohammed@example.com'),
('jamesbrown', 'James', 'Brown', 'mypassword', '1995-07-09', 'jamesbrown@example.com'),
('hadeelabdullah', 'Hadeel', 'Abdullah', 'mypassword', '1993-10-22', 'hadeelabdullah@example.com'),
('williamjones', 'William', 'Jones', 'mypassword', '1991-12-30', 'williamjones@example.com'),
('samaralhajj', 'Samar', 'Alhajj', 'mypassword', '1996-06-02', 'samaralhajj@example.com'),
('mohamedibrahim', 'Mohamed', 'Ibrahim', 'mypassword', '1992-09-15', 'mohamedibrahim@example.com'),
('katewilson', 'Kate', 'Wilson', 'mypassword', '1987-04-28', 'katewilson@example.com'),
('yousefalhamed', 'Yousef', 'Alhamed', 'mypassword', '1995-01-20', 'yousefalhamed@example.com'),
('annasmith', 'Anna', 'Smith', 'mypassword', '1986-11-11', 'annasmith@example.com'),
('thomasharris', 'Thomas', 'Harris', 'mypassword', '1990-08-04', 'thomasharris@example.com');
-- CLIENT_PHONENUMBER

INSERT INTO CLIENT_PHONENUMBER (ClientID, PhoneNumber) VALUES
(1, '20123456789'),
(1, '20123456790'),
(2, '20345678901'),
(2, '20345678902'),
(3, '20234567890'),
(3, '20234567891'),
(4, '20956789012'),
(5, '97567890123'),
(5, '97567890124'),
(6, '69234567890'),
(6, '69234567891'),
(7, '65678901234'),
(8, '66234567890'),
(9, '62012345678'),
(9, '64012345678'),
(10, '40456789012'),
(11, '50234567890'),
(12, '55578901234'),
(13, '82234567890');
-- ORDERS
INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-16', '11:30:00', 'processing', 'Cairo', 'Tahrir Square', 'Building 1', 10, 1);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-16', '10:00:00', 'delivered', 'Giza', 'Pyramids Street', 'Building 2', 20, 2);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-15', '14:45:00', 'canceled', 'Alexandria', 'Corniche Road', 'Building 3', 30, 3);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-15', '09:30:00', 'processing', 'Cairo', 'Nile Street', 'Building 4', 40, 4);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-14', '13:15:00', 'processing', 'Giza', 'Zamalek', 'Building 5', 50, 5);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-14', '16:00:00', 'delivered', 'Cairo', 'Mohandessin', 'Building 6', 60, 1);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-13', '11:45:00', 'processing', 'Alexandria', 'Raml Station', 'Building 7', 70, 2);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-13', '15:30:00', 'delivered', 'Cairo', 'Nasr City', 'Building 8', 80, 3);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-12', '10:00:00', 'processing', 'Giza', 'Dokki', 'Building 9', 90, 4);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-12', '14:15:00', 'delivered', 'Cairo', 'Maadi', 'Building 10', 100, 5);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-11', '12:30:00', 'processing', 'Alexandria', 'Bahary', 'Building 11', 110, 1);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-11', '16:45:00', 'delivered', 'Cairo', 'Heliopolis', 'Building 12', 120, 2);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-10', '13:00:00', 'processing', 'Giza', 'Mohandessin', 'Building 13', 130, 3);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-10', '17:15:00', 'delivered', 'Cairo', 'Zamalek', 'Building 14', 140, 4);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-09', '11:30:00', 'processing', 'Alexandria', 'Roushdy', 'Building 15', 150, 5);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-09', '15:45:00', 'delivered', 'Cairo', 'Nasr City', 'Building 16', 160, 1);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-08', '12:00:00', 'processing', 'Giza', 'Dahshur Road', 'Building 17', 170, 2);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-08', '16:00:00', 'delivered', 'Cairo', 'Maadi', 'Building 18', 180, 3);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-07', '10:30:00', 'processing', 'Alexandria', 'Sidi Gaber', 'Building 19', 190, 4);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-07', '14:45:00', 'delivered', 'Cairo', 'Heliopolis', 'Building 20', 200, 5);

INSERT INTO ORDERS (order_date, order_time, order_status, City, Street, Building, HouseNo, Branch_No)
VALUES ('2023-05-16', '14:45:00', 'processing', 'Cairo', 'Maadi', 'Building 70', 100, 4);


select * from ORDERS
-- CLIENT_ORDER
INSERT INTO CLIENT_ORDER (Client_ID, OrderID) VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 8),
(4, 9),
(5, 10),
(5, 11),
(6, 12),
(6, 13),
(7, 14),
(8, 15),
(9, 16),
(9, 17),
(10, 18),
(11, 19),
(12, 20),
(13, 21);
----------- here------------
-- works_in_branchdep
INSERT INTO works_in_branchdep (Emp_ID, Branch_ID, Dep_ID) VALUES
(1, 1, 1),
(7, 1, 2),
(8, 2, 3),
(9, 2, 4),
(10, 3, 5),
(11, 3, 6),
(12, 3, 7),
(13, 4, 8),
(14, 4, 9),
(15, 5, 10),
(16, 5, 11),
(17, 6, 12),
(18, 7, 13),
(19, 7, 14),
(20, 8, 15),
(21, 8, 16),
(22, 8, 17),
(23, 9, 18),
(24, 9, 19);
-- wishlist_products

INSERT INTO wishlist_products (Customer_ID, Product_ID) VALUES
(1, 11)

select * from client
select * from PRODUCT
-- wishlist_vehicle
INSERT INTO wishlist_vehicle (Customer_ID, vehichle_ID) VALUES
(1, 15)

-- Cart_products
INSERT INTO Cart_products (Customer_ID, Product_ID) VALUES
(1, 11),
(2, 12),
(3, 13),
(4, 14),
(5, 15),
(6, 11),
(7, 12),
(8, 13),
(9, 14),
(10, 15);

-- Cart_vehicle
INSERT INTO Cart_vehicle (Customer_ID, vehichle_ID) VALUES
(1, 15),
(2, 16),
(3, 17),
(4, 18),
(5, 19),
(6, 20),
(7, 20),
(8, 16),
(9, 17),
(10, 17);
 