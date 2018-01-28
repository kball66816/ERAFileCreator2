using System.Windows.Controls;
using System.Windows.Input;
using EraFileCreator.Utility;

namespace EraFileCreator.Views
{
    /// <summary>
    ///     Interaction logic for AddonAdjustmentView.xaml
    /// </summary>
    public partial class AddonAdjustmentView : UserControl
    {
        public AddonAdjustmentView()
        {
            InitializeComponent();
        }

        private void AddonAdjustmentAmountInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToDecimalOnly(e);
        }
    }
}