-- Create the database
CREATE DATABASE IF NOT EXISTS PostServiceDb;

-- Use the newly created database
USE PostServiceDb;

-- Create the Posts table
CREATE TABLE IF NOT EXISTS Posts (
    PostID INT AUTO_INCREMENT PRIMARY KEY,     -- Unique identifier for each post
    PostMessage TEXT NOT NULL,                 -- Content of the post
    PostType INT NOT NULL                      -- Type of the post (e.g., 1 = text, 2 = image, etc.)
);

-- Example insert of test posts
INSERT INTO Posts (PostMessage, PostType)
VALUES 
('This is a text post', 1),
('This is an image post', 2),
('This is a video post', 3);
