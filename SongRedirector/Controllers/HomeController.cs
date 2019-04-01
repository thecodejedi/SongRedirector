﻿using SongRedirector.Services;
using Microsoft.AspNetCore.Mvc;
using SongRedirector.Repository;
using System.Text;
using System.Linq;
using System;
using SongRedirector.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace SongRedirector.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILinkProvider linkProvider;
        private readonly ILinkRepository linkRepository;

        public HomeController(ILinkProvider linkProvider, ILinkRepository linkRepository)
        {
            this.linkProvider = linkProvider;
            this.linkRepository = linkRepository;
        }
        public IActionResult Index([FromRoute]string config)
        {
            var uri = linkProvider.GetLink(config);
            return Redirect(uri);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>().Error;
            var errorViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            var noValidConfigException = exception as NoValidConfigException;
            if (noValidConfigException != null)
            {
                errorViewModel.Message = noValidConfigException.ToString();
            }
            return View(errorViewModel);
        }
    }
}
