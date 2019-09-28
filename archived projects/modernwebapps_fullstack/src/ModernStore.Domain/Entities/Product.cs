using FluentValidator.Validation;
using ModernStore.Shared.Entities;
using System.Collections.Generic;

namespace ModernStore.Domain.Entities
{
    public class Product : Entity
    {
        protected Product() { }

        public Product(string title, decimal price, string image, int quantityOnHand)
        {
            ProductId = 0;
            Title = title;
            Price = price;
            Image = image;
            QuantityOnHand = quantityOnHand;

            AddNotifications(new ValidationContract()
                .IsNotNull(Title, "Title", "Should inform Title")
                .HasMinLen(Title, 3, "Title", "Title caracters min 3")
                .HasMaxLen(Title, 160, "Title", "Title caracters max 160")
            );
        }

        public int ProductId { get; private set; }
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public string Image { get; private set; }
        public int QuantityOnHand { get; private set; }
        public List<OrderItem> OrderItemsFromDB { get; private set; } //because EF Relationship...  

        public void DecreaseQuantity(int quantity) => QuantityOnHand -= quantity;
    }
}
