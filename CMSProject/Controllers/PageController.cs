using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Business.Services.Abstract;
using CMSProject.Models;
using Domain.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMSProject.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        private readonly ILayoutService _layoutService;
        private readonly IMenuService _menuService;
        public PageController(IPageService pageService, ILayoutService layoutService, IMenuService menuService)
        {
            _pageService = pageService;
            _layoutService = layoutService;
            _menuService = menuService;
        }
        public IActionResult Index()
        {
            var model = _pageService.GetPages();
            return View(model);

        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = _layoutService.GetLayouts();
            ViewBag.menulist = _menuService.GetMenus();
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(string pageName)
        {
            using (ComponentContext context = new ComponentContext())
            {
                LayoutModel model = new LayoutModel();
                model.Layouts = context.Layouts.Where(x => x.IsDeleted == false).ToList();
                model.Pages = context.Pages.Include("PageContents").Where(z => z.Name == pageName).ToList();
                ViewBag.page = context.Pages.Include("PageContents").SingleOrDefault(x => x.Name == pageName);
                var page = context.Pages.FirstOrDefault(y => y.Name == pageName);
                ViewBag.layout = context.Layouts.Include("LayoutItems").SingleOrDefault(x => x.Id == page.LayoutId);

                return View(model);
            }

        }

        [HttpGet]
        public IActionResult Delete(string pageName)
        {
            using (ComponentContext ctx = new ComponentContext())
            {
                var page = ctx.Pages.Where(x => x.Name == pageName).FirstOrDefault();
                page.IsDeleted = true;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public JsonResult LayoutCagir(int cagrilacakLayout)
        {
            ComponentContext context = new ComponentContext();
            var bilgiler = context.LayoutItems.Where(x => x.LayoutId == cagrilacakLayout).Select(a => a.Class).ToList();
            return Json(bilgiler);
        }


        [HttpPost]
        public IActionResult Add(string Name, string[] txtArealar, int layoutId, int menuId, string[] Classes)
        {
            _pageService.InsertNewPage(Name, txtArealar, layoutId, menuId, Classes);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(string Name, List<string> txtArealar, int layoutNumarasi, int pageId, string oldPageName)
        {
            _pageService.UpdatePage(Name, txtArealar, layoutNumarasi, oldPageName);
            return RedirectToAction("Index");
        }
    }
}
