CREATE DATABASE IF NOT EXISTS HeavenBooking;
USE HeavenBooking;

CREATE USER 'user'@'localhost' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON HeavenBooking.* TO 'user'@'localhost';
FLUSH PRIVILEGES;

DROP TABLE Users;
CREATE TABLE IF NOT EXISTS Users (
	id VARCHAR(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin PRIMARY KEY,
    user_name VARCHAR(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
    birth_date DATE NOT NULL,
    sex TINYINT NOT NULL,
    passport VARCHAR(20) NOT NULL,
    country_code CHAR(2) NOT NULL,
    account_creation DATE NOT NULL,
    account_status BOOLEAN NOT NULL,
    reservations INT NOT NULL,
    reservations_refunded INT NOT NULL,
	flights INT NOT NULL,
    flights_arrived INT NOT NULL,
    flights_lost INT NOT NULL,
    total_spent DOUBLE NOT NULL
);
    
SELECT COUNT(*) FROM Users;
    
SELECT * FROM INFORMATION_SCHEMA.STATISTICS WHERE TABLE_SCHEMA = 'HeavenBooking';
    
LOAD DATA LOCAL INFILE '/home/andre/Desktop/HeavenBooking/Project/backend/datasets/users.csv'
INTO TABLE Users
FIELDS TERMINATED BY ';'
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
(id, user_name, birth_date, sex, passport, country_code, account_creation, account_status);

INSERT INTO Users(id, user_name, birth_date, sex, passport, country_code, account_creation, account_status)
VALUES ("AndreiaNascimento1944","Andreia-Ariana Nascimento","1984-10-29","1","GY950802","PT","2019-01-06","1");

SHOW CREATE TABLE Users;
SHOW VARIABLES LIKE 'local_infile';
    
SELECT * FROM Users LIMIT 100;
SHOW WARNINGS;

SELECT * FROM Users WHERE id="Mara-BáAntunes854";
    
CREATE INDEX index_name ON Users(user_name);
CREATE INDEX index_account_creation ON Users(account_creation);