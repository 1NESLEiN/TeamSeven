using System;
using System.Data;
using Model;

namespace Control
{
    public class Handler
    {
        private DBHandler _dbHandler;

        #region Singleton-methods

        /// <summary>
        /// Used to check whether the Handler class has been instantiated or to instantiate it and return the handler instance.
        /// </summary>
        private static Handler _handler;

        /// <summary>
        /// Method to get the single instance of the handler class
        /// </summary>
        /// <returns>The instance of the single handler class</returns>
        public static Handler GetInstance()
        {
            if (_handler == null)
            {
                _handler = new Handler();
            }
            return _handler;
        }

        private Handler()
        {
            _dbHandler = DBHandler.GetInstance();
        }

        #endregion

        #region Supporter methods
        public bool AddSupporter(String name, String initials)
        {
            Supporter supporter = new Supporter(initials, name);
            return _dbHandler.AddSupporter(supporter);
        }
        #endregion

        #region Documentation methods
        public bool AddDocumentation(int type, String headline, String description, DateTime? dateCompleted, int timeSpent, int supporter, int state)
        {
            Documentation documentation = new Documentation(type, headline, description, dateCompleted, timeSpent, supporter, state);
            return _dbHandler.AddDocumentation(documentation);
        }
        public DataTable GetDocumentation(int id)
        {
            return _dbHandler.GetDocumentation(id);
        }
        public bool UpdateDocumentation(int id, DateTime? dateCompleted, int timeSpent, int state)
        {
            return _dbHandler.UpdateDocumentation(id, timeSpent, state, dateCompleted);
        }

        public bool UpdateDocumentation(int id, string headline, string description, int type, int supporter,
            DateTime? dateCompleted, int timespent, int status)
        {
            return _dbHandler.UpdateDocumentation(id, headline, description, type, supporter, dateCompleted, timespent,
                status);
        }
        public DataTable GetTypesTable()
        {
            return _dbHandler.GetTypesTable();
        }
        #endregion

        public DataTable GetSupportersTable()
        {
            return _dbHandler.GetSupportersTable();
        }
        public DataTable GetStatesTable()
        {
            return _dbHandler.GetStatesTable();
        }

        public DataTable GetAllDocumentationsTable()
        {
            return _dbHandler.GetAllDocumentationsTable();
        }

        public DataTable GetFilteredDocumentationsTable(string keyword, DateTime startDate, DateTime endDate, int supporterID, int typeID, int StatusID)
        {
            return _dbHandler.GetFilteredDocumentationsTable(keyword, startDate, endDate, supporterID, typeID, StatusID);
        }
    }
}
