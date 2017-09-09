using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MiniMarks {
    public class ApplicationContext: DbContext {
        public ApplicationContext() : base("DefaultConnection") { }

        public DbSet<Mark> Marks { get; set; }
    }
}
