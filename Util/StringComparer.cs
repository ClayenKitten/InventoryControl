using System;
namespace InventoryControl.Util
{
    [Flags]
    public enum StringComparisonArgs
    {
        None = 0,
        CaseSensitive = 1,
        ReplaceQuestionableLetters = 2,
        RemoveSpaces = 4,
        Trim = 8,
        UnmatchIfOtherIsEmpty = 16,
    }
    public static class StringComparer
    {
        public static bool Includes(this string str, string other, StringComparisonArgs args)
        {
            if (args == StringComparisonArgs.None)
                return str == other;
            string x = str;
            string y = other;

            if (!args.HasFlag(StringComparisonArgs.CaseSensitive))
            {
                x = x.ToLowerInvariant();
                y = y.ToLowerInvariant();
            }
            if (args.HasFlag(StringComparisonArgs.ReplaceQuestionableLetters))
            {
                x = x.Replace('¸', 'å').Replace('¨', 'Å');
                y = y.Replace('¸', 'å').Replace('¨', 'Å');
            }
            if (args.HasFlag(StringComparisonArgs.RemoveSpaces))
            {
                x = x.Replace(" ", "");
                y = y.Replace(" ", "");
            }
            if (args.HasFlag(StringComparisonArgs.Trim))
            {
                x = x.Trim();
                y = y.Trim();
            }
            if (args.HasFlag(StringComparisonArgs.UnmatchIfOtherIsEmpty))
            {
                if(string.IsNullOrEmpty(y))
                {
                    return false;
                }
            }
            return x.Contains(y) || string.IsNullOrEmpty(y);
        }

        public static bool Includes(this string str, string other)
        {
            return Includes(str, other,
                StringComparisonArgs.Trim |
                StringComparisonArgs.ReplaceQuestionableLetters
            );
        }
    }
}
