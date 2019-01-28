using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MedicalBot.Common
{
    public class ActionCommand : ICommand
    {
        private readonly Action _methodToExecute;
        private readonly Func<Boolean> _methodToDetectCanExecute;

        public ActionCommand(Action methodToExecute, Func<Boolean> methodToDetectCanExecute)
        {
            _methodToDetectCanExecute = methodToDetectCanExecute;
            _methodToExecute = methodToExecute;
        }

        public ActionCommand(Action methodToExecute) : this(methodToExecute, null) {}

        public bool CanExecute(object parameter)
        {
            return _methodToDetectCanExecute != null ? _methodToDetectCanExecute() : true;
        }

        public void Execute(object parameter)
        {
            try
            {
                _methodToExecute();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void RaiseCanExecutedChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
