CREATE DATABASE IF NOT EXISTS PostServiceDb;
USE PostServiceDb;

DROP TABLE IF EXISTS Posts;
CREATE TABLE IF NOT EXISTS Posts (
    PostID INT AUTO_INCREMENT PRIMARY KEY,
    AccountId INT NOT NULL,
    PostMessage TEXT NOT NULL, 
    PostType INT NOT NULL,
    FOREIGN KEY (AccountId) REFERENCES Accounts(ID)
);

INSERT INTO Posts (PostMessage, PostType, AccountId) VALUES 
('This is a text post', 1, 1),
('This is an image post', 2, 1),
('This is a video post', 3, 1);
