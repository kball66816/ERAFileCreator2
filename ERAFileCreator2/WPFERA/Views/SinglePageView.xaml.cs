using System.Windows.Controls;
using System.Windows.Input;
using EraFileCreator.Utility;

namespace EraFileCreator.Views
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



        private void InsuranceZipCodeInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToNumericOnly(e);
        }

       

    }
}
