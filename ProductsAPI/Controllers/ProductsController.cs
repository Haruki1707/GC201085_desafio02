using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using StackExchange.Redis;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IConnectionMultiplexer _redis;

        public ProductsController(ProductContext context, IConnectionMultiplexer redis)
        {
            _context = context;
            _redis = redis;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var db = _redis.GetDatabase();
            var cacheKey = "productsList";


            var productsCache = await db.StringGetAsync(cacheKey);
            if (!productsCache.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<List<Product>>(productsCache);
            }

            var products = await _context.Products.ToListAsync();
            await db.StringSetAsync(cacheKey, JsonSerializer.Serialize(products), TimeSpan.FromMinutes(5));
            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var db = _redis.GetDatabase();
            string cacheKey = $"product_{id}";
            var productCache = await db.StringGetAsync(cacheKey);

            if (!productCache.IsNullOrEmpty) {
                return JsonSerializer.Deserialize<Product>(productCache);
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            await db.StringSetAsync(cacheKey, JsonSerializer.Serialize(product), TimeSpan.FromMinutes(5));
            return product;
        }
    }
}
