using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace FluentFileFilterBuilder.Tests
{
    public class FileFilterItemTests
    {
        [Fact]
        public void Constructor_DescriptionIsNull_ThrowsArgumentNullException()
        {
            Action act = () => new FileFilterItem(null, new string[] { "ext" });

            act.ShouldThrow<ArgumentNullException>().WithMessage("Value cannot be null.\r\nParameter name: description");
        }

        [Fact]
        public void Constructor_DescriptionIsEmpty_ThrowsArgumentException()
        {
            Action act = () => new FileFilterItem(String.Empty, new string[] { "ext" });

            act.ShouldThrow<ArgumentException>().WithMessage("Value cannot be an empty string.\r\nParameter name: description");
        }

        [Fact]
        public void Constructor_ExtensionsIsNull_ThrowsArgumentNullException()
        {
            Action act = () => new FileFilterItem("description", null);

            act.ShouldThrow<ArgumentNullException>().WithMessage("Value cannot be null.\r\nParameter name: extensions");
        }

        [Fact]
        public void Constructor_ExtensionsIsEmpty_ThrowsArgumentException()
        {
            Action act = () => new FileFilterItem("description", Array.Empty<string>());

            act.ShouldThrow<ArgumentException>().WithMessage("At least one extension must be supplied.\r\nParameter name: extensions");
        }

        [Theory]
        [MemberData(nameof(TestCaseData))]
        public void ToString_ReturnsExpectedValues(object item, string expected)
        {
            ((FileFilterItem)item).ToString().Should().Be(expected);
        }

        private static IEnumerable<object[]> TestCaseData()
        {
            // Single extension.
            yield return new object[]
            {
                new FileFilterItem("All files", new string[] { "*" }),
                "All files (*.*)|*.*"
            };

            // Multiple extensions.
            yield return new object[]
            {
                new FileFilterItem("Image files", new string[] { "bmp", "jpg", "gif" }),
                "Image files (*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif"
            };

            // Dot in extension.
            yield return new object[]
            {
                new FileFilterItem("All files", new string[] { ".*" }),
                "All files (*.*)|*.*"
            };
        }
    }
}
