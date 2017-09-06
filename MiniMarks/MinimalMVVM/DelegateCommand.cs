﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniMarks.MinimalMVVM {
    public class DelegateCommand : ICommand {
        public DelegateCommand(Action<object> ex, Func<object, bool> canEx = null) {
            execute = ex;
            canExecute = canEx;
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Action<object> execute;
        private Func<object, bool> canExecute;

        public bool CanExecute(object parameter) {
            return (canExecute == null || canExecute(parameter));
        }

        public void Execute(object parameter) {
            execute(parameter);
        }
    }
}
