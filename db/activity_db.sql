-- Create the database
CREATE DATABASE IF NOT EXISTS ActivityDb;

-- Use the newly created database
USE ActivityDb;

-- Create the Activities table
CREATE TABLE IF NOT EXISTS Activities (
    ActivityID INT AUTO_INCREMENT PRIMARY KEY,  -- Unique identifier for each activity
    PostId INT NOT NULL,                        -- ID of the associated post
    UserId INT NOT NULL,                        -- ID of the user performing the activity
    Type INT NOT NULL,                          -- Type of activity (e.g., 1 = Like, 2 = Comment, etc.)
    ActivityTimestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP, -- Timestamp of the activity
    FOREIGN KEY (PostId) REFERENCES PostServiceDb.Posts(PostID), -- Foreign key to Posts table
    FOREIGN KEY (UserId) REFERENCES AccountDb.Accounts(ID)      -- Foreign key to Accounts table
);

-- Example insert of test activities
INSERT INTO Activities (PostId, UserId, Type)
VALUES 
(1, 1, 1),  -- User 1 liked Post 1
(2, 1, 2),  -- User 1 commented on Post 2
(3, 2, 1);  -- User 2 liked Post 3
