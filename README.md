# SwaggerExamples

This repository contains the code for the SwaggerExamples project. The project provides functionality for generating Swagger examples in C#.

SwaggerExamples is an essential resource for developers and API designers seeking to enrich their APIs. 
It offers the ability to add one or multiple examples requests and responses, enhancing your API's documentation and usability.


## Usage

To use the SwaggerExamples project, follow these steps:


1. Add the `SwaggerExamples` nuget package to your project.
2. In your startup class, add the following code to configure the service collection:


```csharp

builder.Services.AddSwaggerExamples(options =>
{
	options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
	// ignore null
	options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

```

3. While Configuring SwaggerGenOptions Add UseSwaggerExamples method to configure the swagger examples.
```csharp
builder.Services.Configure<SwaggerGenOptions>(options => 
{
	options.UseSwaggerExamples();
});

```

4. Build and run your project. The Swagger examples will be automatically generated and included in the Swagger documentation.

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
