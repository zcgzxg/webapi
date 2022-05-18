using WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Database;
using WebApi.Models;
using BenchmarkDotNet.Attributes;

namespace WebApi.Benchmark.Controllers
{
    [RPlotExporter]
    [ThreadingDiagnoser]
    [MemoryDiagnoser]
    [EventPipeProfiler(BenchmarkDotNet.Diagnosers.EventPipeProfile.GcCollect | BenchmarkDotNet.Diagnosers.EventPipeProfile.CpuSampling)]
    [IterationTime(120)]
    [IterationCount(10)]
    [InvocationCount(2)]
    public class BMCategoryController
    {
        private readonly ILogger<CategoryController> _logger;

        public BMCategoryController()
        {
            _logger = Mock.Of<ILogger<CategoryController>>();
        }

        [Benchmark]
        public async Task LowerBench()
        {
            await BenchGetCategory(10);
        }

        [Benchmark(Baseline = true)]
        public async Task MediumBench()
        {
            await BenchGetCategory(20);
        }

        [Benchmark]
        public async Task HigherBench()
        {
            await BenchGetCategory(50);
        }

        [Benchmark]
        public async Task HighestBench()
        {
            await BenchGetCategory(80);
        }

        public async Task BenchGetCategory(int parallelCount)
        {
            var tasks = new Task<WebApi.Base.CommonResponse<IEnumerable<CategoryResponse>>>[parallelCount];
            const string connStr = "server=nil.fit; uid=test; pwd=/Ce55edoR6@nlb>B6Qw9; database=test; UseUsageAdvisor=true; CacheServerProperties=true; logging=true; MaximumPoolsize=200;";
            for (var i = 0; i < parallelCount; i++)
            {
                var controller = new CategoryController(_logger);
                tasks[i] = controller.GetCategories(new RelationalDB(connStr), new Token());
            }

            await Task.WhenAll(tasks);
        }
    }
}