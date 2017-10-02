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

       
    }
}
