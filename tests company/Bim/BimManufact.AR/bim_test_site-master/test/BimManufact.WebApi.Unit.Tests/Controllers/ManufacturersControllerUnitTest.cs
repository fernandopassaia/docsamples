using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using BimManufact.WebApi.Controllers;
using BimManufact.WebApi.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BimManufact.WebApi.Unit.Tests.Controllers
{
    [TestFixture]
    public class ManufacturersControllerUnitTest : ControllerUnitTestBase
    {
        private ManufacturersController Controller { get; set; }

        public override void SetUp()
        {
            base.SetUp();

            Controller = new ManufacturersController(WebApiContextMock.Object, UserResolverMock.Object);
        }

        [Test]
        public async Task PostManufacturer_ShouldReturn_SameManufacturer()
        {
            // ARRANGE
            var newManufacturer = GetManufacturerRequestExample();

            // ACT
            var result = await Controller.PostManufacturer(newManufacturer) as CreatedAtRouteNegotiatedContentResult<ManufacturerResponse>;

            // ASSERT
            result.Content.Should().BeEquivalentTo(newManufacturer);
        }

        private ManufacturerRequest GetManufacturerRequestExample()
        {
            return new ManufacturerRequest
            {
                Name = "First Manufacturer"
            };
        }
    }
}
