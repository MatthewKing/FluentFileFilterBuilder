using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace FluentFileFilterBuilder.Tests;

public class FileFilterItemTests
{
    [Fact]
    public void Constructor_DescriptionIsNull_ThrowsArgumentNullException()
    {
        Action act = () => new FileFilterItem(null, new string[] { "ext" });

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_DescriptionIsEmpty_ThrowsArgumentException()
    {
        Action act = () => new FileFilterItem(String.Empty, new string[] { "ext" });

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_ExtensionsIsNull_ThrowsArgumentNullException()
    {
        Action act = () => new FileFilterItem("description", null);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_ExtensionsIsEmpty_ThrowsArgumentException()
    {
        Action act = () => new FileFilterItem("description", Array.Empty<string>());

        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [MemberData(nameof(TestCaseData))]
    public void ToString_ReturnsExpectedValues(object item, string expected)
    {
        ((FileFilterItem)item).ToString().Should().Be(expected);
    }

    public static IEnumerable<object[]> TestCaseData()
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

    [Fact]
    public void TryParse_InvalidFormat_ReturnsFalse()
    {
        var input = "Invalid format";
        var result = FileFilterItem.TryParse(input, out var item);
        result.Should().BeFalse();
        item.Should().BeNull();
    }

    [Fact]
    public void TryParse_ValidFormat_ReturnsExpectedValues()
    {
        var input = "Image files (*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif";

        var result = FileFilterItem.TryParse(input, out var item);

        result.Should().BeTrue();
        item.Should().NotBeNull();
        item.Description.Should().Be("Image files");
        item.Pattern.Should().Be("*.bmp;*.jpg;*.gif");
    }

    [Fact]
    public void TryParse_ValidFormatWithNoParentheses_ReturnsExpectedValues()
    {
        var input = "Image files|*.bmp;*.jpg;*.gif";

        var result = FileFilterItem.TryParse(input, out var item);

        result.Should().BeTrue();
        item.Should().NotBeNull();
        item.Description.Should().Be("Image files");
        item.Pattern.Should().Be("*.bmp;*.jpg;*.gif");
    }
}
