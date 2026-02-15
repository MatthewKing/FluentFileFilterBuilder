using FluentAssertions;
using Xunit;

namespace FluentFileFilterBuilder.Tests;

public class FileFilterTests
{
    [Fact]
    public void ExtractItems_Empty_ReturnsEmptyArray()
    {
        var filter = "";
        var items = FileFilter.ExtractItems(filter);
        items.Length.Should().Be(0);
    }

    [Fact]
    public void ExtractItems_Valid_ReturnsExpectedItems()
    {
        var filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        var items = FileFilter.ExtractItems(filter);

        items.Length.Should().Be(2);
        items[0].Description.Should().Be("Text files");
        items[0].Pattern.Should().Be("*.txt");
        items[1].Description.Should().Be("All files");
        items[1].Pattern.Should().Be("*.*");
    }

    [Fact]
    public void ExtractItems_MissingHalfAnItem_ReturnsOnlyTheValidItems()
    {
        var filter = "Text files (*.txt)|*.txt|All files (*.*)";
        var items = FileFilter.ExtractItems(filter);

        items.Length.Should().Be(1);
        items[0].Description.Should().Be("Text files");
        items[0].Pattern.Should().Be("*.txt");
    }

    [Fact]
    public void ExtractItems_InvalidValues_ReturnsEmptyArray()
    {
        var filter = "Invalid format||||";
        var items = FileFilter.ExtractItems(filter);
        items.Length.Should().Be(0);
    }
}
