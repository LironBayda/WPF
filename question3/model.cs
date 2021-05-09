using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace question3
{
    class Model: INotifyPropertyChanged
    {
        private string _sizeLabel;
        public bool Working { get; set; }
      

        public string SizeLabel
        { 
            get 
            { 
                return _sizeLabel; 
            }
            set
            {
                _sizeLabel = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("SizeLabel");
            }
        }


        private string  _url;
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Url");
            }
        }

        public Model()
        {
            Working = false;
            SizeLabel = " ";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));


            }
        }
    }
}
