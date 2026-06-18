using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Main.Models;


// Alles mit ChatGPT 5.5 / Claude Sonnet 4.6

public sealed class ImageUriBase64Converter : JsonConverter<Uri>
{
    private readonly string _outputDirectory;

    public ImageUriBase64Converter()
    {
        _outputDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "MyApp",
            "Textures"
        );

        Directory.CreateDirectory(_outputDirectory);
    }

    public override Uri Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException("Expected image as Base64 string.");

        var value = reader.GetString();

        if (string.IsNullOrWhiteSpace(value))
            throw new JsonException("Image Base64 string was null or empty.");

        var parsed = ParseBase64Image(value);

        byte[] imageBytes;

        try
        {
            imageBytes = Convert.FromBase64String(parsed.Base64);
        }
        catch (FormatException ex)
        {
            throw new JsonException("Invalid Base64 image string.", ex);
        }

        var fileName = $"{Guid.NewGuid():N}{parsed.Extension}";
        var filePath = Path.Combine(_outputDirectory, fileName);

        File.WriteAllBytes(filePath, imageBytes);

        return new Uri(filePath, UriKind.Absolute);
    }

    public override void Write(
        Utf8JsonWriter writer,
        Uri value,
        JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        string filePath;

        if (value.IsFile)
        {
            filePath = value.LocalPath;
        }
        else
        {
            filePath = value.OriginalString;
        }

        if (!File.Exists(filePath))
            throw new JsonException($"Image file does not exist: {filePath}");

        var imageBytes = File.ReadAllBytes(filePath);
        var base64 = Convert.ToBase64String(imageBytes);
        var mimeType = GetMimeType(filePath);

        writer.WriteStringValue($"data:{mimeType};base64,{base64}");
    }

    private static (string Base64, string Extension) ParseBase64Image(string value)
    {
        if (!value.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            return (value, ".bin");

        var commaIndex = value.IndexOf(',');

        if (commaIndex < 0)
            throw new JsonException("Invalid data URI.");

        var metadata = value[..commaIndex];
        var base64 = value[(commaIndex + 1)..];

        var extension = ".bin";

        if (metadata.Contains("image/png", StringComparison.OrdinalIgnoreCase))
            extension = ".png";
        else if (metadata.Contains("image/jpeg", StringComparison.OrdinalIgnoreCase))
            extension = ".jpg";
        else if (metadata.Contains("image/gif", StringComparison.OrdinalIgnoreCase))
            extension = ".gif";
        else if (metadata.Contains("image/webp", StringComparison.OrdinalIgnoreCase))
            extension = ".webp";
        else if (metadata.Contains("image/bmp", StringComparison.OrdinalIgnoreCase))
            extension = ".bmp";
        else if (metadata.Contains("image/svg+xml", StringComparison.OrdinalIgnoreCase))
            extension = ".svg";

        return (base64, extension);
    }

    private static string GetMimeType(string path)
    {
        var extension = Path.GetExtension(path).ToLowerInvariant();

        return extension switch
        {
            ".png" => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".bmp" => "image/bmp",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream"
        };
    }
}