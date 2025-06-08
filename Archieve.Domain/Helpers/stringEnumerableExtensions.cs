using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Domain.Helpers
{
    public static class stringEnumerableExtensions
    {
        public static IEnumerable<T> ToEnums<T> (this IEnumerable<string> strs) where T : struct, IConvertible
        {
            Type t = typeof(T);
            var ret = new List<T> ();

            if (t.IsEnum)
            {
                T outstr;
                foreach (var item in strs) { 
                    if(Enum.TryParse (item, out outstr))
                    {
                        ret.Add(outstr);
                    }
                }
            }

            return ret;
        }
    }
}
