
USE nordicdoors;

-- Fjerner constraint for foreign keys så vi kan slette uten error
SET foreign_key_checks = 0;
DROP TABLE IF EXISTS Teams, Employees, UserTeams, Events, SuggestionPhase, SuggestionStatus, Suggestions, EmployeeRoles, Pictures;
SET foreign_key_checks = 1;

CREATE TABLE Teams (
                       Team_ID INT AUTO_INCREMENT,
                       Name VARCHAR (60) NOT NULL UNIQUE,
                       PRIMARY KEY (Team_ID)
);

CREATE TABLE Employees (
                           Employee_ID int AUTO_INCREMENT,
                           Name VARCHAR (60) NOT NULL,
                           Email VARCHAR (60) NOT NULL UNIQUE,
                           Password VARCHAR (60) NOT NULL,
                           Is_Admin INT NOT NULL,
                           PRIMARY KEY (Employee_ID)
);

CREATE TABLE EmployeeRoles (
                               EmpRole VARCHAR (20),
                               PRIMARY KEY (EmpRole)
);
INSERT INTO EmployeeRoles (EmpRole) VALUES ('Medarbeider'), ('Teamleder');

CREATE TABLE UserTeams (
                           Employee_ID INT,
                           Team_ID INT,
                           EmpRole VARCHAR (20) NOT NULL DEFAULT ('Medarbeider'),
                           PRIMARY KEY (Employee_ID, Team_ID),
                           FOREIGN KEY (Employee_ID) REFERENCES Employees (Employee_ID),
                           FOREIGN KEY (Team_ID) REFERENCES Teams (Team_ID),
                           FOREIGN KEY (EmpRole) REFERENCES EmployeeRoles (EmpRole)
);

CREATE TABLE SuggestionStatus (
                                  Status VARCHAR (20),
                                  PRIMARY KEY (Status)
);
INSERT INTO SuggestionStatus (Status) VALUES ('Ny'), ('Åpen'), ('Lukket'), ('Avslått');


CREATE TABLE SuggestionPhase (
                                 Phase VARCHAR (20),
                                 PRIMARY KEY (Phase)
);
INSERT INTO SuggestionPhase (Phase) VALUES ('Plan'), ('Do'), ('Study'), ('Act');

CREATE TABLE Suggestions (
                             Suggestion_ID INT AUTO_INCREMENT,
                             CreatedBy_ID INT NOT NULL,
                             Team_ID INT NOT NULL,
                             Responsible_Employee_ID INT,
                             Title VARCHAR (50) NOT NULL,
                             CreatedAt DATETIME NOT NULL,
                             Responsible_Team_ID INT,
                             Deadline DATETIME,
                             LastUpdatedAt DATETIME,
                             Status VARCHAR (20) NOT NULL,
                             Phase VARCHAR (20) NOT NULL,
                             Description VARCHAR (250),
                             PRIMARY KEY (Suggestion_ID),
                             FOREIGN KEY (CreatedBy_ID) REFERENCES Employees (Employee_ID),
                             FOREIGN KEY (Team_ID) REFERENCES Teams (Team_ID),
                             FOREIGN KEY (Responsible_Employee_ID) REFERENCES Employees (Employee_ID),
                             FOREIGN KEY (Status) REFERENCES SuggestionStatus (Status),
                             FOREIGN KEY (Phase) REFERENCES SuggestionPhase (Phase)

);

CREATE TABLE Events (
                        Event_ID INT AUTO_INCREMENT,
                        Employee_ID INT,
                        Suggestion_ID INT,
                        Description VARCHAR (100) NOT NULL,
                        Timestamp DATETIME NOT NULL,
                        PRIMARY KEY (Event_ID),
                        FOREIGN KEY (Suggestion_ID) REFERENCES Suggestions (Suggestion_ID) ,
                        FOREIGN KEY (Employee_ID) REFERENCES Employees (Employee_ID)
);

CREATE TABLE Pictures (
                          Picture_ID INT AUTO_INCREMENT,
                          Employee_ID INT,
                          Suggestion_ID INT,
                          UploadedAt DATETIME,
                          Image LONGBLOB,
                          PRIMARY KEY (Picture_ID),
                          FOREIGN KEY (Employee_ID) REFERENCES Employees (Employee_ID),
                          FOREIGN KEY (Suggestion_ID) REFERENCES Suggestions (Suggestion_ID)
);

