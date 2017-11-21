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
using EraView.Utility;

namespace EraView.Views
{
    /// <summary>
    /// Interaction logic for RenderingProviderView.xaml
    /// </summary>
    public partial class RenderingProviderView : UserControl
    {
        public RenderingProviderView()
        {
            InitializeComponent();
        }

        private void RenderingProviderNpiInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToNumericOnly(e);
        }
    }
}
