
USE nordicdoors;

-- UTF-8 enkoding så ÆØÅ støttes
ALTER DATABASE nordicdoors DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Fjerner constraint for foreign keys så vi kan slette uten error
SET foreign_key_checks = 0;
DROP TABLE IF EXISTS Teams, Employees, UserTeams, Events, SuggestionPhase, SuggestionStatus, Suggestions, EmployeeRoles, SuggestionComments, Pictures;
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
                           Password VARCHAR (70) NOT NULL,
                           Is_Admin INT NOT NULL,
                           Is_Disabled INT NOT NULL,
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

CREATE TABLE SuggestionComments (

                                    Comment_ID INT AUTO_INCREMENT,
                                    Employee_ID INT,
                                    Suggestion_ID INT,
                                    Comment VARCHAR (250),
                                    Timestamp DATETIME NOT NULL ,
                                    PRIMARY KEY (Comment_ID),
                                    FOREIGN KEY (Employee_ID) REFERENCES Employees (Employee_ID),
                                    FOREIGN KEY (Suggestion_ID) REFERENCES Suggestions (Suggestion_ID)
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
                                 'Admin',
                                 'Admin@admin.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 1,
                                 0
                             );

insert INTO Employees values (
                                 2,
                                 'Ola Nordmann',
                                 'OlaN@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 1,
                                 0
                             );

insert INTO Employees values (
                                 3,
                                 'Kari Nordmann',
                                 'KariN@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 1,
                                 0
                             );

insert INTO Employees values (
                                 4,
                                 'John Johnson',
                                 'JonJ@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 0,
                                 0
                             );

insert INTO Employees values (
                                 5,
                                 'Morten Harket',
                                 'MortHa@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 0,
                                 0
                             );

insert INTO Employees values (
                                 6,
                                 'Egil Berntsen',
                                 'EgBer@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 0,
                                 0
                             );

insert INTO Employees values (
                                 7,
                                 'Thor Magne Svendsen',
                                 'THMS@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 0,
                                 0
                             );

insert INTO Employees values (
                                 8,
                                 'Eskil Lie',
                                 'EskLi@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 0,
                                 0
                             );

insert INTO Employees values (
                                 9,
                                 'Katrine Amundsen',
                                 'KatAm@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 0,
                                 0
                             );

insert INTO Employees values (
                                 10,
                                 'Susanne Tyri',
                                 'susT@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 0,
                                 0
                             );

insert INTO Employees values (
                                 11,
                                 'Maria Andersen',
                                 'MarA@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 0,
                                 0
                             );
insert INTO Employees values (
                                 12,
                                 'Mia Ås',
                                 'MiaAA@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 0,
                                 0
                             );
insert INTO Employees values (
                                 13,
                                 'Espen Tømmerstigen',
                                 'Espentom@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 0,
                                 0
                             );
insert INTO Employees values (
                                 14,
                                 'Stig Brenner',
                                 'Stigbrenner@nordicdoors.no',
                                 'AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==',
                                 0,
                                 1
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
                                 2,
                                 2,
                                 1,
                                 'Bedre mikrofoner på Support',
                                 '2018-01-03 14:50:50',
                                 2,
                                 current_timestamp,
                                 current_timestamp,
                                 'Ny',
                                 'Study',
                                 'Tilbakemedling fra kunder. ');

insert into Suggestions values ( default,
                                 1,
                                 1,
                                 2,
                                 'Fellesrom: Nytt kjøleskap',
                                 '2016-01-03 14:50:50',
                                 1,
                                 current_timestamp,
                                 current_timestamp,
                                 'Ny',
                                 'Do',
                                 'Installere nytt kjøleskap på fellesrom. ');

insert into Suggestions values ( default,
                                 4,
                                 4,
                                 3,
                                 'Bestille Flere spikere av lengde 2cm',
                                 '2014-01-03 14:50:50',
                                 4,
                                 current_timestamp,
                                 current_timestamp,
                                 'Åpen',
                                 'Do',
                                 'Trenger for å fullføre dør. ');

insert into Suggestions values ( default,
                                 3,
                                 3,
                                 4,
                                 'Bestille nytt Trelim',
                                 '2013-01-03 14:50:50',
                                 3,
                                 current_timestamp,
                                 current_timestamp,
                                 'Åpen',
                                 'Act',
                                 'Tomme for trelim. ');

insert into Suggestions values ( default,
                                 6,
                                 6,
                                 5,
                                 'Bytte karm',
                                 '2012-01-03 14:50:50',
                                 6,
                                 current_timestamp,
                                 current_timestamp,
                                 'Åpen',
                                 'Plan',
                                 'Splint i dørkarm, må gjøres på nytt. ');

insert into Suggestions values ( default,
                                 5,
                                 5,
                                 6,
                                 'Kaste søppel',
                                 '2011-01-03 14:50:50',
                                 5,
                                 current_timestamp,
                                 current_timestamp,
                                 'Åpen',
                                 'Do',
                                 'Kaste søpla på fellesrom. ');
insert into Suggestions values ( default,
                                 8,
                                 8,
                                 7,
                                 'Lage jule arrangement for avdelingen!',
                                 current_timestamp,
                                 8,
                                 current_timestamp,
                                 '2015-11-03',
                                 'Åpen',
                                 'Do',
                                 'Husk å holde secret santa hemmelig!');
insert into Suggestions values ( default,
                                 9,
                                 9,
                                 8,
                                 'Glassdør til kontor 15 er knust. ',
                                 '2017-01-03 14:50:50',
                                 8,
                                 current_timestamp,
                                 current_timestamp,
                                 'Lukket',
                                 'Do',
                                 'Bestille nytt glassmanter, og installere ny dør. ');
