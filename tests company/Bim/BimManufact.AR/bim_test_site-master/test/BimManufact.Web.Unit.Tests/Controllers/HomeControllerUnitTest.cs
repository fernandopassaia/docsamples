using BimManufact.Web.Clients;
using BimManufact.Web.Controllers;
using BimManufact.Web.Models;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BimManufact.Web.Unit.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerUnitTest
    {
        [Test]
        public async Task Index_ShouldCallClient_ToGetManufacturers()
        {
            // ARRANGE
            var clientMoq = GetMoqClient(Enumerable.Empty<ManufacturerViewModel>());
            var controller = new HomeController(clientMoq.Object);

            // ACT
            var result = await controller.Index() as System.Web.Mvc.ViewResult;

            // ASSERT
            clientMoq.Verify(_ => _.GetManufacturers(), Times.Once());
        }

        [Test]
        public async Task Index_ShouldCallClient_AndGetManufacturersList()
        {
            // ARRANGE
            var clientMoq = GetMoqClientWithFakeManufacturers(out IEnumerable<ManufacturerViewModel> manufacturers);
            var controller = new HomeController(clientMoq.Object);

            // ACT
            var result = await controller.Index() as System.Web.Mvc.ViewResult;

            // ASSERT
            result.Model.Should().BeAssignableTo<IEnumerable<ManufacturerViewModel>>();
            result.Model.As<IEnumerable<ManufacturerViewModel>>().Should().BeEquivalentTo(manufacturers);
        }

        [Test]
        public async Task Index_ShouldReturnError_IfClientResponseIsUnsuccessfully()
        {
            // ARRANGE
            var clientMoq = GetMoqClient(Enumerable.Empty<ManufacturerViewModel>(), HttpStatusCode.BadRequest);
            var controller = new HomeController(clientMoq.Object);

            // ACT
            var result = await controller.Index() as System.Web.Mvc.ViewResult;

            // ASSERT
            result.Should().NotBeNull();
            controller.ModelState.Any(_ => _.Value.Errors.Count > 0).Should().BeTrue();
        }

        [Test]
        public void Create_ShouldReturnView_WithEmptyModel()
        {
            // ARRANGE
            var clientMoq = GetMoqClient(Enumerable.Empty<ManufacturerViewModel>());
            var controller = new HomeController(clientMoq.Object);
            var expectedResult = new ManufacturerRequestViewModel();

            // ACT
            var result = controller.Create() as System.Web.Mvc.ViewResult;

            // ASSERT
            result.Model.Should().BeEquivalentTo(expectedResult);
        }

        private Mock<IManufacturerClient> GetMoqClient(IEnumerable<ManufacturerViewModel> manufacturers, HttpStatusCode response = HttpStatusCode.OK)
        {
            var jsonContent = JsonConvert.SerializeObject(manufacturers);

            var content = new StreamContent(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(jsonContent)));
            content.Headers.Add("Content-Length", new[] { jsonContent.Length.ToString() });
            content.Headers.Add("Content-Type", new[] { "application/json; charset=utf-8" });

            var clientMoq = new Mock<IManufacturerClient>();
            clientMoq
                .Setup(_ => _.GetManufacturers())
                .Returns(Task.FromResult(new HttpResponseMessage(response)
                {
                    Content = content,
                }));

            return clientMoq;
        }

        private Mock<IManufacturerClient> GetMoqClientWithFakeManufacturers(out IEnumerable<ManufacturerViewModel> manufacturers)
        {
            manufacturers = new List<ManufacturerViewModel>
            {
                new ManufacturerViewModel { ManufacturerId = 1, Name = "First" },
                new ManufacturerViewModel { ManufacturerId = 2, Name = "Second" },
                new ManufacturerViewModel { ManufacturerId = 3, Name = "Third" },
                new ManufacturerViewModel { ManufacturerId = 4, Name = "Fourth" }
            };

            return GetMoqClient(manufacturers);
        }
    }
}
