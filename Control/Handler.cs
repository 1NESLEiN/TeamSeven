using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Control
{
    public class Handler
    {
        private DbHandler _dbHandler;

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
        public void AddSupporter(String name, String initials)
        {
            Supporter supporter = new Supporter(initials, name);
            _dbHandler.AddSupporter(supporter);
        }
        #endregion

        #region Documentation methods
        public void AddDocumentation(String headline, String description, int type, int supporter, DateTime dateCreated, int timeSpent)
        {
            Documentation documentation = new Documentation(headline, description, type, supporter, dateCreated, timeSpent);
            _dbHandler.AddDocumentation(documentation);
        }
        #endregion

        public DataTable ViewAllSupporters()
        {
            return _dbHandler.ViewAllSupporters();
        }

        public DataTable ViewAllTypes()
        {
            return _dbHandler.ViewAllTypes();
        }
    }
}
