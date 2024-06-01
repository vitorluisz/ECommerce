using ECommerce.Data;

namespace ECommerce.Models
{
    public class ProductModel
    {
        readonly private ApplicationDbContext _db;

        public ProductModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public string IsNameValid(Product product)
        {
            bool isvalid = !string.IsNullOrWhiteSpace(product.Name);
            string error = isvalid ? string.Empty : "Nome muito curto.";
            return error;
        }

        public string IsDescriptionValid(Product product)
        {
            bool isvalid = !string.IsNullOrWhiteSpace(product.Description);
            string error = isvalid ? string.Empty : "Descrição muito curta.";
            return error;
        }

        public string IsPriceValid(Product product)
        {
            bool isvalid = product.Price is > 0;
            string error = isvalid ? string.Empty : "Adicione um preço válido.";
            return error;
        }

        public string IsCategoryValid(Product product)
        {
            bool isvalid = Enum.IsDefined(typeof(Category), product.Category);
            string error = isvalid ? string.Empty : "Categoria impossivel.";
            return error;
        }

        public string IsImageValid(Product product)
        {
            bool isvalid = !string.IsNullOrWhiteSpace(product.ImageUrl);
            string error = isvalid ? string.Empty : "Produto sem imagem.";
            return error;
        }
    }
}
