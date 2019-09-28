using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using FluentValidator.Validation;
using ModernStore.Domain.Enums;
using ModernStore.Shared.Entities;

namespace ModernStore.Domain.Entities
{
    public class Order : Entity
    {
        private readonly IList<OrderItem> _items;

        protected Order() { }

        public Order(Customer customer, decimal deliveryFee, decimal discount)
        {
            Customer = customer;
            OrderId = 0;
            Number = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            Status = EOrderStatus.Created;            
            DeliveryFee = deliveryFee;
            Discount = discount;

            _items = new List<OrderItem>();

            AddNotifications(new ValidationContract()
                .IsGreaterThan(DeliveryFee, 0, "DeliveryFee", "Delivery Fee canno cannot be 0")
                .IsGreaterThan(Discount, -1, "Discount", $"Discount cannot be negative")
            );
        }

        public int OrderId { get; private set; }        
        public string Number { get; private set; }
        public EOrderStatus Status { get; private set; }
        public ICollection<OrderItem> Items => _items.ToArray(); //a order contains N OrderItems
        public decimal DeliveryFee { get; private set; }
        public decimal Discount { get; private set; }
        public Customer Customer { get; private set; }

        public decimal SubTotal() => Items.Sum(x => x.Total());
        public decimal Total() => SubTotal() + DeliveryFee - Discount;        

        public void AddItem(OrderItem item)
        {
            AddNotifications(item.Notifications);
            if(item.Valid)
                _items.Add(item);
        }
    }
}
