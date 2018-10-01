using System;
using System.Windows.Input;

namespace EraFileCreator.Services
{
    internal class Command : ICommand
    {
        private readonly Predicate<object> _canExecute;

        private readonly Action<object> _execute;

        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public Command(Action<object> execute)
        {
            this._execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            var b = this._canExecute == null ? true : this._canExecute(parameter);
            return b;
        }

        public void Execute(object parameter)
        {
            this._execute(parameter);
        }
    }
}