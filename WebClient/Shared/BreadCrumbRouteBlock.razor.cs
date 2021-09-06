using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebClient.Models;
using WebClient.Routing;

namespace WebClient.Shared
{
    public partial class BreadCrumbRouteBlock
    {
        [Inject]
        public IRouteMatcher RouteMatcher { get; set; }

        private IEnumerable<BreadCrumbRoute> routes;
        public BreadCrumbRoute CurrentRoute { get; private set; }

        protected override void OnInitialized()
        {
            routes = new List<BreadCrumbRoute>
            {
                new BreadCrumbRoute
                {
                    Route = "/users",
                    Items = new List<BreadCrumbRouteItem>
                    {
                        new BreadCrumbRouteItem { Link = "/", Name = "Домой" },
                        new BreadCrumbRouteItem { Link = "/", Name = "Пользователи", Active = true }
                    }
                },
                new BreadCrumbRoute
                {
                    Route = "/users/{id}",
                    Items = new List<BreadCrumbRouteItem>
                    {
                        new BreadCrumbRouteItem { Link = "/", Name = "Домой" },
                        new BreadCrumbRouteItem { Link = "/users", Name = "Пользователи" },
                        new BreadCrumbRouteItem { Link = "/users/{id}", Name = "Пользователь", Active = true }
                    }
                }
            };
        }

        public void SetCurrentRoute(string relativePath)
        {
            CurrentRoute = routes.FirstOrDefault(
                br => RouteMatcher.TryMatch(
                    br.Route,
                    relativePath,
                    out var values));

            StateHasChanged();
        }
    }
}
