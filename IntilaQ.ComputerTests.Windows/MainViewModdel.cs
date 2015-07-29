using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Command;
using IntilaQ.ComputerTests.Client.Mappers;
using IntilaQ.ComputerTests.Client.Models;
using IntilaQ.ComputerTests.Client.Services;
using RelayCommand = IntilaQ.ComputerTests.Client.Tools.RelayCommand;

namespace IntilaQ.ComputerTests.Client.ViewModels
{
    public class MainViewModdel : INotifyPropertyChanged
    {

        private CandidateUser _candidateUser;
        private int _numberOfAnsweredQuestions;
        private int _score = 0;
        private string _errorMessage;
        private bool _isErrorMessageVisible;
        private bool _isBusy;
        private bool _isTestStarted;

        private DispatcherTimer _testPeriodDispatcherTimer;
        private DateTime _testStartedAtDateTime;

        private string _testDuration = "00:00";
        private bool _isTestFinished;

        public CandidateUser CandidateUser
        {
            get { return _candidateUser; }
            set
            {
                _candidateUser = value;
                OnPropertyChanged();
            }
        }

        public string TestDuration
        {
            get { return _testDuration; }
            set
            {
                _testDuration = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfAnsweredQuestions
        {
            get { return _numberOfAnsweredQuestions; }
            set
            {
                _numberOfAnsweredQuestions = value;
                OnPropertyChanged();
            }
        }

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsErrorMessageVisible
        {
            get { return _isErrorMessageVisible; }
            set
            {
                _isErrorMessageVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public bool IsTestStarted
        {
            get { return _isTestStarted; }
            set
            {
                _isTestStarted = value;
                OnPropertyChanged();
            }
        }

        public bool IsTestFinished
        {
            get { return _isTestFinished; }
            set
            {
                _isTestFinished = value; 
                OnPropertyChanged();
            }
        }

        public ICommand HideErrorMessageCommand
        {
            get
            {
                return new RelayCommand(() => IsErrorMessageVisible = false);
            }
        }

        public ICommand SendAnswersCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    if (ValidateDataEntries())
                    {
                        IsBusy = true;

                        var dataServices = new DataServices();
                        Score = await dataServices.SendCandidateUserAsync(_candidateUser);

                        //_dispatcherTimer.Stop();

                        IsBusy = false;
                    }
                });
            }
        }

        public ICommand StartTestCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    IsTestStarted = true;

                    _testPeriodDispatcherTimer.Start();
                    _testStartedAtDateTime = DateTime.Now.AddMinutes(5);
                });
            }
        }

        /// <summary>
        /// Checks if the user have answered all questions 
        /// and entered his Fullname, Email and Phone crrectly.
        /// </summary>
        /// <returns></returns>
        private bool ValidateDataEntries()
        {
            if (_candidateUser.AnswerTests.Count(test => test?.ChosenAnswer != null) !=
                        _candidateUser.AnswerTests.Count)
            {
                ErrorMessage = "Make sure you answered all the questions!";
                IsErrorMessageVisible = true;

                return false;
            }
            if (string.IsNullOrEmpty(_candidateUser.Fullname))
            {
                ErrorMessage = "Make sure you have entered your Fullname!";
                IsErrorMessageVisible = true;

                return false;
            }
            if (string.IsNullOrEmpty(_candidateUser.Email)
                || !_candidateUser.Email.Contains("@")
                || !_candidateUser.Email.Contains("."))
            {
                ErrorMessage = "Make sure you have entered your Email correctly!";
                IsErrorMessageVisible = true;

                return false;
            }
            if (_candidateUser.Phone == 0)
            {
                ErrorMessage = "Make sure you have entered your Phone number!";
                IsErrorMessageVisible = true;

                return false;
            }

            return true;
        }

        public MainViewModdel()
        {

            InitializeTimers();

            InitializeDataAsync();
        }

        private void InitializeTimers()
        {
            //_dispatcherTimer = new DispatcherTimer
            //{
            //    Interval = new TimeSpan(0, 5, 0),
            //};

            //_dispatcherTimer.Tick += (sender, o) =>
            //{
            //    IsTestStarted = false;
            //};

            _testPeriodDispatcherTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1),
            };

            _testPeriodDispatcherTimer.Tick += (sender, o) =>
            {
                var timeSinceTestStarted = (_testStartedAtDateTime - DateTime.Now);
                var secondsSinceTestStarted = timeSinceTestStarted.Seconds.ToString();
                var minutesSinceTestStarted = timeSinceTestStarted.Minutes.ToString();

                if (timeSinceTestStarted.Seconds <= 9)
                {
                    secondsSinceTestStarted = "0" + timeSinceTestStarted.Seconds;
                }

                if (timeSinceTestStarted.Minutes <= 9)
                {
                    minutesSinceTestStarted = "0" + timeSinceTestStarted.Minutes;
                }

                TestDuration = String.Format("{0}:{1}",
                    minutesSinceTestStarted,
                    secondsSinceTestStarted);

                //end of test
                if (timeSinceTestStarted.Minutes == 5)
                {
                    IsTestFinished = true;
                    IsTestStarted = false;
                }
            };
        }

        private async Task InitializeDataAsync()
        {
            IsBusy = true;

            var dataServices = new DataServices();

            CandidateUser = new CandidateUser
            {
                AnswerTests = await dataServices.GetAnswerTestsAsync(),
                //CandidateUser = new CandidateUser()
            };

            IsBusy = false;
        }

        public ICommand SetNumberOfAnsweredQuestionsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    NumberOfAnsweredQuestions =
                        _candidateUser.AnswerTests.Count(test => test.ChosenAnswer != null);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


