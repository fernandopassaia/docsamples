using FluentValidator.Validation;
using Natific.Domain.Enums;
using System;

namespace Natific.Domain.Entities
{
    public class StockPile : Entity
    {
        public StockPile() { } //Because EF
        public StockPile(string description, int entryWithDraw, int quantity, Product product)
        {
            EntryWithDraw = entryWithDraw;
            Description = description;
            Quantity = quantity;
            CreatedIn = DateTime.Now;
            UpdatedIn = DateTime.Now;
            Product = product;
            ProductId = product.ProductId;
            Validate();
        }

        public void Validate()
        {
            AddNotifications(new ValidationContract()
                .HasMaxLen(Description, 80, "Description", "Description caracters max 80. Actual: " + Description.Length)
                .IsNotNullOrEmpty(Description, "Description", "Description cannot be null")
                .IsGreaterThan(Quantity, 0, "Quantity", "Quantity cannot be less than 1")
            );
            if (EntryWithDraw != 1 && EntryWithDraw != 2)
                AddNotification("EntryWithDraw", "Please inform (1) Entry or (2) WithDraw");
        }

        public int StockPileId { get; private set; }        
        public string Description { get; private set; }
        public int EntryWithDraw { get; private set; } //1 Entry 2 WithDraw
        public int Quantity { get; private set; }
        public EStockPileStatus Status { get; private set; }

        public int ProductId { get; private set; }
        public Product Product { get; set; } //a stock-moviment belongs to a product
    }
}
