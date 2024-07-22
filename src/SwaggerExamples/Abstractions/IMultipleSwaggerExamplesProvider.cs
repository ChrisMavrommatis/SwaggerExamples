using ChrisMavrommatis.SwaggerExamples.Abstractions;

namespace ChrisMavrommatis.SwaggerExamples;

internal interface IMultipleSwaggerExamplesProvider
{
    IEnumerable<ISwaggerExample> GetExamples();
}

internal interface IMultipleSwaggerExamplesProvider<T> : IMultipleSwaggerExamplesProvider
    where T : class
{
    IEnumerable<ISwaggerExample<T>> GetExamples();
}