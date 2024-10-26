-- Create a table
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL
);

-- Insert sample data
INSERT INTO users (username, password) VALUES ('user1', 'password1');
INSERT INTO users (username, password) VALUES ('user2', 'password2');
