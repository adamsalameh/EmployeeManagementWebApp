using Microsoft.AspNetCore.Mvc;
using EmployeeManagementWebApp.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace EmployeeManagementWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly string connectionString;

        public EmployeeController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("EmployeeDb");
        }

        public IActionResult Index(string? department = null)
        {
            ViewBag.Department = department;

            var employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees";
                if (!string.IsNullOrEmpty(department))
                {
                    query += " WHERE Department = @Department";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(department))
                {
                    cmd.Parameters.AddWithValue("@Department", department);
                }

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var employee = new Employee
                    {
                        EmployeeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Department = reader.GetString(3),
                        Position = reader.GetString(4),
                        Address = reader.GetString(5),
                        Email = reader.GetString(6),
                        PhoneNumber = reader.GetString(7)
                    };
                    employees.Add(employee);
                }
            }
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create(string? department = null)
        {
            ViewBag.Department = department;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee, string? department = null)
        {
            ViewBag.Department = department;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var query = @"INSERT INTO Employees (FirstName, LastName, Department, Position, Address, Email, PhoneNumber) 
                              VALUES (@FirstName, @LastName, @Department, @Position, @Address, @Email, @PhoneNumber)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            // Set success message in TempData
            TempData["SuccessMessage"] = "New employee has been created successfully!";
            return RedirectToAction("Index", new { department });
        }

        [HttpGet]
        public IActionResult Edit(int id, string? department = null)
        {
            ViewBag.Department = department;
            Employee employee = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    employee = new Employee
                    {
                        EmployeeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Department = reader.GetString(3),
                        Position = reader.GetString(4),
                        Address = reader.GetString(5),
                        Email = reader.GetString(6),
                        PhoneNumber = reader.GetString(7)
                    };
                }
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee, string? department = null)
        {
            ViewBag.Department = department;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var query = @"UPDATE Employees SET 
                              FirstName = @FirstName, 
                              LastName = @LastName, 
                              Department = @Department, 
                              Position = @Position, 
                              Address = @Address, 
                              Email = @Email, 
                              PhoneNumber = @PhoneNumber 
                              WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            // Set success message in TempData
            TempData["SuccessMessage"] = "Employee details have been updated successfully!";
            return RedirectToAction("Index", new { department });
        }

        [HttpPost]
        public IActionResult Delete(int id, string? department = null)
        {
            ViewBag.Department = department;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            // Set success message in TempData
            TempData["SuccessMessage"] = "Employee has been deleted successfully!";
            return RedirectToAction("Index", new { department });
        }

        public IActionResult Details(int id, string department = null)
        {
            ViewBag.Department = department;
            Employee employee = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    employee = new Employee
                    {
                        EmployeeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Department = reader.GetString(3),
                        Position = reader.GetString(4),
                        Address = reader.GetString(5),
                        Email = reader.GetString(6),
                        PhoneNumber = reader.GetString(7)
                    };
                }
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Search(string? department = null)
        {
            ViewBag.Department = department;
            return View();
        }

        [HttpPost]
        public IActionResult Search(int EmployeeID, string? department = null)
        {
            Employee? employee = null;
            ViewBag.Department = department;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    employee = new Employee
                    {
                        EmployeeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Department = reader.GetString(3),
                        Position = reader.GetString(4),
                        Address = reader.GetString(5),
                        Email = reader.GetString(6),
                        PhoneNumber = reader.GetString(7)
                    };
                }
            }

            return View("Search", employee);
        }
    }
}
