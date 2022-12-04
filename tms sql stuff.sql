/*CREATE TABLE New_Orders
(
	newOrderID INT NOT NULL AUTO_INCREMENT,
	clientName VARCHAR (25),
	jobType INT,
	quantity INT,
	origin VARCHAR (50),
	destination VARCHAR (50),
	vanType INT,
	PRIMARY KEY (newOrderID)
);

CREATE TABLE Process_Orders
(
	OrderID INT NOT NULL AUTO_INCREMENT,
	clientName VARCHAR (25),
	jobType INT,
	quantity INT,
	origin VARCHAR (50),
	destination VARCHAR (50),
	vanType INT,
    carrierTotal DECIMAL (10,2),
    numOfTrips INT,
	PRIMARY KEY (OrderID)
    
);

CREATE TABLE Carriers
(
    carrierID INT NOT NULL AUTO_INCREMENT,
    cName VARCHAR(50),
    dCity VARCHAR(50),
    FTLA INT,
    LTLA INT,
    FTLRate DECIMAL (10, 2),
    LTLRate DECIMAL (10, 4),
    reefCharge DECIMAL (10, 3),
    PRIMARY KEY (carrierID)
);
INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('Planet Express', 'Windsor', 50, 640, '5.21', '0.3621', '0.08');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('Planet Express', 'Hamilton', 50, 640, '5.21', '0.3621', '0.08');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('Planet Express', 'Oshawa',    50, 640, '5.21', '0.3621', '0.08');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('Planet Express', 'Belleville', 50, 640, '5.21', '0.3621', '0.08');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('Planet Express', 'Ottawa', 50, 640, '5.21', '0.3621', '0.08');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('Schooner', 'London', 18, 98, '5.05', '0.3434', '0.07');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('Schooner', 'Toronto', 18, 98, '5.05', '0.3434', '0.07');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('Schooner', 'Kingston', 18, 98, '5.05', '0.3434', '0.07');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('Tillman Transport', 'Windsor', 24, 35, '5.11', '0.3012', '0.09');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('Tillman Transport', 'London', 18, 45, '5.11', '0.3012', '0.09');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('Tillman Transport', 'Hamilton', 18, 45, '5.11', '0.3012', '0.09');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES ('We Haul', 'Ottawa', 11, 0, '5.2', 0, '0.065');

INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge)
VALUES    ('We Haul', 'Toronto', 11, 0, '5.2', 0, '0.065');*/


