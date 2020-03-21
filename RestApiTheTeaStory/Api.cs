using System;
using System.Data.SqlClient;

namespace RestApiTheTeaStory
{
    public class Api
    {
        public enum Tables
        {
            Clients,
            Preferences
        }

        private static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ivan.vasquez\source\repos\TheTeaStory\ConsoleTheTeaStory\Resources\Database1.mdf;Integrated Security=True");

        public static bool InsertToClients(string name, string lastname, string email, string programm)
        {
            var result = false;
            var query = $"INSERT INTO [Clients] values('{name}', '{lastname}', '{email}', '{programm}')";

            int rowsAffected = ExecuteSqlCommand(query);

            if (rowsAffected > 0)
                result = true;

            return result;
        }

        public static bool UpdateClients(int id, string name = "", string lastname = "", string email = "", string program = "")
        {
            var result = false;

            var query = $"UPDATE [Clients] SET firstName = '{name}', lastName = '{lastname}', email = '{email}', programme = '{program}' WHERE idClient = {id}";

            var rowsAffected = ExecuteSqlCommand(query);

            if (rowsAffected > 0)
                result = true;

            return result;
        }

        public static int GetNumberOfRows(Enum nameTable)
        {
            var rows = 0;
            var query = string.Empty;
            SqlCommand cmd = null;

            switch (nameTable)
            {
                case Tables.Clients:
                    query = $"SELECT COUNT(*) FROM Clients";
                    cmd = new SqlCommand(query, con);

                    cmd.Connection.Open();
                    rows = (int)cmd.ExecuteScalar();
                    cmd.Connection.Close();

                    break;

                case Tables.Preferences:
                    query = $"SELECT COUNT(*) FROM Preferences";
                    cmd = new SqlCommand(query, con);

                    cmd.Connection.Open();
                    rows = (int)cmd.ExecuteScalar();
                    cmd.Connection.Close();

                    break;
            }

            return rows;
        }

        public static int GetIdByNumberRow(Tables nameTable, int randomRecord)
        {
            var idRecord = 0;
            var query = string.Empty;

            switch (nameTable)
            {
                case Tables.Clients:
                    query = "SELECT * FROM Clients";
                    idRecord = GetId(query, randomRecord);

                    break;

                case Tables.Preferences:
                    query = "SELECT * FROM Preferences";
                    idRecord = GetId(query, randomRecord);

                    break;

                default:
                    break;
            }

            return idRecord;
        }

        public static bool DeleteClient(int idClient)
        {
            var result = false;
            var queryClient = $"DELETE FROM Clients WHERE idClient = {idClient}";
            var queryPreferences = $"DELETE FROM Preferences WHERE idClient = {idClient}";

            ExecuteSqlCommand(queryPreferences);

            var rowsAffected = ExecuteSqlCommand(queryClient);

            if (rowsAffected > 0)
                result = true;

            return result;
        }

        public static bool PrintValidateNumberRecords(Tables nameTable, int numberRows)
        {
            var result = false;
            var recordsPrinted = 0;

            switch (nameTable)
            {
                case Tables.Clients:
                    recordsPrinted = PrintRecordsFromTable(Tables.Clients);

                    break;

                default:
                    break;
            }

            if (numberRows == recordsPrinted)
                result = true;

            return result;
        }

        public static bool IsNameRecordedInDB(string name)
        {
            var isRecorded = false;
            var query = "SELECT * FROM Clients";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader;

            try
            {
                cmd.Connection.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetString(1).Equals(name))
                    {
                        isRecorded = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"DB connection can't be opened \n\nDetails: {ex.Message}");
            }
            finally
            {
                cmd.Connection.Close();
            }

            return isRecorded;
        }

        public static bool IsTherePreferencesByClientId(int idClient)
        {
            var result = false;
            var query = $"SELECT * FROM Preferences WHERE idClient = {idClient}";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader;
            var counterRecords = 0;

            try
            {
                cmd.Connection.Open();
                reader = cmd.ExecuteReader();

                Console.WriteLine($"Preferences registered by IdClient: {idClient}");
                Console.WriteLine("===============================================");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetString(2)} \n");
                    counterRecords++;
                }
                if (counterRecords == 0)
                    Console.WriteLine("N/A");
            }
            catch (Exception ex)
            {
                throw new Exception($"DB connection can't be opened \n\nDetails: {ex.Message}");
            }
            finally
            {
                cmd.Connection.Close();
            }

            if (counterRecords > 0)
                result = true;

            return result;
        }

        private static int PrintRecordsFromTable(Tables nameTable)
        {
            var totalRecords = 0;
            var query = string.Empty;
            SqlCommand cmd;
            SqlDataReader reader;

            switch (nameTable)
            {
                case Tables.Clients:
                    query = "SELECT * FROM Clients";
                    cmd = new SqlCommand(query, con);

                    try
                    {
                        cmd.Connection.Open();
                        reader = cmd.ExecuteReader();

                        Console.WriteLine("ClientID | \t FirstName | \t LastName | \t Email | \t Programme");
                        Console.WriteLine("=================================================================");

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0].ToString()} | " +
                                $"\t {reader[1].ToString()} | " +
                                $"\t {reader[2].ToString()} | " +
                                $"\t {reader[3].ToString()} | " +
                                $"\t {reader[4].ToString()}");
                            totalRecords++;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"DB connection can't be opened \n\nDetails: {ex.Message}");
                    }
                    finally
                    {
                        cmd.Connection.Close();
                    }
                    
                    break;

                case Tables.Preferences:
                    query = "SELECT * FROM Preferences";
                    cmd = new SqlCommand(query, con);

                    try
                    {
                        cmd.Connection.Open();
                        reader = cmd.ExecuteReader();

                        Console.WriteLine("IdPreference | \t IdClient | \t Name");
                        Console.WriteLine("===============================================");

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0].ToString()} | " +
                                $"\t {reader[1].ToString()} | " +
                                $"\t {reader[2].ToString()} | ");
                            totalRecords++;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"DB connection can't be opened \n\nDetails: {ex.Message}");
                    }
                    finally
                    {
                        cmd.Connection.Close();
                    }

                    break;

                default:
                    break;
            }

            return totalRecords;
        }

        private static int GetId(string query, int numberRow)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            var rowCounter = 1;
            int idRecord = 0;

            try
            {
                cmd.Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (rowCounter == numberRow)
                    {
                        idRecord = Convert.ToInt32(reader[0].ToString());
                        break;
                    }

                    rowCounter++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"DB connection can't be opened \n\nDetails: {ex.Message}");
            }
            finally
            {
                cmd.Connection.Close();
            }

            return idRecord;
        }

        private static int ExecuteSqlCommand(string query)
        {
            var result = 0;
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                cmd.Connection.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"DB connection can't be opened \n\nDetails: {ex.Message}");
            }
            finally
            {
                cmd.Connection.Close();
            }

            return result;
        }
    }
}
