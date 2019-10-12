using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace FluentFileFilterBuilder.Tests
{
    public class FileFilterBuilderTests
    {
        [Fact]
        public void Add_DescriptionIsNull_ThrowsArgumentNullException()
        {
            FileFilterBuilder builder = new FileFilterBuilder();
            Action act = () => builder.Add(null, "ext");

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Add_DescriptionIsEmpty_ThrowsArgumentException()
        {
            FileFilterBuilder builder = new FileFilterBuilder();
            Action act = () => builder.Add(String.Empty, "ext");

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Add_ExtensionsIsNull_ThrowsArgumentNullException()
        {
            FileFilterBuilder builder = new FileFilterBuilder();
            Action act = () => builder.Add("description", null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Add_ExtensionsIsEmpty_ThrowsArgumentException()
        {
            FileFilterBuilder builder = new FileFilterBuilder();
            Action act = () => builder.Add("ext", Array.Empty<string>());

            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [MemberData(nameof(TestCaseData))]
        public void ToString_ReturnsExpectedValues(FileFilterBuilder builder, string expected)
        {
            builder.ToString().Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(TestCaseData))]
        public void ImplicitStringOperator_ReturnsExpectedValues(FileFilterBuilder builder, string expected)
        {
            string s = builder;
            s.Should().Be(expected);
        }

        public static IEnumerable<object[]> TestCaseData()
        {
            // Single filter, single extension.
            yield return new object[]
            {
                new FileFilterBuilder().Add("All files", "*"),
                "All files (*.*)|*.*"
            };

            // Single filter, multiple extensions.
            yield return new object[]
            {
                new FileFilterBuilder().Add("Image files", "bmp", "jpg", "gif"),
                "Image files (*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif"
            };

            // Single filter, single extension, dot in extension.
            yield return new object[]
            {
                new FileFilterBuilder().Add("All files", ".*"),
                "All files (*.*)|*.*"
            };

            // Multiple filters, single extension.
            yield return new object[]
            {
                new FileFilterBuilder().Add("Text files", "txt").Add("All files", "*"),
                "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            // Multiple filters, multiple extensions
            yield return new object[]
            {
                new FileFilterBuilder().Add("Image files", "bmp", "jpg", "gif").Add("All files", "*"),
                "Image files (*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif|All files (*.*)|*.*"
            };
        }
    }
}
