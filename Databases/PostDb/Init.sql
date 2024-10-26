-- Drop the table if it exists (for clean re-initialization)
DROP TABLE IF EXISTS posts;

-- Create a table to store posts
CREATE TABLE posts (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    content VARCHAR(280) NOT NULL,  -- Twitter posts can be up to 280 characters
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id)  -- Assuming you have a users table
);

INSERT INTO posts (user_id, content) VALUES 
(1, 'This is my first tweet.'),
(1, 'new features on Twitter! #Excited'),
(2, 'Just had a great coffee☕️'),
(1, 'Check out my latest blog post'),
(3, 'upcoming movie releases? #Movies'),
(2, 'A friendly reminder to enjoy life! 🌼');