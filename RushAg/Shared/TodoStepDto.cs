using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushAg.Shared
{
    public class TodoStepDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateTodoStepDto
    {
        public long ParentId { get; set; }
        public string Name { get; set; }
    }
}
