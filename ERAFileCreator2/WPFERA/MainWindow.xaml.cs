using System;
using System.Windows;
using System.Windows.Threading;
using Common.Common.Services;
using PatientManagement.ViewModel;
using PatientManagement.ViewModel.Services;

namespace EraFileCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, 
                new Action(() =>
                {
                    Messenger.Default.Send(new InitializationCompleteMessage());
                    
                }));

        }
    }
}