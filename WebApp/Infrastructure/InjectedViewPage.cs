using System.Web.Mvc;
using Ninject;
using WebApp4I.WebApp.Models;

namespace WebApp4I.WebApp.Infrastructure
{
    public class InjectedViewPage : WebViewPage
    {
        [Inject]
        public AppSettings AppSettings { get; set; }

        public override void Execute() { }
    }
}
