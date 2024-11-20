
CREATE DATABASE IF NOT EXISTS ActivityDb;
USE ActivityDb;


CREATE TABLE IF NOT EXISTS Activities (
    ActivityID INT AUTO_INCREMENT PRIMARY KEY,
    PostId INT NOT NULL,
    UserId INT NOT NULL,
    Type INT NOT NULL,
    ActivityTimestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (PostId) REFERENCES PostServiceDb.Posts(PostID),
    FOREIGN KEY (UserId) REFERENCES AccountDb.Accounts(ID)
);

INSERT INTO Activities (PostId, UserId, Type)
VALUES
(1, 1, 1),  -- User 1 liked Post 1
(2, 1, 2),  -- User 1 commented on Post 2
(3, 2, 1);  -- User 2 liked Post 3
