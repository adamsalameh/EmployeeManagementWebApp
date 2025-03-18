namespace EmployeeManagementWebApp.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public string Department { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
