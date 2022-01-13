using System;
using System.Windows;
using System.Windows.Input;

namespace FootBallScoreBoard.Control
{

    public class FlickNumericSpinner : System.Windows.Controls.Control
    {
        private Point? _flickStart;

        public event EventHandler ValueChangedByFlick;

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(FlickNumericSpinner), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, ValueCoerceValueCallback));

        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register(nameof(MaxValue), typeof(int), typeof(FlickNumericSpinner), new PropertyMetadata(99));

        public int MinValue
        {
            get => (int)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register(nameof(MinValue), typeof(int), typeof(FlickNumericSpinner), new PropertyMetadata(-9));

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(FlickNumericSpinner), new PropertyMetadata(false));

        static FlickNumericSpinner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlickNumericSpinner), new FrameworkPropertyMetadata(typeof(FlickNumericSpinner)));
        }

        public FlickNumericSpinner()
        {
            Loaded += FlickNumericSpinner_Loaded;
            Unloaded += FlickNumericSpinner_Unloaded;
        }

        private static object ValueCoerceValueCallback(DependencyObject d, object baseValue)
        {
            if (baseValue is int coerceValue
                && d is FlickNumericSpinner spinner)
            {
                if (coerceValue > spinner.MaxValue)
                {
                    return spinner.MaxValue;
                }

                if (coerceValue < spinner.MinValue)
                {
                    return spinner.MinValue;
                }

                return coerceValue;
            }
            return 0;
        }

        private void FlickNumericSpinner_Unloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= FlickNumericSpinner_Unloaded;
            Loaded -= FlickNumericSpinner_Loaded;
            MouseDown -= FlickNumericSpinner_MouseDown;
            MouseMove -= FlickNumericSpinner_MouseMove;
        }

        private void FlickNumericSpinner_Loaded(object sender, RoutedEventArgs e)
        {
            MouseDown += FlickNumericSpinner_MouseDown;
            MouseMove += FlickNumericSpinner_MouseMove;
        }

        private void FlickNumericSpinner_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(IsReadOnly)
            {
                return;
            }

            _flickStart = e.GetPosition(this);
        }

        private void FlickNumericSpinner_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsReadOnly)
            {
                return;
            }

            if (e.LeftButton != MouseButtonState.Pressed
                || _flickStart == null)
            {
                return;
            }

            var flick = e.GetPosition(this);

            //Flick Down
            if (flick.Y > (_flickStart.Value.Y + 10))
            {
                if (Value < MaxValue)
                {
                    Value++;
                    ValueChangedByFlick?.Invoke(this, EventArgs.Empty);
                }
                _flickStart = null;
                return;
            }

            //Flick Up
            if (flick.Y < (_flickStart.Value.Y - 10))
            {
                if (Value > MinValue)
                {
                    Value--;
                    ValueChangedByFlick?.Invoke(this, EventArgs.Empty);
                }
                _flickStart = null;
            }
        }

    }
}
