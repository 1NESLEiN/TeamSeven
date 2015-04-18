using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ContentVisibility : INotifyPropertyChanged
    {
        #region Backing fields
        /// <summary>
        /// Backing field for the Login Visibility property
        /// </summary>     
        private bool _loginVisibility;
        /// <summary>
        /// Backing field for the User Visibility property
        /// </summary>
        private bool _userVisibility;
        #endregion

        #region properties
        
        /// <summary>
        /// Property to get and set the Login Visibility
        /// </summary>
        public bool LoginVisibility
        {
            get
            {
                return _loginVisibility;
            }

            set
            {
                _loginVisibility = value;
                NotifyPropertyChanged("LoginVisibility");
            }
        }
        /// <summary>
        /// Property to get and set the User Visibility
        /// </summary>
        public bool UserVisibility
        {
            get
            {
                return _userVisibility;
            }

            set
            {
                _userVisibility = value;
                NotifyPropertyChanged("UserVisibility");
            }
        }
        #endregion

        /// <summary>
        /// Notify xaml visibility that visibility properties have changed
        /// </summary>
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        /// <summary>
        /// Event Handler for Property Changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
