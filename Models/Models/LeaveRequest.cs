using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeEmail { get; set; }
        [ForeignKey("EmployeeEmail")]
        [ValidateNever]
        public Employee Employee { get; set; }
        [Required]
        [DateRange("End")]
        public DateOnly Start { get; set; }
        [Required]
        public DateOnly End { get; set; }
        public int Duration { get; set; }
    }
}
