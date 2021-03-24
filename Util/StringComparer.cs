using System;
namespace InventoryControl.Util
{
    [Flags]
    public enum StringComparisonArgs
    {
        None = 0,
        CaseSensitive = 1,
        ReplaceQuestionableLetters = 2,
        Trim = 4,
    }
    public static class StringComparer
    {
        public static string Normalized(this string str, StringComparisonArgs args)
        {
            if (!args.HasFlag(StringComparisonArgs.CaseSensitive))
            {
                str = str.ToLowerInvariant();
            }
            if (args.HasFlag(StringComparisonArgs.ReplaceQuestionableLetters))
            {
                str = str.Replace('¸', 'å').Replace('¨', 'Å');
            }
            if (args.HasFlag(StringComparisonArgs.Trim))
            {
                str = str.Trim();
            }
            return str;
        }
        public static string Normalized(this string str) =>
            Normalized(str, StringComparisonArgs.Trim | StringComparisonArgs.ReplaceQuestionableLetters);
        
        public static bool Includes(this string str, string other, StringComparisonArgs args)
        {
            if (args == StringComparisonArgs.None)
                return str == other;
            string x = Normalized(str, args);
            string y = Normalized(other, args);            
            return x.Contains(y) || string.IsNullOrEmpty(y);
        }
        public static bool Includes(this string str, string other)
        {
            string x = Normalized(str);
            string y = Normalized(other);
            return x.Contains(y) || string.IsNullOrEmpty(y);
        }
        
        public static int StartIndexOf(this string str, string other)
        {
            var x = str.Normalized();
            var y = other.Normalized();
            if (x == "" || y == "")
            {
                return -1;
            }
            return x.IndexOf(y);
        }
    }
}
