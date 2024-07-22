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



## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
