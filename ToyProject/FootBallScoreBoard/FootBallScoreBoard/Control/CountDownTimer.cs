using System;
using System.Windows;
using System.Windows.Threading;

namespace FootBallScoreBoard.Control
{
    [TemplatePart(Name = PartMinuteTensDigitName, Type = typeof(FlickNumericSpinner))]
    [TemplatePart(Name = PartMinuteUnitsDigitName, Type = typeof(FlickNumericSpinner))]
    [TemplatePart(Name = PartSecondsTensDigitName, Type = typeof(FlickNumericSpinner))]
    [TemplatePart(Name = PartSecondsUnitsDigitName, Type = typeof(FlickNumericSpinner))]
    public class CountDownTimer : System.Windows.Controls.Control
    {

        private const string PartMinuteTensDigitName = "PART_MinuteTensDigit";
        private const string PartMinuteUnitsDigitName = "PART_MinuteUnitsDigit";
        private const string PartSecondsTensDigitName = "PART_SecondsTensTensDigit";
        private const string PartSecondsUnitsDigitName = "PART_SecondsUnitsDigit";

        private FlickNumericSpinner _minuteTensDigitSpinner;
        private FlickNumericSpinner _minuteUnitsDigitSpinner;
        private FlickNumericSpinner _secondsTensDigitSpinner;
        private FlickNumericSpinner _secondsUnitsDigitSpinner;

        private readonly DispatcherTimer _timer;

        public TimeSpan RemainingTime
        {
            get => (TimeSpan)GetValue(RemainingTimeProperty);
            set => SetValue(RemainingTimeProperty, value);
        }

        public static readonly DependencyProperty RemainingTimeProperty =
            DependencyProperty.Register(nameof(RemainingTime), typeof(TimeSpan), typeof(CountDownTimer), new PropertyMetadata(OnRemainingTimeChanged));

        public bool IsSettingMode
        {
            get => (bool)GetValue(IsSettingModeProperty);
            set => SetValue(IsSettingModeProperty, value);
        }

        public static readonly DependencyProperty IsSettingModeProperty =
            DependencyProperty.Register(nameof(IsSettingMode), typeof(bool), typeof(CountDownTimer), new PropertyMetadata(true));

        public bool IsCountDownEnabled
        {
            get { return (bool)GetValue(IsCountDownEnabledProperty); }
            set { SetValue(IsCountDownEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsCountDownEnabledProperty =
            DependencyProperty.Register(nameof(IsCountDownEnabled), typeof(bool), typeof(CountDownTimer), new PropertyMetadata(false, OnIsCountDownEnabledChanged));

        static CountDownTimer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CountDownTimer), new FrameworkPropertyMetadata(typeof(CountDownTimer)));
        }

        public CountDownTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += Timer_Tick;
            Unloaded += CountDownTimer_Unloaded;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _minuteTensDigitSpinner = GetTemplateChild(PartMinuteTensDigitName) as FlickNumericSpinner;
            _minuteUnitsDigitSpinner = GetTemplateChild(PartMinuteUnitsDigitName) as FlickNumericSpinner;
            _secondsTensDigitSpinner = GetTemplateChild(PartSecondsTensDigitName) as FlickNumericSpinner;
            _secondsUnitsDigitSpinner = GetTemplateChild(PartSecondsUnitsDigitName) as FlickNumericSpinner;

            if (_minuteTensDigitSpinner != null)
            {
                _minuteTensDigitSpinner.ValueChangedByFlick += DigitSpinner_ValueChangedByFlick;
            }
            if (_minuteUnitsDigitSpinner != null)
            {
                _minuteUnitsDigitSpinner.ValueChangedByFlick += DigitSpinner_ValueChangedByFlick;
            }
            if (_secondsTensDigitSpinner != null)
            {
                _secondsTensDigitSpinner.ValueChangedByFlick += DigitSpinner_ValueChangedByFlick;
            }
            if (_secondsUnitsDigitSpinner != null)
            {
                _secondsUnitsDigitSpinner.ValueChangedByFlick += DigitSpinner_ValueChangedByFlick;
            }
        }
    
        private static void OnIsCountDownEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            if (d is CountDownTimer countDownTimer)
            {
                if ((bool)e.NewValue)
                {
                    countDownTimer.StartCountDown();
                    return;
                }
                countDownTimer.StopCountDown();
            }
        }

        private static void OnRemainingTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CountDownTimer)?.SetRemainingTime((TimeSpan)e.NewValue);
        }

        private void CountDownTimer_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Tick -= Timer_Tick;
            Unloaded -= CountDownTimer_Unloaded;

            if (_minuteTensDigitSpinner != null)
            {
                _minuteTensDigitSpinner.ValueChangedByFlick -= DigitSpinner_ValueChangedByFlick;
            }
            if (_minuteUnitsDigitSpinner != null)
            {
                _minuteUnitsDigitSpinner.ValueChangedByFlick -= DigitSpinner_ValueChangedByFlick;
            }
            if (_secondsTensDigitSpinner != null)
            {
                _secondsTensDigitSpinner.ValueChangedByFlick -= DigitSpinner_ValueChangedByFlick;
            }
            if (_secondsUnitsDigitSpinner != null)
            {
                _secondsUnitsDigitSpinner.ValueChangedByFlick -= DigitSpinner_ValueChangedByFlick;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (RemainingTime.TotalSeconds == 0)
            {
                IsCountDownEnabled = false;
            }

            RemainingTime = RemainingTime.Add(new TimeSpan(0, 0, -1));
        }

        private void DigitSpinner_ValueChangedByFlick(object sender, EventArgs e)
        {
            RemainingTime = GetSettingTime();
        }

        private void StartCountDown()
        {
            IsSettingMode = false;
            _timer.Start();
        }

        private void StopCountDown()
        {
            IsSettingMode = true;
            _timer.Stop();
        }

        private TimeSpan GetSettingTime()
        {
            if (_minuteTensDigitSpinner == null || _minuteUnitsDigitSpinner == null ||
                _secondsTensDigitSpinner == null || _secondsUnitsDigitSpinner == null)
            {
                return TimeSpan.Zero;
            }

            var minute = _minuteTensDigitSpinner.Value * 10 + _minuteUnitsDigitSpinner.Value;
            var seconds = _secondsTensDigitSpinner.Value * 10 + _secondsUnitsDigitSpinner.Value;

            return new TimeSpan(0, minute, seconds);
        }

        private void SetRemainingTime(TimeSpan time)
        {
            if (_minuteTensDigitSpinner != null && _minuteUnitsDigitSpinner != null)
            {
                _minuteTensDigitSpinner.Value = (time.TotalMinutes > 0) ? (int)(time.TotalMinutes / 10) : 0;
                _minuteUnitsDigitSpinner.Value = (time.TotalMinutes > 0) ? (int)(time.TotalMinutes % 10) : 0;
            }

            if (_secondsTensDigitSpinner != null && _secondsUnitsDigitSpinner != null)
            {
                _secondsTensDigitSpinner.Value = (time.Seconds > 0) ? time.Seconds / 10 : 0;
                _secondsUnitsDigitSpinner.Value = (time.Seconds > 0) ? time.Seconds % 10 : 0;
            }
        }
    }
}
