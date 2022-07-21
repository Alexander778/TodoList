using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListAppData.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public ICollection<TodoTask> TodoTasks { get; set; }
    }
}
