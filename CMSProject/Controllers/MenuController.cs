using System;
using System.Collections.Generic;
using Business.Services.Abstract;
using Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CMSProject.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly ILogger<MenuController> _logger;

        public MenuController(IMenuService menuService, ILogger<MenuController> logger)
        {
            _menuService = menuService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<MenuDto> model = _menuService.GetMenus();
            //bool response = model.Any(x => x.ParentId == null);
            return View(model);
        }
      

        public IActionResult Add()
        {
            var model = _menuService.GetMenus();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(string Name, int? parentId, string icon)
        {
            _menuService.InsertNewMenu(Name, parentId, icon);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string MenuName)
        {
            _menuService.DeleteMenu(MenuName);
            return RedirectToAction("Index");
        }

        public IActionResult Update(string MenuName)
        {
            MenuDto bilgiler = _menuService.GetMenuByName(MenuName);
            if (bilgiler.Name != null)
            {
                ViewBag.Name = bilgiler.Name;
            }

            if (bilgiler.ParentId != null)
            {
                ViewBag.ParentId = bilgiler.ParentId;
            }

            if (bilgiler.Icon != null)
            {
                ViewBag.Icon = bilgiler.Icon;
            }

            var model = _menuService.GetMenus();

            return View(model);
        }


        [HttpPost]
        public IActionResult Update(string Name, int? parentId, string icon, string oldName)
        {
            _menuService.UpdateMenu(Name, parentId, icon, oldName);
            return RedirectToAction("Index");
        }
    }
}
    