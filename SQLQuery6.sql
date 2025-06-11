create table Matchup (
id int identity(1,1) primary key not null,
tournamentId int not null,
teamAId int  null,
teamBId int null,
teamAScore int null,
teamBScore int null,
roundNumber int null,
winnerID int null,
foreign key (tournamentId) references Tournaments(id),
foreign key (teamAId) references Teams(id),
foreign key (teamBId) references Teams(id)
);


