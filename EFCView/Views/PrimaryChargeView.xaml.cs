using System;
using System.Runtime.InteropServices;
using System.Windows;
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

        private void HightlightAllText(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void IgnoreDecimalInput(object sender, KeyEventArgs e)
        {
            InputRules.IgnoreDecimalInput(sender, e);
        }
    }
}