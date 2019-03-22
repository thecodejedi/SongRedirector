﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SongRedirector.Services;
using Microsoft.AspNetCore.Mvc;

namespace SongRedirector.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILinkProvider linkProvider;

        private readonly ITenantLinkProvider tenantLinkProvider;

        public HomeController(ILinkProvider linkProvider, ITenantLinkProvider tenantLinkProvider)
        {
            this.linkProvider = linkProvider;
            this.tenantLinkProvider = tenantLinkProvider;
        }
        public IActionResult Index([FromQuery] string tenant = "")
        {

            ILinkProvider linkProviderForRequest = linkProvider;
            if (!string.IsNullOrWhiteSpace(tenant))
            {
                linkProviderForRequest = tenantLinkProvider.Resolve(tenant);
            }

            var uri = linkProviderForRequest.GetLink();
            return Redirect(uri);


        }

    }
}