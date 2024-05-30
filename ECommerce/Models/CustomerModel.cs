using ECommerce.Data;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.Models
{
    public class CustomerModel
    {
        private readonly IConfiguration _configuration;
        readonly private ApplicationDbContext _db;

        public CustomerModel(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        
        public Customer DatabaseCustomer(Customer model)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            // consulta SQL
            string query = "SELECT TOP 1 * FROM Customer WHERE Email = @email AND Password = @password";

            // parâmetros da consulta
            SqlParameter emailParameter = new SqlParameter("@email", model.Email);
            SqlParameter passwordParameter = new SqlParameter("@password", model.Password);

            // comando SQL
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(emailParameter);
            command.Parameters.Add(passwordParameter);

            // abrir conexão e executar consulta
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // ler resultado e armazenar em variável customer
            Customer customer = null;
            if (reader.Read())
            {
                customer = new Customer
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    Email = (string)reader["Email"],
                    LastName = (string)reader["LastName"],
                    IsAdmin = (bool)reader["IsAdmin"]
                };
            }

            // fechar conexão
            connection.Close();

            return customer;
        }
    }
}
