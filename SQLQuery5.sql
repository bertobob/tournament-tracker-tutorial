create table MatchupEntries(
id int identity(1,1) primary key not null,
matchupId int,
roundNumber int,
winnerID int,
foreign key (matchupId) references matchup(id),
foreign key (winnerID) references Teams(id)
);
