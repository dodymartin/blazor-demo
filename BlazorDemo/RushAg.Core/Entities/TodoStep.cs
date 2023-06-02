using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Entities;

public partial class TodoStep
{
    public long Id { get; set; }
    public string Name { get; private set; }
    public bool IsComplete { get; private set; } = false;
    public DateTime? CompleteDate { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;

    public TodoStep(string name)
    {
        Name = name;
    }

    public void UpdateStepName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            throw new ArgumentNullException(nameof(newName));

        Name = newName;
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
