namespace Notey
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class LabelPad : Pad
    {
        private Label labelPadDisplay;

        private TextBlock blockPadDisplay;

        private TextBox labelPadEditor;

        private string defaultText = "New Label";

        public LabelPad()
        {
            InitializeComponent();
            this.labelPadDisplay = new Label();
            this.blockPadDisplay = new TextBlock();
            this.labelPadEditor = new TextBox();
            this.RegisterEvents();
        }

        public override void Load()
        {
            if (this.PadSerialContent != null && this.PadSerialContent.Trim().Length > 0)
            { this.GetContent(); }
            else
            { this.blockPadDisplay.Text = this.defaultText; }

            this.labelPadDisplay.Content = this.blockPadDisplay;
            this.labelPadEditor.Text = this.blockPadDisplay.Text;
            this.labelPadEditor.Visibility = System.Windows.Visibility.Hidden;
            this.labelPadEditor.Margin = new Thickness(0,2,0,0);

            this.SetColors();
            this.SetFontWeight();
            this.SetFontStyle();
            this.SetUnderline();

            this.PadContainer.Children.Add(this.labelPadEditor);
            this.PadContainer.Children.Add(this.labelPadDisplay);
        }

        private void RegisterEvents()
        {
            this.labelPadEditor.LostKeyboardFocus += LabelPadLostKeyboardFocus;
        }

        public override void SetFontWeight()
        {
            this.labelPadDisplay.FontWeight = this.PadFontWeight;
            this.labelPadEditor.FontWeight = this.PadFontWeight;
        }

        public override void SetFontStyle()
        {
            this.labelPadDisplay.FontStyle = this.PadFontStyle;
            this.labelPadEditor.FontStyle = this.PadFontStyle;
        }

        public override void SetUnderline()
        {
            if (this.PadUnderlined)
            { this.blockPadDisplay.TextDecorations = new TextDecorationCollection(TextDecorations.Underline); }
            else
            { this.blockPadDisplay.TextDecorations = null; }
        }

        public override void CopyContent(bool retry = false)
        {
            try
            {
                if (retry)
                { System.Threading.Thread.Sleep(1000); }
                Clipboard.SetText(this.blockPadDisplay.Text);
            }
            catch
            {
                if (!retry)
                { this.CopyContent(true); }
            }
        }

        public override void StartEdit()
        {
            this.labelPadEditor.Visibility = System.Windows.Visibility.Visible;
            this.labelPadDisplay.Visibility = System.Windows.Visibility.Hidden;
            this.labelPadEditor.Text = this.blockPadDisplay.Text;
            this.labelPadEditor.Focus();
        }

        public override void ChangeColor()
        {
            this.FormatGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void EndEdit()
        {
            this.blockPadDisplay.Text = this.labelPadEditor.Text;
            this.labelPadDisplay.Visibility = System.Windows.Visibility.Visible;
            this.labelPadEditor.Visibility = System.Windows.Visibility.Hidden;
            if (this.blockPadDisplay.Text.Trim().Length == 0)
            {
                this.blockPadDisplay.Text = this.defaultText;
            }
            else
            {
                this.SetContent();
            }
        }

        public override void SetColors()
        {
            this.labelPadDisplay.Foreground = new SolidColorBrush(this.PadForecolor);
            this.labelPadDisplay.Background = new SolidColorBrush(this.PadBackcolor);
        }

        private void SetContent()
        {
            try 
            {
                this.PadSerialContent = SerialOp.SerializeObject(this.blockPadDisplay.Text);
            }
            catch
            {
                throw; //TODO: Handle
            }
        }

        private void GetContent()
        {
            try
            {
                this.blockPadDisplay.Text = SerialOp.DeserializeOject(this.PadSerialContent).ToString().Trim();
            }
            catch
            {
                throw; //TODO: Handle
            }
        }

        private void LabelPadLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.EndEdit();
        }
    }
}
