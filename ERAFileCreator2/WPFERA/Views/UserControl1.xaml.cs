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

namespace WPFERA.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void RestrictTextToNumericOnly(TextCompositionEventArgs e)
        {

            if (!(int.TryParse(e.Text, out int result)))
            {
                e.Handled = true;
            }
        }

        private void InsuranceZipCodeInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            RestrictTextToNumericOnly(e);
        }

        private void Preferences_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
