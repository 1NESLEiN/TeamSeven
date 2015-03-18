using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace Control
{
    public class DBHandler
    {
        private const string ConnectionString = @"Data Source=tcp:home.happyjazz.eu,1435;Initial Catalog=supportTool;
                   Integrated Security=False;User ID=sa;Password=GoSCRUM2015;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

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
        public void AddDocumentation(Documentation documentation)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
