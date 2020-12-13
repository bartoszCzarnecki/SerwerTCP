CREATE database UsersDB;
USE UsersDB;
CREATE TABLE Users(
id BINARY(16) PRIMARY KEY,
login VARCHAR(30),
password VARCHAR(30),
createdOn datetime
);

INSERT INTO Users (id,login,password, createdOn) 
VALUES (UUID_TO_BIN(UUID()),'admin','admin', NOW() + INTERVAL 1 HOUR),
(UUID_TO_BIN(UUID()),'user1','pass1', NOW() + INTERVAL 1 HOUR),
(UUID_TO_BIN(UUID()),'user2','pass2', NOW() + INTERVAL 1 HOUR),
(UUID_TO_BIN(UUID()),'user3','password', NOW() + INTERVAL 1 HOUR);
