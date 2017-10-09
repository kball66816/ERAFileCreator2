using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EraView.Views
{
    /// <summary>
    /// Interaction logic for TestView.xaml
    /// </summary>
    public partial class SinglePageView : UserControl
    {
        public SinglePageView()
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
