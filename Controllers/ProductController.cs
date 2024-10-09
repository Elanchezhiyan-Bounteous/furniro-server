using furniro_server.Contracts;
using furniro_server.Models;
using Microsoft.AspNetCore.Mvc;
using Supabase;
using Newtonsoft.Json;

namespace furniro_server.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly Supabase.Client _client;

        public ProductController(Supabase.Client client)
        {
            _client = client;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Desc = request.Desc,
                Category = request.Category,
                Price = request.Price,
                Src = request.Src,
                OriginalPrice = request.OriginalPrice,
                Discount = request.Discount,
                Reviews = request.Reviews.Select(rev => new Review
                {
                    Name = rev.Name,
                    Feedback = rev.Feedback,
                }).ToList(),
                Rating = request.Rating,
                Sku = request.Sku,
                Tags = request.Tags,
                Sizes = request.Sizes,
                Colors = request.Colors.Select(col => new ProductColor
                {
                    Name = col.Name,
                    Value = col.Value,
                }).ToList(),

                ProductGallery = request.ProductGallery.Select(gal => new Image
                {
                    Alt = gal.Alt,
                    ImageUrl = gal.ImageUrl,
                }).ToList(),

                DescriptionImages = request.DescriptionImages.Select(x => new Image
                {
                    Alt = x.Alt,
                    ImageUrl = x.ImageUrl,
                }).ToList(),


            };

            var response = await _client.From<Product>().Insert(product);
            var newProduct = response.Models.First();

            return Ok(JsonConvert.SerializeObject(newProduct));
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _client.From<Product>().Get();

            var products = response.Models;

            if (!products.Any())
            {
                return NotFound("No products found.");
            }

            var productResponses = products.Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Desc = product.Desc,
                Category = product.Category,
                Src = product.Src,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice,  
                Discount = product.Discount,
                Reviews = product.Reviews,
                Rating = product.Rating,
                Sku = product.Sku,
                Tags = product.Tags,
                Sizes = product.Sizes,
                Colors = product.Colors,
                DescriptionImages = product.DescriptionImages,
                ProductGallery = product.ProductGallery,
                CreatedAt = product.CreatedAt
            }).ToList();

            return Ok(productResponses);
        }

        [HttpGet("{category:string}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            var response = await _client.From<Product>()
                .Where(n => n.Category == category)
                .Get();

            var products = response.Models;

            if (products is null || !products.Any())
            {
                return NotFound();
            }

            var productResponses = products.Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Desc = product.Desc,
                Category = product.Category,
                Src = product.Src,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice,
                Discount = product.Discount,
                Reviews = product.Reviews,
                Rating = product.Rating,
                Sku = product.Sku,
                Tags = product.Tags,
                Sizes = product.Sizes,
                Colors = product.Colors,
                DescriptionImages = product.DescriptionImages,
                ProductGallery = product.ProductGallery,
                CreatedAt = product.CreatedAt
            }).ToList();

            return Ok(productResponses);
        }


        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var response = await _client.From<Product>()
                .Where(n => n.Id == id)
                .Get();

            var product = response.Models.FirstOrDefault();

            if (product is null)
            {
                return NotFound();
            }

            var productResponse = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Desc = product.Desc,
                Category = product.Category,
                Src = product.Src,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice,
                Discount = product.Discount,
                Reviews = product.Reviews,
                Rating = product.Rating,
                Sku = product.Sku,
                Tags = product.Tags,
                Sizes = product.Sizes,
                Colors = product.Colors,
                DescriptionImages = product.DescriptionImages,
                ProductGallery = product.ProductGallery,
                CreatedAt = product.CreatedAt
            };

            return Ok(productResponse);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            await _client.From<Product>()
                .Where(n => n.Id == id)
                .Delete();

            return NoContent();
        }
    }
}
