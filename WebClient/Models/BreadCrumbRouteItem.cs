using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class BreadCrumbRouteItem
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }

    public class BreadCrumbRoute
    {
        public string Route { get; set; }
        public IEnumerable<BreadCrumbRouteItem> Items { get; set; }
    }
}
