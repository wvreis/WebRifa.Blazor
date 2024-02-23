namespace WebRifa.Blazor.Helpers;

public static class StringHelper {
    public static string RemoveQuotes(this string str)
    {
        return str.Replace("\"", string.Empty);
    }
}
