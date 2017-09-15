using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniMarks.MinimalMVVM {
    /// <summary>
    /// Класс для обработки комманд
    /// </summary>
    public class DelegateCommand : ICommand {
        /// <summary>
        /// Конструктор класса DelegateCommand
        /// </summary>
        /// <param name="ex">Делегат Action, принимающий методы с одним параметром типа object</param>
        /// <param name="canEx">Делегат Func, принимающий методы с одним параметром object и возвращающие тип bool</param>
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
        /// <summary>
        /// Метод CanExecute, контролирующий возможность вызова команды
        /// </summary>
        /// <param name="parameter">Параметр типа object</param>
        /// <returns>Возвращает true или false - возможно ли запустить логику команды</returns>
        public bool CanExecute(object parameter) {
            return (canExecute == null || canExecute(parameter));
        }
        /// <summary>
        /// Метод Execute, являющийся логикой команды
        /// </summary>
        /// <param name="parameter">Параметр типа object</param>
        public void Execute(object parameter) {
            execute(parameter);
        }
    }
}
