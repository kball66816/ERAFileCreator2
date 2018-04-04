﻿using System;
using System.Windows;
using System.Windows.Threading;
using Common.Common.Services;

namespace EraFileCreator
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                new Action(() =>
                {
                    Messenger.Default.Send(new InitializationCompleteMessage(),"Initialization Complete");
                }));
        }
    }
}