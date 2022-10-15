using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushAg.Core.Entities;

public partial class TodoStep
{
    public long TodoStepId { get; set; }
    public string StepName { get; private set; }

    public bool IsComplete { get; private set; } = false;
    public DateTime? CompleteDate { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;

    public TodoStep(string stepName)
    {
        StepName = stepName;
    }

    public void UpdateStepName(string newStepName)
    {
        if (string.IsNullOrEmpty(newStepName))
            throw new ArgumentNullException(nameof(newStepName));

        StepName = newStepName;
    }

    public void ToggleCompleted()
    {
        if (!IsComplete)
        {
            IsComplete = true;
            CompleteDate = DateTime.Now;
        }
        else
        {
            IsComplete = false;
            CompleteDate = null;
        }
    }
}
