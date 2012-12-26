using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HWIX.Extensions {
    public static class StringExt {
        public static string TrimBreakLines(this string value) {
            return value.Replace("<br />", "").Replace("<br >", "").Replace("<br/>", "").Replace("<br>", "");
        }

        public static bool StartsWith(this string str, string value, bool ignoreCase) {
            return ignoreCase ? str.ToLower().StartsWith(value.ToLower()) : str.StartsWith(value);
        }

        public static bool Contains(this string str, string value, bool ignoreCase) {
            return ignoreCase ? str.ToLower().Contains(value.ToLower()) : str.Contains(value);
        }
    }
}