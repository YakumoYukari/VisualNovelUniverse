using System.Text.RegularExpressions;

namespace Visual_Novel_Universe
{
    public static class WebUtils
    {
        public static bool IsVndbPage(string Url)
        {
            return Regex.IsMatch(Url, @"^.*?vndb.org/v\d+$");
        }
    }
}
