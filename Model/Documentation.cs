using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Documentation
    {
        private string _headline;
        private string _description;
        private DateTime _dateCreated;
        private int _timeSpent;
        private int _type;
        private int _supporter;
        private int _documentationID;

        #region Properties

        public String Headline
        {
            get { return _headline; }
            set { _headline = value; }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        public int TimeSpent
        {
            get { return _timeSpent; }
            set { _timeSpent = value; }
        }

        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int Supporter
        {
            get { return _supporter; }
            set { _supporter = value; }
        }

        public int DocumentationID
        {
            get { return _documentationID; }
            set { _documentationID = value; }
        }

        #endregion

        public Documentation(String headline, String description, int type, int supporter, DateTime dateCreated, int timeSpent)
        {
            Headline = headline;
            Description = description;
            Type = type;
            Supporter = supporter;
            DateCreated = dateCreated;
            TimeSpent = timeSpent;
        }
    }
}
