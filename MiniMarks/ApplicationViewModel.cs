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
        public ObservableCollection<MarkModel> Marks { get; set; }

        public MarkModel SelectedMark {
            get { return selectedMark; }
            set { selectedMark = value; OnPropertyChanged(nameof(SelectedMark)); }
        }

        public bool IsEditOpen {
            get { return isEditOpen; }
            set { isEditOpen = value; OnPropertyChanged(nameof(IsEditOpen)); }
        }

        public DelegateCommand OpenEditiorCommand {
            get { return new DelegateCommand(obj => { SelectedMark = obj as MarkModel; IsEditOpen = true; }); }
        }
        
        public ApplicationViewModel() {
            Marks = new ObservableCollection<MarkModel> {
                new MarkModel {Title = "First Mark", MarkContent = "It's my first mark\nGood job" },
                new MarkModel {Title = "Remeber the Milk", MarkContent = "From Russia\nWith love" }
            };
        }
    }
}
