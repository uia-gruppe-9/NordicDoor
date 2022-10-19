
USE nordicdoors;

-- Fjerner constraint for foreign keys så vi kan slette uten error
SET foreign_key_checks = 0;
DROP TABLE IF EXISTS Teams, Employees, UserTeam, Events, History, Suggestions, Admins;
SET foreign_key_checks = 1;

CREATE TABLE Teams (
                       Team_ID int AUTO_INCREMENT,
                       Leader_ID int,
                       Name varchar (60) NOT NULL,
                       PRIMARY KEY (Team_ID)

);

CREATE TABLE Employees (
                           Employee_ID int AUTO_INCREMENT,
                           Team_ID int,
                           Name varchar (60) NOT NULL,
                           Email varchar (60),
                           Password varchar (60) NOT NULL,
                           Role int NOT NULL,
                           PRIMARY KEY (Employee_ID),
                           FOREIGN KEY (Team_ID) REFERENCES Teams (Team_ID)
);


CREATE TABLE UserTeam (
                          Employee_ID int,
                          Team_ID int,
                          FOREIGN KEY (Employee_ID) REFERENCES Employees (Employee_ID),
                          FOREIGN KEY (Team_ID) REFERENCES Teams (Team_ID)
);

CREATE TABLE Admins (
                        Admin_ID int AUTO_INCREMENT,
                        Employee_ID int,
                        PRIMARY KEY (Admin_ID),
                        FOREIGN KEY (Employee_ID) REFERENCES Employees (Employee_ID)
);

CREATE TABLE Suggestions (
                             Suggestion_ID int AUTO_INCREMENT,
                             CreatedBy_ID int,
                             Team_ID int NOT NULL,
                             Responsible_Employee_ID int,
                             Title varchar (50) NOT NULL,
                             CreatedAt DATETIME NOT NULL,
                             Responsible_Team_ID int,
                             Deadline DATETIME,
                             LastUpdatedAt DATETIME,
                             Status varchar (50),
                             Phase varchar (50),
                             Description varchar (100),
                             PRIMARY KEY (Suggestion_ID),
                             FOREIGN KEY (CreatedBy_ID) REFERENCES Employees (Employee_ID),
                             FOREIGN KEY (Team_ID) REFERENCES Teams (Team_ID),
                             FOREIGN KEY (Responsible_Employee_ID) REFERENCES Employees (Employee_ID)
);

CREATE TABLE History (
                         History_ID int AUTO_INCREMENT,
                         Suggestion_ID int,
                         PRIMARY KEY (History_ID),
                         FOREIGN KEY (Suggestion_ID) REFERENCES Suggestions (Suggestion_ID)
);

CREATE TABLE Events (
                        Event_ID int AUTO_INCREMENT,
                        History_ID int,
                        Employee_ID int,
                        Description varchar (100) NOT NULL,
                        Timestamp DATETIME not null,
                        PRIMARY KEY (Event_ID),
                        FOREIGN KEY (History_ID) REFERENCES History (History_ID),
                        FOREIGN KEY (Employee_ID) REFERENCES Employees (Employee_ID)
);

ALTER TABLE Teams
    ADD FOREIGN KEY (Leader_ID) REFERENCES Employees (Employee_ID) ;



INSERT INTO Teams values (
                             1,
                             null,
                             'Salg'
                         );

INSERT INTO Teams values (
                             2,
                             null,
                             'Listing'
                         );

INSERT INTO Teams values (
                             3,
                             null,
                             'Lager'
                         );

insert INTO Employees values (
                                 1,
                                 1,
                                 'Ogundleif Rollgangvolden',
                                 'ogro@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert INTO Employees values (
                                 2,
                                 1,
                                 'Gertrude Huggunbunden',
                                 'gehu@nordicdoors.no',
                                 '1234',
                                 0
                             );
insert INTO Employees values (
                                 3,
                                 1,
                                 'Berthilde Furminken',
                                 'befi@nordicdoors.no',
                                 '1234',
                                 1
                             );
insert INTO Employees values (
                                 4,
                                 2,
                                 'Bertram',
                                 'bert@nordicdoors.no',
                                 '1234',
                                 0
                             );
insert INTO Employees values (
                                 5,
                                 2,
                                 'Martin Hommeland Monsen',
                                 'martinhomo@nordicdoors.no',
                                 '1234',
                                 0
                             );
insert INTO Employees values (
                                 6,
                                 2,
                                 'Jon Karlsson von Schmetterling',
                                 'jonsch@nordicdoors.no',
                                 '1234',
                                 0
                             );
insert INTO Employees values (
                                 7,
                                 2,
                                 'Jèsus Hernández García Sánchez',
                                 'jesus@nordicdoors.no',
                                 '1234',
                                 1
                             );

insert INTO Employees values (
                                 8,
                                 3,
                                 'Fredrik Aspelien',
                                 'racoon@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert INTO Employees values (
                                 9,
                                 3,
                                 'Kjell Terje Ringen',
                                 'Kjell.T.ringen@nordicdoors.no',
                                 '1234',
                                 1
                             );
UPDATE LOW_PRIORITY Teams
SET Leader_ID = 6
WHERE Team_ID = 1;

UPDATE LOW_PRIORITY Teams
SET Leader_ID = 7
WHERE Team_ID = 2;

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
                                 'Nødvendig med en oppgradering på mikrofonene til salgteamet etter negative tilbakemeldinger fra kunder. ');

insert into Suggestions values ( default,
                                 2,
                                 2,
                                 2,
                                 'Nye sikkerhetstiltak',
                                 current_timestamp,
                                 2,
                                 current_timestamp,
                                 current_timestamp,
                                 'test',
                                 'Do',
                                 'Etter flere uhell med spikerpistol, må avdelingen få innført strengere sikkerhetstiltak.');

insert into Suggestions values ( default,
                                 3,
                                 5,
                                 3,
                                 'Ladestasjon til truck',
                                 current_timestamp,
                                 2,
                                 current_timestamp,
                                 current_timestamp,
                                 'pending',
                                 'act',
                                 'Ladestasjonen til truckene bør endre lokasjon til utenfor pauserommet, da avdelingen er late og sure');

insert into Suggestions values ( default,
                                 4,
                                 6,
                                 2,
                                 'Bytte av treverk',
                                 current_timestamp,
                                 2,
                                 current_timestamp,
                                 current_timestamp,
                                 'test',
                                 'phase',
                                 'Foreslår en bytting av treverk fra eik til bjørk ettersom eik suger');

insert into Suggestions values ( default,
                                 5,
                                 1,
                                 1,
                                 'Nettside',
                                 current_timestamp,
                                 1,
                                 current_timestamp,
                                 current_timestamp,
                                 'test',
                                 'phase',
                                 'Etter dårlige salgstall grunnet konkuranse fra NorwayDoors foreslår jeg en ny salgstilnerming');
