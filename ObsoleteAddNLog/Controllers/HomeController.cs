namespace ObsoleteAddNLog.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        // GET: Home
        public ActionResult Index()
        {
            this.logger.LogInformation("The home page was requested.");
            return Content("Foobar");
        }
    }
}