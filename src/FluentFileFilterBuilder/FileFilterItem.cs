using System;
using System.Linq;

namespace FluentFileFilterBuilder;

/// <summary>
/// Represents a single item in a filter.
/// </summary>
public sealed class FileFilterItem
{
    /// <summary>
    /// Gets the filter description.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets filter pattern.
    /// </summary>
    public string Pattern { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileFilterItem"/> class.
    /// </summary>
    /// <param name="description">The filter description.</param>
    /// <param name="extensions">The extensions included in the filter.</param>
    public FileFilterItem(string description, string[] extensions)
    {
        if (description == null) throw new ArgumentNullException(nameof(description));
        if (description.Length == 0) throw new ArgumentException("Value cannot be an empty string.", nameof(description));
        if (extensions == null) throw new ArgumentNullException(nameof(extensions));
        if (extensions.Length == 0) throw new ArgumentException("At least one extension must be supplied.", nameof(extensions));

        Description = description;
        Pattern = String.Join(";", extensions.Select(NormalizeExtension));
    }

    /// <summary>
    /// Normalizes the specified extension.
    /// </summary>
    /// <param name="extension">The extension to normalize.</param>
    private static string NormalizeExtension(string extension)
    {
        var normalizedExtension = extension.Trim().TrimStart('*', '.');
        if (string.IsNullOrEmpty(normalizedExtension))
        {
            normalizedExtension = "*";
        }
        return $"*.{normalizedExtension}";
    }

    /// <summary>
    /// Returns a string representation of the filter item.
    /// </summary>
    /// <returns>A string representation of the filter item.</returns>
    public override string ToString()
    {
        return $"{Description} ({Pattern})|{Pattern}";
    }

    /// <summary>
    /// Attempts to parse a file filter item string (for example, "Image files (*.bmp;*.jpg)|*.bmp;*.jpg")
    /// into a <see cref="FileFilterItem"/> instance.
    /// </summary>
    /// <param name="value">The filter item string to parse.</param>
    /// <param name="item">When this method returns, contains the parsed <see cref="FileFilterItem"/>, if parsing succeeded; otherwise, null.</param>
    /// <returns>true if the string was successfully parsed; otherwise, false.</returns>
    public static bool TryParse(string value, out FileFilterItem item)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            item = null;
            return false;
        }

        var parts = value.Split(['|']);
        if (parts.Length != 2)
        {
            item = null;
            return false;
        }

        return TryCreateFromParts(parts[0], parts[1], out item);
    }

    /// <summary>
    /// Attempts to create a new <see cref="FileFilterItem"/> from parts.
    /// </summary>
    /// <param name="descriptionPart">The description part.</param>
    /// <param name="extensionsPart">The extensions part.</param>
    /// <param name="item">When this method returns, contains the created <see cref="FileFilterItem"/>; otherwise, null.</param>
    /// <returns>true if the item was successfully created; otherwise, false.</returns>
    internal static bool TryCreateFromParts(string descriptionPart, string extensionsPart, out FileFilterItem item)
    {
        // Get description, excluding the pattern in parentheses.
        var descriptionParenthesesIndex = descriptionPart.IndexOf('(');
        var description = descriptionParenthesesIndex > 0
            ? descriptionPart.Substring(0, descriptionParenthesesIndex).Trim()
            : descriptionPart;

        // Get extensions, split on ';' to get individual patterns, and trim.
        var extensions = extensionsPart.Split([';'], StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray();

        if (!string.IsNullOrWhiteSpace(description) && extensions.Length > 0)
        {
            item = new FileFilterItem(description, extensions);
            return true;
        }
        else
        {
            item = default;
            return false;
        }
    }
}
