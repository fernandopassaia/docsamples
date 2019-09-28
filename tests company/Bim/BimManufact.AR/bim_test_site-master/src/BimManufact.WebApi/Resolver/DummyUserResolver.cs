using System;

namespace BimManufact.WebApi.Resolver
{
    public interface IDummyUserResolver
    {
        Guid CurrentUserId { get; }
    }

    public class DummyUserResolver : IDummyUserResolver
    {
        public Guid CurrentUserId { get; } = new Guid("{B62AB425-8D64-429E-96A6-72FE79580720}");
    }
}