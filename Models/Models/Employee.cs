using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public enum CRUDAction {
        ADD, UPDATE
    }

    public class Employee
    {
        [Key]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [NotMapped]
        [ValidateNever]
        public CRUDAction Action { get; set; }
    }
}
