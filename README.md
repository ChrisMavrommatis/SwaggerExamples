# SwaggerExamples

This repository contains the code for the SwaggerExamples project. The project provides functionality for generating Swagger examples in C#.

SwaggerExamples is an essential resource for developers and API designers seeking to enrich their APIs. 
It offers the ability to add one or multiple examples requests and responses, enhancing your API's documentation and usability.


## Usage

To use the SwaggerExamples project, follow these steps:

1. Add the `SwaggerExamples` nuget package to your project.
2. In your startup class, use the `AddSwaggerExamples` extension method to configure the service collection:
```csharp
builder.Services.AddSwaggerExamples(options =>
{
	options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
	// ignore null
	options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
```

3. While Configuring `SwaggerGenOptions` user the `UseSwaggerExamples` extension method to configure swagger to be able to read the examples.
```csharp
builder.Services.Configure<SwaggerGenOptions>(options => 
{
	options.UseSwaggerExamples();
});
```

4. Build and run your project. The Swagger examples you make and provide will be generated and included in the Swagger documentation.

## Making Examples

In order to make an example you must create a class that inherits either from `SingleSwaggerExamplesProvider<T>` or from `MultipleSwaggerExamplesProvider<T>` where T is the model in the API.

If using the `SingleSwaggerExamplesProvider<T>` then you must implement the `GetExample` method that returns the API object
If you want to use the `MultipleSwaggerExamplesProvider<T>` then you must implement the `GetExamples` method that returns an `IEnumerable<ISwaggerExample<T>>`

### Single Example
```csharp
internal class CustomQueryRequestExample : SingleSwaggerExamplesProvider<CustomQueryRequest>
{
	public override CustomQueryRequest GetExample()
	{
		return new CustomQueryRequest
		{
			Bins = new List<Bin>
			{
				new () { ID = "custom_bin_1", Length = 10, Width = 40, Height = 60 },
				new () { ID = "custom_bin_2", Length = 20, Width = 40, Height = 60 },

			},
			Items = new List<Box>
			{
				new () { ID = "box_1", Quantity = 2, Length = 2, Width = 5, Height = 10 },
				new () { ID = "box_2", Quantity = 1, Length = 12, Width = 15, Height = 10 },
				new() { ID = "box_3", Quantity = 1, Length = 12, Width = 10, Height = 15 },
			}
		};
	}
}
```

### Multiple Example
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

In order for Swagger to display the custom examples you must indicate the example to be used on each method.
You can use the `SwaggerRequestExampleAttribute` to set the example for the request and the `SwaggerResponseExampleAttribute` for the response.
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
  ...
}
```


## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
