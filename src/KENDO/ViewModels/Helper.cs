using System;
using System.IO;
using Avalonia.Platform;

namespace Main.ViewModels;

public static class Helper
{
    public static Stream OpenImageStream(Uri uri)
    {
        if (uri.Scheme == "avares")
            return AssetLoader.Open(uri);

        if (uri.IsFile)
            return File.OpenRead(uri.LocalPath);

        throw new NotSupportedException($"Unsupported image uri: {uri}");
    }
}