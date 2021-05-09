using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace question2
{
    class ViewModel: BindableBase
    {
        private IList<EquationDetailes> _equationDetailes;
        int _numOfEquation;
        int MAX_NUM_OF_EQUATION=10;
        public DelegateCommand<int?> SubmitCommand { get; private set; }
        DispatcherTimer _dispatcherTimer = new DispatcherTimer();


      
        private string _timerColor;

        public string TimerColor
        {
            get
            {
                return _timerColor;
            }
            set
            {
                SetProperty<string>(ref _timerColor, value);
          

            }
        }

        private string _windowBackground;

        public string WindowBackground
        {
            get
            {
                return _windowBackground;
            }
            set
            {
                SetProperty<string>(ref _windowBackground, value);

         
            }
        }
      
        private int _time;

        public int Time
        {
            get
            {
                return _time;
            }
            set
            {
                SetProperty<int>(ref _time, value);

                SubmitCommand.RaiseCanExecuteChanged();
                if (_time <= 15)
                    TimerColor= "DarkRed";
                if (_time == 0)
                {
                    WindowBackground = "red";
                    StopWaitContinue();

                }
                Debug.WriteLine(_time);
            }
        }

        private void StopWaitContinue()
        {
            _dispatcherTimer.Stop();
            _numOfEquation++;

            // i want that the UI will keep working therefore i using task 
            if (_numOfEquation < MAX_NUM_OF_EQUATION)
            {
                Task.Run(() =>
                {
                    Thread.Sleep(5000);
                    TimerColor = "black";
                    WindowBackground = "white";
                    IsClick = false;
                    Time = 30;

                    OnPropertyChanged(new PropertyChangedEventArgs("answer1"));
                    OnPropertyChanged(new PropertyChangedEventArgs("answer2"));
                    OnPropertyChanged(new PropertyChangedEventArgs("answer3"));
                    OnPropertyChanged(new PropertyChangedEventArgs("answer4"));
                    OnPropertyChanged(new PropertyChangedEventArgs("equation"));

                    _dispatcherTimer.Start();

                });


            }
        }

     

        public ViewModel()
        {
            _equationDetailes = new List<EquationDetailes>();
            for (int i = 0; i < MAX_NUM_OF_EQUATION; i++)
                _equationDetailes.Add(new EquationDetailes());

            _numOfEquation = 0;
            SubmitCommand = new DelegateCommand<int?>(Submit, CanSubmit);
            _isClick = false;

            _time = 30;
            _dispatcherTimer.Tick += (object sender, EventArgs e) => { Time--; };
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();

            TimerColor = "black";
            WindowBackground = "white";
        }


        
        private bool CanSubmit(int? arg)
        {
            if (IsClick == false && Time > 0)
                return true;
            return false;
        
        }

        private void Submit(int? num)
        {
            IsClick = true;
            if (num == _equationDetailes[_numOfEquation].Result)
                WindowBackground = "LightGreen";
            else
                WindowBackground = "red";


            StopWaitContinue();



        }


        private bool _isClick;

        public bool IsClick
        {
            get
            {
                return _isClick ;
            }
            set
            {
                _isClick = value;
                SubmitCommand.RaiseCanExecuteChanged();


            }
        }

        public int Answer1
        {
            get { return _equationDetailes[_numOfEquation].Answers[0]; }
        }
        public int Answer2
        {
            get { return _equationDetailes[_numOfEquation].Answers[1]; }
        }
        public int Answer3
        {
            get { return _equationDetailes[_numOfEquation].Answers[2]; }
        }
        public int Answer4
        {
            get { return _equationDetailes[_numOfEquation].Answers[3]; }
        }

        public string Equation
        {
            get { return  _equationDetailes[_numOfEquation].Equation; }
        }


        

    }
}