insert into Suggestions values ( default,
                                 9,
                                 9,
                                 10,
                                 'Bestille nye skruer av slag "lengde:1.5cm"',
                                 current_timestamp,
                                 10,
                                 current_timestamp,
                                 '2017-01-03 14:50:50',
                                 'Lukket',
                                 'Do',
                                 'Trenger skruer til dørkarm. ');

insert into Suggestions values ( default,
                                 9,
                                 9,
                                 10,
                                 'Fiks dør til kontor',
                                 current_timestamp,
                                 10,
                                 current_timestamp,
                                 '2015-11-03',
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


INSERT INTO SuggestionComments values
    (
        DEFAULT,
        1,
        1,
        'Jeg syntes dette var en veldig god ide!',
        current_timestamp
    );

INSERT INTO SuggestionComments values
    (
        DEFAULT,
        2,
        2,
        'Fint Forslag!',
        current_timestamp
    );

INSERT INTO SuggestionComments values
    (
        DEFAULT,
        3,
        3,
        'Jeg syntes vi skal ha fokus på dette til neste uke!',
        current_timestamp
    );

INSERT INTO SuggestionComments values
    (
        DEFAULT,
        4,
        4,
        'Jeg skal gjøre dette nå!',
        current_timestamp
    );
INSERT INTO SuggestionComments values
    (
        DEFAULT,
        5,
        5,
        'Kan du starte med det idag?',
        current_timestamp
    );
INSERT INTO SuggestionComments values
    (
        DEFAULT,
        6,
        6,
        'Ville vært interessant!',
        current_timestamp
    );
INSERT INTO SuggestionComments values
    (
        DEFAULT,
        7,
        7,
        'Jeg kan sette igang!',
        current_timestamp
    );
INSERT INTO SuggestionComments values
    (
        DEFAULT,
        8,
        8,
        'Kan du be Ola se på dette imorgen?',
        current_timestamp
    );
INSERT INTO SuggestionComments values
    (
        DEFAULT,
        9,
        9,
        'Kan du be Kari se på dette til imorgen?',
        current_timestamp
    );
INSERT INTO SuggestionComments values
    (
        DEFAULT,
        10,
        10,
        'Flott!',
        current_timestamp
    );
<<<<<<< HEAD

INSERT INTO Pictures values
    (
         DEFAULT,
         1,
         1,
         DEFAULT,
         x'89504E470D0A1A0A0000000D494844520000001000000010080200000090916836000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001E49444154384F6350DAE843126220493550F1A80662426C349406472801006AC91F1040F796BD0000000049454E44AE426082'
    );
INSERT INTO Pictures values
    (
        DEFAULT,
        2,
        2,
        DEFAULT,
        x'89504E470D0A1A0A0000000D494844520000001000000010080200000090916836000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001E49444154384F6350DAE843126220493550F1A80662426C349406472801006AC91F1040F796BD0000000049454E44AE426082'
    );
INSERT INTO Pictures values
    (
        DEFAULT,
        3,
        3,
        DEFAULT,
        x'89504E470D0A1A0A0000000D494844520000001000000010080200000090916836000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001E49444154384F6350DAE843126220493550F1A80662426C349406472801006AC91F1040F796BD0000000049454E44AE426082'
    );
INSERT INTO Pictures values
    (
        DEFAULT,
        4,
        4,
        DEFAULT,
        x'89504E470D0A1A0A0000000D494844520000001000000010080200000090916836000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001E49444154384F6350DAE843126220493550F1A80662426C349406472801006AC91F1040F796BD0000000049454E44AE426082'
    );
INSERT INTO Pictures values
    (
        DEFAULT,
        5,
        5,
        DEFAULT,
        x'89504E470D0A1A0A0000000D494844520000001000000010080200000090916836000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001E49444154384F6350DAE843126220493550F1A80662426C349406472801006AC91F1040F796BD0000000049454E44AE426082'
    );
INSERT INTO Pictures values
    (
        DEFAULT,
        6,
        6,
        DEFAULT,
        x'89504E470D0A1A0A0000000D494844520000001000000010080200000090916836000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001E49444154384F6350DAE843126220493550F1A80662426C349406472801006AC91F1040F796BD0000000049454E44AE426082'
    );
INSERT INTO Pictures values
    (
        DEFAULT,
        7,
        7,
        DEFAULT,
        x'89504E470D0A1A0A0000000D494844520000001000000010080200000090916836000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001E49444154384F6350DAE843126220493550F1A80662426C349406472801006AC91F1040F796BD0000000049454E44AE426082'
    );
INSERT INTO Pictures values
    (
        DEFAULT,
        8,
        8,
        DEFAULT,
        x'89504E470D0A1A0A0000000D494844520000001000000010080200000090916836000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001E49444154384F6350DAE843126220493550F1A80662426C349406472801006AC91F1040F796BD0000000049454E44AE426082'
    );
INSERT INTO Pictures values
    (
        DEFAULT,
        9,
        9,
        DEFAULT,
        x'89504E470D0A1A0A0000000D494844520000001000000010080200000090916836000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001E49444154384F6350DAE843126220493550F1A80662426C349406472801006AC91F1040F796BD0000000049454E44AE426082'
    );
INSERT INTO Pictures values
    (
        DEFAULT,
        10,
        10,
        DEFAULT,
        x'89504E470D0A1A0A0000000D494844520000001000000010080200000090916836000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001E49444154384F6350DAE843126220493550F1A80662426C349406472801006AC91F1040F796BD0000000049454E44AE426082'
    );
=======
>>>>>>> main
