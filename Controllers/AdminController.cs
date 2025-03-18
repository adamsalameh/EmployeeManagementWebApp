using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EmployeeManagementWebApp.Models;
using System.Data.SqlClient;

namespace EmployeeManagementWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly string connectionString;

        public AdminController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("EmployeeDb");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin model)
        {
            bool isAuthenticated = false;
            string department = string.Empty;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var query = "SELECT Department FROM Admins WHERE Username = @Username AND PasswordHash = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", model.Username);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isAuthenticated = true;
                    department = reader["Department"].ToString();
                }
            }

            if (isAuthenticated)
            {
                ViewBag.Department = department;
                return RedirectToAction("Index", "Employee", new { department });
            }
            else
            {
                ViewBag.Message = "Invalid credentials";
                return View();
            }
        }
    }
}
