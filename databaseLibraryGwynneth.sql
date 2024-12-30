/*==============================================================*/
/* DBMS name:      Sybase SQL Anywhere 12                       */
/* Created on:     30/12/2024 15:23:16                          */
/*==============================================================*/
drop database lps_library;
create database lps_library;
use lps_library;
/*==============================================================*/
/* Table: BOOK                                                  */
/*==============================================================*/
create table BOOK 
(
   ID_BOOK              varchar(6)                     primary key not null,
   NAME_BOOK            varchar(100)                   not null,
   GENRE_BOOK           varchar(50)                    not null,
   AUTHOR_BOOK          varchar(50)                    not null,
   STATUS_BOOK          integer                        not null default 0,
   DELETE_BOOK          integer                        not null default 0
);

/*==============================================================*/
/* Table: LOAN                                                  */
/*==============================================================*/
create table LOAN 
(
   ID_LOAN              integer                        primary key not null auto_increment,
   ID_MEMBER            varchar(6)                     not null references member(id_member),
   ID_BOOK              varchar(6)                     not null references book(id_book),
   DATEBORROWED_LOAN    date                           not null,
   DUEDATE_LOAN         date                           not null,
   DATERETURN_LOAN      date                           null,
   DELETE_LOAN          integer                        not null default 0
);

/*==============================================================*/
/* Table: MEMBER                                                */
/*==============================================================*/
create table MEMBER 
(
   ID_MEMBER            varchar(6)                     primary key not null,
   NAMA_MEMBER          varchar(50)                    not null,
   EMAIL_MEMBER         varchar(50)                    not null,
   DELETE_MEMBER        integer                        not null default 0
);


/*==============================================================*/
/* Table: STAFF                                                 */
/*==============================================================*/
create table STAFF 
(
   ID_STAFF             varchar(6)                     primary key not null,
   NAMA_STAFF           varchar(50)                    not null,
   PASSWORD_STAFF       varchar(100)                   not null,
   DELETE_STAFF         integer                        not null default 0
);
/*==============================================================*/
/*==============================================================*/
/* trigger                                                     */
/*==============================================================*/
DELIMITER $$
CREATE TRIGGER tAutoGenerateBookID
BEFORE INSERT ON BOOK
FOR EACH ROW
BEGIN
    DECLARE nextID INT;
    DECLARE newID VARCHAR(6);

    SELECT IFNULL(MAX(CAST(SUBSTRING(ID_BOOK, 2) AS UNSIGNED)), 0) + 1 
    INTO nextID
    FROM BOOK;

    SET newID = CONCAT('B', LPAD(nextID, 5, '0'));

    SET NEW.ID_BOOK = newID;
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER tAutoGenerateMemberID
BEFORE INSERT ON `MEMBER`
FOR EACH ROW
BEGIN
    DECLARE nextID INT;
    DECLARE newID VARCHAR(6);

    SELECT IFNULL(MAX(CAST(SUBSTRING(id_member, 2) AS UNSIGNED)), 0) + 1 
    INTO nextID
    FROM `MEMBER`;

    SET newID = CONCAT('M', LPAD(nextID, 5, '0'));

    SET NEW.id_member = newID;
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER tAutoGenerateStaffID
BEFORE INSERT ON STAFF
FOR EACH ROW
BEGIN
    DECLARE nextID INT;
    DECLARE newID VARCHAR(6);

    SELECT IFNULL(MAX(CAST(SUBSTRING(id_staff, 2) AS UNSIGNED)), 0) + 1 
    INTO nextID
    FROM STAFF;

    SET newID = CONCAT('M', LPAD(nextID, 5, '0'));

    SET NEW.id_staff = newID;
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER tSetDueDate
BEFORE INSERT ON LOAN
FOR EACH ROW
BEGIN
   SET NEW.DUEDATE_LOAN = DATE_ADD(NEW.DATEBORROWED_LOAN, INTERVAL 7 DAY);
END$$

DELIMITER ;
/*==============================================================*/
/* insert dummy                                                 */
/*==============================================================*/

insert into book(name_book, genre_book, author_book) values ('The Hunger Games','Fiction','Suzanne Collins');
insert into book(name_book, genre_book, author_book) values ('Pride and Prejudice','Romance','Jane');

insert into `member`(nama_member, email_member) values ("Roni", "roni@yahoo.com");

insert into staff(nama_staff, password_staff) values ("Gwen", "123");