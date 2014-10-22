namespace Notey
{
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Xml;

    public partial class RichPad : Pad
    {
        private RichTextBox richNoteEditor;

        private FlowDocument flowDoc;

        private Border richPadHeader;

        private Border richPadFooter;

        private bool isLoaded;

        public RichPad()
        {
            InitializeComponent();
            this.richNoteEditor = new RichTextBox();
            this.flowDoc = new FlowDocument();
            this.RegisterEvents();
        }

        private void RegisterEvents()
        {
            this.richNoteEditor.TextChanged += richNoteEditorTextChanged;
        }

        public override void Load()
        {
            this.PadContainer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10) });
            this.PadContainer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            this.PadContainer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10) });

            this.Width = 300;
            this.FontSize = 16;

            this.SetColors();

            this.richNoteEditor.BorderThickness = new Thickness(0);

            this.richPadHeader = new Border()
            {
                CornerRadius = new CornerRadius(3, 3, 0, 0),
                Background = new SolidColorBrush(Color.FromArgb(66, 255, 255, 255))
            };
            this.richPadFooter = new Border()
            {
                CornerRadius = new CornerRadius(0, 0, 3, 3),
                Background = new SolidColorBrush(Color.FromArgb(66, 255, 255, 255))
            };

            this.PadContainer.Children.Add(this.richPadHeader);
            this.PadContainer.Children.Add(this.richNoteEditor);
            this.PadContainer.Children.Add(this.richPadFooter);

            Grid.SetRow(this.richPadHeader, 0);
            Grid.SetRow(this.richNoteEditor, 1);
            Grid.SetRow(this.richPadFooter, 2);

            if (this.PadSerialContent != null && this.PadSerialContent.Length > 0)
            {
                this.LoadRtf(this.PadSerialContent); 
            }
            else
            {
                this.NewNote();
            }
        }

        public override void ChangeColor()
        {
            this.FormatGrid.Visibility = System.Windows.Visibility.Visible;
        }

        public override void SetColors()
        {
            this.richNoteEditor.Foreground = new SolidColorBrush(this.PadForecolor);
            this.richNoteEditor.Background = new SolidColorBrush(Color.FromArgb(66, this.PadBackcolor.R, this.PadBackcolor.G, this.PadBackcolor.B));
        }

        public void NewNote()
        {
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Run("New Note"));
            this.flowDoc.Blocks.Add(p);
            this.richNoteEditor.Document = this.flowDoc;
            this.isLoaded = true;
        }

        public void LoadRtf(string s)
        {
            StringReader stringReader = new StringReader(s);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            this.flowDoc = (FlowDocument)XamlReader.Load(xmlReader);
            this.PadSerialContent = s;
            this.richNoteEditor.Document = this.flowDoc;
            this.isLoaded = true;
        }

        public void SaveRtf()
        {
            this.PadSerialContent = XamlWriter.Save(this.flowDoc);
        }

        private void richNoteEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.isLoaded)
            { this.SaveRtf(); }
        }
    }
}
