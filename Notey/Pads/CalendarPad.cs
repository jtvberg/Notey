using System.Windows.Media;
namespace Notey
{
    public partial class CalendarPad : Pad
    {
        public CalendarPad()
        {

        }

        public override void Load()
        {
            CalendarElement ce = new CalendarElement();
            ce.Background = new SolidColorBrush(Colors.Transparent);
            this.PadContainer.Children.Add(ce);
        }

        public override void ChangeColor()
        {
            this.FormatGrid.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
