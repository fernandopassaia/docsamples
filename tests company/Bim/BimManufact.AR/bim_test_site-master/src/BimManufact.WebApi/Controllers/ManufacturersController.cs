using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BimManufact.WebApi.Models;
using BimManufact.WebApi.Resolver;

namespace BimManufact.WebApi.Controllers
{
    public class ManufacturersController : ApiControllerBase
    {
        public ManufacturersController(IBimManufactWebApiContext bimManufactWebApiContext, IDummyUserResolver userResolver)
            : base(bimManufactWebApiContext, userResolver)
        {
            
        }

        [Route("api/manufacturers")]
        public IQueryable<ManufacturerResponse> GetManufacturers()
        {
            return WebApiContext.Manufacturers.Select(_ => new ManufacturerResponse
            {
                ManufacturerId = _.ManufacturerId,
                Name = _.Name,
                ProductsCount = WebApiContext.Products.Count(p => p.ManufacturerId == _.ManufacturerId)
            });
        }

        [Route("api/manufacturers/{manufacturerId}", Name = nameof(GetManufacturer))]
        [ResponseType(typeof(ManufacturerResponse))]
        public async Task<IHttpActionResult> GetManufacturer(int manufacturerId)
        {
            var manufacturer = await WebApiContext.Manufacturers.FindAsync(manufacturerId);

            if (manufacturer == null)
            {
                return NotFound();
            }

            var result = new ManufacturerResponse
            {
                ManufacturerId = manufacturerId,
                Name = manufacturer.Name,
                ProductsCount = await WebApiContext.Products.CountAsync(_ => _.ManufacturerId == manufacturerId)
            };

            return Ok(result);
        }

        [Route("api/manufacturers/{manufacturerId}/logo")]
        public async Task<IHttpActionResult> GetManufacturerLogo(int manufacturerId)
        {
            var manufacturerLogo = await WebApiContext.ManufacturerLogos.FirstOrDefaultAsync(_ => _.ManufacturerLogoId == manufacturerId);

            if (manufacturerLogo?.Content != null)
            {
                using (var stream = new System.IO.MemoryStream(manufacturerLogo.Content))
                {
                    var result = new System.Net.Http.HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new System.Net.Http.ByteArrayContent(stream.ToArray());
                    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

                    return ResponseMessage(result);
                }
            }
            else
            {
                using (var stream = new System.IO.MemoryStream())
                {
                    Properties.Resources.no_image_info.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                    var result = new System.Net.Http.HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new System.Net.Http.ByteArrayContent(stream.ToArray());
                    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

                    return ResponseMessage(result);
                }
            }
        }

        [Route("api/manufacturers/{manufacturerId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutManufacturer(int manufacturerId, ManufacturerRequest manufacturerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (manufacturerId != manufacturerRequest.ManufacturerId)
            {
                return BadRequest();
            }

            var manufacturer = await WebApiContext.Manufacturers.FirstOrDefaultAsync(_ => _.ManufacturerId == manufacturerId);

            if (manufacturer != null)
            {
                manufacturer.AuditLastModifiedBy = UserResolver.CurrentUserId;
                manufacturer.AuditLastModifiedDate = DateTime.Now;
                manufacturer.Name = manufacturerRequest.Name;
            }
            else
            {
                return NotFound();
            }

            WebApiContext.Entry(manufacturer).State = EntityState.Modified;

            try
            {
                await WebApiContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(manufacturerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/manufacturers")]
        [ResponseType(typeof(ManufacturerResponse))]
        public async Task<IHttpActionResult> PostManufacturer(ManufacturerRequest manufacturerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var now = DateTime.Now;

            var manufacturer = new Manufacturer
            {
                Name = manufacturerRequest.Name,
                AuditCreatedBy = UserResolver.CurrentUserId,
                AuditCreatedDate = now,
                AuditLastModifiedBy = UserResolver.CurrentUserId,
                AuditLastModifiedDate = now
            };

            WebApiContext.Manufacturers.Add(manufacturer);
            await WebApiContext.SaveChangesAsync();

            var response = new ManufacturerResponse
            {
                ManufacturerId = manufacturer.ManufacturerId,
                Name = manufacturer.Name
            };

            return CreatedAtRoute(nameof(GetManufacturer), new { manufacturerId = response.ManufacturerId }, response);
        }

        [Route("api/manufacturers/{manufacturerId}/logo")]
        public async Task<IHttpActionResult> PostManufacturerLogo(int manufacturerId)
        {
            var imageBytes = Convert.FromBase64String(await Request.Content.ReadAsStringAsync());
            var manufacturer = await WebApiContext.Manufacturers.FirstOrDefaultAsync(_ => _.ManufacturerId == manufacturerId);

            if (manufacturer != null)
            {
                var manufacturerLogo = await WebApiContext.ManufacturerLogos.FirstOrDefaultAsync(_ => _.ManufacturerLogoId == manufacturerId);

                if (manufacturerLogo == null)
                {
                    var newLogo = new ManufacturerLogo
                    {
                        Content = imageBytes,
                        Manufacturer = manufacturer,
                        ManufacturerLogoId = manufacturer.ManufacturerId
                    };

                    WebApiContext.ManufacturerLogos.Add(newLogo);
                    await WebApiContext.SaveChangesAsync();
                }
                else
                {
                    manufacturerLogo.Content = imageBytes;
                    WebApiContext.Entry(manufacturerLogo).State = EntityState.Modified;

                    try
                    {
                        await WebApiContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await WebApiContext.ManufacturerLogos.AnyAsync(_ => _.ManufacturerLogoId == manufacturerId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            return Ok();
        }

        [Route("api/manufacturers/{manufacturerId}")]
        [ResponseType(typeof(ManufacturerResponse))]
        public async Task<IHttpActionResult> DeleteManufacturer(int manufacturerId)
        {
            var manufacturer = await WebApiContext.Manufacturers.FindAsync(manufacturerId);

            if (manufacturer == null)
            {
                return NotFound();
            }

            var products = WebApiContext.Products
                .Where(_ => _.ManufacturerId == manufacturerId);

            var productImages = WebApiContext.ProductImages
                .Where(_ => products.Any(p => p.ProductId == _.ProductImageId));

            if (productImages.Any())
            {
                WebApiContext.ProductImages.RemoveRange(productImages);
            }

            if (products.Any())
            {
                WebApiContext.Products.RemoveRange(products);
            }

            var manufacturerLogo = await WebApiContext.ManufacturerLogos
                .FirstOrDefaultAsync(_ => _.ManufacturerLogoId == manufacturerId);

            if (manufacturerLogo != null)
            {
                WebApiContext.ManufacturerLogos.Remove(manufacturerLogo);
            }

            WebApiContext.Manufacturers.Remove(manufacturer);
            await WebApiContext.SaveChangesAsync();

            var response = new ManufacturerResponse
            {
                ManufacturerId = manufacturerId,
                Name = manufacturer.Name
            };

            return Ok(response);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                WebApiContext.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool ManufacturerExists(int id)
        {
            return WebApiContext.Manufacturers.Count(e => e.ManufacturerId == id) > 0;
        }
    }
}