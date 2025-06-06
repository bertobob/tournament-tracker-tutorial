using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;



using tournament_tracker.Models;

namespace tournament_tracker.Configuration
{
    public class SqlConnector :IDataConnection
    {
        //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TournamentTrackerDB;Integrated Security=True;";

        public Prize CreatePrize(Prize model) 
        {
            model.Id = 1;
            return model;              
        }

        public List <Person> GetAllPersons()
        {
            List <Person> persons = new List <Person> ();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TournamentTrackerDB;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                string query = "select * From People";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            persons.Add(new Person((int)reader["ID"],reader["FirstName"].ToString(), reader["LastName"].ToString(), reader["EmailAddress"].ToString(), reader["CellphoneNumber"].ToString()));
                        }
                    }
                }

            }
            return persons;
        }

        public List <Team> GetAllTeams()
        {
            List<Team> teams = new();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TournamentTrackerDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "select * From Teams";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            teams.Add(new Team((int)reader["ID"], reader["TeamName"].ToString()));
                        }
                    }
                }

            }
            return teams;
        
        }

        public bool AddPersonToDB(Person person)        // TODO gleiche emailadressen verbieten
                                                        // TODO id an person objekt zuweisen
        {
            try
            {
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TournamentTrackerDB;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                   
                    string query = @"insert into People values (@name,@lastName,@email,@cellphone)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", person.FirstName);
                        command.Parameters.AddWithValue("@LastName", person.LastName);
                        command.Parameters.AddWithValue("@Email", person.EmailAdress);
                        command.Parameters.AddWithValue("@Phone", person.CellphoneNumber);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                }
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("Sqlfehler in AddPersonToDB");
                return false;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Fehler in AddPersonToDB");
                return false;
            }

        }

        public int CreateTeam(string teamName)
        {
            try
            {
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TournamentTrackerDB;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"insert into Teams values (@teamName);
                                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@teamName", teamName);
                        connection.Open();
                        
                        return (int)command.ExecuteScalar();    // gibt die neu erstelle ID zurueck
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB fehler in CreateTeam");
                return -1;
            }
        }

        public bool AddTeamMember(int teamID,int personID)
        {
            try
            {
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TournamentTrackerDB;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"insert into TeamMembers Values(@teamID,@personID)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@teamID", teamID);
                        command.Parameters.AddWithValue("@personID", personID);
                        connection.Open();  
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB Fehler in addTeamMember");
                return false;

            }
        }

        public int AddTournament(string tournamentName,decimal fee)
        {
            try
            {
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TournamentTrackerDB;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"insert into Tournaments values (@tournamentName,@entryFee);
                                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@tournamentName", tournamentName);
                        command.Parameters.AddWithValue("@entryFee", fee);
                        connection.Open();

                        return (int)command.ExecuteScalar();    // gibt die neu erstelle ID zurueck
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB fehler in AddTournament");
                Debug.WriteLine(ex.Message);
                return -1;
            }

        }

        public bool AddTournamentEntries(int tournamentId, int teamId)
        {
            try
            {
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TournamentTrackerDB;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"insert into TournamentEntries values (@tournamentId,@teamId);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@tournamentId", tournamentId);
                        command.Parameters.AddWithValue("@teamId", teamId);
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB fehler in AddTournamentEntries");
                Debug.WriteLine(ex.Message);
                return false;
            }

        }

        public bool AddPrize(int placeNumber, string placeName, decimal prizeAmount)
        {
            try
            {
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TournamentTrackerDB;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"insert into Prizes values (@placeNumber,@placeName,@prizeAmount);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@placeNumber",placeNumber);
                        command.Parameters.AddWithValue("@placeName",placeName);
                        command.Parameters.AddWithValue("@prizeAmount", prizeAmount);
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB fehler in AddPrize");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool AddTournamentPrize(int tournamentId, int prizeId)
        {
            try
            {
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TournamentTrackerDB;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"insert into Prizes values (@placeNumber,@placeName,@prizeAmount);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@tournamentId",tournamentId);
                        command.Parameters.AddWithValue("@prizeId",prizeId);                        
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB fehler in AddTournamentPrize");
                Debug.WriteLine(ex.Message);
                return false;
            }

        }
        public List<Prize> GetAllPrizes()
        {
            List<Prize> prizes=new();
            try
            {
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TournamentTrackerDB;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"select * from Prizes";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                prizes.Add(new Prize((int)reader["id"],(int)reader["PlaceNumber"], reader["PlaceName"].ToString(), (decimal)reader["PrizeAmount"]));
                            }
                        }
                    }

                }
                return prizes;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB fehler in AddPrize");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }

}
