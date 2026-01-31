using BenchmarkDotNet.Running;
using Wolfgang.Extensions.IAsyncEnumerable.Benchmarks;

BenchmarkRunner.Run<ChunkAsyncBenchmarks>();
