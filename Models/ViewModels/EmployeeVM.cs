using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class EmployeeVM
    {
        public Employee Employee { get; set; }

        [ValidateNever]
        public int TotalVacationDays { get; set; }
        [ValidateNever]
        public int TotalRemoteDays { get; set; }
        [ValidateNever]
        public int TotalSickDays { get; set; }
    }
}
