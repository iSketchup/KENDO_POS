using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Main.Models;

public class UriConverter : JsonConverter<Uri>
{
    public override Uri Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if (value is null)
            throw new JsonException("Uri was null");

        return new Uri(value, UriKind.RelativeOrAbsolute);
    }

    public override void Write(
        Utf8JsonWriter writer,
        Uri value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

}