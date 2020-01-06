using Bim.Repository.DataContext;
using Bim.Util.Resolver;
using System.Web.Http;

namespace Bim.WebApi.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected IBimContext DbContext { get; private set; }

        protected IFakeUserResolver UserResolver { get; private set; }

        public ApiControllerBase(IBimContext bimContext, IFakeUserResolver userResolver)
        {
            DbContext = bimContext;
            UserResolver = userResolver;
        }
    }
}