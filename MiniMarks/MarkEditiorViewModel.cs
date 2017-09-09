using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MiniMarks.MinimalMVVM;

namespace MiniMarks {
    public class MarkEditiorViewModel: Observer {
        private bool isNew;
        public bool IsNew {
            get { return isNew; }
            set { isNew = value; OnPropertyChanged(nameof(IsNew)); }
        }

        private string flyoutHeader;
        public string FlyoutHeader {
            get { return flyoutHeader; }
            set { flyoutHeader = value; OnPropertyChanged(nameof(FlyoutHeader)); }
        }

        private Mark editContext;
        public Mark EditContext {
            get { return editContext; }
            set { editContext = value; OnPropertyChanged(nameof(EditContext)); }
        }

        private bool isEditOpen = false;
        public bool IsEditOpen {
            get { return isEditOpen; }
            set { isEditOpen = value; OnPropertyChanged(nameof(IsEditOpen)); }
        }

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

        public MarkEditiorViewModel() { editContext = new Mark(); flyoutHeader = "FUCK YOU!"; isNew = false; }
    }
}
