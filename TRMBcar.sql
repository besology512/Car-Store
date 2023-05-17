CREATE DATABASE TRMBcar
USE TRMBcar

CREATE TABLE EMPLOYEE(  
	ID INT IDENTITY(1,1), 
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
	UserType INT, -- maybe 1 is admin
	Constraint PK_Emp PRIMARY KEY(ID), 
	Constraint FK_Emp_SuperID FOREIGN KEY (SuperID) REFERENCES EMPLOYEE(ID)
);


CREATE TABLE BRANCH(
	BranchID INT IDENTITY(1,1),
	MgrID INT,
	Bname NVARCHAR(50),
	City NVARCHAR(50),
	Street NVARCHAR(100),
	BuildingNo NVARCHAR(20),
	CONSTRAINT PK_Branch PRIMARY KEY(BranchID),
	CONSTRAINT FK_MgrID_EmpId FOREIGN KEY(MgrID) REFERENCES EMPLOYEE(ID)
	ON DELETE SET NULL
	ON UPDATE CASCADE
);



CREATE TABLE PRODUCT(
	ProductID INT IDENTITY(1,1) ,
	CATEGORY VARCHAR(100) NOT NULL,
	BranchNo INT,
	Quantity INT NOT NULL,
	brand VARCHAR(100),
	Price INT NOT NULL,
	P_image VARCHAR(MAX),
	Statuss INT, --PRODUCT AVALABILE OR NOT
	P_description varchar(200),
	CONSTRAINT PK_Product PRIMARY KEY(ProductID),
	CONSTRAINT FK_PRODUCT_BRANCH_NO FOREIGN KEY(BranchNo) REFERENCES BRANCH(BranchID)
	ON DELETE SET NULL
	ON UPDATE cascade --added

);

drop table PRODUCT




