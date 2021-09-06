using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;

namespace WebClient.Routing
{
    public class RouteMatcher:IRouteMatcher
    {
        public bool TryMatch(string routeTemplate, string requestPath, out RouteValueDictionary routeValues)
        {
            routeValues = new RouteValueDictionary();

            var template = TemplateParser.Parse(routeTemplate);

            var matcher = new TemplateMatcher(template, GetDefaults(template));

            return matcher.TryMatch(requestPath, routeValues);
        }

        public RouteValueDictionary GetDefaults(RouteTemplate routeTemplate)
        {
            var routeValueDictionary = new RouteValueDictionary();

            foreach (var parameter in routeTemplate.Parameters)
            {
                if (parameter.DefaultValue != null)
                {
                    routeValueDictionary.Add(parameter.Name, parameter.DefaultValue);
                }
            }

            return routeValueDictionary;
        }
    }

    public interface IRouteMatcher
    {
        /// <summary>
        /// Tries match request path in route template
        /// </summary>
        /// <param name="routeTemplate">Route template</param>
        /// <param name="requestPath">Target request path</param>
        /// <param name="routeValues">Route values</param>
        /// <returns>Request path is matched</returns>
        bool TryMatch(string routeTemplate, string requestPath, out RouteValueDictionary routeValues);

        /// <summary>
        /// Extracts the default argument values from the template
        /// </summary>
        /// <param name="routeTemplate">Route template</param>
        /// <returns>Route values</returns>
        RouteValueDictionary GetDefaults(RouteTemplate routeTemplate);
    }
}
