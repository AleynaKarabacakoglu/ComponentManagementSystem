using Business.Services.Abstract;
using Common.Dtos;
using Domain.Concrete;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services.Concrete
{
    public class MenuService : IMenuService
    {
        public IEnumerable<MenuDto> GetMenus()
        {
            IEnumerable<MenuDto> list;
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                 list= _repo.SetContext<Menu>().Where(m => !m.IsDeleted).Select(m => new MenuDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    UpdatedAt = m.UpdatedAt,
                    ParentId = m.ParentId,
                    Pages = m.Page.Select(x => new PageDto()
                    {
                        Name = x.Name,
                        LayoutId = x.LayoutId
                    })
                }).ToList();
                
            }
            return list;
        }

        public string GetMenuRecursion()
        {
            using (BaseRepository<Menu> repo = new BaseRepository<Menu>())
            {
                var all = repo.SetContext<Menu>().Where(x => !x.IsDeleted).ToList();
                var pageTum = repo.SetContext<Page>().Where(k => !k.IsDeleted).ToList();
                var strBuilder = new StringBuilder();
                var parentItems = all.Where(x => x.ParentId == null).ToList();

                foreach (var parentcat in parentItems)
                {
                    var childItems = all.Where(x => x.ParentId == parentcat.Id);
                    if (childItems.Count() > 0)
                    {
                        var page = repo.SetContext<Page>().Where(k => k.MenuId == parentcat.Id).FirstOrDefault();
                        strBuilder.Append("<li class='dropdown' ><a href='Home/Preview/" + parentcat.Id + "'>" +
                                          page.Name + "</a>");
                        AddChildItem(parentcat, strBuilder, all, pageTum);
                        strBuilder.Append("</li>");
                    }
                    else
                    {
                        var page = repo.SetContext<Page>().Where(k => k.MenuId == parentcat.Id).FirstOrDefault();
                        strBuilder.Append("<li><a href='Home/Preview/" + parentcat.Id + "'>" + page.Name + "</a>" +
                                          "</li>");
                    }
                }

                return strBuilder.ToString();
            }
        }

        public void AddChildItem(Menu childItem, StringBuilder strBuilder, List<Menu> MenuTum, List<Page> pageTum)
        {
            using (BaseRepository<Menu> repo = new BaseRepository<Menu>())
            {
                strBuilder.Append("<ul>");
                var childItems = MenuTum.Where(x => x.Id == childItem.Id);
                foreach (Menu cItem in childItems)
                {
                    var subChilds = MenuTum.Where(x => x.Id == cItem.Id);
                    if (subChilds.Count() > 0)
                    {
                        var page = pageTum.Where(k => k.MenuId == cItem.Id).FirstOrDefault();
                        strBuilder.Append("<li class='dropdown-menu'><a href='Home/Preview/" + cItem.Id + "'>" +
                                          page.Name + "</a>");
                        AddChildItem(cItem, strBuilder, MenuTum, pageTum);
                        strBuilder.Append("</li>");
                    }
                    else
                    {
                        var page = repo.SetContext<Page>().Where(k => k.MenuId == cItem.Id).FirstOrDefault();
                        strBuilder.Append("<li><a href='Home/Preview/" + cItem.Id + "'>" + page.Name + "</a></li>");
                    }
                }

                strBuilder.Append("</ul>");
            }
        }

        public MenuDto GetMenuByName(string Name)
        {
            MenuDto menu;
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                menu= _repo.SetContext<Menu>().Where(p => p.Name == Name).Select(p => new MenuDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ParentId = p.ParentId,
                    Icon = p.Icon
                }).FirstOrDefault();
            }
            return menu;
        }

        public void InsertNewMenu(string Name, int? ParentId, string icon)
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                Menu menu = new Menu
                {
                    Name = Name,
                    Icon = icon,
                    ParentId = ParentId
                };
                _repo.Add(menu);
            }
        }

        public void UpdateMenu(string Name, int? ParentId, string icon, string oldName)
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                //Layout Güncelleme İşlemi
                var menuBilgiler = _repo.SetContext<Menu>().FirstOrDefault(c => c.Name == oldName);
                menuBilgiler.Name = Name;
                menuBilgiler.ParentId = ParentId;
                menuBilgiler.Icon = icon;
                menuBilgiler.UpdatedAt = DateTime.Now;
                _repo.Update(menuBilgiler);
            }
        }

        public void DeleteMenu(string Name)
        {
            using (BaseRepository<Menu> repo = new BaseRepository<Menu>())
            {
                Menu menu = repo.SetContext<Menu>().Where(c => c.Name == Name).FirstOrDefault();
                repo.Delete(menu);
            }
        }
    }

}
