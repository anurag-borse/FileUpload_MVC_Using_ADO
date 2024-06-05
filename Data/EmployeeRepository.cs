using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using FileUpload_MVC_ADO.Models;



namespace FileUpload_MVC_ADO.Data
{
    public class EmployeeRepository
    {
        private SqlConnection _connection;


        public EmployeeRepository()
        {
            string connectionString = "Data Source=.;Initial Catalog=FileUpload_MVC_ADO;Integrated Security=True; TrustServerCertificate = True";


            _connection = new SqlConnection(connectionString);
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            SqlCommand command = new SqlCommand("getAllEmployee", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            foreach (DataRow emp in dataTable.Rows)
            {
                employees.Add(
                    new Employee
                    {
                        ID = Convert.ToInt32(emp["ID"]),
                        Name = emp["Name"].ToString(),
                        
                        Email = emp["Email"].ToString(),
                        
                        Department = emp["Department"].ToString(),
                        ImagePath = emp["ImagePath"].ToString()
                    }

                    );
            }

            return employees;

        }

        public bool AddEmployee(Employee employee)
        {
            SqlCommand command = new SqlCommand("addEmployee", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Name", employee.Name);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@Department", employee.Department);
            command.Parameters.AddWithValue("@ImagePath", employee.ImagePath);

            _connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            _connection.Close();


            if (rowAffected >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }


        }


        public Employee GetEmployeesByID(int id)
        {
            Employee employees = new Employee();

            SqlCommand command = new SqlCommand("getEmployeeById", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter parameter;
            command.Parameters.Add(new SqlParameter("@ID", id));



            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            foreach (DataRow emp in dataTable.Rows)
            {
                employees = new Employee
                {
                    ID = Convert.ToInt32(emp["ID"]),
                    Name = emp["Name"].ToString(),
                    ImagePath = emp["ImagePath"].ToString(),
                    Email = emp["Email"].ToString(),
                    Department = emp["Department"].ToString()
                };

            }

            return employees;

        }

        public bool UpdateEmployee(int Id, Employee employee)
        {
            SqlCommand command = new SqlCommand("updateEmployee", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;



            command.Parameters.AddWithValue("@ID", Id);
            command.Parameters.AddWithValue("@Name", employee.Name);
            command.Parameters.AddWithValue("@Imagepath", employee.ImagePath);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@Department", employee.Department);

            _connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            _connection.Close();

            if (rowAffected >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }


        }





        public bool DeleteEmployee(int Id)
        {
            SqlCommand command = new SqlCommand("deleteEmployee", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ID", Id);

            _connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            _connection.Close();

            if (rowAffected >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }

}