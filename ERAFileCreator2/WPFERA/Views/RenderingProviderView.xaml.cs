using System.Windows.Controls;
using System.Windows.Input;
using EraFileCreator.Utility;

namespace EraFileCreator.Views
{
    /// <summary>
    ///     Interaction logic for RenderingProviderView.xaml
    /// </summary>
    public partial class RenderingProviderView : UserControl
    {
        public RenderingProviderView()
        {
            InitializeComponent();
        }

        private void RenderingProviderNpiInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToIntegerOnly(e);
        }
    }
}