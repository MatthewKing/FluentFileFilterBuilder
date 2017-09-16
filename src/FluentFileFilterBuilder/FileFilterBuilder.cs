using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentFileFilterBuilder
{
    /// <summary>
    /// Provides functionality to fluently build filter strings (such as those used
    /// by System.Windows.Forms.OpenFileDialog and System.Windows.Forms.SaveFileDialog).
    /// </summary>
    public sealed class FileFilterBuilder
    {
        /// <summary>
        /// A list of the <see cref="FileFilterItem"/> instances that make up the filter.
        /// </summary>
        private readonly IList<FileFilterItem> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileFilterBuilder"/> class.
        /// </summary>
        public FileFilterBuilder()
        {
            _items = new List<FileFilterItem>();
        }

        /// <summary>
        /// Add a filter.
        /// </summary>
        /// <param name="description">The filter description.</param>
        /// <param name="extensions">The extensions included in the filter.</param>
        /// <returns>The <see cref="FileFilterBuilder"/> instance.</returns>
        public FileFilterBuilder Add(string description, params string[] extensions)
        {
            _items.Add(new FileFilterItem(description, extensions));
            return this;
        }

        /// <summary>
        /// Returns a string representation of the filter.
        /// </summary>
        /// <returns>A string representation of the filter.</returns>
        public override string ToString()
        {
            return String.Join("|", _items.Select(x => x.ToString()));
        }

        /// <summary>
        /// Implicit conversion from <see cref="FileFilterBuilder"/> to string.
        /// </summary>
        /// <param name="builder">The <see cref="FileFilterBuilder"/> to convert.</param>
        /// <returns>A string representation of the filter.</returns>
        public static implicit operator string(FileFilterBuilder builder)
        {
            return builder.ToString();
        }
    }
}
