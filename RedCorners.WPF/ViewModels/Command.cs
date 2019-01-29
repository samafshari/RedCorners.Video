using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedCorners.WPF.ViewModels
{
    public class Command : System.Windows.Input.ICommand
    {
        private Action _action;
        private bool _canExecute;
        public Command(Action action, bool canExecute = true)
        {
            _action = action;
            _canExecute = canExecute;
            CanExecuteChanged?.Invoke(this, null);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
