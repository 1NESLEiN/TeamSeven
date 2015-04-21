using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Model
{
    public class DbHandler
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; protected set; }

        /// <summary>
        /// sql connection object
        /// </summary>
        public SqlConnection Con { get; protected set; }

        #region Singleton methods
        /// <summary>
        /// Used to check whether the DBHandler class has been instantiated or to instantiate it and return the dbhandler instance.
        /// </summary>
        protected static DbHandler _dbhandler;

        /// <summary>
        /// Method to get the single instance of the dbhandler class
        /// </summary>
        /// <returns>The instance of the single dbhandler class</returns>
        public static DbHandler GetInstance()
        {
            if (_dbhandler == null)
            {
                _dbhandler = new DbHandler();
            }
            return _dbhandler;
        }

        /// <summary>
        /// Method create a database. No longer needed?
        /// </summary>
        protected DbHandler()
        {
            ConnectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Database=Database;Integrated Security=True;Connect Timeout=30";
            Con = new SqlConnection(ConnectionString);
        }

        #endregion

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
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    Con.Open();

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
                Con.Close();
            }
        }

        /// <summary>
        /// Method to check for remaining admins
        /// </summary>
        /// <returns>A datatable with the supporter of the id</returns>
        public DataTable LookForAdmin()
        {
            String query = @"
            SELECT * FROM dbo.Supporters WHERE UserAccess = 1";

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }

        /// <summary>
        /// Method to get a supporter with a specific id
        /// </summary>
        /// <param name="id">The id needed to get the specific supporter</param>
        /// <returns>A datatable with the supporter of the id</returns>
        public DataTable GetSupporter(int id)
        {
            String query = @"
            SELECT * FROM dbo.Supporters WHERE ID = " + id;

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }

        /// <summary>
        /// Method to delete a supporter with a specific id
        /// </summary>
        /// <param name="id">The id needed to delete the specific supporter</param>
        public bool DeleteSupporter(int id)
        {
            String query1 = @"
            DELETE FROM dbo.jobDocumentations WHERE Supporter = " + id;
            String query2 = @"
            DELETE FROM dbo.Supporters WHERE ID = " + id;

            try
            {
                using (SqlCommand cmd = new SqlCommand(query1, Con))
                {
                    Con.Open();

                    cmd.ExecuteNonQuery();

                    Con.Close();
                }
                using (SqlCommand cmd = new SqlCommand(query2, Con))
                {
                    Con.Open();

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
                Con.Close();
            }
        }

        /// <summary>
        /// Method to resign a supporter with a specific username and pass
        /// </summary>
        /// <param name="id">The id needed to resign the specific supporter</param>
        public bool ResignSupporter(int id)
        {
            String query =
            string.Format("UPDATE dbo.Supporters SET Position = 2, UserAccess = 2 WHERE ID = {0}", id);

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    Con.Open();
                    cmd.Parameters.AddWithValue("Position", 2);
                    cmd.Parameters.AddWithValue("UserAccess", 2);

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
                Con.Close();
            }
        }

        /// <summary>
        /// Method to assign a supporter with a specific username and pass an access id
        /// </summary>
        /// <param name="id">The id needed to get the specific supporter</param>
        /// <param name="accessid">The accessid needed to assign the specific supporter</param>
        /// <returns>A datatable with the supporter of the id</returns>
        public bool AssignSupporter(int id, int accessid)
        {
            String query =
            string.Format("UPDATE dbo.Supporters SET UserAccess = {0} WHERE ID = {1}", accessid, id);

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    Con.Open();
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
                Con.Close();
            }
        }

        /// <summary>
        /// Method to get all supporters
        /// </summary>
        /// <returns>A datatable with all supporters</returns>
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

        /// <summary>
        /// Method to get all working supporters
        /// </summary>
        /// <returns>A datatable with all working supporters</returns>
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
        #endregion

        #region Login methods
        /// <summary>
        /// Method to get a suporter with a specific username and pass
        /// </summary>
        /// <param name="username">The username needed to get the specific documentation</param>
        /// <param name="pass">The pass needed to get the specific documentation</param>
        /// <returns>A datatable with the supporter of the id</returns>
        public DataTable Login(string username, string pass)
        {
            string query = string.Format("SELECT * FROM Supporters WHERE Name = '{0}' OR Initials = '{0}' AND Pass = '{1}' AND Position = 1", username, pass);

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
        /// <summary>
        /// Method to add a documentation
        /// </summary>
        /// <param name="documentation">the documentation object</param>
        public bool AddDocumentation(Documentation documentation)
        {
            String query = @"INSERT INTO JobDocumentations (Headline, Description, Type, Supporter, TimeSpent, DateCreated, Status) VALUES (@Headline, @Description, @Type, @Supporter, @TimeSpent, @DateCreated, @Status)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    Con.Open();

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
                Con.Close();
            }
        }

        /// <summary>
        /// Method to get a documentation with a specific id
        /// </summary>
        /// <param name="id">The id needed to get the specific documentation</param>
        /// <returns>A datatable with the jobdocumentation of the id</returns>
        public DataTable GetDocumentation(int id)
        {
            String query = "SELECT * FROM JobDocumentations WHERE JobDocumentations.ID = " + id;

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                Con.Close();

                return dt;
            }
        }

        /// <summary>
        /// Method to update a documentation with a specific id
        /// </summary>
        /// <param name="id">specifies the id used in the documentation to be updated</param>
        /// <param name="headline">specifies the headline used in the documentation to be update</param>
        /// <param name="description">specifies the description in the documentation to be update</param>
        /// <param name="type">specifies the type used in the documentation to be update</param>
        /// <param name="supporter">specifies the supporter used in the documentation to be update</param>
        /// <param name="datecompleted">specifies the date completed used in the documentation to be update</param>
        /// <param name="timespent">specifies the time spent used in the documentation to be update</param>
        /// <param name="status">specifies the status used in the documentation to be update</param>
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
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    Con.Open();

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
                Con.Close();
            }
        }

        ///// <summary>
        ///// Method to update a documentation with a specific id
        ///// </summary>
        ///// <param name="id">specifies the id used in the documentation to be updated</param>
        ///// <param name="timeSpent">specifies the time spent used in the documentation to be update</param>
        ///// <param name="status">specifies the status id in the documentation to be update</param>
        ///// <param name="dateCompleted">specifies the date completed used in the documentation to be update</param>
        //public bool UpdateDocumentation(int id, int timeSpent, int status, DateTime? dateCompleted)
        //{
        //    String query = "";

        //    if (dateCompleted.HasValue && status != 0)
        //    {
        //        query = (string.Format("UPDATE JobDocumentations SET DateCompleted='{0}', TimeSpent={1}, Status={2} WHERE JobDocumentations.ID={3}", dateCompleted.Value.Date.ToString("MM/dd/yyyy"), timeSpent, status, id));
        //    }
        //    if (status == 0 && dateCompleted.HasValue)
        //    {
        //        query = (string.Format("UPDATE JobDocumentations SET DateCompleted='{0}', TimeSpent={1} WHERE JobDocumentations.ID={2}", dateCompleted.Value.Date.ToString("MM/dd/yyyy"), timeSpent, id));
        //    }
        //    if (status == 0 && !dateCompleted.HasValue)
        //    {
        //        query = (string.Format("UPDATE JobDocumentations SET TimeSpent={0} WHERE JobDocumentations.ID={1}", timeSpent, id));
        //    }
        //    if (status != 0 && !dateCompleted.HasValue)
        //    {
        //        query = (string.Format("UPDATE JobDocumentations SET TimeSpent={0}, Status={1} WHERE JobDocumentations.ID={2}", timeSpent, status, id));
        //    }
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query, Con))
        //        {
        //            Con.Open();

        //            if (dateCompleted != null) { cmd.Parameters.AddWithValue("DateCompleted", dateCompleted.Value.Date.ToString("MM/dd/yyyy")); }
        //            cmd.Parameters.AddWithValue("TimeSpent", timeSpent);
        //            if (status != 0) cmd.Parameters.AddWithValue("Status", status);

        //            cmd.ExecuteNonQuery();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        Con.Close();
        //    }
        //}

        /// <summary>
        /// Method to get a documentation with a specific id
        /// </summary>
        /// <returns>A datatable with all the documentations</returns>
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

        /// <summary>
        /// Method to get a documentation with a specific id
        /// </summary>
        /// <param name="keyword">specifies the keyword used to search in the headline of a documentation</param>
        /// <param name="startDate">specifies the start date used to search for of a documentation</param>
        /// <param name="endDate">specifies the end date used to search for a documentation</param>
        /// <param name="supporterId">specifies the supporter id used to search for a documentation</param>
        /// <param name="typeId">specifies the type id used to search for a documentation</param>
        /// <param name="stateId">specifies the state id used to search for a documentation</param>
        /// <returns>A datatable with all filtered documentations</returns>
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
                    int[] resultsInts = stateId.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();

                    if (counter > 0) query += " AND ";
                    counter++;

                    int counter2 = 0;
                    foreach (var i in resultsInts)
                    {
                        counter2++;
                        query += "Statuses.ID = " + i;
                        if (counter2 < resultsInts.Length)
                        {
                            query += " OR ";
                        }
                    }
                }
                if (typeId != 0)
                {
                    if (counter > 0) query += " AND ";
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
        #endregion

        #region Types methods
        /// <summary>
        /// Method to get all types
        /// </summary>
        /// <returns>A datatable with all types</returns>
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

        #region States methods
        /// <summary>
        /// Method to get all states
        /// </summary>
        /// <returns>A datatable with all states</returns>
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

        #region UserAccess methods
        /// <summary>
        /// Method to get all User access
        /// </summary>
        /// <returns>A datatable with all user access</returns>
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
        #endregion
    }
}
