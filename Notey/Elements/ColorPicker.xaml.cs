namespace Notey
{
    using System.Drawing;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
        }

        private void ColorPickerContainerMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Bitmap myBitmap = new Bitmap(Properties.Resources.ColorSwatchSquare2);
                var x = e.GetPosition(this).X >= myBitmap.Width ? myBitmap.Width - 1 : e.GetPosition(this).X;
                var y = e.GetPosition(this).Y >= myBitmap.Height ? myBitmap.Height - 1 : e.GetPosition(this).Y;
                Canvas.SetTop(this.ColorPickerFocus, y);
                Canvas.SetLeft(this.ColorPickerFocus, x);
                Color pixelColor = myBitmap.GetPixel((int)x, (int)y);
                System.Windows.Media.Color newColor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#" + pixelColor.Name.ToUpper());
                if (Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    TreeHelpers.FindParent<Pad>(this).PadBackcolor = newColor;
                }
                else
                {
                    TreeHelpers.FindParent<Pad>(this).PadForecolor = newColor;
                }
                TreeHelpers.FindParent<Pad>(this).SetColors();
            }
            catch
            {
                // Do nothing
            }
        }
    }
}
