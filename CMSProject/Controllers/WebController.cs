using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services.Abstract;
using Common.Dtos;
using Domain.Context;
using Microsoft.AspNetCore.Mvc;

namespace CMSProject.Controllers
{
    public class WebController : Controller
    {
        private readonly IPageService _pageService;
        private readonly ILayoutService _layoutService;
        private readonly IMenuService _menuService;
        public WebController(IPageService pageService, ILayoutService layoutService, IMenuService menuService)
        {
            _pageService = pageService;
            _layoutService = layoutService;
            _menuService = menuService;
        }
        public IActionResult Index(int pageId)
        {
           List<PageContentDto> model= _pageService.GetPageById(pageId);
            int menuıd = _pageService.getMenuId(pageId);
            using (ComponentContext context = new ComponentContext())
            {
                ViewBag.menu = context.Menus.Where(x => x.Id == menuıd).FirstOrDefault();
            }
            return View(model);
        }
        //public IActionResult Website()
        //{
        //    IEnumerable<PageDto> model = _pageService.GetPages();
        //    int menuıd = _pageService.getMenuId(pageId);
        //    using (ComponentContext context = new ComponentContext())
        //    {
        //        ViewBag.menu = context.Menus.Where(x => x.Id == menuıd).FirstOrDefault();
        //    }
        //    return View(model);
        //}
    }
}
