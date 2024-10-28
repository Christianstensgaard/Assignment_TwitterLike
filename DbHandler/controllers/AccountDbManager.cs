using MySql.Data.MySqlClient;
using Service;

namespace DbHandler.controllers
{
    public class AccountDbManager : ServiceFunction
    {
        readonly string connectionString = "Server=localhost;Database=your_database;User ID=your_user;Password=your_password;";

        public override void OnInit(FunctionConfig config)
        {
            config.FunctionName = "CreateAccount";
        }

        public override void OnRequest()
        {
            Console.WriteLine("Creating Account...");

            string username = "user3";
            string password = "password3";//should be hash

            // Attempt to save the account to the database
            bool accountCreated = SaveAccountToDatabase(username, password);

            if (accountCreated)
            {
                ToolBox.Request(RequestState.Finish);
            }
            else
            {
                ToolBox.Request(RequestState.Error);
            }

            ToolBox.Request("databaseService", "CreateAccount", [0xff]);


        }


        private bool SaveAccountToDatabase(string username, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO users (username, password) VALUES (@username, @password)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                ToolBox.Request(RequestState.Error);
                return false;
            }
        }
    }
}