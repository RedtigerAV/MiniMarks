using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MiniMarks.MinimalMVVM {
    /// <summary>
    /// Класс, следящий за изменениями пользовательского интерфейса засчет привязок
    /// </summary>
    public abstract class Observer : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Метод, вызывающий свойство PropertyChanged для рендеринга
        /// </summary>
        /// <param name="name">Имя свойства, подлежащего рендерингу</param>
        public void OnPropertyChanged(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
