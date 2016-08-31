using System;
using System.Web.Mvc;

namespace WebUI.Helpers
{
    public static class UrlCurrentControllerActionExtensions
    {
        public static string GetController(this UrlHelper url)
        {
            return url.RequestContext.RouteData.Values["controller"].ToString();
        }

        public static string GetAction(this UrlHelper url)
        {
            return url.RequestContext.RouteData.Values["action"].ToString();
        }

        public static string GetArea(this UrlHelper url)
        {
            if (url.RequestContext.RouteData.DataTokens.ContainsKey("area"))
            {
                return url.RequestContext.RouteData.DataTokens["area"].ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        public static bool IsController(this UrlHelper url, string controllerName, string areaName = "")
        {
            return controllerName.Equals(GetController(url), StringComparison.InvariantCultureIgnoreCase) && IsArea(url, areaName);
        }

        public static bool IsAction(this UrlHelper url, string actionName)
        {
            return actionName.Equals(GetAction(url), StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsArea(this UrlHelper url, string areaName)
        {
            return areaName.Equals(GetArea(url), StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsControllerAction(this UrlHelper url, string controllerName, string actionName, string areaName = "") // We can't use String.Empty here: http://stackoverflow.com/questions/507923/why-isnt-string-empty-a-constant
        {
            return IsController(url, controllerName, areaName) && IsAction(url, actionName) && IsArea(url, areaName);
        }
    }
}