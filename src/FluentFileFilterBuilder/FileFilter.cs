using System;
using System.Collections.Generic;

namespace FluentFileFilterBuilder;

/// <summary>
/// Exposes methods to work with file filter strings.
/// </summary>
public static class FileFilter
{
    /// <summary>
    /// Returns a builder instance used to create file filter strings.
    /// </summary>
    /// <returns>A new <see cref="FileFilterBuilder"/> instance.</returns>
    public static FileFilterBuilder Create()
    {
        return new FileFilterBuilder();
    }

    /// <summary>
    /// Extracts file filter items from a file filter string.
    /// </summary>
    /// <param name="value">A file filter string.</param>
    /// <returns>
    /// An array containing the <see cref="FileFilterItem"/> instances
    /// represented by the specified file filter string.
    /// </returns>
    public static FileFilterItem[] ExtractItems(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Array.Empty<FileFilterItem>();
        }

        var items = new List<FileFilterItem>();

        var parts = value.Split(['|']);

        // If we have an odd number of parts, we want to ignore the last one.
        // Luckily just dividing by 2 and ignoring the remainder does exactly that.
        for (int i = 0; i <= parts.Length / 2; i += 2)
        {
            var descriptionPart = parts[i];
            var extensionsPart = parts[i + 1];
            if (FileFilterItem.TryCreateFromParts(descriptionPart, extensionsPart, out var item))
            {
                items.Add(item);
            }
        }

        return items.ToArray();
    }
}
