using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MiniMarks.MinimalMVVM;

namespace MiniMarks {
    /// <summary>
    /// Класс - модель представления для редактора заметок
    /// </summary>
    public class MarkEditiorViewModel: Observer {
        private bool isNew;
        /// <summary>
        /// Свойство - определяет, является ли изменяемая заметка новой
        /// </summary>
        public bool IsNew {
            get { return isNew; }
            set { isNew = value; OnPropertyChanged(nameof(IsNew)); }
        }

        private string flyoutHeader;
        /// <summary>
        /// Свойство - название для редактора заметок
        /// </summary>
        public string FlyoutHeader {
            get { return flyoutHeader; }
            set { flyoutHeader = value; OnPropertyChanged(nameof(FlyoutHeader)); }
        }

        private Mark editContext;
        /// <summary>
        /// Свойство - изменяемая заметка
        /// </summary>
        public Mark EditContext {
            get { return editContext; }
            set { editContext = value; OnPropertyChanged(nameof(EditContext)); }
        }

        private bool isEditOpen = false;
        /// <summary>
        /// Свойство, определяющее, является ли редактор заметок открытым или закрытым
        /// </summary>
        public bool IsEditOpen {
            get { return isEditOpen; }
            set { isEditOpen = value; OnPropertyChanged(nameof(IsEditOpen)); }
        }

        /// <summary>
        /// Метод SetContext - устанавливает заметку как контекст данных для редактора заметок
        /// </summary>
        /// <param name="mark"></param>
        public void SetContext(Mark mark = null) {
            if (mark == null) {
                EditContext = new Mark();
                IsNew = true;
                FlyoutHeader = "Новая заметка";
            }
            else {
                EditContext = new Mark {
                    Id = mark.Id,
                    Title = mark.Title,
                    MarkContent = mark.MarkContent
                };
                IsNew = false;
                FlyoutHeader = "Выбранная заметка";
            }
            IsEditOpen = true;
        }

        public MarkEditiorViewModel() { editContext = new Mark(); flyoutHeader = ""; isNew = false; }
    }
}
