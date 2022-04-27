using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using webapi.Models;

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

    [HttpGet("")]
    public async Task<IEnumerable<Product>> Get([FromServices] MySqlConnection conn)
    {
        try
        {
            await conn.OpenAsync();
            var sql = @"select p.*,p.categoryid,c.categoryname from products p inner join categories c on p.categoryid = c.categoryid where productid < 3";
            var products = await conn.QueryAsync<Product, Category, Product>(sql, (p, c) =>
            {
                p.Category = c;
                return p;
            }, splitOn: "CategoryId");
            return products;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Enumerable.Empty<Product>();
        }
    }
}
