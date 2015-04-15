using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows.Forms;
using Model;

namespace Control
{
    public class DBHandler
    {
        //Change this bool to change whether the create database is run or not on instantiation.
        private bool _CreateDatabaseFromScript = false;

        private const string ConnectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Database=Database;Integrated Security=True;Connect Timeout=30";

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
            INSERT INTO dbo.Supporters (Name, Initials, Pass, UserAccess, Position)
            VALUES (@Name, @Initials, @Pass, @UserAccess, @Position)
            ";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                    _con.Open();

                    cmd.Parameters.Add(new SqlParameter("@Name", supporter.Name));
                    cmd.Parameters.Add(new SqlParameter("@Initials", supporter.Initials));
                    cmd.Parameters.Add(new SqlParameter("@Pass", supporter.Pass));
                    cmd.Parameters.Add(new SqlParameter("@UserAccess", supporter.AccessId));
                    cmd.Parameters.Add(new SqlParameter("@Position", supporter.PositionId));

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

        public bool DeleteSupporter(int id)
        {
            String query1 = @"
            DELETE FROM dbo.jobDocumentations WHERE Supporter = " + id;
            String query2 = @"
            DELETE FROM dbo.Supporters WHERE ID = " + id;

            try
            {
                using (SqlCommand cmd = new SqlCommand(query1, _con))
                {
                    _con.Open();

                    cmd.ExecuteNonQuery();

                    _con.Close();
                }
                using (SqlCommand cmd = new SqlCommand(query2, _con))
                {
                    _con.Open();

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
        public bool ResignSupporter(int id)
        {
            String query =
            string.Format("UPDATE dbo.Supporters SET Position = 2 WHERE ID = {0}", id);

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                    _con.Open();
                    cmd.Parameters.AddWithValue("Position", 2);

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
        public bool AssignSupporter(int id, int accessid)
        {
            String query =
            string.Format("UPDATE dbo.Supporters SET UserAccess = {0} WHERE ID = {1}", accessid, id);

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                    _con.Open();
                    cmd.Parameters.AddWithValue("Position", 2);

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

        #region Login methods

        public DataTable Login(string username, string pass)
        {
            string query = string.Format("SELECT * FROM Supporters WHERE Name = '{0}' AND Pass = '{1}' AND Position = 1", username, pass);

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

        #region Documentation methods
        public bool AddDocumentation(Documentation documentation)
        {
            String query = @"INSERT INTO JobDocumentations (Headline, Description, Type, Supporter, TimeSpent, DateCreated, Status) VALUES (@Headline, @Description, @Type, @Supporter, @TimeSpent, @DateCreated, @Status)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                    _con.Open();

                    cmd.Parameters.Add(new SqlParameter("@Type", documentation.Type));
                    cmd.Parameters.Add(new SqlParameter("@Headline", documentation.Headline));
                    cmd.Parameters.Add(new SqlParameter("@Description", documentation.Description));
                    cmd.Parameters.Add(new SqlParameter("@TimeSpent", documentation.TimeSpent));
                    cmd.Parameters.Add(new SqlParameter("@DateCreated", documentation.DateCreated));
                    cmd.Parameters.Add(new SqlParameter("@Supporter", documentation.Supporter));
                    cmd.Parameters.Add(new SqlParameter("@Status", documentation.Status));

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


        public DataTable GetDocumentation(int id)
        {
            String query = "SELECT * FROM JobDocumentations WHERE JobDocumentations.ID = " + id;

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                _con.Close();

                return dt;
            }
        }

        public bool UpdateDocumentation(int id, string headline, string description, int type, int supporter,
            DateTime? datecompleted, int timespent, int status)
        {
            string query;


            if (datecompleted.HasValue)
            {
                query = string.Format("UPDATE JobDocumentations SET Headline = '{0}', Description = '{1}', Type = {2}, Supporter = {3}, DateCompleted = '{4}', TimeSpent = {5}, Status = {6} WHERE ID = {7}", headline, description, type, supporter, datecompleted.Value.Date.ToString("MM/dd/yyyy"), timespent, status, id);
            }
            else
            {
                query = string.Format("UPDATE JobDocumentations SET Headline = '{0}', Description = '{1}', Type = {2}, Supporter = {3}, TimeSpent = {4}, Status = {5} WHERE ID = {6}", headline, description, type, supporter, timespent, status, id);
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                    _con.Open();

                    cmd.Parameters.AddWithValue("Headline", headline);
                    cmd.Parameters.AddWithValue("Description", description);
                    cmd.Parameters.AddWithValue("Type", type);
                    cmd.Parameters.AddWithValue("Supporter", supporter);
                    if (datecompleted != null) cmd.Parameters.AddWithValue("DateCompleted", datecompleted.Value.Date.ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("TimeSpent", type);
                    cmd.Parameters.AddWithValue("Status", status);

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
        public bool UpdateDocumentation(int id, int timeSpent, int status, DateTime? dateCompleted)
        {
            String query = "";

            if (dateCompleted.HasValue && status != 0)
            {
                query = (string.Format("UPDATE JobDocumentations SET DateCompleted='{0}', TimeSpent={1}, Status={2} WHERE JobDocumentations.ID={3}", dateCompleted.Value.Date.ToString("MM/dd/yyyy"), timeSpent, status, id));
            }
            if (status == 0 && dateCompleted.HasValue)
            {
                query = (string.Format("UPDATE JobDocumentations SET DateCompleted='{0}', TimeSpent={1} WHERE JobDocumentations.ID={2}", dateCompleted.Value.Date.ToString("MM/dd/yyyy"), timeSpent, id));
            }
            if (status == 0 && !dateCompleted.HasValue)
            {
                query = (string.Format("UPDATE JobDocumentations SET TimeSpent={0} WHERE JobDocumentations.ID={1}", timeSpent, id));
            }
            if (status != 0 && !dateCompleted.HasValue)
            {
                query = (string.Format("UPDATE JobDocumentations SET TimeSpent={0}, Status={1} WHERE JobDocumentations.ID={2}", timeSpent, status, id));
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                    _con.Open();

                    if (dateCompleted != null) { cmd.Parameters.AddWithValue("DateCompleted", dateCompleted.Value.Date.ToString("MM/dd/yyyy")); }
                    cmd.Parameters.AddWithValue("TimeSpent", timeSpent);
                    if (status != 0) cmd.Parameters.AddWithValue("Status", status);

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
        public DataTable GetStatesTable()
        {
            string query = "SELECT * FROM dbo.Statuses";

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
        public DataTable GetSupportersWorkingTable()
        {
            string query = "SELECT * FROM dbo.Supporters WHERE Position = 1";

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }
        public DataTable GetUserAccessTable()
        {
            string query = "SELECT * FROM dbo.UserAccesses";

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }

        public DataTable GetAllDocumentationsTable()
        {
            string query = "SELECT JobDocumentations.ID, Headline, Description, DateCreated, DateDue, TimeSpent, Supporters.Name AS SupporterName, Statuses.Name AS Status, Initials, Types.Name AS TypeName FROM dbo.JobDocumentations JOIN dbo.Supporters ON dbo.Supporters.ID = dbo.JobDocumentations.Supporter JOIN dbo.Types ON dbo.Types.ID = dbo.JobDocumentations.Type JOIN dbo.States ON dbo.States.ID = dbo.JobDocumentations.Status";


            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }

        public DataTable GetFilteredDocumentationsTable(string keyword, DateTime startDate, DateTime endDate, int supporterId, int typeId, int stateId)
        {
            string query = "SELECT JobDocumentations.ID, Headline, Description, DateCreated, DateCompleted, TimeSpent, Supporters.Name AS SupporterName, Statuses.Name AS Status, Initials, Types.Name AS TypeName FROM dbo.JobDocumentations JOIN dbo.Supporters ON dbo.Supporters.ID = dbo.JobDocumentations.Supporter JOIN dbo.Types ON dbo.Types.ID = dbo.JobDocumentations.Type JOIN dbo.Statuses ON dbo.Statuses.ID = dbo.JobDocumentations.Status";
            if (keyword != "" || !startDate.Equals(DateTime.MinValue.Date) || !endDate.Equals(DateTime.MinValue.Date) || supporterId != 0 || typeId != 0 || stateId != 0)
            {
                int counter = 0;
                query += " WHERE ";
                if (keyword != "")
                {
                    counter++;
                    query += "Headline LIKE @Keyword";
                }
                if (endDate.Equals(DateTime.MinValue.Date))
                {
                    endDate = DateTime.MaxValue.Date;
                }
                if (startDate.Equals(DateTime.MinValue.Date))
                {
                    //For some reason SQL doesn't like that MinValue date is a very low number, makes an out of range error. So it is added up by 2000 years here.
                    startDate = DateTime.MinValue.Date.AddYears(2000);
                }
                if (!startDate.Equals(DateTime.MinValue.Date) || !endDate.Equals(DateTime.MinValue.Date))
                {
                    if (counter > 0) query += " AND ";
                    counter++;

                    query += "DateCreated BETWEEN '" + startDate.ToString("MM/dd/yyyy") + "' AND '" + endDate.ToString("MM/dd/yyyy") + "'";
                }
                if (supporterId != 0)
                {
                    if (counter > 0) query += " AND ";
                    counter++;
                    query += "Supporters.ID = " + supporterId;
                }
                if (stateId != 0)
                {
                    if (counter > 0) query += " AND ";
                    counter++;
                    query += "Statuses.ID = " + stateId;
                }
                if (typeId != 0)
                {
                    if (counter > 0) query += " AND ";
                    counter++;
                    query += "Types.ID = " + typeId;
                }
            }
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Keyword";
                param.Value = "%" + keyword + "%";
                // 3. add new parameter to command object
                cmd.Parameters.Add(param);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }


        }
    }
}
