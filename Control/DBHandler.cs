using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Control
{
    public class DbHandler
    {
        private const string ConnectionString = @"Data Source=tcp:home.happyjazz.eu,1435;Initial Catalog=supportTool;
                   Integrated Security=False;User ID=sa;Password=GoSCRUM2015;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        private readonly SqlConnection _con;

        #region Singleton methods

        /// <summary>
        /// Used to check whether the DBHandler class has been instantiated or to instantiate it and return the dbhandler instance.
        /// </summary>
        private static DbHandler _dbhandler;

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

        private DbHandler()
        {
            _con = new SqlConnection(ConnectionString);
        }

        #endregion

        #region Supporter methods

        /// <summary>
        /// Method to add a supporter to the database
        /// </summary>
        /// <param name="supporter">Used so we can get the name of the supporter and initials and add these to the query</param>
        public void AddSupporter(Supporter supporter)
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
            }
            finally
            {
                _con.Close();
            }
        }
        #endregion

        #region Documentation methods
        public void AddDocumentation(Documentation documentation)
        {
            String query = @"
            INSERT INTO dbo.JobDocumentations (Headline, Description, Type, Supporter, DateCreated, TimeSpent)
            VALUES (@Headline, @Description, @Type, @Supporter, @DateCreated, @TimeSpent)
            ";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                    _con.Open();

                    cmd.Parameters.Add(new SqlParameter("@Headline", documentation.Headline));
                    cmd.Parameters.Add(new SqlParameter("@Description", documentation.Description));
                    cmd.Parameters.Add(new SqlParameter("@Type", documentation.Type));
                    cmd.Parameters.Add(new SqlParameter("@Supporter", documentation.Supporter));
                    cmd.Parameters.Add(new SqlParameter("@DateCreated", documentation.DateCreated));
                    cmd.Parameters.Add(new SqlParameter("@TimeSpent", documentation.TimeSpent));

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _con.Close();
            }
        }
        #endregion

        public DataTable ViewAllSupporters()
        {
            String query = @"
            SELECT ID AS SupporterID, Name AS Navn, Initials AS Initialer FROM dbo.Supporters ORDER BY Initialer ASC
            ";
            DataTable result = QueryGetDataTable(query);
            return result;
        }

        private DataTable QueryGetDataTable(String query)
        {
            DataSet dataSet = new DataSet();
            DataTable result = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                    _con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataSet);
                }
                result = dataSet.Tables[0];
            }
            finally
            {
                _con.Close();
            }
            return result;
        }

        public DataTable ViewAllTypes()
        {
            String query = @"
            SELECT ID AS TypeID, Name AS Navn FROM dbo.Types ORDER BY Navn ASC
            ";
            DataTable result = QueryGetDataTable(query);
            return result;
        }
    }
}
