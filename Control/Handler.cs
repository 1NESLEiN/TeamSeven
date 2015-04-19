using System;
using System.Data;
using Model;

namespace Control
{
    public class Handler
    {
        private readonly DbHandler _dbHandler;

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
            _dbHandler = DbHandler.GetInstance();
        }

        #endregion

        #region Supporter methods
        /// <summary>
        /// Method to add supporter
        /// </summary>
        /// <param name="name">The name of the supporter to be added</param>
        /// <param name="initials">The initials of the supporter to be added</param>
        /// <param name="pass">The pass of the supporter to be added</param>
        /// <param name="accessid">The access of the supporter to be added</param>
        /// <param name="positionid">The position of the supporter to be added</param>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public bool AddSupporter(String name, String initials, string pass, int accessid, int positionid)
        {
            Supporter supporter = new Supporter(initials, name, pass, accessid, positionid);
            return _dbHandler.AddSupporter(supporter);
        }
        /// <summary>
        /// Method to look for admin
        /// </summary>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public DataTable LookForAdmin()
        {
            return _dbHandler.LookForAdmin();
        }
        /// <summary>
        /// Method to get a supporter with a specific id
        /// </summary>
        /// <param name="id">The id of the supporter to be added</param>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public DataTable GetSupporter(int id)
        {
            return _dbHandler.GetSupporter(id);
        }
        /// <summary>
        /// Method to delete a supporter with a specific id
        /// </summary>
        /// <param name="id">The id of the supporter to be deleted</param>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public bool DeleteSupporter(int id)
        {
            return _dbHandler.DeleteSupporter(id);
        }
        /// <summary>
        /// Method to resign a supporter with a specific id
        /// </summary>
        /// <param name="id">The id of the supporter to be resigned</param>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public bool ResignSupporter(int id)
        {
            return _dbHandler.ResignSupporter(id);
        }
        /// <summary>
        /// Method to assign a supporter with a specific id an accessid
        /// </summary>
        /// <param name="id">The id of the supporter to be deleted</param>
        /// <param name="accessid">The accessid to be assigned to the supporter</param>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public bool AssignSupporter(int id, int accessid)
        {
            return _dbHandler.AssignSupporter(id, accessid);
        }
        /// <summary>
        /// Method to get all supporters
        /// </summary>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public DataTable GetSupportersTable()
        {
            return _dbHandler.GetSupportersTable();
        }
        /// <summary>
        /// Method to get all working supporters
        /// </summary>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public DataTable GetSupportersWorkingTable()
        {
            return _dbHandler.GetSupportersWorkingTable();
        }
        #endregion

        #region Documentation methods
        /// <summary>
        /// Method to add a documentation
        /// </summary>
        /// <param name="headline">The headline of the documentation to be added</param>
        /// <param name="description">The description of the documentation to the added</param>
        /// <param name="type">The type of the documentation to the added</param>
        /// <param name="supporter">The supporter of the documentation to the added</param>
        /// <param name="dateCompleted">The date compelted of the documentation to the added</param>
        /// <param name="timeSpent">The time spent of the documentation to the added</param>
        /// <param name="dateCreated">The date created of the documentation to the added</param>
        /// <param name="status">The status of the documentation to the added</param>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public bool AddDocumentation(String headline, String description, int type, int supporter, DateTime? dateCompleted, int timeSpent, DateTime dateCreated, int status)
        {
            Documentation documentation = new Documentation(headline, description, type, supporter, dateCompleted, timeSpent, dateCreated, status);
            return _dbHandler.AddDocumentation(documentation);
        }
        /// <summary>
        /// Method to get a documentation with a specific id
        /// </summary>
        /// <param name="id">The id of the documentation to be fetched</param>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public DataTable GetDocumentation(int id)
        {
            return _dbHandler.GetDocumentation(id);
        }
        //public bool UpdateDocumentation(int id, DateTime? dateCompleted, int timeSpent, int state)
        //{
        //    return _dbHandler.UpdateDocumentation(id, timeSpent, state, dateCompleted);
        //}
        /// <summary>
        /// Method to update a documentation
        /// </summary>
        /// <param name="id">The id of the documentation to be updated</param>
        /// <param name="headline">The headline of the documentation to be updated</param>
        /// <param name="description">The description of the documentation to the updated</param>
        /// <param name="type">The type of the documentation to the updated</param>
        /// <param name="supporter">The supporter of the documentation to the updated</param>
        /// <param name="dateCompleted">The date compelted of the documentation to the updated</param>
        /// <param name="timespent">The time spent of the documentation to the updated</param>
        /// <param name="status">The status of the documentation to the updated</param>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public bool UpdateDocumentation(int id, string headline, string description, int type, int supporter,
            DateTime? dateCompleted, int timespent, int status)
        {
            return _dbHandler.UpdateDocumentation(id, headline, description, type, supporter, dateCompleted, timespent,
                status);
        }
        /// <summary>
        /// Method to get all documentations
        /// </summary>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public DataTable GetAllDocumentationsTable()
        {
            return _dbHandler.GetAllDocumentationsTable();
        }
        /// <summary>
        /// Method to get filtered documentations 
        /// </summary>
        /// <param name="keyword">The keyword of the documentations to be filtered</param>
        /// <param name="startDate">The start date of the documentation to be filtered</param>
        /// <param name="endDate">The end date of the documentation to the filtered</param>
        /// <param name="supporterId">The supporter of the documentation to the filtered</param>
        /// <param name="typeID">The type of the documentation to the filtered</param>
        /// <param name="statusId">The status compelted of the documentation to the filtered</param>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public DataTable GetFilteredDocumentationsTable(string keyword, DateTime startDate, DateTime endDate, int supporterId, int typeID, int statusId)
        {
            return _dbHandler.GetFilteredDocumentationsTable(keyword, startDate, endDate, supporterId, typeID, statusId);
        }

        #endregion

        #region Login methods
        /// <summary>
        /// Method to log ind with a username and pass
        /// </summary>
        /// <param name="username">The username of the supporter</param>
        /// <param name="pass">The pass of the supporter</param>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public DataTable Login(string username, string pass)
        {
            return _dbHandler.Login(username, pass);
        }
        #endregion

        #region UserAccess methods
        /// <summary>
        /// Method to get all user access
        /// </summary>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public DataTable GetUserAccessTable()
        {
            return _dbHandler.GetUserAccessTable();
        }
        #endregion

        #region States methods
        /// <summary>
        /// Method to get all states
        /// </summary>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public DataTable GetStatesTable()
        {
            return _dbHandler.GetStatesTable();
        }
        #endregion

        #region Types methods
        /// <summary>
        /// Method to get all types
        /// </summary>
        /// <returns>returns the corresponding method in the DBhandler</returns>
        public DataTable GetTypesTable()
        {
            return _dbHandler.GetTypesTable();
        }
        #endregion

    }
}
