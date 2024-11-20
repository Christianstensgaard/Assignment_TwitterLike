CREATE DATABASE IF NOT EXISTS PostServiceDb;
USE PostServiceDb;
CREATE TABLE IF NOT EXISTS Posts (
    PostID INT AUTO_INCREMENT PRIMARY KEY,   
    PostMessage TEXT NOT NULL, 
    PostType INT NOT NULL
);
INSERT INTO Posts (PostMessage, PostType)
VALUES 
('This is a text post', 1),
('This is an image post', 2),
('This is a video post', 3);
