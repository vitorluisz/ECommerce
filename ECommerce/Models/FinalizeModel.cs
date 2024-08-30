using ECommerce.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;
using System;
using System.Net;
using System;
using System.IO;
using RestSharp;
using RestSharp.Authenticators;
using Microsoft.Extensions.Options;
using System.Text;

namespace ECommerce.Models
{
    public class FinalizeModel
    {
        private readonly ClaimsPrincipal _user;
        readonly private ApplicationDbContext _db;
        private readonly EmailSettings _emailSettings;

        public FinalizeModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, IOptions<EmailSettings> emailSettings)
        {
            _db = db;
            _user = httpContextAccessor.HttpContext.User;
            _emailSettings = emailSettings.Value;
        }

        public void SendEmail(List<Product> products, string email)
        {
            try
            {
                decimal valorTotal = 0;
                var productDetails = new StringBuilder();
                productDetails.AppendLine("Seus produtos:");
                foreach (var product in products)
                {
                    productDetails.AppendLine($"- {product.Name}: Quantidade: {product.Quantity} (Preço: {product.Price * product.Quantity})");
                    valorTotal = valorTotal + product.Price * product.Quantity;
                }
                productDetails.AppendLine($"Valor total: {valorTotal}");

                var options = new RestClientOptions("https://api.mailgun.net/v3")
                {
                    Authenticator = new HttpBasicAuthenticator("api", _emailSettings.ApiKey)
                };

                var client = new RestClient(options);

                var request = new RestRequest("{domain}/messages", Method.Post);
                request.AddParameter("domain", _emailSettings.Domain, ParameterType.UrlSegment);
                request.AddParameter("from", $"Excited User <{_emailSettings.SenderEmail}>");
                request.AddParameter("to", email);
                request.AddParameter("subject", "Envio de produtos");
                request.AddParameter("text", productDetails.ToString());

                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    Console.WriteLine($"Erro ao enviar email: StatusCode: {response.StatusCode}, ErrorMessage: {response.ErrorMessage}, Content: {response.Content}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar email: {ex.Message}");
            }

        }
    }
}
