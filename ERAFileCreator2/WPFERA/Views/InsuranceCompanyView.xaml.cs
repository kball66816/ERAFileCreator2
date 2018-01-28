using System.Windows.Controls;
using System.Windows.Input;
using EraFileCreator.Utility;

namespace EraFileCreator.Views
{
    /// <summary>
    ///     Interaction logic for InsuranceCompanyView.xaml
    /// </summary>
    public partial class InsuranceCompanyView : UserControl
    {
        public InsuranceCompanyView()
        {
            InitializeComponent();
        }

        private void InsuranceZipCodeInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToIntegerOnly(e);
        }
    }
}