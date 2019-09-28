using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleOrder
{
    class DoAOrder
    {
        static List<Customer> Customers = new List<Customer>();
        static List<Product> Products = new List<Product>();
        static void Main(string[] args)
        {
            Customers.Add(new Customer() { FirstName = "Fernando", LastName = "Passaia", Addresses = new List<string>() { "Rua Cliente 1", "Rua Cliente 2" }, telephone = "112233", CreatedIn = DateTime.Now, Id = 1 });
            Customers.Add(new Customer() { FirstName = "Jaqueline", LastName = "Guimaraes", Addresses = new List<string>() { "Rua Cliente 2", "Rua Cliente 4", "Rua Cliente 5" }, telephone = "112233", CreatedIn = DateTime.Now, Id = 2 });
            Customers.Add(new Customer() { FirstName = "Hubinha", LastName = "Bundocorica", Addresses = new List<string>() { "Eu moro com meus pais", "So quero saber de Ossinho" }, telephone = "777888", CreatedIn = DateTime.Now, Id = 3 });

            Products.Add(new Product() { Description = "Product 1", Price = 10, Quantity = 10, CreatedIn = DateTime.Now, Id = 1 });
            Products.Add(new Product() { Description = "Product 2", Price = 15, Quantity = 10, CreatedIn = DateTime.Now, Id = 2 });
            Products.Add(new Product() { Description = "Product 3", Price = 20, Quantity = 10, CreatedIn = DateTime.Now, Id = 3 });
            Products.Add(new Product() { Description = "Product 4", Price = 25, Quantity = 10, CreatedIn = DateTime.Now, Id = 4 });
            Products.Add(new Product() { Description = "Product 5", Price = 30, Quantity = 10, CreatedIn = DateTime.Now, Id = 5 });

            //I know that i could do it in a TEST class, making Asserts, but, i want to see it in Console... so, anyway
            var Order = new Order(Customers[1], "Anamao");
            Order.AddItem(Products[1], 5);
            Order.AddItem(Products[3], 5);
            Order.PlaceOrder();
            Order.PrintOrder();

            var Order2 = new Order(Customers[0], "Anamao");
            Order2.AddItem(Products[1], 5);
            Order2.AddItem(Products[3], 6); //should not ADD because items > stock (10)
            Order2.PlaceOrder();
            Order2.PrintOrder();
        }
    }

    #region Customer
    public class Customer : Log
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Addresses { get; set; }
        public string telephone { get; set; }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }

        public override string ToString()
        {
            //if i call Customer.ToString() it will return the Full Name
            return FullName().ToString();
        }
    }
    #endregion

    #region Product
    public class Product : Log
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public bool decreaseStock(int idProduct, int quantitySell)
        {
            if(Quantity >= quantitySell)
            {
                Quantity = Quantity - quantitySell;
                return true;
            }
            Console.WriteLine(" *** Impossible to Decrease Stock, Actual Quantity = " + Quantity + ". Quantity Selled = " + quantitySell);
            return false;
        }
    }
    #endregion

    #region Order
    public class Order : Log
    {
        public Order(Customer customer, string moreInfo)
        {
            Customer = customer;
            MoreInfo = moreInfo;
            CreatedIn = DateTime.Now;
            Id = 1;
            OrderItems = new List<OrderItem>();
        }

        public Customer Customer { get; private set; }
        public string MoreInfo { get; private set; }
        public List<OrderItem> OrderItems { get; set; }
                
        public void AddItem(Product product, int quantity)
        {            
            if (product.decreaseStock(product.Id, quantity))
            {
                OrderItems.Add(new OrderItem() { Order = this, Price = product.Price, Product = product, quantity = quantity } );
            }
        }

        public decimal ReturnOrderValue()
        {                        
            return OrderItems.Sum(p => p.Price * p.quantity);            
        }

        public void PlaceOrder()
        {
            Status = 3;            
        }

        public void PrintOrder()
        {
            Console.WriteLine(" *** ");
            Console.WriteLine("Printing Order Right Now:");
            Console.WriteLine("Customer: " + this.Customer.ToString());
            Console.WriteLine("Customer Adresses: ");
            this.Customer.Addresses.ForEach(item =>
            {
                Console.WriteLine("     " + item);
            });            
            Console.WriteLine("Order Status: " + this.Status + " (Status 1 (Removed) Status 2 (Active) Status 3 (Selled - just in Order))");
            Console.WriteLine("Order Items: ");
            this.OrderItems.ForEach(item =>
            {
                Console.WriteLine("     " + item.Product.Description + " " + item.quantity + "x" + item.Price + "=" + (item.quantity * item.Price));
            });
            Console.WriteLine("Order Price: " + this.ReturnOrderValue());
            Console.WriteLine(" *** ");
            Console.WriteLine(" ");
            Console.ReadKey();
        }
    }
    #endregion

    #region Order Item
    public class OrderItem
    {
        public Order Order {get;set;}
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int quantity { get; set; }
    }
    #endregion

    #region Log
    public class Log
    {
        public int Id { get; set; } //Status 1 (Removed) Status 2 (Active) Status 3 (Selled - just in Order)
        public DateTime CreatedIn { get; set; }
        public int Status { get; set; }
    }
    #endregion
}
