namespace WebApi.Benchmark
{
    public class Program
    {
#pragma warning disable IDE0060
        public static void Main(string[] args)
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}