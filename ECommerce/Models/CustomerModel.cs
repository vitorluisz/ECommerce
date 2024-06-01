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
        
        public Customer DatabaseCustomer(Customer model, string query)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);

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

        public string IsPasswordValid(Customer customers)
        {
            bool isvalid = false;
            if (customers.Password != null)
            {
                isvalid = customers.Password.Length >= 6 && customers.Password.Any(c => char.IsNumber(c)) && customers.Password.Any(c => char.IsUpper(c)) && customers.Password.Any(c => char.IsLower(c));
            }
            string error = isvalid ? string.Empty : "Sua senha precisa conter no mínimo 6 caracteres, 1 letra maiúscula, 1 letra minúscula e 1 número";
            return error;
        }

        public string IsNameValid(Customer customers)
        {
            bool isvalid = !string.IsNullOrWhiteSpace(customers.FirstName) && !string.IsNullOrWhiteSpace(customers.LastName);
            string error = isvalid ? string.Empty : "Nome muito curto.";
            return error;
        }

        public string IsEmailValid(Customer customers)
        {
            bool isvalid = false;
            if (customers.Email != null)
            {
                isvalid = customers.Email.Length >= 6 && customers.Email.Contains("@");
            }
            string error = isvalid ? string.Empty : "Email não reconhecido.";
            return error;
        }
    }
}
