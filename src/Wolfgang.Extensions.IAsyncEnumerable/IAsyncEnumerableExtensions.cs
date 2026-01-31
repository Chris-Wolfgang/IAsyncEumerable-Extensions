using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Wolfgang.Extensions.IAsyncEnumerable.Benchmarks")]

namespace Wolfgang.Extensions.IAsyncEnumerable;

/// <summary>
/// A collection of extension methods for IAsyncEnumerable{T}.
/// </summary>
// ReSharper disable once InconsistentNaming
public static class IAsyncEnumerableExtensions
{
	/// <summary>
	/// Splits an IAsyncEnumerable{T} into chunks of a specified maximum size.
	/// </summary>
	/// <param name="source">The source IAsyncEnumerable{T} to chunk.</param>
	/// <param name="maxChunkSize">The maximum size of each chunk.</param>
	/// <param name="token">A cancellation token to cancel the operation.</param>
	/// <typeparam name="T">The type of elements in the IAsyncEnumerable{T}.</typeparam>
	/// <returns>An IAsyncEnumerable{ICollection{T}} representing the chunks.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Thrown when maxChunkSize is less than or equal to zero.</exception>
	public static async IAsyncEnumerable<ICollection<T>> ChunkAsync<T>
	(
		this IAsyncEnumerable<T> source,
		int maxChunkSize,
		[EnumeratorCancellation] CancellationToken token = default
	)
	{
		if ( source == null )
		{
			throw new ArgumentNullException( nameof( source ) );
		}

		token.ThrowIfCancellationRequested();

		if (maxChunkSize <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(maxChunkSize), "Chunk size must be greater than zero.");
		}

		var chunk = new List<T>(maxChunkSize);
		await foreach (var item in source.WithCancellation(token))
		{
			token.ThrowIfCancellationRequested();

			chunk.Add(item);
			if (chunk.Count < maxChunkSize)
			{
				continue;
			}

			yield return chunk;
			token.ThrowIfCancellationRequested();
			chunk = new List<T>(maxChunkSize);
		}

		if (chunk.Count > 0)
		{
			token.ThrowIfCancellationRequested();
			yield return chunk;
		}
	}

    

    /// <summary>
    /// Splits an IAsyncEnumerable{T} into chunks of a specified maximum size.
    /// </summary>
    /// <param name="source">The source IAsyncEnumerable{T} to chunk.</param>
    /// <param name="maxChunkSize">The maximum size of each chunk.</param>
    /// <param name="token">A cancellation token to cancel the operation.</param>
    /// <typeparam name="T">The type of elements in the IAsyncEnumerable{T}.</typeparam>
    /// <returns>An IAsyncEnumerable{ICollection{T}} representing the chunks.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when maxChunkSize is less than or equal to zero.</exception>
    internal static async IAsyncEnumerable<ICollection<T>> ChunkAsyncV2<T>
    (
        this IAsyncEnumerable<T> source,
        int maxChunkSize,
        [EnumeratorCancellation] CancellationToken token = default
    )
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        token.ThrowIfCancellationRequested();

        if (maxChunkSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxChunkSize), "Chunk size must be greater than zero.");
        }

        //var chunk = new List<T>(maxChunkSize);
        var chunk = new T[maxChunkSize];
        var index = 0;
        await foreach (var item in source.WithCancellation(token))
        {
            token.ThrowIfCancellationRequested();

            chunk[index++] = item;
            if (index < maxChunkSize)
            {
                continue;
            }

            yield return chunk;
            token.ThrowIfCancellationRequested();
            chunk = new T[maxChunkSize];
            index = 0;
        }

        if (index == 0)
        {
            yield break;
        }

        token.ThrowIfCancellationRequested();
        Array.Resize(ref chunk, index);
        yield return chunk;
    }



}


