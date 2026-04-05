create database TransportDB;
use TransportDB;

CREATE TABLE Routes (
    RouteID INT PRIMARY KEY IDENTITY(1,1),
    RouteCode NVARCHAR(10) NOT NULL, -- e.g., 'MRT-L1' or 'BUS-01'
    RouteName NVARCHAR(100) NOT NULL,
    OriginStation NVARCHAR(100) NOT NULL,
    DestinationStation NVARCHAR(100) NOT NULL,
    EstimatedDuration NVARCHAR(50), -- e.g., '25 mins'
    TicketPrice DECIMAL(10, 2) NOT NULL, -- Stored as decimal for calculations
    RouteType NVARCHAR(10) CHECK (RouteType IN ('MRT', 'BUS')), -- Used for FR1.5 Color logic
    IsActive BIT DEFAULT 1
);