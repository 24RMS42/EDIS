using System.Collections.Generic;

namespace EDIS.Shared.Extensions
{
    public static class ListExtensions
    {
        public static List<string> RemoveNulls(this List<string> list)
        {
            var newList = new List<string>();

            foreach (var item in list)
            {
                newList.Add(item ?? "");
            }

            return newList;
        }
    }
}