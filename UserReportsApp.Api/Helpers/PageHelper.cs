using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserReportsApp.Api.Helpers
{
    public static class PageHelper
    {
        public static int GetOffset(int page, int pageSize)
        {
            return (page - 1) * pageSize;
        }
    }
}
