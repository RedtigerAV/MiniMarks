using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniMarks.MinimalMVVM;

namespace MiniMarks {
    public class MarkModel: Observer {

        private string title;
        private string content;
        public string Title {
            get { return title; }
            set { title = value; OnPropertyChanged(nameof(Title)); }
        }
        public string MarkContent {
            get { return content; }
            set { content = value; OnPropertyChanged(nameof(MarkContent)); }
        }

        public MarkModel(string title = "", string markContent = "") {
            Title = title;
            MarkContent = markContent;
        }
    }
}
