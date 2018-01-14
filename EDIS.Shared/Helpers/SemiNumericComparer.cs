using System;
using System.Collections.Generic;

namespace EDIS.Shared.Helpers
{
    public class SemiNumericComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            if (IsNumeric(s1) && IsNumeric(s2))
            {
                if (double.Parse(s1) > double.Parse(s2)) return 1;
                if (double.Parse(s1) < double.Parse(s2)) return -1;
                if (double.Parse(s1) == double.Parse(s2)) return 0;
            }

            if (IsNumeric(s1) && !IsNumeric(s2))
                return -1;

            if (!IsNumeric(s1) && IsNumeric(s2))
                return 1;

            return string.Compare(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsNumeric(object value)
        {
            try
            {
                var i = double.Parse(value.ToString());
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}