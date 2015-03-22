using System;

namespace Model
{
    public class Documentation
    {
        private string _headline;
        private string _description;
        private DateTime _dateCompleted;
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

        public DateTime DateCompleted
        {
            get { return _dateCompleted; }
            set { _dateCompleted = value; }
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

        public Documentation(int type, String headline, String description, DateTime dateCompleted, int timeSpent, int supporter)
        {
            Type = type;
            Headline = headline;
            Description = description;
            DateCompleted = dateCompleted;
            TimeSpent = timeSpent;
            Supporter = supporter;
        }
    }
}
