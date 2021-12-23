using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EmailParser
    {
        public static string Parse(string template, Dictionary<string, string> model)
        {
            var result = template;

            foreach(var m in model)
            {
                var c = "@Model." + m.Key;
                result = result.Replace(c, m.Value);
            }

            return result;
        }
    }
}