INSERT INTO Teams values (
                             1,
                             'Salg og Marked'

                         );

INSERT INTO Teams values (
                             2,
                             'Produksjon'
                         );

INSERT INTO Teams values (
                             3,
                             'Teknisk-Avdeling'
                         );

INSERT INTO Teams values (
                             4,
                             'Logistikk'
                         );

INSERT INTO Teams values (
                             5,
                             'Produksjonsutvikling'
                         );

INSERT INTO Teams values (
                             6,
                             'IT-Avdeling'
                         );

INSERT INTO Teams values (
                             7,
                             'Økonomi og finans'
                         );

INSERT INTO Teams values (
                             8,
                             'HR-Avdeling'
                         );

INSERT INTO Teams values (
                             9,
                             'Administrasjon'
                         );

INSERT INTO Teams values (
                             10,
                             'Quick lager'
                         );

insert INTO Employees values (
                                 1,
                                 'Ola Nordmann',
                                 'OlaN@nordicdoors.no',
                                 '1234',
                                 1
                             );

insert INTO Employees values (
                                 2,
                                 'Kari Nordmann',
                                 'KariN@nordicdoors.no',
                                 '1234',
                                 1
                             );

insert INTO Employees values (
                                 3,
                                 'John Johnson',
                                 'JonJ@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert INTO Employees values (
                                 4,
                                 'Morten Harket',
                                 'MortHa@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert INTO Employees values (
                                 5,
                                 'Egil Berntsen',
                                 'EgBer@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert INTO Employees values (
                                 6,
                                 'Thor Magne Svendsen',
                                 'THMS@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert INTO Employees values (
                                 7,
                                 'Eskil Lie',
                                 'EskLi@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert INTO Employees values (
                                 8,
                                 'Katrine Amundsen',
                                 'KatAm@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert INTO Employees values (
                                 9,
                                 'Susanne Tyri',
                                 'susT@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert INTO Employees values (
                                 10,
                                 'Maria Andersen',
                                 'MarA@nordicdoors.no',
                                 '1234',
                                 0
                             );
insert INTO Employees values (
                                 11,
                                 'Mia Ås',
                                 'MiaAA@nordicdoors.no',
                                 '1234',
                                 0
                             );
insert INTO Employees values (
                                 12,
                                 'Espen Tømmerstigen',
                                 'Espentom@nordicdoors.no',
                                 '1234',
                                 0
                             );

insert into UserTeams values (
                                 1,
                                 1,
                                 'Medarbeider'
                             );

insert into UserTeams values (
                                 2,
                                 2,
                                 'Teamleder'
                             );

insert into UserTeams values (
                                 3,
                                 3,
                                 'TeamLeder'
                             );

insert into UserTeams values (
                                 4,
                                 4,
                                 'Teamleder'
                             );

insert into UserTeams values (
                                 5,
                                 5,
                                 'Medarbeider'
                             );

insert into UserTeams values (
                                 6,
                                 6,
                                 'Teamleder'
                             );

insert into UserTeams values (
                                 7,
                                 7,
                                 'Teamleder'
                             );

insert into UserTeams values (
                                 8,
                                 8,
                                 'Teamleder'
                             );

insert into UserTeams values (
                                 9,
                                 9,
                                 'Medarbeider'
                             );

insert into UserTeams values (
                                 10,
                                 10,
                                 'Medarbeider'
                             );

insert into UserTeams values (
                                 11,
                                 1,
                                 'Medarbeider'
                             );
insert into UserTeams values (
                                 12,
                                 2,
                                 'Medarbeider'
                             );

insert into Suggestions values ( default,
                                 1,
                                 2,
                                 1,
                                 'Bedre mikrofoner på Support',
                                 current_timestamp,
                                 2,
                                 current_timestamp,
                                 current_timestamp,
                                 'Ny',
                                 'Study',
                                 'Tilbakemedling fra kunder. ');

insert into Suggestions values ( default,
                                 2,
                                 1,
                                 2,
                                 'Fellesrom: Nytt kjøleskap',
                                 current_timestamp,
                                 1,
                                 current_timestamp,
                                 current_timestamp,
                                 'Ny',
                                 'Do',
                                 'Installere nytt kjøleskap på fellesrom. ');

