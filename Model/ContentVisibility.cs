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
        private bool _borderVisible;

        public bool BorderVisible
        {
            get
            {
                return _borderVisible;
            }

            set
            {
                _borderVisible = value;
                NotifyPropertyChanged("BorderVisible");
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
