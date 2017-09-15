using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MiniMarks {
    /// <summary>
    /// Класс - контекст данных из базы данных
    /// </summary>
    public class ApplicationContext: DbContext {
        public ApplicationContext() : base("DefaultConnection") { }
        /// <summary>
        /// Коллекция объектов из базы данных
        /// </summary>
        public DbSet<Mark> Marks { get; set; }
    }
}
