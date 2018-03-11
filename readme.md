FluentFileFilterBuilder
===================

A lightweight fluent builder that easily creates file filter strings used in the various WinForms and WPF file dialogs.

### Filter strings overview

The various file dialogs in WinForms ([OpenFileDialog](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog) / [SaveFileDialog](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.savefiledialog)) and WPF ([OpenFileDialog](https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.openfiledialog) / [SaveFileDialog](https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.savefiledialog)) all use file filter strings to control how they filter files. These strings are set in the `Filter` property of the various dialogs.

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
