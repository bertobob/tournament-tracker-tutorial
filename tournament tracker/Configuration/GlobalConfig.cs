using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tournament_tracker.Models;

namespace tournament_tracker.Configuration
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections {  get;private set; } = new List<IDataConnection>();
        public static List<Person> Persons { get; private set; } = new(); // <-- global
        public static List<Team> Teams { get; private set; } = new();
        public static List<Prize> Prizes { get; set; } = new();
        public static void InitializeConnections(bool database, bool textFiles)
        {
            if (database)
            {
                SqlConnector sql = new SqlConnector();
                Connections.Add(sql);   
            }
            if (textFiles)
            {
                TextConnector textConnector = new TextConnector();
                Connections.Add(textConnector);
            }
            Persons = Connections[0].GetAllPersons();
            Teams = Connections[0].GetAllTeams();
            Prizes = Connections[0].GetAllPrizes();

    }
    }
}
