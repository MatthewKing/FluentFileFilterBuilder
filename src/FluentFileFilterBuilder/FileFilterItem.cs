using System;
using System.IO;
using System.Linq;

namespace FluentFileFilterBuilder
{
    /// <summary>
    /// Represents a single item in a filter.
    /// </summary>
    internal sealed class FileFilterItem
    {
        /// <summary>
        /// The filter description.
        /// </summary>
        private readonly string _description;

        /// <summary>
        /// The filter pattern.
        /// </summary>
        private readonly string _pattern;

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

            _description = description;
            _pattern = String.Join(";", extensions.Select(x => Path.ChangeExtension("*", x)));
        }

        /// <summary>
        /// Returns a string representation of the filter item.
        /// </summary>
        /// <returns>A string representation of the filter item.</returns>
        public override string ToString()
        {
            return $"{_description} ({_pattern})|{_pattern}";
        }
    }
}
