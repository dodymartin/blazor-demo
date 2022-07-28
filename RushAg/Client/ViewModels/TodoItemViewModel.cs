using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushAg.Client.ViewModels
{
    public class TodoItemViewModel
    {
        public long TodoItemId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool IsComplete { get; set; } = false;
        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }

        public List<TodoStepViewModel> Steps { get; set; }
    }
}
