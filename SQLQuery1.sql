CREATE TABLE People (
    id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    EmailAddress NVARCHAR(100),
    CellphoneNumber NVARCHAR(20)
);

CREATE TABLE Teams (
    id INT PRIMARY KEY IDENTITY(1,1),
    TeamName NVARCHAR(100)
);

CREATE TABLE TeamMembers (
    id INT PRIMARY KEY IDENTITY(1,1),
    TeamId INT,
    PersonId INT,
    FOREIGN KEY (TeamId) REFERENCES Teams(id),
    FOREIGN KEY (PersonId) REFERENCES People(id)
);

CREATE TABLE Prizes (
    id INT PRIMARY KEY IDENTITY(1,1),
    PlaceNumber INT,
    PlaceName NVARCHAR(50),
    PrizeAmount DECIMAL(10,2),
    PrizePercentage DECIMAL(5,2)
);

CREATE TABLE Tournaments (
    id INT PRIMARY KEY IDENTITY(1,1),
    TournamentName NVARCHAR(100),
    EntryFee DECIMAL(10,2)
);

CREATE TABLE TournamentEntries (
    id INT PRIMARY KEY IDENTITY(1,1),
    TournamentId INT,
    TeamId INT,
    FOREIGN KEY (TournamentId) REFERENCES Tournaments(id),
    FOREIGN KEY (TeamId) REFERENCES Teams(id)
);

CREATE TABLE TournamentPrizes (
    id INT PRIMARY KEY IDENTITY(1,1),
    TournamentId INT,
    PrizeId INT,
    FOREIGN KEY (TournamentId) REFERENCES Tournaments(id),
    FOREIGN KEY (PrizeId) REFERENCES Prizes(id)
);

CREATE TABLE Matchups (
    id INT PRIMARY KEY IDENTITY(1,1),
    WinnerId INT,
    MatchupRound INT,
    FOREIGN KEY (WinnerId) REFERENCES Teams(id)
);

CREATE TABLE MatchupEntries (
    id INT PRIMARY KEY IDENTITY(1,1),
    MatchupId INT,
    ParentMatchupId INT NULL,
    TeamCompetingId INT,
    Score DECIMAL(5,2),
    FOREIGN KEY (MatchupId) REFERENCES Matchups(id),
    FOREIGN KEY (ParentMatchupId) REFERENCES Matchups(id),
    FOREIGN KEY (TeamCompetingId) REFERENCES Teams(id)
);
