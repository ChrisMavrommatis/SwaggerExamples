# SwaggerExamples

Welcome to the `ChrisMavrommatis.SwaggerExamples` repository. This project provides a solution for generating Swagger examples in C#, aimed at enriching API documentation and enhancing usability for developers and API designers.

## Overview
The SwaggerExamples project enables you to add one or multiple example requests and responses to your API documentation. These examples help improve the clarity and effectiveness of your API's documentation.

## Installation
To integrate SwaggerExamples into your project, follow these steps:

**1. Add the NuGet package**
   ```bash
   dotnet add package ChrisMavrommatis.SwaggerExamples
   ```

**2. Configure the service collection**
 
   In your startup class, use the AddSwaggerExamples extension method to configure the service collection:
   ```csharp
   builder.Services.AddSwaggerExamples(options =>
   {
       options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
       options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
   });
   ```

**3. Configure Swagger:**

   While configuring SwaggerGenOptions, use the UseSwaggerExamples extension method to enable Swagger to read the examples:
   ```csharp
   builder.Services.Configure<SwaggerGenOptions>(options => 
   {
       options.UseSwaggerExamples();
   });
   ```

**4. Build and run your project**

The Swagger examples you create and assign to actions will be automatically included in the Swagger documentation.

## Creating Examples

To create an example, you must create a class that inherits from either `SingleSwaggerExamplesProvider<T>` or `MultipleSwaggerExamplesProvider<T>`, where `T` is the model used in the API.

### Single Example
For a single example, implement the GetExample method:

```csharp
internal class CustomQueryRequestExample : SingleSwaggerExamplesProvider<CustomQueryRequest>
{
    public override CustomQueryRequest GetExample()
    {
        return new CustomQueryRequest
        {
            Bins = new List<Bin>
            {
                new() { ID = "custom_bin_1", Length = 10, Width = 40, Height = 60 },
                new() { ID = "custom_bin_2", Length = 20, Width = 40, Height = 60 },
            },
            Items = new List<Box>
            {
                new() { ID = "box_1", Quantity = 2, Length = 2, Width = 5, Height = 10 },
                new() { ID = "box_2", Quantity = 1, Length = 12, Width = 15, Height = 10 },
                new() { ID = "box_3", Quantity = 1, Length = 12, Width = 10, Height = 15 },
            }
        };
    }
}
```

### Multiple Examples
For multiple examples, implement the GetExamples method:

```csharp
public class CustomQueryResponseExamples : MultipleSwaggerExamplesProvider<QueryResponse>
{
    public override IEnumerable<ISwaggerExample<QueryResponse>> GetExamples()
    {
        yield return SwaggerExample.Create("success", "Found Bin", "Response example when a bin is found", new QueryResponse
        {
            Result = ResultType.Success,
            Bin = new() { ID = "custom_bin_1", Length = 10, Width = 40, Height = 60 },
        });

        yield return SwaggerExample.Create("failure", "Bin not Found", "Response example when a bin is not found", new QueryResponse
        {
            Result = ResultType.Failure,
            Message = "Failed to find bin. Reason: ItemDimensionExceeded"
        });
    }
}
```

## Using the Examples
To display the custom examples in Swagger, annotate your methods with SwaggerRequestExampleAttribute for request examples and SwaggerResponseExampleAttribute for response examples:

```csharp
[HttpPost("by-custom")]
[Consumes("application/json")]
[Produces("application/json")]
[MapToApiVersion(v1.ApiVersion.Number)]
[SwaggerRequestExample(typeof(CustomQueryRequest), typeof(CustomQueryRequestExample))]
[ProducesResponseType(typeof(QueryResponse), StatusCodes.Status200OK)]
[SwaggerResponseExample(typeof(QueryResponse), typeof(CustomQueryResponseExamples), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
[SwaggerResponseExample(typeof(ErrorResponse), typeof(BadRequestErrorResponseExamples), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
[SwaggerResponseExample(typeof(ErrorResponse), typeof(ServerErrorResponseExample), StatusCodes.Status500InternalServerError)]
public override async Task<IActionResult> HandleAsync(CustomQueryRequestWithBody request, CancellationToken cancellationToken = default)
{
    // Your implementation here
}
```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
