using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;

namespace FootBallScoreBoard.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private int _homeScore;
        public int HomeScore
        {
            get => _homeScore;
            set => Set(ref _homeScore, value);
        }

        private int _awayScore;
        public int AwayScore
        {
            get => _awayScore;
            set => Set(ref _awayScore, value);
        }


        private TimeSpan _remainingTime;
        public TimeSpan RemainingTime
        {
            get => _remainingTime;
            set => Set(ref _remainingTime, value);
        }


        private ICommand _resetScoreCommand;
        public ICommand ResetScoreCommand => _resetScoreCommand ?? (_resetScoreCommand = new RelayCommand(ResetScore));

        private ICommand _swapScoreCommand;
        public ICommand SwapScoreCommand => _swapScoreCommand ?? (_swapScoreCommand = new RelayCommand(SwapScore));

        private ICommand _setMinuteCommand;
        public ICommand SetMinuteCommand => _setMinuteCommand ?? (_setMinuteCommand = new RelayCommand<int>(SetRemainingMinute));

        private ICommand _addHomeScoreCommand;
        public ICommand AddHomeScoreCommand => _addHomeScoreCommand ?? (_addHomeScoreCommand = new RelayCommand<int>(AddHomeScore));

        private ICommand _addAwayScoreCommand;
        public ICommand AddAwayScoreCommand => _addAwayScoreCommand ?? (_addAwayScoreCommand = new RelayCommand<int>(AddAwayScore));

        private void AddHomeScore(int value)
        {
            HomeScore += value;
        }

        private void AddAwayScore(int value)
        {
            AwayScore += value;
        }

        private void ResetScore()
        {
            HomeScore = 0;
            AwayScore = 0;
        }

        private void SwapScore()
        {
            int temp = HomeScore;
            HomeScore = AwayScore;
            AwayScore = temp;
        }

        private void SetRemainingMinute(int minute)
        {
            RemainingTime = new TimeSpan(0, minute, 0);
        }
    }
}