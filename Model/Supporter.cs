using System;

namespace Model
{
    public class Supporter
    {
        #region Backing fields
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
        private int _supporterId;
        /// <summary>
        /// Backing field for the Pass property
        /// </summary>
        private string _pass;
        /// <summary>
        /// Backing field for the Position property
        /// </summary>
        private int _positionId;
        /// <summary>
        /// Backing field for the AccessId property
        /// </summary>
        private int _accessId;

        #endregion

        #region Properties
        /// <summary>
        /// Property to get and set the Pass ID
        /// </summary>
        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }
        /// <summary>
        /// Property to get and set the Position ID
        /// </summary>
        public int PositionId
        {
            get { return _positionId; }
            set { _positionId = value; }
        }
        /// <summary>
        /// Property to get and set the Access ID
        /// </summary>
        public int AccessId
        {
            get { return _accessId; }
            set { _accessId = value; }
        }

        /// <summary>
        /// Property to get and set the Supporters ID
        /// </summary>
        public int SupporterID
        {
            get { return _supporterId; }
            set { _supporterId = value; }
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
        /// <param name="name">Specifies what the name is for the supporter to be added</param>
        /// <param name="pass">Specifies what the password is for the supporter to be added</param>
        /// <param name="accessid">Specifies what the access id is for the supporter to be added</param>
        /// <param name="positionid">Specifies what the position id is for the supporter to be added</param>
        public Supporter(String initials, String name, string pass, int accessid, int positionid)
        {
            Initials = initials;
            Name = name;
            Pass = pass;
            AccessId = accessid;
            PositionId = positionid;
        }
    }
}
