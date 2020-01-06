using BimManufact.WebApi.Models;
using BimManufact.WebApi.Resolver;
using Moq;
using NUnit.Framework;
using System;
using System.Data.Entity;

namespace BimManufact.WebApi.Unit.Tests.Controllers
{
    public abstract class ControllerUnitTestBase
    {
        protected DbSet<Manufacturer> ManufacturerDbSet { get; } = new TestDbSet<Manufacturer>();

        protected DbSet<Product> ProductDbSet { get; } = new TestDbSet<Product>();

        protected Mock<IBimManufactWebApiContext> WebApiContextMock { get; } = new Mock<IBimManufactWebApiContext>();

        protected Mock<IDummyUserResolver> UserResolverMock { get; } = new Mock<IDummyUserResolver>();

        [SetUp]
        public virtual void SetUp()
        {
            WebApiContextMock
                .SetupGet(_ => _.Manufacturers)
                .Returns(ManufacturerDbSet);

            WebApiContextMock
                .SetupGet(_ => _.Products)
                .Returns(ProductDbSet);

            UserResolverMock
                .SetupGet(_ => _.CurrentUserId)
                .Returns(Guid.NewGuid());
        }
    }
}
