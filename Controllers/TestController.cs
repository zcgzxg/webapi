using Microsoft.AspNetCore.Mvc;
using DapperExtensions;
using Dapper;
using webapi.Models;
using webapi.Base;

namespace webapi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Category>> GetCategories([FromServices] AppDB db)
    {
        try
        {
            var conn = db.Conn;
            await conn.OpenAsync();
            var sql = @"
                SELECT c.*,
                    p.productid,
                    p.*
                FROM categories c
                    INNER JOIN products p ON p.categoryid = c.categoryid";
            var categories = await conn.QueryAsync<Category, Product, Category>(sql, (c, p) =>
            {
                c.Products?.Add(p);
                return c;
            }, splitOn: "ProductId");

            return categories.GroupBy(c => c.CategoryId).Select(g =>
            {
                var c = g.First();
                c.Products = g.Select(c => c.Products.Single()).ToList();
                return c;
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Enumerable.Empty<Category>();
        }
    }
    [HttpPost]
    public async Task<Category?> EditCategory(Category cate, [FromServices] AppDB db)
    {
        try
        {
            var conn = db.Conn;
            cate = await conn.InsertAsync(cate);
            return cate;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
