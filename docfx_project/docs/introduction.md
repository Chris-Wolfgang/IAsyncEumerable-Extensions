# Introduction

## What is Wolfgang.Extensions.IAsyncEnumerable?

Wolfgang.Extensions.IAsyncEnumerable is a high-performance .NET library that provides extension methods for working with `IAsyncEnumerable<T>`. It's designed to make asynchronous stream processing easier, more efficient, and more maintainable.

## Why Use This Library?

Working with asynchronous streams in .NET can be challenging. While `IAsyncEnumerable<T>` provides a powerful abstraction for asynchronous sequences, common operations like chunking, batching, and transformation often require repetitive boilerplate code.

This library provides:

- **Production-Ready Extensions**: Battle-tested extension methods for common async enumerable operations
- **High Performance**: Optimized implementations with minimal allocations
- **Comprehensive Test Coverage**: Extensive unit tests ensure reliability
- **Multi-Framework Support**: Works with .NET Framework 4.6.2, .NET Standard 2.0, .NET 8.0, and .NET 10.0
- **Strict Code Quality**: Enforced through 7 specialized analyzers and async-first patterns

## Key Features

### ChunkAsync<T>

The `ChunkAsync` extension method allows you to split an asynchronous stream into fixed-size chunks for efficient batch processing:

```csharp
using Wolfgang.Extensions.IAsyncEnumerable;

// Process a large async stream in batches of 100
await foreach (var batch in largeAsyncStream.ChunkAsync(maxChunkSize: 100))
{
    // Each batch is an ICollection<T> with up to 100 items
    await ProcessBatchAsync(batch);
}
```

**Benefits:**
- Enables efficient batch processing of async streams
- Reduces memory overhead by processing data in manageable chunks
- Supports cancellation tokens for responsive applications
- Handles edge cases like incomplete final chunks automatically

## Async-First Design

This library is built with an **async-first philosophy**:

- All operations are truly asynchronous and non-blocking
- No usage of `Task.Wait()`, `Task.Result`, or other blocking operations
- Enforced through banned API analyzers and code quality rules
- Optimized for async/await patterns throughout

## Code Quality Standards

The library maintains exceptional code quality through:

1. **7 Specialized Analyzers**: Including AsyncFixer, Roslynator, and SonarAnalyzer
2. **Banned API Enforcement**: Prevents usage of synchronous and obsolete APIs
3. **Comprehensive Testing**: High test coverage with both unit and integration tests
4. **Strict Formatting**: Enforced through `.editorconfig` and `dotnet format`
5. **Continuous Integration**: Automated builds, tests, and quality checks on every commit

## Use Cases

This library is ideal for:

- **Batch Processing**: Processing large datasets in manageable chunks
- **Stream Processing**: Working with continuous data streams from APIs, databases, or message queues
- **Rate Limiting**: Controlling the rate of operations on async streams
- **Resource Management**: Managing memory usage when processing large collections asynchronously
- **Performance Optimization**: Improving throughput in async data pipelines

## Next Steps

Ready to get started? Check out the [Getting Started](getting-started.md) guide to install and use the library in your project.