insert into Suggestions values ( default,
                                 3,
                                 2,
                                 3,
                                 'Bestille Flere spikere av lengde 2cm',
                                 current_timestamp,
                                 2,
                                 current_timestamp,
                                 current_timestamp,
                                 'Åpen',
                                 'do',
                                 'Trenger for å fullføre dør. ');

insert into Suggestions values ( default,
                                 4,
                                 4,
                                 4,
                                 'Bestille nytt Trelim',
                                 current_timestamp,
                                 4,
                                 current_timestamp,
                                 current_timestamp,
                                 'Åpen',
                                 'Act',
                                 'Tomme for trelim. ');

insert into Suggestions values ( default,
                                 5,
                                 5,
                                 5,
                                 'Bytte karm',
                                 current_timestamp,
                                 5,
                                 current_timestamp,
                                 current_timestamp,
                                 'Åpen',
                                 'Plan',
                                 'Splint i dørkarm, må gjøres på nytt. ');

insert into Suggestions values ( default,
                                 6,
                                 6,
                                 6,
                                 'Kaste søppel',
                                 current_timestamp,
                                 6,
                                 current_timestamp,
                                 current_timestamp,
                                 'Åpen',
                                 'Do',
                                 'Kaste søpla på fellesrom. ');

insert into Suggestions values ( default,
                                 7,
                                 7,
                                 7,
                                 'Trenger ny spraylakker',
                                 current_timestamp,
                                 7,
                                 current_timestamp,
                                 current_timestamp,
                                 'Åpen',
                                 'Do',
                                 'Spraylakker slutta å fungere. ');

insert into Suggestions values ( default,
                                 8,
                                 8,
                                 8,
                                 'Bare 1 dorull igjen',
                                 current_timestamp,
                                 8,
                                 current_timestamp,
                                 current_timestamp,
                                 'Åpen',
                                 'Do',
                                 'Trenger mer dopapir til do 2. ');

insert into Suggestions values ( default,
                                 9,
                                 9,
                                 9,
                                 'Bestille nye skruer av slag "lengde:1.5cm"',
                                 current_timestamp,
                                 9,
                                 current_timestamp,
                                 current_timestamp,
                                 'Åpen',
                                 'Do',
                                 'Trenger skruer til dørkarm. ');

insert into Suggestions values ( default,
                                 10,
                                 10,
                                 10,
                                 'Fiks dør til kontor',
                                 current_timestamp,
                                 10,
                                 current_timestamp,
                                 current_timestamp,
                                 'Lukket',
                                 'Do',
                                 'Dør til kontor vil ikke lukkes. ');

insert into Events values (
                              DEFAULT,
                              1,
                              1,
                              'Kommentar: Får mikrofoner tilsendt.',
                              current_timestamp );

insert into Events values (
                              DEFAULT,
                              2,
                              2,
                              'Kommentar: Fant ingen feil i data tekst.',
                              current_timestamp );

insert into Events values (
                              DEFAULT,
                              3,
                              3,
                              'Kommentar: Spikere skal komme i løpet av noen få strakser.',
                              current_timestamp );

insert into Events values (
                              DEFAULT,
                              4,
                              4,
                              'Lukket Forbedringsforslag: Fant trelim gjemt.',
                              current_timestamp );

insert into Events values (
                              DEFAULT,
                              5,
                              5,
                              'Lukket Forbedringsforlag: Kunne sandpapires bort',
                              current_timestamp );

insert into Events values (
                              DEFAULT,
                              6,
                              6,
                              'Opprettet Forbedrinsforslag: Kaste søppel på fellesrom.',
                              current_timestamp );

insert into Events values (
                              DEFAULT,
                              7,
                              7,
                              'Kommentar: Fikk rensa den.',
                              current_timestamp );

insert into Events values (
                              DEFAULT,
                              8,
                              8,
                              'Lukket Forbedringsforslag: Kjøpte med dopapir i pausen.',
                              current_timestamp );

insert into Events values (
                              DEFAULT,
                              9,
                              9,
                              'Lukket Forbedringsforslag: Bestille nye skruer av slag "lengde:1.5cm"',
                              current_timestamp );

insert into Events values (
                              DEFAULT,
                              10,
                              10,
                              'Opprettet Forbedringsforslag: Fiks dør til kontor.',
                              current_timestamp );


INSERT INTO Pictures VALUES (
                             DEFAULT,
                             1,
                             1,
                             CURRENT_TIME,
                             LOAD_FILE('C:/Users/starm/Downloads/Test.GIF')


                            )