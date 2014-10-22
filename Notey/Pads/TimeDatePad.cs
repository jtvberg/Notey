namespace Notey
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    public partial class TimeDatePad : Pad
    {
        public bool ShowDate { get; set; }

        public bool ShowTime { get; set; }

        private Label dateContainer;

        private Label timeContainer;

        public TimeDatePad(bool showDate = true, bool showTime = true)
        {
            InitializeComponent();
            this.dateContainer = new Label();
            this.timeContainer = new Label();
            this.ShowDate = showDate;
            this.ShowTime = showTime;
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0,0,1), DispatcherPriority.Normal, delegate { this.UpdateDateTime(); }, this.Dispatcher);
        }

        public override void Load()
        {
            this.PadContainer.RowDefinitions.Add(new RowDefinition());
            this.PadContainer.RowDefinitions.Add(new RowDefinition());
            this.PadContainer.RowDefinitions.Add(new RowDefinition());
            var vbd = new Viewbox()
            {
                Stretch = Stretch.UniformToFill,
                Margin = new Thickness(-this.FontSize/10)
            };
            var vbt = new Viewbox()
            {
                Stretch = Stretch.UniformToFill,
                Margin = new Thickness(-this.FontSize/10)
            };

            if (this.ShowDate)
            {
                this.dateContainer.Content = DateTime.Now.ToShortDateString();
                this.dateContainer.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.dateContainer.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (this.ShowTime)
            {
                this.timeContainer.Content = DateTime.Now.ToShortTimeString();
                this.timeContainer.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.timeContainer.Visibility = System.Windows.Visibility.Collapsed;
            }

            vbd.Child = this.dateContainer;
            vbt.Child = this.timeContainer;

            this.SetColors();
            this.SetFontWeight();
            this.SetFontStyle();

            this.PadContainer.Children.Add(vbd);
            this.PadContainer.Children.Add(vbt);
            Grid.SetRow(vbd, 1);
        }

        private void UpdateDateTime()
        {
            this.dateContainer.Content = DateTime.Now.ToShortDateString();
            this.timeContainer.Content = DateTime.Now.ToShortTimeString();
        }

        public override void ChangeColor()
        {
            this.FormatGrid.Visibility = System.Windows.Visibility.Visible;
        }

        public override void SetColors()
        {
            this.dateContainer.Foreground = new SolidColorBrush(this.PadForecolor);
            this.dateContainer.Background = new SolidColorBrush(this.PadBackcolor);
            this.timeContainer.Foreground = new SolidColorBrush(this.PadForecolor);
            this.timeContainer.Background = new SolidColorBrush(this.PadBackcolor);
        }
        public override void SetFontWeight()
        {
            this.dateContainer.FontWeight = this.PadFontWeight;
            this.timeContainer.FontWeight = this.PadFontWeight;
        }

        public override void SetFontStyle()
        {
            this.dateContainer.FontStyle = this.PadFontStyle;
            this.timeContainer.FontStyle = this.PadFontStyle;
        }

        public override void StartEdit()
        {
            //TODO: Add edit
        }

        private void EndEdit()
        {
            //TODO: End edit
        }

        public override void CopyContent(bool retry = false)
        {
            try
            {
                if (retry)
                { System.Threading.Thread.Sleep(1000); }
                string dt = DateTime.Now.ToShortDateString();
                string tm = DateTime.Now.ToShortTimeString();
                Clipboard.SetText(((ShowTime ? tm : "") + " " + (ShowDate ? dt : "")).Trim());
            }
            catch
            {
                if (!retry)
                { this.CopyContent(true); }
            }
        }
    }
}
