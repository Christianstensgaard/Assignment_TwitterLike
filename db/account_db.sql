-- Create the database
CREATE DATABASE IF NOT EXISTS AccountDb;

-- Use the newly created database
USE AccountDb;

-- Create the Accounts table
CREATE TABLE IF NOT EXISTS Accounts (
    ID INT AUTO_INCREMENT PRIMARY KEY,          -- Unique identifier for each account
    Username VARCHAR(50) NOT NULL UNIQUE,       -- Username (unique and not null)
    Password VARCHAR(255) NOT NULL,              -- Password (hashed and not null)
    Activity int NOT NULL
);

INSERT INTO Accounts (Username, Password)
VALUES ('testuser', 'hashed_password',0);
VALUES ('testuser', 'hashed_password',0);
