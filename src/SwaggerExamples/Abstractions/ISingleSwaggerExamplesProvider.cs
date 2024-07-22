namespace ChrisMavrommatis.SwaggerExamples;

internal interface ISingleSwaggerExamplesProvider
{
    object GetExample();
}

internal interface ISingleSwaggerExamplesProvider<TModel> : ISingleSwaggerExamplesProvider
    where TModel : class
{
    TModel GetExample();
}

