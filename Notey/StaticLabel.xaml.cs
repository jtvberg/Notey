namespace Notey
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class StaticLabel : UserControl
    {

        public double StaticLabelFontSize
        {
            get { return this.StaticLabelContainer.FontSize; }
            set { this.StaticLabelContainer.FontSize = value; }
        }

        public FontFamily StaticLabelFontFamily
        {
            get { return this.StaticLabelContainer.FontFamily; }
            set { this.StaticLabelContainer.FontFamily = value; }
        }

        public string StaticLabelText
        {
            get { return this.StaticLabelContainer.Content.ToString(); }
            set { this.StaticLabelContainer.Content = value; }
        }

        public Color StaticLabelForecolor
        {
            get { return ((SolidColorBrush)this.StaticLabelContainer.Foreground).Color; }
            set { this.StaticLabelContainer.Foreground = new SolidColorBrush(value); }
        }

        public Color StaticLabelBackground
        {
            get { return ((SolidColorBrush)this.StaticLabelContainer.Background).Color; }
            set { this.StaticLabelContainer.Background = new SolidColorBrush(value); }
        }

        public StaticLabel()
        {
            InitializeComponent();
        }

        public string GetContent()
        {
            try
            {
                SerialOp.SerializeObject(this.StaticLabelContainer.Content);
            }
            catch
            {

            }
            return string.Empty;
        }

        public void SetContent(string s)
        {
            try
            {
                this.StaticLabelText = (string)SerialOp.DeserializeOject(s);
            }
            catch
            {

            }
        }

        private void StaticLabelEditorLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.StaticLabelEditor.IsEnabled = false;
            this.StaticLabelContainer.Content = this.StaticLabelEditor.Text;
            TreeHelpers.FindParent<Pad>(this).PadContent = SerialOp.SerializeObject(this.StaticLabelContainer.Content);
            if(this.StaticLabelContainer.Content.ToString().Trim().Length == 0)
            {
                this.StaticLabelContainer.Content = "New Label";
            }
        }

        public void ToggleEdit()
        {
            this.StaticLabelEditor.IsEnabled = true;
            this.StaticLabelEditor.Text = this.StaticLabelContainer.Content.ToString();
            this.StaticLabelEditor.Focus();
        }

        public void CopyContent()
        {
            try
            {
                Clipboard.SetText(this.StaticLabelContainer.Content.ToString());
            }
            catch
            {
                // Do Nothing
            }
        }
    }
}
