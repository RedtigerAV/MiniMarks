using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniMarks.MinimalMVVM;
using System.Runtime.CompilerServices;

namespace MiniMarks {
    public class Mark: Observer {

        private string title;
        private string content;

        public int Id { get; set; }
        public string Title {
            get { return title; }
            set { title = value; OnPropertyChanged(nameof(Title)); }
        }
        public string MarkContent {
            get { return content; }
            set { content = value; OnPropertyChanged(nameof(MarkContent)); }
        }

        public Mark(string title = "", string markContent = "") {
            Title = title;
            MarkContent = markContent;
        }

        public Mark() { }
    }
}
