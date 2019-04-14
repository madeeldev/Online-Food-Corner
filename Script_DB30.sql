USE [DB30]
GO

/****** Object:  Table Customers ******/
CREATE TABLE Customers(
	customer_id int IDENTITY(1,1) NOT NULL,
	customer_name varchar(255) NOT NULL,
	customer_address varchar(255) NOT NULL,
	cell_no varchar(255) NOT NULL,
	customer_email varchar(255) NOT NULL,
	customer_order_id int NOT NULL
	CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
	(
		customer_id ASC
	)
)
/****** Object:  Table Orders ******/
CREATE TABLE Orders(
	order_id int IDENTITY(1, 1) NOT NULL,
	customer_id int NOT NULL,
	total_price int NOT NULL,
	timing DATETIME NOT NULL,
	delivery_mode varchar(10) NOT NULL
	CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED
	(
		order_id ASC
	)
)
ALTER TABLE Orders ADD order_date DATETIME NOT NULL

/****** Object:  Table Products ******/
CREATE TABLE Products(
	product_id int IDENTITY(1, 1) NOT NULL,
	product_name varchar(255) NOT NULL
	CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED
	(
		Product_id ASC
	)
)
GO
ALTER TABLE Products ADD product_type varchar(255) NOT NULL
ALTER TABLE Products ADD product_Price int NOT NULL
ALTER TABLE Products ADD product_picture image NOT NULL
ALTER TABLE Products ADD product_quantity int NOT NULL
GO
/****** Object:  Table Workers ******/
CREATE TABLE Workers(
	worker_id int IDENTITY(1, 1) NOT NULL,
	worker_name varchar(255) NOT NULL,
	salary int NOT NULL,
	worker_status varchar(10) NOT NULL
	CONSTRAINT [PK_Workers] PRIMARY KEY CLUSTERED
	(
		worker_id ASC
	)
)
ALTER TABLE Workers ADD order_id int NOT NULL
/****** Object:  Table Delivery ******/
CREATE TABLE Delivery(
	delivery_id int IDENTITY(1, 1) NOT NULL,
	order_id int NOT NULL,
	delivery_status int NOT NULL,
	CONSTRAINT [PK_Delivery] PRIMARY KEY CLUSTERED
	(
		delivery_id ASC
	)
)

/****** Object:  Table Admin ******/
CREATE TABLE Admin(
	admin_id int NOT NULL,
	email varchar(255) NOT NULL,
	admin_password varchar(255) NOT NULL,
	CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED
	(
		admin_id ASC
	)
)
/****** Object:  ForeignKey [FK_Customer_Order] ******/
ALTER TABLE Customers ADD CONSTRAINT FK_Customer_Order FOREIGN KEY (customer_order_id) REFERENCES Orders(order_id)

/****** Object:  ForeignKey [FK_Order_Customer] ******/
ALTER TABLE Orders ADD CONSTRAINT FK_Order_Customer FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)

ALTER TABLE Customers ADD product_id int;

ALTER TABLE Orders ADD product_id int;

/****** Object:  ForeignKey [FK_Customer_Product] ******/
ALTER TABLE Customers ADD CONSTRAINT FK_Customer_Product FOREIGN KEY (product_id) REFERENCES Products(product_id)

/****** Object:  ForeignKey [FK_Order_Product] ******/
ALTER TABLE Orders ADD CONSTRAINT FK_Order_Product FOREIGN KEY (product_id) REFERENCES Products(product_id)

/****** Object:  ForeignKey [FK_Worker_Order] ******/
ALTER TABLE Workers ADD CONSTRAINT FK_Worker_Order FOREIGN KEY (order_id) REFERENCES Orders(order_id)

/****** Object:  ForeignKey [FK_Delivery_Order] ******/
ALTER TABLE Delivery ADD CONSTRAINT FK_Delivery_Order FOREIGN KEY (order_id) REFERENCES Orders(order_id)

/****** Object:  ForeignKey [FK_Admin_Products] ******/
ALTER TABLE Admin ADD product_id int NOT NULL
ALTER TABLE Admin ADD CONSTRAINT FK_Admin_Product FOREIGN KEY (product_id) REFERENCES Products(product_id)

/****** Object:  ForeignKey [FK_Delivery_Order] ******/
ALTER TABLE Admin ADD order_id int NOT NULL
ALTER TABLE Admin ADD CONSTRAINT FK_Admin_Order FOREIGN KEY (order_id) REFERENCES Orders(order_id)

/****** Object:  ForeignKey [FK_Delivery_Order] ******/
ALTER TABLE Admin ADD delivery_id int NOT NULL
ALTER TABLE Admin ADD CONSTRAINT FK_Admin_Delivery FOREIGN KEY (delivery_id) REFERENCES Delivery(delivery_id)

