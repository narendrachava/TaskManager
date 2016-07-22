using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Domain.Entities
{
    public class UserTask
    {
        public int UserTaskId { get; set; }

        public string Description { get; set; }

        public bool IsComplete { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
