namespace Notey
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    public partial class StaticImage : UserControl
    {
        public string StaticImageContent { get; set; }

        public StaticImage()
        {
            InitializeComponent();
            this.AllowDrop = true;
        }

        public void SetImageSource(string s)
        {
            this.StaticImageContainer.Source = SerialOp.ConvertToImage((byte[])SerialOp.DeserializeOject(s));
            this.StaticImageContent = s;
            this.StaticImageOuterBorder.Background = null;
        }

        public void ToggleEdit()
        {

        }

        public void CopyContent()
        {
            try
            {
                Clipboard.SetData(DataFormats.Bitmap, this.StaticImageContainer.Source);
            }
            catch
            {
                // Do Nothing
            }
        }

        private void HandleFileOpen(string p)
        {
            BitmapImage bi = new BitmapImage(new Uri(p));
            this.StaticImageContainer.Source = bi;
            this.StaticImageOuterBorder.Background = null;
            this.StaticImageContent = SerialOp.SerializeObject(SerialOp.ConvertToBytes(bi));
            TreeHelpers.FindParent<Pad>(this).PadSerialContent = this.StaticImageContent;
        }

        private void StaticImageContainerDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                this.HandleFileOpen(files[0]);
            }
        }
    }
}
