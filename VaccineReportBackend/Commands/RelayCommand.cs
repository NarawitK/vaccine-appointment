using System;
using System.Windows.Input;

namespace VaccineReportBackend.Commands
{
    public class RelayCommand : ICommand
    {
        readonly Action _DoWork;
        Func<bool> _canExecuteEvaluator;

        public RelayCommand(Action work)
        : this(work, null) { }

        public RelayCommand(Action work, Func<bool> canExecuteEvaluator)
        {
            _DoWork = work ?? throw new ArgumentNullException("execute is null");
            _canExecuteEvaluator = canExecuteEvaluator;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteEvaluator == null ? true : _canExecuteEvaluator();
        }

        public void Execute(object parameter)
        {
            _DoWork();
        }
    }
}
