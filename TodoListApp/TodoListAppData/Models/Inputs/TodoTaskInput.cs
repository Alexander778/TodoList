using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListAppData.Models.Inputs
{
    public class TodoTaskInput
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Text { get; set; }
        public int StatusId { get; set; }
    }
}
