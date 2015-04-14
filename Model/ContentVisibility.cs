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
        private bool _loginVisibility;
        private bool _userVisibility;

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
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
