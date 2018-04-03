using System.Windows.Controls;
using System.Windows.Input;
using Common.Common.Extensions;
using EraFileCreator.Utility;

namespace EraFileCreator.Views
{
    /// <summary>
    ///     Interaction logic for PrimaryChargeView.xaml
    /// </summary>
    public partial class PrimaryChargeView : UserControl
    {
        public PrimaryChargeView()
        {
            InitializeComponent();
        }

        private void PrimaryChargesAmountInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.ToString() == ".")
            {

            }
        }
    }
}