using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MiniMarks.MinimalMVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Data.Entity;
using System.Runtime.CompilerServices;

namespace MiniMarks {
    /// <summary>
    /// Модель представления для основной страницы приложения
    /// Наследуется от класса Observer
    /// </summary>
    public class ApplicationViewModel: Observer {
        ApplicationContext db;
        private Mark selectedMark;
        private MarkEditiorViewModel editVM;
        public ObservableCollection<Mark> marks;
        /// <summary>
        /// Модель представления для редактора заметок
        /// </summary>
        public MarkEditiorViewModel EditVM { get { return editVM; } }
        /// <summary>
        /// Выбранная заметка
        /// </summary>
        public Mark SelectedMark {
            get { return selectedMark; }
            set { selectedMark = value; OnPropertyChanged(nameof(SelectedMark)); }
        }
        /// <summary>
        /// Все заметки (видимые из базы данных)
        /// </summary>
        public ObservableCollection<Mark> Marks {
            get { return marks; }
            set { marks = value; OnPropertyChanged(nameof(Marks)); }
        }
        /// <summary>
        /// Команда, вызываемая при нажатии на кнопку "Add_Mark"
        /// </summary>
        public DelegateCommand OpenNewMarkCommand {
            get { return new DelegateCommand(obj => { onOpenNewMark(); }); }
        }
        /// <summary>
        /// Команда, вызываемая при нажатии на кнопку "Save_Mark"
        /// </summary>
        public DelegateCommand SaveMarkCommand {
            get { return new DelegateCommand(obj => { onSaveMark(); }); }
        }
        /// <summary>
        /// Команда, вызываемая при клике на любую заметку в списке заметок. Открывает редактор заметок.
        /// </summary>
        public DelegateCommand OpenEditiorCommand {
            get {
                return new DelegateCommand(obj => {
                    SelectedMark = obj as Mark;
                    EditVM.SetContext(SelectedMark);
                });
            }
        }
        /// <summary>
        /// Команда, вызываемая при клике на "Del_Mark" и удаляющая заметку из "Marks" и асинхронно из базы данных.
        /// </summary>
        public DelegateCommand DeleteMarkCommand {
            get { return new DelegateCommand(obj => {
                Mark mark = obj as Mark;
                if (mark == null) return;
                Marks.Remove(mark);
                db.Marks.Remove(mark);
                db.SaveChanges();
            }); }
        }
        /// <summary>
        /// Метод, вызываемый командой "SaveMarkCommand" и сохраняющий данные о заметке внутри программы.
        /// Вызывает метод "onSaveMarkAsync"
        /// </summary>
        private void onSaveMark() {
            if (EditVM.EditContext == null) return;

            if (EditVM.IsNew) {
                Marks.Insert(0, EditVM.EditContext);
            } else {
                SelectedMark.Title = EditVM.EditContext.Title;
                SelectedMark.MarkContent = EditVM.EditContext.MarkContent;
            }
            onSaveMarkAsync();
            EditVM.IsEditOpen = false;
        }
        /// <summary>
        /// Асинхронный метод сохранения данных о заметке в базу данных. Вызывается из метода "onSaveMark"
        /// </summary>
        /// <returns></returns>
        private async Task onSaveMarkAsync() {
            if (EditVM.IsNew) {
                db.Marks.Add(EditVM.EditContext);
                await db.SaveChangesAsync();
            } else {
                Mark mark = await db.Marks.FindAsync(EditVM.EditContext.Id);
                if (mark == null) return;
                mark.Title = EditVM.EditContext.Title;
                mark.MarkContent = EditVM.EditContext.MarkContent;
                db.Entry(mark).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Метод, вызываемый командой "OpenNewMarkCommand" и обновляющий контекст данных для модели представления EditVM
        /// </summary>
        private void onOpenNewMark() {
            EditVM.SetContext();
            SelectedMark = null;
            EditVM.IsEditOpen = true;
        }
        /// <summary>
        /// Конструктор класса.
        /// Загружает данные из базы данных.
        /// </summary>
        public ApplicationViewModel() {
            editVM = new MarkEditiorViewModel();
            db = new ApplicationContext();
            db.Marks.Load();
            Marks = new ObservableCollection<Mark>(db.Marks.Local.OrderByDescending(mark => mark.Id));
        }
    }
}
