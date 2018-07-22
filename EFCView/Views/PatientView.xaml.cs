using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EraFileCreator.Utility;

namespace EraFileCreator.Views
{
    /// <summary>
    ///     Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView : UserControl
    {
        public PatientView()
        {
            this.InitializeComponent();
        }

        private void RenderingProviderNpiInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToIntegerOnly(e);
        }

        private void PatientFirstNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PatientFirstNameInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void SubscriberFirstNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void SubscriberLastNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void RenderingProviderNpiInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void SubscriberMemberIdnput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void RenderingProviderFirstNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PatientLastNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PatientMemberId_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void RenderingProviderLastNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }
    }
}