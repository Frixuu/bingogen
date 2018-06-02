# BingoGen
This project was created to test [new .NET Core feature - global tools](https://blogs.msdn.microsoft.com/dotnet/2018/02/27/announcing-net-core-2-1-preview-1/).
It shows how .NET handles dependencies and tools' resources, embedded or copied to output directory.

This simple app takes words from a .txt file, shuffles them, creates a 5x5 bingo card and saves it to PDF.
## Prerequisites
- .NET Core SDK 2.1.300 or newer
- Make sure your PATH includes the tools' directory (```$HOME/.dotnet/tools``` on Linux/macOS or ```%USERPROFILE%\.dotnet\tools``` on Windows)
## Install from NuGet (recommended)
```dotnet tool install -g bingogen```
## Install from source
1. Clone this repo.
2. Create a package: ```dotnet pack --output ./```
3. Install package from local source: ```dotnet tool install -g bingogen --add-source "$PWD"```

Note: 2.1 RC1 doesn't understand ```--add-source```. Use ```--source-feed``` instead.

If you're on Windows, you may also want to use ```"%cd%"``` (for cmd) or ```${pwd}``` (for PowerShell) instead of ```"$PWD"```.
## Usage
```bingogen [input file] [title] [output file]```

If input file isn't specified, BingoGen will use [its own word list](https://github.com/frixu/bingogen/blob/master/words.txt).
