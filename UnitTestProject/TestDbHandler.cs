using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace UnitTestProject
{
    public class TestDbHandler : DbHandler
    {
        private static TestDbHandler _testDbHandler;

        public TestDbHandler()
        {
            ConnectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Database=TestDatabase;Integrated Security=True;Connect Timeout=30";
            Con = new SqlConnection(ConnectionString);
        }

        public static TestDbHandler GetInstance()
        {
            if (_testDbHandler == null)
            {
                _testDbHandler = new TestDbHandler();
            }
            return _testDbHandler;
        }

        public void BuildDatabase()
        {
            FileInfo fileInfo = new FileInfo(@".\Scripts\BuildTestDatabase.sql");
            string scriptText = fileInfo.OpenText().ReadToEnd();

            //split the script on "GO" commands
            string[] splitter = { "\r\nGO\r\n" };
            string[] commandTexts = scriptText.Split(splitter,
              StringSplitOptions.RemoveEmptyEntries);
            foreach (string commandText in commandTexts)
            {
                try
                {
                    Con.Open();
                    using (SqlCommand cmd = new SqlCommand(commandText, Con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                finally
                {
                    Con.Close();
                }
            }
        }
    }
}
