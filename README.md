# Wolfgang.Extensions.IAsyncEnumerable

High-performance, production-grade extension methods for `IAsyncEnumerable<T>` with comprehensive test coverage and strict code quality enforcement.

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-Multi--Targeted-purple.svg)](https://dotnet.microsoft.com/)

---

## 📦 Installation

```bash
dotnet add package Wolfgang.Extensions.IAsyncEnumerable
```

**NuGet Package:** Coming soon to [NuGet.org](https://www.nuget.org/)

---

## 🚀 Quick Start

```csharp
using Wolfgang.Extensions.IAsyncEnumerable;

// Chunk an async stream into batches
await foreach (var chunk in asyncStream.ChunkAsync(maxChunkSize: 100, token: cancellationToken))
{
    // Process each chunk (ICollection<T>)
    await ProcessBatchAsync(chunk);
}
```

---

## ✨ Features

### Current Extension Methods

#### **`ChunkAsync<T>`**
Splits an `IAsyncEnumerable<T>` into fixed-size chunks for batch processing.

```csharp
public static async IAsyncEnumerable<ICollection<T>> ChunkAsync<T>(
    this IAsyncEnumerable<T> source,
    int maxChunkSize,
    CancellationToken token = default)
```

**Parameters:**
- `source` - The source async enumerable to chunk
- `maxChunkSize` - Maximum size of each chunk (must be > 0)
- `token` - Optional cancellation token

**Returns:** An async enumerable of collections, where each collection contains up to `maxChunkSize` elements.

**Example:**
```csharp
var numbers = GetAsyncNumbers(); // IAsyncEnumerable<int>
await foreach (var batch in numbers.ChunkAsync(50))
{
    Console.WriteLine($"Processing batch of {batch.Count} items");
    // Last batch may be smaller than 50
}
```

---

## 🎯 Target Frameworks

This library supports multiple .NET versions:
- **.NET Framework 4.6.2** (`net462`)
- **.NET Standard 2.0** (`netstandard2.0`)
- **.NET 8.0** (`net8.0`)
- **.NET 10.0** (`net10.0`)

---

## 🔍 Code Quality & Static Analysis

This project enforces **strict code quality standards** through **7 specialized analyzers** and custom async-first rules:

### Analyzers in Use

1. **Microsoft.CodeAnalysis.NetAnalyzers** - Built-in .NET analyzers for correctness and performance
2. **Roslynator.Analyzers** - Advanced refactoring and code quality rules
3. **AsyncFixer** - Async/await best practices and anti-pattern detection
4. **Microsoft.VisualStudio.Threading.Analyzers** - Thread safety and async patterns
5. **Microsoft.CodeAnalysis.BannedApiAnalyzers** - Prevents usage of banned synchronous APIs
6. **Meziantou.Analyzer** - Comprehensive code quality rules
7. **SonarAnalyzer.CSharp** - Industry-standard code analysis

### Async-First Enforcement

This library uses **`BannedSymbols.txt`** to prohibit synchronous APIs and enforce async-first patterns:

**Blocked APIs Include:**
- ❌ `Task.Wait()`, `Task.Result` - Use `await` instead
- ❌ `Thread.Sleep()` - Use `await Task.Delay()` instead
- ❌ Synchronous file I/O (`File.ReadAllText`) - Use async versions
- ❌ Synchronous stream operations - Use `ReadAsync()`, `WriteAsync()`
- ❌ `Parallel.For/ForEach` - Use `Task.WhenAll()` or `Parallel.ForEachAsync()`
- ❌ Obsolete APIs (`WebClient`, `BinaryFormatter`)

**Why?** To ensure all code is **truly async** and **non-blocking** for optimal performance in async contexts.

---

## 🛠️ Building from Source

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later
- Optional: [PowerShell Core](https://github.com/PowerShell/PowerShell) for formatting scripts

### Build Steps

```bash
# Clone the repository
git clone https://github.com/Chris-Wolfgang/IAsyncEnumerable-Extensions.git
cd IAsyncEnumerable-Extensions

# Restore dependencies
dotnet restore

# Build the solution
dotnet build --configuration Release

# Run tests
dotnet test --configuration Release

# Run code formatting (PowerShell Core)
pwsh ./format.ps1
```

### Code Formatting

This project uses `.editorconfig` and `dotnet format`:

```bash
# Format code
dotnet format

# Verify formatting (as CI does)
dotnet format --verify-no-changes
```

See [README-FORMATTING.md](README-FORMATTING.md) for detailed formatting guidelines.

---

## 🤝 Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for:
- Code quality standards
- Build and test instructions
- Pull request guidelines
- Analyzer configuration details

---

## 📄 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## 📚 Documentation

- **API Documentation:** Generated via DocFX (see `docfx_project/`)
- **Formatting Guide:** [README-FORMATTING.md](README-FORMATTING.md)
- **Contributing Guide:** [CONTRIBUTING.md](CONTRIBUTING.md)

---

## 🙏 Acknowledgments

Built with:
- [Microsoft.Bcl.AsyncInterfaces](https://www.nuget.org/packages/Microsoft.Bcl.AsyncInterfaces/) for backward compatibility
- Comprehensive analyzer packages for code quality enforcement
- .NET async/await patterns for optimal performance
