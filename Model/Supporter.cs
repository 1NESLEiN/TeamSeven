using System;

namespace Model
{
    public class Supporter
    {
        /// <summary>
        /// Backing field for the Name property
        /// </summary>
        private string _name;
        /// <summary>
        /// Backing field for the Initials property
        /// </summary>
        private string _initials;
        /// <summary>
        /// Backing field for the SupporterID property
        /// </summary>
        private int _supporterID;

        #region Properties
        /// <summary>
        /// Property to get and set the Supporters ID
        /// </summary>
        public int SupporterID
        {
            get { return _supporterID; }
            set { _supporterID = value; }
        }

        /// <summary>
        /// Property to get and set the Supporters Name
        /// </summary>
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Property to get and set the Supporters Initials
        /// </summary>
        public String Initials
        {
            get { return _initials; }
            set { _initials = value; }
        }

        #endregion

        /// <summary>
        /// Constructor for the supporter class
        /// </summary>
        /// <param name="initials">Specifies what the initials are for the supporter to be added</param>
        /// <param name="name">Specifies what the name are for the supporter to be added</param>
        public Supporter(String initials, String name)
        {
            Initials = initials;
            Name = name;
        }
    }
}
