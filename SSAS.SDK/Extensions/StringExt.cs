using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAS.SDK.Extensions {
    internal static class StringExt {
        public static string StripBrackets(this string str) {
            return str.Replace("[", String.Empty).Replace("]", String.Empty);
        }
    }
}
