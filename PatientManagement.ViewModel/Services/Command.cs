using System;
using System.Windows.Input;

namespace PatientManagement.ViewModel.Services
{
    internal class Command : ICommand
    {
        private readonly Predicate<object> canExecute;

        private readonly Action<object> execute;

        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public Command(Action<object> execute)
        {
            this.execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            var b = canExecute == null ? true : canExecute(parameter);
            return b;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}