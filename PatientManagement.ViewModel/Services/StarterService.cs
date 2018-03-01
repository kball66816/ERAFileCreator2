using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Common.Services;

namespace PatientManagement.ViewModel.Services
{
    public static class StarterService
    {
        static StarterService()
        {
            Messenger.Default.Register<InitializationCompleteMessage>(typeof(StarterService),OnInitializationComplete);
        }

        private static void OnInitializationComplete(InitializationCompleteMessage obj)
        {
            InitializationComplete = true;
        }

        public static bool InitializationComplete { get; private set; }
    }
}
