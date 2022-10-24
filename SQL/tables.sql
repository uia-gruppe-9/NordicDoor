
USE nordicdoors;

-- Fjerner constraint for foreign keys så vi kan slette uten error
SET foreign_key_checks = 0;
DROP TABLE IF EXISTS Teams, Employees, UserTeam, Events, History, Suggestions, Admins;
SET foreign_key_checks = 1;

CREATE TABLE Teams (
                       Team_ID int AUTO_INCREMENT,
                       Name varchar (60) NOT NULL,
                       PRIMARY KEY (Team_ID)
);

CREATE TABLE Employees (
                           Employee_ID int AUTO_INCREMENT,
                           Name varchar (60) NOT NULL,
                           Email varchar (60) NOT NULL,
                           Password varchar (60) NOT NULL,
                           Is_Admin int NOT NULL,
                           PRIMARY KEY (Employee_ID)
);


CREATE TABLE UserTeam (
                          Employee_ID int,
                          Team_ID int,
                          Role int NOT NULL,
                          PRIMARY KEY (Employee_ID, Team_ID),
                          FOREIGN KEY (Employee_ID) REFERENCES Employees (Employee_ID),
                          FOREIGN KEY (Team_ID) REFERENCES Teams (Team_ID)
);

CREATE TABLE Suggestions (
                             Suggestion_ID int AUTO_INCREMENT,
                             CreatedBy_ID int NOT NULL,
                             Team_ID int NOT NULL,
                             Responsible_Employee_ID int,
                             Title varchar (50) NOT NULL,
                             CreatedAt DATETIME NOT NULL,
                             Responsible_Team_ID int,
                             Deadline DATETIME,
                             LastUpdatedAt DATETIME,
                             Status varchar (50),
                             Phase varchar (50),
                             Description varchar (250),
                             PRIMARY KEY (Suggestion_ID),
                             FOREIGN KEY (CreatedBy_ID) REFERENCES Employees (Employee_ID),
                             FOREIGN KEY (Team_ID) REFERENCES Teams (Team_ID),
                             FOREIGN KEY (Responsible_Employee_ID) REFERENCES Employees (Employee_ID)
);

CREATE TABLE Events (
                        Event_ID int AUTO_INCREMENT,
                        Employee_ID int,
                        Suggestion_ID int,
                        Description varchar (100) NOT NULL,
                        Timestamp DATETIME not null,
                        PRIMARY KEY (Event_ID),
                        FOREIGN KEY (Suggestion_ID) REFERENCES Suggestions (Suggestion_ID) ,
                        FOREIGN KEY (Employee_ID) REFERENCES Employees (Employee_ID)
);



INSERT INTO Teams values (
                             1,
                             'Salg'
                         );

INSERT INTO Teams values (
                             2,
                             'Maling'
                         );

INSERT INTO Teams values (
                             3,
                             'Konstruksjon'
                         );

insert INTO Employees values (
                                 1,
                                 'Ogundleif Rollgangvolden',
                                 'ogro@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert INTO Employees values (
                                 2,
                                 'Ogundleif Rollgangvolden',
                                 'ogro@nordicdoors.no',
                                 '1234',
                                 1
                             );

insert INTO Employees values (
                                 3,
                                 'Ogundleif Rollgangvolden',
                                 'ogro@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert into Suggestions values ( default,
                                 1,
                                 1,
                                 1,
                                 'Bedre mikrofoner',
                                 current_timestamp,
                                 1,
                                 current_timestamp,
                                 current_timestamp,
                                 'new',
                                 'plan',
                                 'Nødvendiger fra kunder. ');

insert into Suggestions values ( default,
                                 2,
                                 2,
                                 2,
                                 'tester data',
                                 current_timestamp,
                                 2,
                                 current_timestamp,
                                 current_timestamp,
                                 'pending',
                                 'do',
                                 'Teste data teskt. ');

insert into Suggestions values ( default,
                                 3,
                                 3,
                                 3,
                                 'tester data',
                                 current_timestamp,
                                 3,
                                 current_timestamp,
                                 current_timestamp,
                                 'pending',
                                 'do',
                                 'Teste data teskt. ');

insert into UserTeam values (1,1,1);


