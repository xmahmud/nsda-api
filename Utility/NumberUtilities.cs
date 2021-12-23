using System;
using System.Collections.Generic;
using System.Text;

namespace Utility {
    public class NumberUtilities {
        public static long GetUniqueNumber() {
            var buffer = Guid.NewGuid().ToByteArray();
            return Math.Abs(BitConverter.ToInt32(buffer, 0));
        }
    }
}
