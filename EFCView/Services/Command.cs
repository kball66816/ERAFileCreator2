using System;
using System.Windows.Input;

namespace EraFileCreator.Services
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
            var b = this.canExecute == null ? true : this.canExecute(parameter);
            return b;
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}