using System.Text.RegularExpressions;

namespace Weather.Services.ProductApi.Helpers;

public static class StringHelpers
{
    static readonly Regex trimmer = new Regex(@"\s\s+");
    public static string RemoveExtraSpaces(this string str)
    {
        return trimmer.Replace(str, " ");
    }
}
