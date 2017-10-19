using System;
using System.Collections.Generic;
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

namespace EraView.Views
{
    /// <summary>
    /// Interaction logic for PrimaryChargeView.xaml
    /// </summary>
    public partial class PrimaryChargeView : UserControl
    {
        public PrimaryChargeView()
        {
            InitializeComponent();
        }

        private void EditCharge_Click(object sender, RoutedEventArgs e)
        {
            if (EditCharge.Content.ToString() == "Edit Charge")
            {
                EditCharge.Content = "Disable Charge Edit Mode";
            }

            else if (EditCharge.Content.ToString() == "Disable Charge Edit Mode")
            {
                EditCharge.Content = "Edit Charge";
            }
        }
    }
}
