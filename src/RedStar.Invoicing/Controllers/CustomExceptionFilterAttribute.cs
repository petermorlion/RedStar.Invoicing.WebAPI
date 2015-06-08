using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Internal;

namespace RedStar.Invoicing.Controllers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new HttpStatusCodeResult(500);
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new HttpStatusCodeResult(500);
            return base.OnExceptionAsync(context);
        }
    }
}
