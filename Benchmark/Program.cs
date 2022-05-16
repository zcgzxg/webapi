using WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Database;
using WebApi.Models;
using BenchmarkDotNet.Attributes;

namespace ControllerBenchmark
{
    [RPlotExporter]
    public class CategoryBench
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryBench()
        {
            _logger = Mock.Of<ILogger<CategoryController>>();
        }

        [Benchmark]
        public async Task BenchGetCategory()
        {
            const string connStr = "server=nil.fit; uid=test; pwd=/Ce55edoR6@nlb>B6Qw9; database=test; UseUsageAdvisor=true; CacheServerProperties=true; logging=true;";
            var controller = new CategoryController(_logger);
            await controller.GetCategories(new RelationalDB(connStr), new Token());
        }
    }

    public class Program
    {
#pragma warning disable IDE0060
        public static void Main(string[] args)
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}