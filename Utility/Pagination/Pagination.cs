using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Pagination {
    public class Pagination<T> {

        public IQueryable<T> ApplyPagination(IQueryable<T> query, PaginationOption option) {
            if(option.Limit > 0) {
                return query.Skip(option.Offset).Take(option.Limit);
            }
            else {
                return query;
            }
            
        }

    }
}
