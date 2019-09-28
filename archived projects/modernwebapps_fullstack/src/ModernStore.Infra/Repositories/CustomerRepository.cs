using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Infra.Contexts;
using ModernStore.Shared;
using static Dapper.SqlMapper;

namespace ModernStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ModernStoreDataContext _context;

        public CustomerRepository(ModernStoreDataContext context)
        {
            _context = context;
        }

        public Customer Get(int id)
        {
            return _context
                .Customer
                .Include(x => x.User)
                .FirstOrDefault(x => x.CustomerId == id);
            
        }

        public Customer GetByUsername(string username)
        {
            return _context
                .Customer
                .Include(x => x.User)
                .AsNoTracking()
                .FirstOrDefault(x => x.User.Username == username);            
        }

        public void Save(Customer customer)
        {
            _context.Customer.Add(customer);            
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;            
        }

        public bool DocumentExists(string document)
        {
            return _context.Customer.Any(x => x.Document.Number == document);            
        }

        public GetCustomerCommandResult Get(string username)
        {
            //return _context
            //    .Customers
            //    .Include(x => x.User)
            //    .AsNoTracking()
            //    .Select(x => new GetCustomerCommandResult
            //    {
            //        Name = x.Name.ToString(),
            //        Document = x.Document.Number,
            //        Active = x.User.Active,
            //        Email = x.Email.Address,
            //        Password = x.User.Password,
            //        Username = x.User.Username
            //    })
            //    .FirstOrDefault(x => x.Username == username);

            var query = "SELECT * FROM [GetCustomerInfoView] WHERE [Active]=1 AND [Username]=@username";
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                conn.Open();
                return conn
                    .Query<GetCustomerCommandResult>(query,
                    new { username = username })
                    .FirstOrDefault();
            }
        }
    }
}
