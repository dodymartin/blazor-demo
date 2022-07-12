using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushAg.Shared
{
    public class TodoItemDto
    {
        public long TodoItemId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool IsComplete { get; set; } = false;
        
        public List<TodoStepDto> Steps { get; set; }
    }
}
