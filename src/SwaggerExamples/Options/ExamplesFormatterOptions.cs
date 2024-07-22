using System.Text.Json;

namespace ChrisMavrommatis.SwaggerExamples.Options;

public class ExamplesFormatterOptions
{
    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new();
}