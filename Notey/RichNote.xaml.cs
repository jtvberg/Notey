namespace Notey
{
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Markup;
    using System.Xml;

    public partial class RichNote : UserControl
    {
        public string RichNoteContent { get; set; }

        private FlowDocument flowDoc;

        private bool isLoaded;

        public RichNote()
        {
            InitializeComponent();
            this.flowDoc = new FlowDocument();
        }

        public void NewNote()
        {
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Run("New Note"));
            this.flowDoc.Blocks.Add(p);
            this.RichNoteContainer.Document = this.flowDoc;
            this.isLoaded = true;
        }

        public void LoadRtf(string s)
        {
            StringReader stringReader = new StringReader(s);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            this.flowDoc = (FlowDocument)XamlReader.Load(xmlReader);
            this.RichNoteContent = s;
            this.RichNoteContainer.Document = this.flowDoc;
            this.isLoaded = true;
        }

        public void SaveRtf()
        {
            this.RichNoteContent = XamlWriter.Save(this.flowDoc);
            TreeHelpers.FindParent<Pad>(this).PadSerialContent = this.RichNoteContent;
        }

        private void RichNoteContainerTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.isLoaded)
            { this.SaveRtf(); }
        }
    }
}
