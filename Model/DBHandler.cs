using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using Model;

namespace Control
{
    public class DBHandler
    {
        //Change this bool to change whether the create database is run or not on instantiation.
        private bool _CreateDatabaseFromScript = false;

        private const string ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;Connect Timeout=30";

        private readonly SqlConnection _con;

        #region Singleton methods

        /// <summary>
        /// Used to check whether the DBHandler class has been instantiated or to instantiate it and return the dbhandler instance.
        /// </summary>
        private static DBHandler _dbhandler;

        /// <summary>
        /// Method to get the single instance of the dbhandler class
        /// </summary>
        /// <returns>The instance of the single dbhandler class</returns>
        public static DBHandler GetInstance()
        {
            if (_dbhandler == null)
            {
                _dbhandler = new DBHandler();
            }
            return _dbhandler;
        }

        private DBHandler()
        {
            _con = new SqlConnection(ConnectionString);
            if (_CreateDatabaseFromScript)
            {
                BuildDatabase();
            }
        }

        #endregion

        private void BuildDatabase()
        {
            FileInfo fileInfo = new FileInfo(@".\createDatabase.sql");
            string scriptText = fileInfo.OpenText().ReadToEnd();

            //split the script on "GO" commands
            string[] splitter = { "\r\nGO\r\n" };
            string[] commandTexts = scriptText.Split(splitter,
              StringSplitOptions.RemoveEmptyEntries);
            foreach (string commandText in commandTexts)
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(commandText, _con))
                    {
                        _con.Open();

                        cmd.ExecuteNonQuery();

                        _con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #region Supporter methods

        /// <summary>
        /// Method to add a supporter to the database
        /// </summary>
        /// <param name="supporter">Used so we can get the name of the supporter and initials and add these to the query</param>
        public bool AddSupporter(Supporter supporter)
        {
            String query = @"
            INSERT INTO dbo.Supporters (Name, Initials)
            VALUES (@Name, @Initials)
            ";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                    _con.Open();

                    cmd.Parameters.Add(new SqlParameter("@Name", supporter.Name));
                    cmd.Parameters.Add(new SqlParameter("@Initials", supporter.Initials));

                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                _con.Close();
            }
        }
        #endregion

        #region Documentation methods
        public bool AddDocumentation(Documentation documentation)
        {
            String query = @"INSERT INTO JobDocumentations (Headline, Description, Type, Supporter,DateCompleted,TimeSpent) VALUES (@Headline, @Description, @Type, @Supporter, @DateCompleted, @TimeSpent)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                    _con.Open();

                    cmd.Parameters.Add(new SqlParameter("@Type", documentation.Type));
                    cmd.Parameters.Add(new SqlParameter("@Headline", documentation.Headline));
                    cmd.Parameters.Add(new SqlParameter("@Description", documentation.Description));
                    cmd.Parameters.Add(new SqlParameter("@DateCompleted", documentation.DateCompleted));
                    cmd.Parameters.Add(new SqlParameter("@TimeSpent", documentation.TimeSpent));
                    cmd.Parameters.Add(new SqlParameter("@Supporter", documentation.Supporter));

                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                _con.Close();
            }
        }

        public DataTable GetTypesTable()
        {
            string query = "SELECT * FROM dbo.Types";

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }
        #endregion

        public DataTable GetSupportersTable()
        {
            string query = "SELECT * FROM dbo.Supporters";

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }
    }
}
