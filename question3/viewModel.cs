using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace question3
{
    class viewModel: Window, INotifyPropertyChanged
    {
        public DelegateCommand<string> SubmitCommand { get; private set; }

        private int _time;

        DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        private Model _model;
        
        public event PropertyChangedEventHandler PropertyChanged;


        public int Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (_time == value)
                    return;

                _time = value;
                OnPropertyChanged("Time");

            }
        }



        public Model Model
        {
            get { return _model; }
            set
            {
                if (_model == value)
                    return;
                
                _model = value;

         
            }
        }

        public bool Working
        {
            get
            {
                return _model.Working;

            }
            set
            {
                _model.Working = value;
                SubmitCommand.RaiseCanExecuteChanged();

            }
        }

        public string Url
        {
            get { return _model.Url; }
            set
            {
                if (_model.Url == value)
                    return;

                _model.Url = value;
                OnPropertyChanged("Url");
                SubmitCommand.RaiseCanExecuteChanged();
                

            }
        }
        public viewModel()
        {
            DataContext = this;
            _model = new Model();
            SubmitCommand = new DelegateCommand<string>(Submit, CanSubmit).ObservesProperty(() => Size);

            _time = 0;
            _dispatcherTimer.Tick += (object sender, EventArgs e) => { Time++; };
            _dispatcherTimer.Interval = new TimeSpan(0, 0,0,0, 1);
        }

        public string Size
        {
            get { return _model.SizeLabel; }
            set
            {
             

                if (_model.SizeLabel == value)
                    return;

                _model.SizeLabel = value;
                OnPropertyChanged("Size");
         
        
               
            }
        }
        /* 
        void Submit(string URL)
        {
            _time = 0;
            Size = "Plaese wait...";
            Working= true;
            _dispatcherTimer.Start();
            //Size = await connectAndCalculate(URL);
            Task.Run(() => { connectAndCalculate(URL); });
        }


        private void connectAndCalculate(string URL)
        {
            WebRequest webRequest = WebRequest.Create(URL);
            var response = webRequest.GetResponse();

            var text = "";
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                text = reader.ReadToEnd();
                // text.Length == is the length of the result
            }

            Application.Current.Dispatcher.Invoke(() => Size = text.Length.ToString());
            _dispatcherTimer.Stop();
            Working = false;


        }
        */
        //with async await 

        async void Submit(string URL)
        {
            _time = 0;
            Size = "Plaese wait...";
            Working = true;
            _dispatcherTimer.Start();
            Size = await connectAndCalculateAsync(URL);
            _dispatcherTimer.Stop();
            Working = false;


        }
        


        async private Task<string> connectAndCalculateAsync(string URL)
        {

            WebRequest webRequest = WebRequest.Create(URL); 
             var response = await webRequest.GetResponseAsync();
            
              var text="";
              using (StreamReader reader = new StreamReader(response.GetResponseStream()))
              {
                      text =await reader.ReadToEndAsync();
                    // text.Length == is the length of the result
              }

              return text.Length.ToString();
       


              

        }


    
        bool CanSubmit(string URL)
        {
            if (string.IsNullOrEmpty(URL))
            {
                return false;
            }
            else if (!URL.StartsWith("https"))
            {
                return false;
            }
            return true;
        }

        void OnPropertyChanged(string name )
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));


            }
        }

       














    }
}
