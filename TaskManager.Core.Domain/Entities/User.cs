using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace TaskManager.Core.Domain.Entities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<UserTask> Tasks { get; set; }
    }
}
