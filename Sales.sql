
CREATE DATABASE Sales;
GO


USE Sales;
GO


CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50)
);
GO


CREATE TABLE Sellers (
    SellerId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50)
);
GO


CREATE TABLE Sales (
    SaleId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    SellerId INT,
    SaleAmount DECIMAL(10, 2),
    SaleDate DATE,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    FOREIGN KEY (SellerId) REFERENCES Sellers(SellerId)
);
GO


INSERT INTO Customers (FirstName, LastName) VALUES ('John', 'Doe');
INSERT INTO Customers (FirstName, LastName) VALUES ('Jane', 'Smith');
GO


INSERT INTO Sellers (FirstName, LastName) VALUES ('Alice', 'Johnson');
INSERT INTO Sellers (FirstName, LastName) VALUES ('Bob', 'Brown');
GO


INSERT INTO Customers (FirstName, LastName) VALUES ('Michael', 'Scott');
INSERT INTO Customers (FirstName, LastName) VALUES ('Pam', 'Beesly');
INSERT INTO Customers (FirstName, LastName) VALUES ('Jim', 'Halpert');
INSERT INTO Customers (FirstName, LastName) VALUES ('Dwight', 'Schrute');
GO


INSERT INTO Sellers (FirstName, LastName) VALUES ('Phyllis', 'Vance');
INSERT INTO Sellers (FirstName, LastName) VALUES ('Stanley', 'Hudson');
INSERT INTO Sellers (FirstName, LastName) VALUES ('Andy', 'Bernard');
INSERT INTO Sellers (FirstName, LastName) VALUES ('Oscar', 'Martinez');
GO


INSERT INTO Sales (CustomerId, SellerId, SaleAmount, SaleDate) VALUES (1, 1, 200.00, '2023-02-01');
INSERT INTO Sales (CustomerId, SellerId, SaleAmount, SaleDate) VALUES (2, 2, 250.00, '2023-02-02');
INSERT INTO Sales (CustomerId, SellerId, SaleAmount, SaleDate) VALUES (3, 3, 300.00, '2023-02-03');
INSERT INTO Sales (CustomerId, SellerId, SaleAmount, SaleDate) VALUES (4, 4, 350.00, '2023-02-04');
INSERT INTO Sales (CustomerId, SellerId, SaleAmount, SaleDate) VALUES (5, 1, 400.00, '2023-02-05');
INSERT INTO Sales (CustomerId, SellerId, SaleAmount, SaleDate) VALUES (6, 2, 450.00, '2023-02-06');
INSERT INTO Sales (CustomerId, SellerId, SaleAmount, SaleDate) VALUES (1, 3, 500.00, '2023-02-07');
INSERT INTO Sales (CustomerId, SellerId, SaleAmount, SaleDate) VALUES (2, 4, 550.00, '2023-02-08');
INSERT INTO Sales (CustomerId, SellerId, SaleAmount, SaleDate) VALUES (3, 1, 600.00, '2023-02-09');
INSERT INTO Sales (CustomerId, SellerId, SaleAmount, SaleDate) VALUES (4, 2, 650.00, '2023-02-10');
GO


