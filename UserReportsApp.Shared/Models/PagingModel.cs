using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserReportsApp.Shared.Models
{
    public class PagingModel<T>
    {
        public int Count { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
