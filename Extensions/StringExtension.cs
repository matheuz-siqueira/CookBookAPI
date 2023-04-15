using System.Globalization;

namespace cookbook_api.Extensions;

public static class StringExtension
{
    public static bool CompareNoCase(this string source, string search)
    {
        var index = CultureInfo.CurrentCulture.CompareInfo
        .IndexOf(source, search, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
        return index >= 0;
    }
}
