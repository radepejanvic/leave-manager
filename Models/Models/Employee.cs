using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Employee
    {
        [Key]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
