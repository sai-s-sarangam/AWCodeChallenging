using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingChallenge.UI
{
    public class AllowCrossSiteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:4200");

            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");

            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");

            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Methods", "*");

            if ((filterContext.HttpContext.Request.HttpMethod == "OPTIONS"))
                filterContext.HttpContext.Response.Flush();

            base.OnActionExecuting(filterContext);
        }
    }
}