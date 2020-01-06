using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BimManufact.WebApi.Models
{
    public class ManufacturerResponse : ManufacturerBase
    {
        public int ProductsCount { get; set; }
    }
}