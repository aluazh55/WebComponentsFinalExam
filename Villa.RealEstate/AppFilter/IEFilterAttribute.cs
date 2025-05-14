using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Villa.RealEstate.AppFilter
{
    public class IEFilterAttribute : Attribute, IResourceFilter
    {
        // после завершения этапа stage
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new NotImplementedException();
        }
        //перед этапом Stage
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string userAgent = context.HttpContext.Request
                .Headers["User-Agent"].ToString();
            if (userAgent.Contains("sadsfgsvas"))
           // if (userAgent.Contains("Edg/"))
            {
                context.Result = new ContentResult
                {
                    Content = "Ваш браузер устарел"
                };
            }
        }
    }
}
