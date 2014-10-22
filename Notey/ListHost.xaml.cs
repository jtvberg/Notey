namespace Notey
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    public partial class ListHost : UserControl
    {
        private ObservableCollection<ListItem> ListItems;

        public ListHost()
        {
            InitializeComponent();
        }

        public ListHost(ObservableCollection<ListItem> o)
        {
            InitializeComponent();
            this.ListItems = o;
            foreach (ListItem li in this.ListItems)
            {
                this.AddChild(li);
            }
        }

        private void AddListItem()
        {
            this.ListItems.Add(new ListItem());
        }

        public void ToggleEdit()
        {

        }
    }
}
