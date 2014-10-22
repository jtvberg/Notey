namespace Notey
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public partial class ImagePad : Pad
    {
        private Image imagePadDisplay;

        public ImagePad()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.imagePadDisplay = new Image();
            this.RegisterEvents();
        }

        private void RegisterEvents()
        {
            this.Drop += ImagePadDrop;
        }

        private void ImagePadDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                this.HandleFileOpen(files[0]);
            }
        }

        public override void Load()
        {
            if (this.PadSerialContent == null)
            {
                this.PadContainer.Background = new SolidColorBrush(Colors.Red);
                this.imagePadDisplay.Width = 300;
                this.imagePadDisplay.Height = 300;
            }
            else
            {
                this.SetImageSource();
            }

            this.PadContainer.Children.Add(this.imagePadDisplay);
        }

        public override void CopyContent(bool retry = false)
        {
            try
            {
                if (retry)
                { System.Threading.Thread.Sleep(1000); }
                Clipboard.SetData(DataFormats.Bitmap, this.imagePadDisplay.Source);
            }
            catch
            {
                if (!retry)
                { this.CopyContent(true); }
            }
        }

        public void SetImageSource()
        {
            try
            {
                this.imagePadDisplay.Source = SerialOp.ConvertToImage((byte[])SerialOp.DeserializeOject(this.PadSerialContent));
            }
            catch
            {
                //TODO: Handle
            }
        }

        private void HandleFileOpen(string p)
        {
            try
            {
                BitmapImage bi = new BitmapImage(new Uri(p));
                this.imagePadDisplay.Source = bi;
                this.PadSerialContent = SerialOp.SerializeObject(SerialOp.ConvertToBytes(bi));
                this.PadContainer.Background = null;
            }
            catch
            {
                //TODO: Handle
            }
        }

        public override void StartEdit()
        {
            //TODO: Add edit
        }

        private void EndEdit()
        {
            //TODO: End edit
        }
    }
}
