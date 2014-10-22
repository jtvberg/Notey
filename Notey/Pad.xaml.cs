namespace Notey
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class Pad : UserControl
    {
        public bool IsLocked { get; set; }

        public string PadSerialContent { get; set; }

        public Color PadForecolor { get; set; }

        public Color PadBackcolor { get; set; }

        public FontWeight PadFontWeight { get; set; }

        public FontStyle PadFontStyle { get; set; }

        public bool PadUnderlined { get; set; }

        public double PadLeft
        {
            get { return Canvas.GetLeft(this); }
            set { Canvas.SetLeft(this, value); }
        }

        public double PadTop
        {
            get { return Canvas.GetTop(this); }
            set { Canvas.SetTop(this, value); }
        }

        protected bool isDragging;

        protected bool isResizing;
        
        private Point clickPosition;

        public Pad() 
        {
            InitializeComponent();
            this.RegisterEvents();
        }

        public virtual void Load() { }

        public virtual void SetColors() { }

        public virtual void CopyContent(bool retry = false) { }

        public virtual void SetDefaults()
        {
            this.FontFamily = new FontFamily("Century Gothic");
            this.FontSize = 72;
            this.PadForecolor = Colors.White;
            this.Foreground = new SolidColorBrush(this.PadForecolor);
            this.PadBackcolor = Colors.Transparent;
            this.Background = new SolidColorBrush(this.PadBackcolor);
        }

        private void RegisterEvents()
        {
            this.MouseLeftButtonDown += PadMouseLeftButtonDown;
            this.MouseMove += PadMouseMove;
            this.MouseLeftButtonUp += PadMouseLeftButtonUp;
            this.Loaded += PadLoaded;
        }

        private void PadLoaded(object sender, RoutedEventArgs e)
        {
            this.Load();
        }

        private void PadMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.isDragging = !this.IsLocked;
            this.clickPosition = e.GetPosition(this);
            Cursor = Cursors.SizeAll;
            this.CaptureMouse();
        }

        private void PadMouseMove(object sender, MouseEventArgs e)
        {
            if (this.isDragging)
            {
                Point currentPosition = e.GetPosition(this.Parent as UIElement);
                Canvas.SetLeft(this, currentPosition.X - clickPosition.X);
                Canvas.SetTop(this, currentPosition.Y - clickPosition.Y);
            }

            if (this.isResizing)
            {
                Point currentPosition = e.GetPosition(this.Parent as UIElement);
                this.Width = currentPosition.X - clickPosition.X;
                this.Height = currentPosition.Y - clickPosition.Y;
            }
        }

        private void PadMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.isDragging = false;
            Cursor = Cursors.Arrow;
            this.ReleaseMouseCapture();
        }

        public virtual void StartEdit() { }

        public virtual void ChangeColor() { }

        private void BtnFontSizeIncreaseClick(object sender, RoutedEventArgs e)
        {
            this.FontSize+=2;
        }

        private void BtnFontSizeDecreaseClick(object sender, RoutedEventArgs e)
        {
            this.FontSize-=2;
        }

        private void BtnBoldClick(object sender, RoutedEventArgs e)
        {
            this.PadFontWeight = this.PadFontWeight == FontWeights.Bold ? FontWeights.Normal : FontWeights.Bold;
            this.SetFontWeight();
        }

        public virtual void SetFontWeight() { }

        private void BtnItalicClick(object sender, RoutedEventArgs e)
        {
            this.PadFontStyle = this.PadFontStyle == FontStyles.Italic ? FontStyles.Normal : FontStyles.Italic;
            this.SetFontStyle();
        }

        public virtual void SetFontStyle() { }

        private void BtnUnderlineClick(object sender, RoutedEventArgs e)
        {
            this.PadUnderlined = !this.PadUnderlined;
            this.SetUnderline();
        }

        public virtual void SetUnderline() { }

        private void ComboFontFamilySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.FontFamily = new FontFamily(this.ComboFontFamily.SelectedValue.ToString());
        }

        private void PadContainerMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.FormatGrid.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
