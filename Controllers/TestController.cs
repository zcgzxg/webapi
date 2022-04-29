using Microsoft.AspNetCore.Mvc;
using Dapper.Contrib.Extensions;
using Dapper;
using webapi.Models;
using webapi.Base;

namespace webapi.Controllers;

/// <summary>
/// Test Controller
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    /// <summary>
    /// Test Controller
    /// </summary>
    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 获取分类列表及其下的商品
    /// </summary>
    [HttpGet]
    public async Task<CommonResponse<IEnumerable<CategoryORM>>> GetCategories([FromServices] AppDB db)
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
            var categories = await conn.QueryAsync<CategoryORM, Product, CategoryORM>(sql, (c, p) =>
            {
                c.Products?.Add(p);
                return c;
            }, splitOn: "ProductId");

            return new CommonResponse<IEnumerable<CategoryORM>>(categories.GroupBy(c => c.CategoryId).Select(g =>
            {
                var c = g.First();
                c.Products = g.Select(c => c.Products.Single()).ToList();
                return c;
            }));
        }
        catch (Exception e)
        {
            _logger.LogTrace(exception: e, message: nameof(TestController));
            return new CommonResponse<IEnumerable<CategoryORM>>(Enumerable.Empty<CategoryORM>(), ErrorCodes.Error, e.Message);
        }
    }
    /// <summary>
    /// 编辑分类
    /// </summary>
    [HttpPost]
    public async Task<CommonResponse<EditCategoryModel>> EditCategory([FromBody] EditCategoryModel cate, [FromServices] AppDB db)
    {
        try
        {
            var conn = db.Conn;
            var id = await conn.InsertAsync<EditCategoryModel>(cate);
            cate.CategoryId = (UInt32)id;
            return new CommonResponse<EditCategoryModel>(cate);
        }
        catch (Exception e)
        {
            _logger.LogTrace(exception: e, message: nameof(TestController) + " " + e.Message);
            return new CommonResponse<EditCategoryModel>(null, ErrorCodes.Error, e.Message);
        }
    }
}
