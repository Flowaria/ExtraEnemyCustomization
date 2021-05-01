using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Extensions
{
    public static class StringExtension
    {
        public static bool EqualsAnyIgnoreCase(this string input, params string[] args)
        {
            return EqualsAny(input, true, args);
        }

        public static bool EqualsAny(this string input, bool ignoreCase, params string[] args)
        {
            var comparisonMode = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            foreach(var arg in args)
            {
                if (input.Equals(arg, comparisonMode))
                    return true;
            }
            return false;
        }
    }
}
