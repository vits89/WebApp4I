using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApp4I.Infrastructure
{
    public class MultipartContentAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutingAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            if (!actionContext.Request.Content.IsMimeMultipartContent())
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "The request content type not is multipart");
            }

            return Task.CompletedTask;
        }
    }
}
