﻿using System;
using System.Collections.Generic;
using System.Linq;
using Business.Services.Abstract;
using Domain.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CMSProject.Controllers
{
    public class LayoutController : Controller
    {

        private readonly ILayoutService _layoutService;
        private readonly ILogger<LayoutController> _logger;

        public LayoutController(ILayoutService layoutService, ILogger<LayoutController> logger)
        {
            _layoutService = layoutService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = _layoutService.GetLayouts();
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string Name, List<string> kolonlar)
        {
            _layoutService.InsertNewLayout(Name, kolonlar);
            return RedirectToAction("Index");
        }

        public IActionResult Update(string layoutName)
        {
            using (ComponentContext context = new ComponentContext())
            {
                
                ViewBag.layout = context.Layouts.Include("LayoutItems").SingleOrDefault(x => x.Name == layoutName);
                //ViewBag.layout = _layoutService.GetLayoutsForUpdate(layoutName);
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(string oldLayout, string Name, List<string> kolonlar)
        {
            _layoutService.UpdateLayout(oldLayout, Name, kolonlar);


            return RedirectToAction("Index");
        }

        public IActionResult Delete(string layoutName)
        {
            _layoutService.DeleteLayout(layoutName);
            return RedirectToAction("Index");
        }
    }
}
