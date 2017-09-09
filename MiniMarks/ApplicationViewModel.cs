﻿using System;
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
    public class ApplicationViewModel: Observer {
        ApplicationContext db;
        private Mark selectedMark;
        private MarkEditiorViewModel editVM;
        public ObservableCollection<Mark> marks;

        public MarkEditiorViewModel EditVM { get { return editVM; } }

        public Mark SelectedMark {
            get { return selectedMark; }
            set { selectedMark = value; OnPropertyChanged(nameof(SelectedMark)); }
        }

        public ObservableCollection<Mark> Marks {
            get { return marks; }
            set { marks = value; OnPropertyChanged(nameof(Marks)); }
        }

        public DelegateCommand OpenNewMarkCommand {
            get { return new DelegateCommand(obj => { onOpenNewMark(); }); }
        }

        public DelegateCommand SaveMarkCommand {
            get { return new DelegateCommand(obj => { onSaveMark(); }); }
        }

        public DelegateCommand OpenEditiorCommand {
            get {
                return new DelegateCommand(obj => {
                    SelectedMark = obj as Mark;
                    EditVM.SetContext(SelectedMark);
                });
            }
        }

        public DelegateCommand DeleteMarkCommand {
            get { return new DelegateCommand(obj => {
                Mark mark = obj as Mark;
                if (mark == null) return;
                Marks.Remove(mark);
                db.Marks.Remove(mark);
                db.SaveChanges();
            }); }
        }

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

        private void onOpenNewMark() {
            EditVM.SetContext();
            SelectedMark = null;
            EditVM.IsEditOpen = true;
        }
        
        public ApplicationViewModel() {
            editVM = new MarkEditiorViewModel();
            db = new ApplicationContext();
            db.Marks.Load();
            Marks = new ObservableCollection<Mark>(db.Marks.Local.OrderByDescending(mark => mark.Id));
        }
    }
}
