
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tennis.Controllers
{
    internal class CustomFilterExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
        }
    }
}