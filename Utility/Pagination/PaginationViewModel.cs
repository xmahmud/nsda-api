using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Pagination {
    public class PaginationViewModel<T> {

        public int Offset { get; set; }
        public int Limit { get; set; }
       // public int Size { get; set; }

        public IEnumerable<T> List { get; set; }

    }
}
