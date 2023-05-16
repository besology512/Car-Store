CREATE DATABASE MYCARSTORE
USE MYCARSTORE

CREATE TABLE EMPLOYEE(  
	ID INT, 
	Fname NVARCHAR(50),   
	Mname NVARCHAR(50),   
	Lname NVARCHAR(50),    
	SSN INT UNIQUE NOT NULL,   
	BirthDate DATE,   
	Gender CHAR(1),  
	Salary INT, 
	SuperID INT,
	UserType INT , -- maybe 1 is admin
	PRIMARY KEY(ID), 
	FOREIGN KEY (SuperID) REFERENCES EMPLOYEE(ID)
);



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
	ProductID INT PRIMARY KEY,
	CATEGORY VARCHAR(100) NOT NULL,
	BranchNo INT,
	Quantity INT NOT NULL,
	brand VARCHAR(100),
	Price INT NOT NULL,
	iimage VARCHAR(150),
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
	DeparmentID INT,
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
	Car_Status VARCHAR(20) NOT NULL,
	SHOWROOM VARCHAR(100),
	Brand VARCHAR(50) NOT NULL,
	CC_Rnage INT NOT NULL,
	Color VARCHAR(20) NOT NULL,
	Year_Model INT NOT NULL,
	Gearing VARCHAR(20) NOT NULL,
	Body_Style VARCHAR(20),
	iimage VARCHAR(150)
);

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

select * from SHOWROOM

CREATE TABLE SERVICES_CENTER(
	Service_Center_Name VARCHAR(100) PRIMARY KEY,
	City VARCHAR(50) NOT NULL,
	Street VARCHAR(100) NOT NULL,
	BuildingNo VARCHAR(10)
);

CREATE TABLE SERVICES_CENTER_PHONENUMBER(
	Service_Center_Name VARCHAR(100),
	PhoneNumber INT,
	PRIMARY KEY(Service_Center_Name,PhoneNumber),
	FOREIGN KEY (Service_Center_Name) REFERENCES SERVICES_CENTER(Service_Center_Name)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE SERVICES_CENTER_BRANDS(
	Service_Center_Name VARCHAR(100),
	CarBrand VARCHAR(50),
	PRIMARY KEY(Service_Center_Name,CarBrand),
	FOREIGN KEY (Service_Center_Name) REFERENCES SERVICES_CENTER(Service_Center_Name)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
);

CREATE TABLE ORDERS(
	Order_No INT,
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
	ClientID INT Unique IDENTITY(1,1),
	Client_Username VARCHAR(100) Primary Key,
	Client_FName VARCHAR(20),
	Client_LName VARCHAR(20),
	pass varchar(30),
	bdate Date,
	Mail VARCHAR(100),
	UserType INT Default 0,
);


CREATE TABLE CLIENT_PHONENUMBER(
	ClientID INT,
	PhoneNumber INT,
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

ALTER TABLE VEHICLE
ADD CONSTRAINT SHOW_ROOM_INVEHICLE_TO_SHOWROOM FOREIGN KEY(SHOWROOM) REFERENCES SHOWROOM(Show_Room_Name)
ON DELETE SET NULL
ON UPDATE CASCADE

-- Till Here--





ALTER TABLE PRODUCT 
ADD Pdescription varchar(100);

delete EMPLOYEE

INSERT INTO EMPLOYEE (Fname, Mname, Lname, SSN, BirthDate, Gender, Salary, SuperID, UserType)
VALUES ('John', 'Michael', 'Doe', 123456789, '1980-01-01', 'M', 50000, NULL, 0);

INSERT INTO EMPLOYEE (Fname, Mname, Lname, SSN, BirthDate, Gender, Salary, SuperID, UserType)
VALUES ('Jane', 'Maria', 'Doe', 987654321, '1985-02-01', 'F', 60000, Null, 0);

INSERT INTO EMPLOYEE (Fname, Mname, Lname, SSN, BirthDate, Gender, Salary, SuperID, UserType)
VALUES ('Jim', 'Robert', 'Smith', 456789123, '1990-03-01', 'M', 70000, NUll, 0);

select * from EMPLOYEE

-- Insert data into BRANCH table
INSERT INTO BRANCH (MgrID, Bname, City, Street, BuildingNo)
VALUES (29, 'Main Branch', 'New York', '5th Avenue', '123');

INSERT INTO BRANCH (MgrID, Bname, City, Street, BuildingNo)
VALUES (31, 'Second Branch', 'Los Angeles', 'Hollywood Boulevard', '456');

select * from BRANCH
-- Insert data into PRODUCT table
INSERT INTO PRODUCT (CATEGORY, BranchNo, Quantity, brand, Price, iimage, Statuss, Pdescription)
VALUES ('Car', 8, 10, 'Toyota', 20000, 'toyota.jpg', 1, 'A popular sedan');

INSERT INTO PRODUCT (CATEGORY, BranchNo, Quantity, brand, Price, iimage, Statuss, Pdescription)
VALUES ('Car', 9, 5, 'Honda', 25000, 'honda.jpg', 1, 'A reliable SUV');

-- Insert data into SHOWROOM table
INSERT INTO SHOWROOM (Show_Room_Name, City, STreet, phoneNumber)
VALUES ('Main Showroom', 'New York', '5th Avenue', '555-1234');

INSERT INTO SHOWROOM (Show_Room_Name, City, STreet, phoneNumber)
VALUES ('Second Showroom', 'Los Angeles', 'Hollywood Boulevard', '555-5678');

-- Insert data into CLIENT table
INSERT INTO CLIENT (Client_Username, Client_FName, Client_LName, pass, bdate, Mail, UserType)
VALUES ('johndoe', 'John', 'Doe', 'password', '1990-01-01', 'johndoe@example.com', 0);

INSERT INTO CLIENT (Client_Username, Client_FName, Client_LName, pass, bdate, Mail, UserType)
VALUES ('janedoe', 'Jane', 'Doe', 'password', '1995-01-01', 'janedoe@example.com', 0);

