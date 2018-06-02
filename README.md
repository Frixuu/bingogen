# BingoGen
This project was created to test [new .NET Core feature - global tools](https://blogs.msdn.microsoft.com/dotnet/2018/02/27/announcing-net-core-2-1-preview-1/).
It shows how .NET handles dependencies and tools' resources, embedded or copied to output directory.

This simple app takes words from a .txt file, shuffles them, creates a 5x5 bingo card and saves it to PDF.
## Prerequsites
- .NET Core SDK 2.1.300 or newer
## Install from source
1. Clone this repo.
2. Create a package: ```dotnet pack --output ./```
3. Install package from local source: ```dotnet tool install -g bingogen --add-source ./```

Note: For RC1 use ```--source-feed``` instead of ```--add-source```.

You may also want to use ```"%cd%"``` (for cmd) or ```${pwd}``` (for PowerShell) instead of ```./```
## Usage
```bingogen [input file] [title] [output file]```

If input file isn't specified, BingoGen will use [its own word list](https://github.com/frixu/bingogen/blob/master/words.txt).
