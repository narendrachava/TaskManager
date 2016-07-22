using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.API.Models
{
    public class UserTaskApiModel
    {
        public int UserTaskId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Description { get; set; }

        [Display(Name = "Is Task Complete")]
        public bool IsComplete { get; set; }
    }
}