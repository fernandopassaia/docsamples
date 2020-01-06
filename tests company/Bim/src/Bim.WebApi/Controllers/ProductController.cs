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
    public class ProductController : ApiControllerBase
    {
        //TO-DO: Externalize the BimContext to ProductRepo and inside it Get, Save, Count...
        public ProductController(IBimContext bimContext, IFakeUserResolver userResolver)
            : base(bimContext, userResolver)
        {
        }

        #region Gets
        [Route("api/manufacturers/{manufacturerId}/products")]
        public IQueryable<ProductResponse> GetProducts(int manufacturerId)
        {
            return DbContext.Products
                .Where(dbProducts => dbProducts.ManufacturerId == manufacturerId)
                .Select(dbProducts => new ProductResponse
                {
                    ManufacturerId = dbProducts.ManufacturerId,
                    Name = dbProducts.Name,
                    id = dbProducts.id
                });
        }

        [Route("api/manufacturers/{manufacturerId}/products/{productId}", Name = nameof(GetProduct))]
        [ResponseType(typeof(ProductResponse))]
        public async Task<IHttpActionResult> GetProduct(int manufacturerId, int productId)
        {
            var product = await DbContext.Products
                .Where(dbProduct => dbProduct.ManufacturerId == manufacturerId)
                .Where(dbProduct => dbProduct.id == productId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            var response = new ProductResponse
            {
                ManufacturerId = product.ManufacturerId,
                Name = product.Name,
                id = product.id
            };

            return Ok(response);
        }

        [Route("api/manufacturers/{manufacturerId}/products/{productId}/image")]
        public async Task<IHttpActionResult> GetProductImage(int manufacturerId, int productId)
        {
            var productImage = await DbContext.ProductImages.FirstOrDefaultAsync(DbContext => DbContext.ImageId == productId);

            if (productImage?.Content != null)
            {
                using (var stream = new System.IO.MemoryStream(productImage.Content))
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
        [Route("api/manufacturers/{manufacturerId}/products")]
        [ResponseType(typeof(ProductResponse))]
        public async Task<IHttpActionResult> PostProduct(int manufacturerId, ProductRequest productRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await DbContext.Manufacturers.AnyAsync(dbManufacturer => dbManufacturer.id == manufacturerId))
            {
                return BadRequest("Manufacturer does not exist.");
            }
            
            var product = new Product
            {
                createBy = UserResolver.CurrentUserId,
                createIn = DateTime.Now,
                updateBy = UserResolver.CurrentUserId,
                updateIn = DateTime.Now,
                ManufacturerId = manufacturerId,
                Name = productRequest.Name,
                id = productRequest.id
            };

            DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();

            var response = new ProductResponse
            {
                ManufacturerId = product.ManufacturerId,
                Name = product.Name,
                id = product.id
            };

            return CreatedAtRoute(nameof(GetProduct), new { manufacturerId = response.ManufacturerId, productId = response.id }, response);
        }

        [Route("api/manufacturers/{manufacturerId}/products/{productId}/image")]
        public async Task<IHttpActionResult> PostProductImage(int manufacturerId, int productId)
        {
            var imageBytes = Convert.FromBase64String(await Request.Content.ReadAsStringAsync());
            var product = await DbContext.Products.FirstOrDefaultAsync(dbProduct => dbProduct.ManufacturerId == manufacturerId && dbProduct.id == productId);

            if (product != null)
            {
                var productImage = await DbContext.ProductImages.FirstOrDefaultAsync(_ => _.ProductId == productId);

                if (productImage == null)
                {
                    var newImage = new ProductImage
                    {
                        Content = imageBytes,
                        Product = product,
                        ProductId = productId
                    };

                    DbContext.ProductImages.Add(newImage);
                    await DbContext.SaveChangesAsync();
                }
                else
                {
                    productImage.Content = imageBytes;
                    DbContext.Entry(productImage).State = EntityState.Modified;

                    try
                    {
                        await DbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await DbContext.ProductImages.AnyAsync(dbProductImage => dbProductImage.ProductId == productId))
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
        [Route("api/manufacturers/{manufacturerId}/products/{productId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int manufacturerId, int productId, ProductRequest productRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (manufacturerId != productRequest.ManufacturerId || productId != productRequest.id)
            {
                return BadRequest();
            }

            var product = await DbContext.Products.FirstOrDefaultAsync(dbProduct => dbProduct.ManufacturerId == manufacturerId && dbProduct.id == productId);

            if (product != null)
            {
                product.updateBy = UserResolver.CurrentUserId;
                product.updateIn = DateTime.Now;
                product.Name = productRequest.Name;
            }
            else
            {
                return NotFound();
            }

            DbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(manufacturerId, productId))
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

        [Route("api/manufacturers/{manufacturerId}/products/{productId}")]
        [ResponseType(typeof(ProductResponse))]
        public async Task<IHttpActionResult> DeleteProduct(int manufacturerId, int productId)
        {
            var product = await DbContext.Products
                .Where(dbProduct => dbProduct.ManufacturerId == manufacturerId)
                .Where(dbProduct => dbProduct.id == productId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            var productImage = await DbContext.ProductImages
                .FirstOrDefaultAsync(dbProduct => dbProduct.ImageId == productId);

            if (productImage != null)
            {
                DbContext.ProductImages.Remove(productImage);
            }

            DbContext.Products.Remove(product);
            await DbContext.SaveChangesAsync();

            var response = new ProductResponse
            {
                ManufacturerId = product.ManufacturerId,
                Name = product.Name,
                id = product.id
            };

            return Ok(response);
        }        

        private bool ProductExists(int manufacturerId, int productId)
        {
            return DbContext.Products.Count(dbProduct => dbProduct.ManufacturerId == manufacturerId && dbProduct.id == productId) > 0;
        }
        #endregion

        #region Aux Methods (TO-DO: Move to Handler / Service)
        private bool CheckIfProductExists(int manufacturerId, int productId)
        {
            return DbContext.Products.Count(dbProduct => dbProduct.ManufacturerId == manufacturerId && dbProduct.id == productId) > 0;
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