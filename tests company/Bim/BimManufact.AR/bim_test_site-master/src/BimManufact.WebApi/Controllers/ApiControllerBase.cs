using System.Web.Http;
using BimManufact.WebApi.Models;
using BimManufact.WebApi.Resolver;

namespace BimManufact.WebApi.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected IBimManufactWebApiContext WebApiContext { get; private set; }

        protected IDummyUserResolver UserResolver { get; private set; }

        public ApiControllerBase(IBimManufactWebApiContext bimManufactWebApiContext, IDummyUserResolver userResolver)
        {
            WebApiContext = bimManufactWebApiContext;
            UserResolver = userResolver;
        }
    }
}