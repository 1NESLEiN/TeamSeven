using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool AddDocumentation(int type, String headline, String description, DateTime dateCreated, int timeSpent, int supporter)
        {
            Documentation documentation = new Documentation(type, headline, description, dateCreated, timeSpent, supporter);
            return _dbHandler.AddDocumentation(documentation);
        }
        #endregion
    }
}
