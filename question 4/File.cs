using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;


namespace question_4
{
        public class File : INotifyPropertyChanged
        {
            private string _fileName;

            public string FileName
            {
                get { return _fileName; }
                set
                {
                    _fileName = value;
                    OnPropertyChanged("FileName");
                }
            }

    

            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        public File(string fileName)
        {
            FileName = fileName;
        }
    }
}
