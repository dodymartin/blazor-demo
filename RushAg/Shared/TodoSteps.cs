using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushAg.Shared
{
    public partial class TodoSteps
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [StringLength(150)]
        [Required]
        public string StepName { get; set; }

        public bool IsComplete { get; set; } = false;
        public DateTime? CompleteDate { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public long TodoId { get; set; }
        public TodoItem TodoItem { get; set; }
    }
}
