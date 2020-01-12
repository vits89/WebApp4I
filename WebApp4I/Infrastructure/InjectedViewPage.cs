using System.Web.Mvc;
using Ninject;
using WebApp4I.Models;

namespace WebApp4I.Infrastructure
{
    public class InjectedViewPage : WebViewPage
    {
        [Inject]
        public AppSettings AppSettings { get; set; }

        public override void Execute() { }
    }
}
