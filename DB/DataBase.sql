-- Step 1: Create the Database
CREATE DATABASE EmployeeManagement;
GO

-- Step 2: Use the Database
USE EmployeeManagement;
GO

-- Step 3: Create the Employees Table
CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY, -- Auto-incrementing primary key
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Department NVARCHAR(50),
    Position NVARCHAR(50), -- Position column
    Address NVARCHAR(255), -- Address column
    Email NVARCHAR(100) UNIQUE, -- Ensure unique email addresses
    PhoneNumber NVARCHAR(15)
);
GO

-- Step 4: Create the Admins Table
CREATE TABLE Admins (
    AdminID INT IDENTITY(1,1) PRIMARY KEY, -- Auto-incrementing primary key
    Username NVARCHAR(50) NOT NULL UNIQUE, -- Unique username
    PasswordHash NVARCHAR(255) NOT NULL, -- Hashed password for security
    Email NVARCHAR(100) UNIQUE, -- Ensure unique email addresses
    Department NVARCHAR(50) -- Optional department association
);
GO

-- Step 5: Insert Sample Data into Employees Table
INSERT INTO Employees (FirstName, LastName, Department, Position, Address, Email, PhoneNumber)
VALUES 
    ('John', 'Doe', 'IT', 'Software Engineer', '123 Elm St, Springfield', 'john.doe@example.com', '123-456-7890'),
    ('Jane', 'Smith', 'HR', 'HR Manager', '456 Maple Ave, Springfield', 'jane.smith@example.com', '234-567-8901'),
    ('Mark', 'Brown', 'Finance', 'Accountant', '789 Oak Rd, Springfield', 'mark.brown@example.com', '345-678-9012'),
    ('Emma', 'Davis', 'Marketing', 'Marketing Specialist', '101 Pine Blvd, Springfield', 'emma.davis@example.com', '456-789-0123');
GO

-- Step 6: Insert Sample Data into Admins Table
INSERT INTO Admins (Username, PasswordHash, Email, Department)
VALUES 
    ('adminIT', '1234567890abcdef', 'admin.it@example.com', 'IT'),
    ('adminHR', 'abcdef1234567890', 'admin.hr@example.com', 'HR'),
    ('adminFinance', 'fedcba0987654321', 'admin.finance@example.com', 'Finance'),
    ('adminMarketing', '0987654321fedcba', 'admin.marketing@example.com', 'Marketing');
GO
