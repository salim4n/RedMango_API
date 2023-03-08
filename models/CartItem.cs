using System.ComponentModel.DataAnnotations.Schema;

namespace RedMango_API.models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; }
        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
