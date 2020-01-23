using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace PingPong.Blazor.Utils
{
    public static class NavigationManagerExtensions
    {
        public static int ReadQueryStringAsInt(this NavigationManager navigationManager, string parameter,
            int defaultValue)
        {
            var uri = new Uri(navigationManager.Uri);
            string strValue = QueryHelpers.ParseQuery(uri.Query).TryGetValue(parameter, out var results)
                ? results.First()
                : "";

            return int.TryParse(strValue, out var result) ? result : defaultValue;
        }
    }
}