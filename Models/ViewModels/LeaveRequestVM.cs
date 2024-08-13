using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.ViewModels
{
    public class LeaveRequestVM
    {
        public LeaveRequest LeaveRequest { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Types { get; set; }
    }
}
