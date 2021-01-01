using System;
using System.Text.RegularExpressions;

namespace WeeklyXamarin.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string CleanCacheKey(this string uri)
        {
            string pattern = "[\\~#%&*{}/:<>?|\"-]";
            string replacement = " ";

            Regex regEx = new Regex(pattern);
            string sanitized = Regex.Replace(regEx.Replace(uri, replacement), @"\s+", "_");

            return sanitized;
        }

    }
}
