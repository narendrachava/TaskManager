using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Domain.BusinessModels.Requests
{
    public class AllTasksRequest : RequestBase, IAuthenticatedRequest
    {
        public string UserId
        {
            get; set;
        }
    }
}
