using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementWebApp.Models
{
    public class Admin
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
