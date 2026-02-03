CREATE DATABASE salesdata;
USE salesdata;

CREATE TABLE Sales_Raw (
    OrderID INT,
    OrderDate DATE,
    CustomerName VARCHAR(100),
    CustomerPhone VARCHAR(20),
    CustomerCity VARCHAR(50),
    ProductName VARCHAR(100),
    Quantity INT,
    UnitPrice INT,
    SalesPerson VARCHAR(100)
);

INSERT INTO Sales_Raw VALUES
(101,'2024-01-05','Ravi Kumar','9876543210','Chennai','Laptop',1,55000,'Anitha'),
(101,'2024-01-05','Ravi Kumar','9876543210','Chennai','Mouse',2,500,'Anitha'),
(102,'2024-01-06','Priya Sharma','9123456789','Bangalore','Keyboard',1,1500,'Anitha'),
(102,'2024-01-06','Priya Sharma','9123456789','Bangalore','Mouse',1,500,'Anitha'),
(103,'2024-01-10','Ravi Kumar','9876543210','Chennai','Laptop',1,54000,'Suresh'),
(104,'2024-02-01','John Peter','9988776655','Hyderabad','Monitor',1,12000,'Anitha'),
(104,'2024-02-01','John Peter','9988776655','Hyderabad','Mouse',1,500,'Anitha'),
(105,'2024-02-10','Priya Sharma','9123456789','Bangalore','Laptop',1,56000,'Suresh'),
(105,'2024-02-10','Priya Sharma','9123456789','Bangalore','Keyboard',1,1500,'Suresh');

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(100),
    CustomerPhone VARCHAR(20),
    CustomerCity VARCHAR(50)
);

CREATE TABLE SalesPersons (
    SalesPersonID INT PRIMARY KEY,
    SalesPersonName VARCHAR(100)
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(100),
    UnitPrice INT
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    OrderDate DATE,
    CustomerID INT,
    SalesPersonID INT
);

CREATE TABLE Order_Details (
    OrderID INT,
    ProductID INT,
    Quantity INT,
    PRIMARY KEY (OrderID, ProductID)
);

INSERT INTO Customers VALUES
(1,'Ravi Kumar','9876543210','Chennai'),
(2,'Priya Sharma','9123456789','Bangalore'),
(3,'John Peter','9988776655','Hyderabad');

INSERT INTO SalesPersons VALUES
(1,'Anitha'),
(2,'Suresh');

INSERT INTO Products VALUES
(1,'Laptop',55000),
(2,'Mouse',500),
(3,'Keyboard',1500),
(4,'Monitor',12000);

INSERT INTO Orders VALUES
(101,'2024-01-05',1,1),
(102,'2024-01-06',2,1),
(103,'2024-01-10',1,2),
(104,'2024-02-01',3,1),
(105,'2024-02-10',2,2);

INSERT INTO Order_Details VALUES
(101,1,1),
(101,2,2),
(102,3,1),
(102,2,1),
(103,1,1),
(104,4,1),
(104,2,1),
(105,1,1),
(105,3,1);

SELECT SUM(od.Quantity * p.UnitPrice) AS TotalSales
FROM Orders o
JOIN Order_Details od ON o.OrderID = od.OrderID
JOIN Products p ON od.ProductID = p.ProductID
GROUP BY o.OrderID
ORDER BY TotalSales DESC
LIMIT 1 OFFSET 2;

SELECT sp.SalesPersonName
FROM SalesPersons sp
JOIN Orders o ON sp.SalesPersonID = o.SalesPersonID
JOIN Order_Details od ON o.OrderID = od.OrderID
JOIN Products p ON od.ProductID = p.ProductID
GROUP BY sp.SalesPersonName
HAVING SUM(od.Quantity * p.UnitPrice) > 60000;

SELECT c.CustomerName,
       SUM(od.Quantity * p.UnitPrice) AS TotalSpent
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
JOIN Order_Details od ON o.OrderID = od.OrderID
JOIN Products p ON od.ProductID = p.ProductID
GROUP BY c.CustomerName
HAVING SUM(od.Quantity * p.UnitPrice) >
(
    SELECT AVG(TotalAmount)
    FROM (
        SELECT SUM(od2.Quantity * p2.UnitPrice) AS TotalAmount
        FROM Orders o2
        JOIN Order_Details od2 ON o2.OrderID = od2.OrderID
        JOIN Products p2 ON od2.ProductID = p2.ProductID
        GROUP BY o2.CustomerID
    ) avg_table
);

SELECT 
    UPPER(c.CustomerName) AS CustomerName,
    MONTH(o.OrderDate) AS OrderMonth
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
WHERE o.OrderDate BETWEEN '2024-01-01' AND '2024-01-31';
