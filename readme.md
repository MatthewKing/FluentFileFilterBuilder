FluentFileFilterBuilder
===================

A lightweight fluent builder that easily creates file filter strings such as those used in [OpenFileDialog](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog) and [SaveFileDialog](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.savefiledialog).

### Filter strings overview

WinForms file dialogs such as [OpenFileDialog](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog) and [SaveFileDialog](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.savefiledialog) use file filter strings to control how they filter files.

File filter strings are described as follows:

> For each filtering option, the filter string contains a description of the filter, followed by the vertical bar (|) and the filter pattern. The strings for different filtering options are separated by the vertical bar.

Here are some example file filter strings:

`Text files (*.txt)|*.txt|All files (*.*)|*.*`

`Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*`

### Installation

`PM> Install-Package FluentFileFilterBuilder`

### Usage

The first example filter string described above can be created as so:

```csharp
var filter = new FileFilterBuilder()
    .Add("Text files", "txt")
    .Add("All files", "*")
    .ToString();
```

The second example filter string described above can be created as so:

```csharp
var filter = new FileFilterBuilder()
    .Add("Image files", "BMP", "JPG", "GIF")
    .Add("All files", "*")
    .ToString();
```

### License

FluentFileFilterBuilder is distributed under the MIT license.
