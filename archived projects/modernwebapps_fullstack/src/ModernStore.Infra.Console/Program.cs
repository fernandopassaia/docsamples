using ModernStore.Infra.Contexts;
using System;
using System.Linq;

namespace ModernStore.Infra.Console
{
    class Program
    {        
        //this project is just to test EF core and to Run the Migrations Here...
        static void Main(string[] args)
        {
            //without DI, just for tests purpose
            ModernStoreDataContext _context = new ModernStoreDataContext();
            var data = _context.Order.ToList();
        }
    }
}
