using Microsoft.AspNetCore.Http.Extensions;

namespace WebRifa.Blazor.Helpers;

public static class QueryStringBuilderHelper {

    public static string GenerateQueryString<T>(T model)
    {
        if (model is null)
            return string.Empty;

        var queryStringBuilder = new QueryBuilder();

        var properties = typeof(T).GetProperties();

        foreach (var property in properties) {
            var value = property.GetValue(model);

            if (value is not null) {
                queryStringBuilder.Add(
                    property.Name, value.ToString() ?? string.Empty);
            }
        }

        return queryStringBuilder.ToString();
    }
}
