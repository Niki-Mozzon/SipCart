-- Create database
CREATE DATABASE [app];
GO

USE [app];
GO

-- Create tables
CREATE TABLE [drinks] (
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Name] VARCHAR(50) NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
);

CREATE TABLE [Coupons] (
    [Code] VARCHAR(10) PRIMARY KEY,
    [PercentageReduction] DECIMAL CHECK ([PercentageReduction] BETWEEN 0 AND 100) NOT NULL,
);

CREATE TABLE [orders] (
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [CouponCode]VARCHAR(10),
    [TotalPrice] DECIMAL(18,2) NOT NULL,
    [PaymentMethod] VARCHAR(4) NOT NULL,
    FOREIGN KEY ([CouponCode]) REFERENCES [Coupons]([Code])
);

-- Insert test data
INSERT INTO [drinks] ([Name], [Price]) 
VALUES 
( 'American Coffee',1.5),
( 'Italian Coffee',1.99),
( 'Chocolate',3),
( 'Tea',5)
;

INSERT INTO [Coupons] ([Code], [PercentageReduction]) 
VALUES 
('ABC', 50),
('XYZ', 20)
;