using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFERA.Views
{
    /// <summary>
    /// Interaction logic for ProgramView.xaml
    /// </summary>
    public partial class PatientView : UserControl
    {
        public PatientView()
        {
            InitializeComponent();
        }

        private void Preferences_Click(object sender, RoutedEventArgs e)
        {
            NavigationBar.SelectedIndex = 3;
        }

        private void LeftPatientTab_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
