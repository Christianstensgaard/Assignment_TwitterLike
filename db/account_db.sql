CREATE DATABASE IF NOT EXISTS AccountDb;
USE AccountDb;
CREATE TABLE IF NOT EXISTS Accounts (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(50) NOT NULL,
    Activity int NOT NULL
);
INSERT INTO Accounts (Username, Password)
VALUES ('testuser', 'hashed_password',0);
VALUES ('testuser', 'hashed_password',0);
