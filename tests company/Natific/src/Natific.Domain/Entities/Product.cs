using FluentValidator.Validation;
using System;
using System.Collections.Generic;

namespace Natific.Domain.Entities
{
    public class Product : Entity
    {
        public Product() { } //Because EF
        public Product(string name, decimal price, string description, decimal weight, int quantityOnCreation)
        {
            Name = name;
            Price = price;
            Description = description;
            Weight = weight;
            QuantityOnHand = quantityOnCreation;
            Activate();
            CreatedIn = DateTime.Now;
            UpdatedIn = DateTime.Now;
            Validate();
        }

        public int ProductId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public decimal Weight { get; private set; }
        public int QuantityOnHand { get; private set; }
        public bool Active { get; private set; }
        public List<StockPile> StockPiles { get; private set; } //A Product contains N StockPiles (Moviments)

        public void Validate()
        {
            AddNotifications(new ValidationContract()
                .IsNotNullOrEmpty(Name, "Name", "Name cannot be null")
                .HasMaxLen(Name, 60, "Name", "Name caracters max 60. Actual: " + Name.Length)
                .HasMaxLen(Description, 120, "Description", "Description caracters max 120. Actual: " + Description.Length)
                .IsGreaterThan(Price, 0, "Price", "Price needs to be greater than 0.")
                .IsGreaterThan(Weight, 0, "Weight", "Weight needs to be greater than 0.")
            );
        }

        public void DecreaseQuantity(int quantity)
        {
            if (QuantityOnHand >= quantity)
            {
                QuantityOnHand -= quantity;                
            }
            else
                AddNotification("QuantityOnHand", "Cannot Decrease Stock because Quantity is Greater than Actual-Stock: " + QuantityOnHand);
        }

        public void IncreaseQuantity(int quantity)
        {
            QuantityOnHand += quantity;
        }

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;

        public void Update(string name, decimal price, string description, decimal weight)
        {
            Name = name;
            Price = price;
            Description = description;
            Weight = weight;
            UpdatedIn = DateTime.Now;
            Validate();
        }
    }
}