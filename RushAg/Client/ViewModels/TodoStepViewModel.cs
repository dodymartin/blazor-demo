using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushAg.Client.ViewModels
{
    public class TodoStepViewModel
    {
        public long TodoStepId { get; set; }
        public string StepName { get; set; } = default!;

        public bool IsComplete { get; set; } = false;
        public DateTime? CompleteDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
