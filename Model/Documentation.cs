using System;
using System.Data;

namespace Model
{
    public class Documentation
    {
        #region Backing fields
        /// <summary>
        /// Backing field for the Headline property
        /// </summary>
        private string _headline;
        /// <summary>
        /// Backing field for the Description property
        /// </summary>
        private string _description;
        /// <summary>
        /// Backing field for the DateCompleted property
        /// </summary>
        private DateTime? _dateCompleted;
        /// <summary>
        /// Backing field for the DateCreated property
        /// </summary>
        private DateTime _dateCreated;
        /// <summary>
        /// Backing field for the TimeSpent property
        /// </summary>
        private int _timeSpent;
        /// <summary>
        /// Backing field for the Type property
        /// </summary>
        private int _type;
        /// <summary>
        /// Backing field for the Supporter property
        /// </summary>
        private int _supporter;
        /// <summary>
        /// Backing field for the DocumentationId property
        /// </summary>
        private int _documentationId;
        /// <summary>
        /// Backing field for the Status property
        /// </summary>
        private int _status;

        #endregion

        #region Properties
        /// <summary>
        /// Property to get and set the Headline
        /// </summary>
        public String Headline
        {
            get { return _headline; }
            set { _headline = value; }
        }
        /// <summary>
        /// Property to get and set the Status
        /// </summary>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// Property to get and set the Description
        /// </summary>
        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// Property to get and set the DateCompleted
        /// </summary>
        public DateTime? DateCompleted
        {
            get { return _dateCompleted; }
            set { _dateCompleted = value; }
        }
        /// <summary>
        /// Property to get and set the DateCreated
        /// </summary>
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }
        /// <summary>
        /// Property to get and set the TimeSpendt
        /// </summary>
        public int TimeSpent
        {
            get { return _timeSpent; }
            set { _timeSpent = value; }
        }
        /// <summary>
        /// Property to get and set the Type
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// Property to get and set the Supporter
        /// </summary>
        public int Supporter
        {
            get { return _supporter; }
            set { _supporter = value; }
        }
        /// <summary>
        /// Property to get and set the DocumentationId
        /// </summary>
        public int DocumentationID
        {
            get { return _documentationId; }
            set { _documentationId = value; }
        }
        #endregion

        /// <summary>
        /// Constructor for the Documentation class
        /// </summary>
        /// <param name="headline">Specifies what the Headline is for the documentation to be added</param>
        /// <param name="description">Specifies what the description is for the documentation to be added</param>
        /// <param name="type">Specifies what the password type is for the documentation to be added</param>
        /// <param name="supporter">Specifies what the supporter Headline is for the documentation to be added</param>
        /// <param name="dateCompleted">Specifies what the date completed id is for the documentation to be added</param>
        /// /// <param name="timeSpent">Specifies what the time spent id is for the documentation to be added</param>
        /// /// <param name="dateCreated">Specifies what the date created id is for the documentation to be added</param>
        /// /// <param name="status">Specifies what the status id is for the documentation to be added</param>
        public Documentation(String headline, String description, int type, int supporter, DateTime? dateCompleted, int timeSpent, DateTime dateCreated, int status)
        {
            Type = type;
            Headline = headline;
            Description = description;
            DateCompleted = dateCompleted;
            TimeSpent = timeSpent;
            Supporter = supporter;
            Status = status;
            DateCreated = dateCreated;
        }

        public Documentation(DataRow tableRowData)
        {
            DocumentationID = (int)tableRowData["ID"];
            Type = (int)tableRowData["Type"];
            Headline = (string)tableRowData["Headline"];
            Description = (string)tableRowData["Description"];
            DateCompleted = tableRowData["DateCompleted"] as DateTime?;
            TimeSpent = (int)tableRowData["TimeSpent"];
            Supporter = (int)tableRowData["Supporter"];
            Status = (int)tableRowData["Status"];
            DateCreated = (DateTime)tableRowData["DateCreated"];
        }

        public bool Equals(Documentation obj)
        {
            if (
                    this.Headline == obj.Headline &&
                    this.Description == obj.Description &&
                    this.DateCompleted == obj.DateCompleted &&
                    this.TimeSpent == obj.TimeSpent &&
                    this.Supporter == obj.Supporter &&
                    this.Status == obj.Status &&
                    this.DateCreated == obj.DateCreated)
            {
                return true;
            }

            return false;
        }
    }
}
