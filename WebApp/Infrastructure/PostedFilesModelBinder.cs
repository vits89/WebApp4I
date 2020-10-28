using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using WebApp4I.WebApp.Models;

namespace WebApp4I.WebApp.Infrastructure
{
    public class PostedFilesModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (!actionContext.Request.Content.IsMimeMultipartContent())
            {
                return false;
            }

            var streamProvider = actionContext.Request.Content.ReadAsMultipartAsync().Result;

            var postedFiles = new List<PostedFileModel> { Capacity = streamProvider.Contents.Count };

            foreach (var content in streamProvider.Contents)
            {
                var fileName = content.Headers.ContentDisposition.FileName.Trim('\"');
                var fileContent = content.ReadAsByteArrayAsync().Result;

                postedFiles.Add(new PostedFileModel
                {
                    Name = fileName,
                    Content = fileContent
                });
            }

            bindingContext.Model = postedFiles;

            return true;
        }
    }
}
