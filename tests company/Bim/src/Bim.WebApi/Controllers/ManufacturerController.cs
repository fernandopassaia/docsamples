using Bim.Util.Resolver;
using System.Linq;
using System.Web.Http;
using Bim.Domain.Dtos;
using Bim.Repository.DataContext;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Net;
using System;
using Bim.Domain.Entities;
using System.Data.Entity.Infrastructure;

namespace Bim.WebApi.Controllers
{
    public class ManufacturerController : ApiControllerBase
    {        
        //TO-DO: Externalize the BimContext to ManufacturerRepo and inside it Get, Save, Count...
        public ManufacturerController(IBimContext bimContext, IFakeUserResolver userResolver)
            : base(bimContext, userResolver)
        {
        }

        #region Gets
        [Route("api/manufacturers")]
        public IQueryable<ManufacturerResponse> GetManufacturers()
        {
            return DbContext.Manufacturers.Select(_dbManufacturer => new ManufacturerResponse
            {
                id = _dbManufacturer.id,
                Name = _dbManufacturer.Name,
                createBy = _dbManufacturer.createBy,
                createIn = _dbManufacturer.createIn,
                updateBy = _dbManufacturer.updateBy,
                updateIn = _dbManufacturer.updateIn,
                TotalNumberOfProducts = DbContext.Products.Count(p => p.ManufacturerId == _dbManufacturer.id)
            });
        }

        [Route("api/manufacturers/{id}", Name = nameof(GetManufacturer))]
        [ResponseType(typeof(ManufacturerResponse))]
        public async Task<IHttpActionResult> GetManufacturer(int id)
        {
            var manufacturer = await DbContext.Manufacturers.FindAsync(id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            var result = new ManufacturerResponse
            {
                id = manufacturer.id,
                Name = manufacturer.Name,
                createBy = manufacturer.createBy,
                createIn = manufacturer.createIn,
                updateBy = manufacturer.updateBy,
                updateIn = manufacturer.updateIn,
                TotalNumberOfProducts = ReturnNumberOfProductsPerManufacturer(id)
            };

            return Ok(result);
        }

        [Route("api/manufacturers/{id}/image")]
        public async Task<IHttpActionResult> GetManufacturerImage(int id)
        {
            var manufacturerLogo = await DbContext.ManufacturerImages.FirstOrDefaultAsync(dbManufacturer => dbManufacturer.ManufacturerId == id);

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
                    Properties.Resources.noimage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                    var result = new System.Net.Http.HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new System.Net.Http.ByteArrayContent(stream.ToArray());
                    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

                    return ResponseMessage(result);
                }
            }
        }
        #endregion

        #region Posts
        [Route("api/manufacturers")]
        [ResponseType(typeof(ManufacturerResponse))]
        public async Task<IHttpActionResult> PostManufacturer(ManufacturerRequest manufacturerRequest)
        {
            if (!ModelState.IsValid)
            {                
                return BadRequest(ModelState);
            }            

            var manufacturer = new Manufacturer
            {
                Name = manufacturerRequest.Name,
                createBy = UserResolver.CurrentUserId,
                createIn = DateTime.Now,
                updateBy = UserResolver.CurrentUserId,
                updateIn = DateTime.Now
            };

            DbContext.Manufacturers.Add(manufacturer);
            await DbContext.SaveChangesAsync();

            var response = new ManufacturerResponse
            {             
                id = manufacturer.id,
                Name = manufacturer.Name,
                createBy = manufacturer.createBy,
                createIn = manufacturer.createIn,
                updateBy = manufacturer.updateBy,
                updateIn = manufacturer.updateIn,
                TotalNumberOfProducts = ReturnNumberOfProductsPerManufacturer(manufacturer.id)
            
        };

            return CreatedAtRoute(nameof(GetManufacturer), new { id = response.id }, response);
        }

        [Route("api/manufacturers/{id}/image")]
        public async Task<IHttpActionResult> PostManufacturerImage(int id)
        {
            var imageBytes = Convert.FromBase64String(await Request.Content.ReadAsStringAsync());
            var manufacturer = await DbContext.Manufacturers.FirstOrDefaultAsync(dbManufacturer => dbManufacturer.id == id);

            if (manufacturer != null)
            {
                var manufacturerLogo = await DbContext.ManufacturerImages.FirstOrDefaultAsync(dbManufacturer => dbManufacturer.ManufacturerId == id);

                if (manufacturerLogo == null)
                {
                    var newLogo = new ManufacturerImage
                    {
                        Content = imageBytes,
                        Manufacturer = manufacturer,
                        ImageId = manufacturer.id,
                        ManufacturerId = manufacturer.id
                    };

                    DbContext.ManufacturerImages.Add(newLogo);
                    await DbContext.SaveChangesAsync();
                }
                else
                {
                    manufacturerLogo.Content = imageBytes;
                    DbContext.Entry(manufacturerLogo).State = EntityState.Modified;

                    try
                    {
                        await DbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await DbContext.ManufacturerImages.AnyAsync(dbManufacturer => dbManufacturer.ManufacturerId == id))
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
        #endregion

        #region Put/Delete
        [Route("api/manufacturers/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutManufacturer(int id, ManufacturerRequest manufacturerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != manufacturerRequest.id)
            {
                return BadRequest();
            }

            var manufacturer = await DbContext.Manufacturers.FirstOrDefaultAsync(dbManufacturer => dbManufacturer.id == id);

            if (manufacturer != null)
            {
                manufacturer.updateBy = UserResolver.CurrentUserId;
                manufacturer.updateIn = DateTime.Now;
                manufacturer.Name = manufacturerRequest.Name;
            }
            else
            {
                return NotFound();
            }

            DbContext.Entry(manufacturer).State = EntityState.Modified;

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckIfManufacturerExists(id))
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

        [Route("api/manufacturers/{id}")]
        [ResponseType(typeof(ManufacturerResponse))]
        public async Task<IHttpActionResult> DeleteManufacturer(int id)
        {
            var manufacturer = await DbContext.Manufacturers.FindAsync(id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            var products = DbContext.Products //get Products from Manufacturer to Remove
                .Where(dbManufacturer => dbManufacturer.ManufacturerId == id);

            var productImages = DbContext.ProductImages //get ProductsImages from Manufacturer to Remove
                .Where(dbProduct => products.Any(p => p.id == dbProduct.ImageId));

            if (productImages.Any())
            {
                DbContext.ProductImages.RemoveRange(productImages);
            }

            if (products.Any())
            {
                DbContext.Products.RemoveRange(products);
            }

            var manufacturerImages = await DbContext.ManufacturerImages //get ManufacturerImage to Remove
                .FirstOrDefaultAsync(ddManufacturerImage => ddManufacturerImage.ManufacturerId == id);

            if (manufacturerImages != null)
            {
                DbContext.ManufacturerImages.Remove(manufacturerImages);
            }

            DbContext.Manufacturers.Remove(manufacturer);
            await DbContext.SaveChangesAsync();

            var response = new ManufacturerResponse
            {
                id = id,
                Name = "Removed" //manufacturer.Name
            };

            return Ok(response);
        }
        #endregion

        #region Aux Methods (TO-DO: Move to Handler / Service)
        private bool CheckIfManufacturerExists(int id)
        {
            return DbContext.Manufacturers.Count(e => e.id == id) > 0;
        }

        private int ReturnNumberOfProductsPerManufacturer(int id)
        {
            return DbContext.Products.Count(dbManufaturar => dbManufaturar.ManufacturerId == id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}