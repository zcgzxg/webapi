using Microsoft.AspNetCore.Mvc;
using Dapper.Contrib.Extensions;
using Dapper;
using webapi.Models;
using webapi.Database;
using webapi.Base;
using Microsoft.AspNetCore.Authorization;

namespace webapi.Controllers;

/// <summary>
/// Category Controller
/// </summary>
[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;

    /// <summary>
    /// Test Controller
    /// </summary>
    public CategoryController(ILogger<CategoryController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 获取分类列表及其下的商品
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<CommonResponse<IEnumerable<CategoryResponse>>> GetCategories([FromServices] IRelationalDB db, [FromServices] User user)
    {
        _logger.LogInformation(@"User: " + user.Name + user.ID);
        var conn = db.Conn;
        await conn.OpenAsync();
        var sql = @"
                SELECT c.*,
                    p.productid,
                    p.productname,
                    p.categoryid
                FROM categories c
                    LEFT JOIN products p ON p.categoryid = c.categoryid";
        var categories = await conn.QueryAsync<CategoryResponse, Product, CategoryResponse>(sql, (c, p) =>
        {
            if (p is not null)
            {
                c.Products?.Add(p);
            }
            return c;
        }, splitOn: "ProductId");

        return new CommonResponse<IEnumerable<CategoryResponse>>(categories.GroupBy(c => c.CategoryId).Select(g =>
        {
            var c = g.First();
            if (c.Products.Any())
            {
                c.Products = g.Select(c => c.Products.Single()).ToList();
            }
            return c;
        }));
    }
    /// <summary>
    /// 编辑分类
    /// </summary>
    [HttpPost]
    public async Task<CommonResponse<Category>> EditCategory([FromBody] Category cate, [FromServices] IRelationalDB db)
    {
        var conn = db.Conn;
        if (cate.CategoryId == 0)
        {
            cate.CategoryId = (uint)(await conn.InsertAsync(cate));
        }
        else
        {
            if (!await conn.UpdateAsync(cate))
            {
                return new CommonResponse<Category>(null, ErrorCodes.Error, "更新失败");
            }
        }
        return new CommonResponse<Category>(cate);
    }

    /// <summary>
    /// 删除分类
    /// </summary>
    [HttpDelete]
    public async Task<CommonResponse<bool>> DeleteCategory([FromQuery] uint categoryId, [FromServices] IRelationalDB db)
    {
        var conn = db.Conn;
        var cate = await conn.GetAsync<Category>(categoryId);
        if (cate == null)
        {
            return new CommonResponse<bool>(false, ErrorCodes.Error, "分类不存在");
        }
        if (!await conn.DeleteAsync(cate))
        {
            return new CommonResponse<bool>(false, ErrorCodes.Error, "删除失败");
        }
        return new CommonResponse<bool>(true);
    }
}
