using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Villa.RealEstate.AppFilter
{
    public class TimeElapsedFilter : Attribute, IActionFilter
    {
        private Stopwatch timer;
        // после Stage
        public void OnActionExecuted(ActionExecutedContext context)
        {
            timer.Stop();
            string result = "Time elapsed:" + timer.Elapsed.TotalMilliseconds;
        }
        //вызывается перед этапом Stage
        public void OnActionExecuting(ActionExecutingContext context)
        {
            timer = Stopwatch.StartNew();
        }
    }
}
