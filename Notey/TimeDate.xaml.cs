namespace Notey
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Threading;

    public partial class TimeDate : UserControl
    {
        public bool ShowDate { get; set; }

        public bool ShowTime { get; set; }

        public double TimeDateFontSize
        {
            get { return this.DateContainer.FontSize; }
            set { this.DateContainer.FontSize = value; }
        }

        public FontFamily TimeDateFontFamily
        {
            get { return this.DateContainer.FontFamily; }
            set { this.DateContainer.FontFamily = value; }
        }

        public Color TimeDateForecolor
        {
            get { return ((SolidColorBrush)this.DateContainer.Foreground).Color; }
            set { this.DateContainer.Foreground = new SolidColorBrush(value); }
        }

        public Color TimeDateBackground
        {
            get { return ((SolidColorBrush)this.DateContainer.Background).Color; }
            set { this.DateContainer.Background = new SolidColorBrush(value); }
        }

        public TimeDate(bool showDate = true, bool showTime = true)
        {
            InitializeComponent();
            this.ShowDate = showDate;
            this.ShowTime = showTime;
            this.Load();
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0,0,1), DispatcherPriority.Normal, delegate { this.UpdateDateTime(); }, this.Dispatcher);
        }

        private void Load()
        {
            if (this.ShowDate)
            {
                this.DateContainer.Content = DateTime.Now.ToShortDateString();
                this.DateContainer.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.DateContainer.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (this.ShowTime)
            {
                this.TimeContainer.Content = DateTime.Now.ToShortTimeString();
                this.TimeContainer.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.TimeContainer.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void UpdateDateTime()
        {
            this.DateContainer.Content = DateTime.Now.ToShortDateString();
            this.TimeContainer.Content = DateTime.Now.ToShortTimeString();
        }

        public void CopyContent()
        {
            try
            {
                string dt = DateTime.Now.ToShortDateString();
                string tm = DateTime.Now.ToShortTimeString();
                Clipboard.SetText(((ShowTime ? tm : "") + " " + (ShowDate ? dt : "")).Trim());
            }
            catch
            {
                // Do Nothing
            }
        }

        public void ToggleEdit()
        {
            this.ShowEdit(true);
        }

        private void ShowEdit(bool show = true)
        {
            if (show)
            {
                this.ToggleDate.Visibility = System.Windows.Visibility.Visible;
                this.ToggleTime.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.ToggleDate.Visibility = System.Windows.Visibility.Collapsed;
                this.ToggleTime.Visibility = System.Windows.Visibility.Collapsed;
                TreeHelpers.FindParent<PadHost>(this).ClearEditFocus();
                this.Load();
            }
            this.ToggleDate.IsEnabled = this.ShowTime;
            this.ToggleTime.IsEnabled = this.ShowDate;
        }

        private void ToggleDateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ShowDate = !this.ShowDate;
            this.ToggleDate.Content = this.ShowDate ? "Ó" : "Ì";
            this.ShowEdit(false);
        }

        private void ToggleTimeClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ShowTime = !this.ShowTime;
            this.ToggleTime.Content = this.ShowTime ? "Ó" : "Ì";
            this.ShowEdit(false);
        }
    }
}
