using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Domain.BusinessModels
{
    public class BaseResult
    {
        public List<string> Errors { get; set; }

        public bool IsSucceeded { get; set; }
    }
}
