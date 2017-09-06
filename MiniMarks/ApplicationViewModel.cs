using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniMarks.MinimalMVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace MiniMarks {
    public class ApplicationViewModel: Observer {
        private MarkModel selectedMark;
        private bool isEditOpen = false;
        private string flyoutHeader;
        private Visibility saveButtonVs;
        public ObservableCollection<MarkModel> Marks { get; set; }

        public MarkModel SelectedMark {
            get { return selectedMark; }
            set { selectedMark = value; OnPropertyChanged(nameof(SelectedMark)); }
        }

        public Visibility SaveButtonVs {
            get { return saveButtonVs; }
            set { saveButtonVs = value; OnPropertyChanged(nameof(SaveButtonVs)); }
        }

        public bool IsEditOpen {
            get { return isEditOpen; }
            set { isEditOpen = value;
                OnPropertyChanged(nameof(IsEditOpen));
                if (value == false) SelectedMark = null;
            }
        }

        public string FlyoutHeader {
            get { return flyoutHeader; }
            set { flyoutHeader = value; OnPropertyChanged(nameof(FlyoutHeader)); }
        }

        public DelegateCommand OpenNewMarkCommand {
            get { return new DelegateCommand(obj => { onOpenNewMark(); }); }
        }

        public DelegateCommand SaveNewMarkCommand {
            get { return new DelegateCommand(obj => onSaveNewMark()); }
        }

        public DelegateCommand OpenEditiorCommand {
            get {
                return new DelegateCommand(obj => {
                    SelectedMark = obj as MarkModel;
                    FlyoutHeader = "Выбранная заметка";
                    SaveButtonVs = Visibility.Collapsed; IsEditOpen = true;
                });
            }
        }

        public DelegateCommand DeleteMarkCommand {
            get { return new DelegateCommand(obj => {
                MarkModel mark = obj as MarkModel;
                Marks.Remove(mark);
            }); }
        }

        private void onSaveNewMark() {
            Marks.Insert(0, SelectedMark);
            IsEditOpen = false;
        }

        private void onOpenNewMark() {
            FlyoutHeader = "Новая заметка";
            SaveButtonVs = Visibility.Visible;
            SelectedMark = new MarkModel();
            IsEditOpen = true;
        }
        
        public ApplicationViewModel() {
            Marks = new ObservableCollection<MarkModel> {
                new MarkModel {Title = "First Mark", MarkContent = "It's my first mark\nGood job" },
                new MarkModel {Title = "Remeber the Milk", MarkContent = "From Russia\nWith love" }
            };
        }
    }
}
