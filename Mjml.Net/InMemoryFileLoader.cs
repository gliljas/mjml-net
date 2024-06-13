using Mjml.Net;

namespace Tests.Internal;

/// <summary>
/// Provides the files from an in memory store.
/// </summary>
/// <remarks>
/// Useful for preloading.
/// </remarks>
public sealed class InMemoryFileLoader : IFileLoader
{
    private readonly Stack<string> pathStack = new Stack<string>();
    private readonly IReadOnlyDictionary<string, string> content;

    public InMemoryFileLoader(IReadOnlyDictionary<string, string> content)
    {
        this.content = content;
    }

    /// <inheritdoc />
    public string? LoadText(string path)
    {
        if (path is null)
        {
            throw new ArgumentNullException(nameof(path));
        }
#if NETSTANDARD2_0
        string parentPath = default;
        if (pathStack.Count > 0)
        {
            parentPath = pathStack.Peek();
        }
#else
        pathStack.TryPeek(out var parentPath);
#endif
        path = BuildPath(path, parentPath);

        pathStack.Push(path);

        return content.GetValueOrDefault(path);
    }

    private static string BuildPath(string path, string? parentPath)
    {
        if (path.StartsWith('/') || path.StartsWith('\\'))
        {
            return path;
        }

        if (parentPath == null)
        {
            return path;
        }

        var folderIndex = parentPath.LastIndexOf('/');

        if (folderIndex >= 0)
        {
            var folderPart = parentPath[..folderIndex];

            return $"{folderPart}/{path}";
        }

        folderIndex = parentPath.LastIndexOf('\\');

        if (folderIndex >= 0)
        {
            var folderPart = parentPath[..folderIndex];

            return $"{folderPart}\\{path}";
        }

        return path;
    }
}
