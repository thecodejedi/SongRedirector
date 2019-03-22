﻿using SongRedirector.Services;
using Microsoft.AspNetCore.Mvc;

namespace SongRedirector.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITenantConfigResolver tenantLinkProvider;

        public HomeController(ITenantConfigResolver tenantLinkProvider)
        {
            this.tenantLinkProvider = tenantLinkProvider;
        }
        public IActionResult Index([FromQuery] string tenant = "")
        {
            var linkProviderForRequest = tenantLinkProvider.Resolve(tenant);
            var uri = linkProviderForRequest.GetLink();
            return Redirect(uri);
        }

    }
}