CREATE TABLE SUPPLIER(
	Supplier_Name VARCHAR(100),
	BranchNo int,
	Supply_Type VARCHAR(100),
	CONSTRAINT PK_SUPPLIER PRIMARY KEY (Supplier_Name,BranchNo),
	CONSTRAINT  FK_TO_BRANCH FOREIGN KEY (BranchNo) REFERENCES BRANCH(BranchID)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE DEPARTMENT(
	DeparmentID INT IDENTITY(1,1) ,
	Department_Name VARCHAR(100),
	BranchNo INT,
	CONSTRAINT PK_DEPARTMENT PRIMARY KEY(DeparmentID, BranchNo),
	CONSTRAINT FK_DEPARTMENT_TO_BRANCH FOREIGN KEY (BranchNo) REFERENCES BRANCH(BranchID)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
);

CREATE TABLE DEPMGR(
	DepID INT,
	Mgr_ID INT,
	Branch_No INT,
	CONSTRAINT PK_DEPMGR PRIMARY KEY(DepID, Branch_No),
	CONSTRAINT FK_DEPMGR_TO_DEPARTMENT FOREIGN KEY (DepID, Branch_No) REFERENCES Department(DeparmentID, BranchNo)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
);


CREATE TABLE VEHICLE(
	Vehicle_No INT  IDENTITY(1,1),
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
	CarDescription varchar(1000),
	Tank_Capacity int,
	C_image1 varchar(max),
	C_image2 varchar(max),
	C_image3 varchar(max),
	CONSTRAINT PK_VEHICLE PRIMARY KEY (Vehicle_No)
);
alter table Vehicle add visibality INT check (visibality >= 0 and visibality <=2 )
---- 0 = penidng , 1 = instock and visible , 2 = out of stock





CREATE TABLE NEW_VEHICLE(
	Vehicle_ID INT,
	Price INT NOT NULL,
	CONSTRAINT PK_NEW_VEHICLE PRIMARY KEY (Vehicle_ID),
	CONSTRAINT NEW_VEHICLE_TO_VEHICLE FOREIGN KEY(Vehicle_ID) REFERENCES VEHICLE(Vehicle_No)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);





CREATE TABLE USED_VEHICLE(
	Vehicle_ID INT ,
	Kilometers INT NOT NULL,
	Price INT NOT NULL,
	Posting_Date DATE NOT NULL,
	Class INT NOT NULL,
	CONSTRAINT PK_USED_VEHICLE PRIMARY KEY (Vehicle_ID),
	CONSTRAINT USED_VEHICLE_TO_VEHICLE FOREIGN KEY(Vehicle_ID) REFERENCES VEHICLE(Vehicle_No)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);
ALTER TABLE USED_VEHICLE
ADD CITY VARCHAR(20)


CREATE TABLE SHOWROOM(
	Show_Room_Name VARCHAR(100),
	City VARCHAR(50),
	STreet VARCHAR(100),
	phoneNumber varchar(20),
	CONSTRAINT PK_SHOWROOM PRIMARY KEY (Show_Room_Name)
);




CREATE TABLE SERVICES_CENTER(
	ID INT  IDENTITY(1,1),
	Name VARCHAR(100) Unique NOT NULL,
	Address VARCHAR(200) NOT NULL,
	Services VARCHAR(300),
	PhoneNumber INT,
	latitude DECIMAL(17, 14),
	longitude DECIMAL(17, 14),
	Stars INT,
	CONSTRAINT PK_SERVICES_CENTER PRIMARY KEY(ID)
);








CREATE TABLE ORDERS(
	Order_No INT  IDENTITY(1,1),
	order_date DATE NOT NULL,
	order_time TIME,
	order_status VARCHAR(20),
	City VARCHAR(50),
	Street VARCHAR(100),
	Building VARCHAR(20),
	HouseNo INT,
	Branch_No INT,
	CONSTRAINT PK_ORDERS PRIMARY KEY (Order_No),
	CONSTRAINT FK_ORDERS_BRANCH FOREIGN KEY (Branch_No) REFERENCES BRANCH
	ON UPDATE CASCADE
);

CREATE TABLE CLIENT(
	ClientID INT  IDENTITY(1,1),
	Client_Username VARCHAR(100) Unique,
	Client_FName VARCHAR(20),
	Client_LName VARCHAR(20),
	Client_image varbinary(MAX),
	Client_phone varchar(20),
	pass varchar(30),
	bdate Date,
	Mail VARCHAR(100),
	UserType INT Default 0,
	CONSTRAINT PK_CLIENT Primary Key (ClientID)
);



CREATE TABLE CLIENT_ORDER(
	Client_ID INT,
	OrderID INT,
	CONSTRAINT PK_CLIENT_ORDER PRIMARY KEY(Client_ID,OrderID),
	CONSTRAINT FK_CLIENT_ORDERS_TO_CLIENT FOREIGN KEY (Client_ID) REFERENCES CLIENT(ClientID)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	CONSTRAINT FK_CLIENT_ORDERS_TO_ORDERS FOREIGN KEY (OrderID) REFERENCES ORDERS(Order_No)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE works_in_branchdep(
	Emp_ID INT,
	Branch_ID INT,
	Dep_ID INT,
	CONSTRAINT PK_works_in_branchdep PRIMARY KEY(Emp_ID),
	CONSTRAINT FK_works_in_branchdep_TO_BRANCHID FOREIGN KEY(Branch_ID) REFERENCES Branch(BranchID)
	ON DELETE SET NULL
	ON UPDATE CASCADE
);

CREATE TABLE wishlist_products(
	Customer_ID int,
	Product_ID int
	CONSTRAINT PK_wishlist_products primary key (Customer_ID, Product_ID)
	CONSTRAINT FK_wishlist_products_TO_CLIENT Foreign key (Customer_ID) references CLIENT(ClientID)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	CONSTRAINT FK_wishlist_products_TO_PRODUCT Foreign key (Product_ID) references PRODUCT(ProductID)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);



CREATE TABLE wishlist_vehicle(
	Customer_ID int,
	vehichle_ID int
	CONSTRAINT PK_wishlist_vehicle primary key (Customer_ID, vehichle_ID)
	CONSTRAINT FK_wishlist_vehicle_TO_CLIENT Foreign key (Customer_ID) references CLIENT(ClientID)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	CONSTRAINT FK_wishlist_vehicle_TO_VEHICLE_NO Foreign key (Vehichle_ID) references vehicle(Vehicle_No)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);



CREATE TABLE Cart_products(
	Customer_ID int,
	Product_ID int
	CONSTRAINT PK_Cart_products primary key (Customer_ID, Product_ID)
	CONSTRAINT FK_Cart_products_TO_CLIENTID Foreign key (Customer_ID) references CLIENT(ClientID)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	CONSTRAINT FK_Cart_products_TO_productID Foreign key (Product_ID) references PRODUCT(ProductID)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);





CREATE TABLE Cart_vehicle(
	Customer_ID int,
	vehichle_ID int
	CONSTRAINT PK_Cart_vehicle primary key (Customer_ID, vehichle_ID)
	CONSTRAINT FK_Cart_vehicle_TO_CLIENTID Foreign key (Customer_ID) references CLIENT(ClientID)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	CONSTRAINT FK_Cart_vehicle_TO_Vehicle_No Foreign key (Vehichle_ID) references vehicle(Vehicle_No)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);


create table Client_Posts(
	ClientId INT,
	VehcileId INT,
	CONSTRAINT PK_Client_Posts PRIMARY KEY(ClientId,VehcileId),
	CONSTRAINT FK_Client_Posts_ClientId FOREIGN KEY  (ClientId) REFERENCES CLIENT(ClientID)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	CONSTRAINT FK_Client_Posts_vehicle_no FOREIGN KEY (VehcileId) REFERENCES VEHICLE(Vehicle_No)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

ALTER TABLE VEHICLE
ADD CONSTRAINT SHOW_ROOM_INVEHICLE_TO_SHOWROOM FOREIGN KEY(SHOWROOM) REFERENCES SHOWROOM(Show_Room_Name)
ON DELETE SET NULL
ON UPDATE CASCADE
CREATE TABLE PENDING_POSTS(
	CLIENT_ID INT,
	VEHCILE_ID INT,
	CONSTRAINT PK_PENDING_POSTS PRIMARY KEY(CLIENT_ID,VEHCILE_ID),
	CONSTRAINT FK_PENDING_POSTS_TO_CLIENTID FOREIGN KEY (CLIENT_ID) REFERENCES CLIENT(ClientID)
);


alter table vehicle
add rating int check(rating >=0 and rating <=5)


---------------queries


select 
VEHICLE.CarDescription,
USED_VEHICLE.Posting_Date,
USED_VEHICLE.Price,
VEHICLE.CC_Rnage,
PENDING_POSTS.VEHCILE_ID,
CLIENT.Client_Username,
PENDING_POSTS.CLIENT_ID
from (VEHICLE JOIN USED_VEHICLE ON VEHICLE.Vehicle_No = USED_VEHICLE.Vehicle_ID) 
JOIN PENDING_POSTS  ON VEHICLE.Vehicle_No = PENDING_POSTS.VEHCILE_ID JOIN CLIENT ON CLIENT.ClientID = PENDING_POSTS.CLIENT_ID



select * from PENDING_POSTS

select * from Client_Posts

select * from CLIENT












