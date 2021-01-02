using Business.Services.Abstract;
using Common.Dtos;
using Domain.Concrete;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.Services.Concrete
{
    public class PageService : IPageService
    {
        public IEnumerable<PageDto> GetPages()
        {
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {
                return _repo.SetContext<Page>().Where(x => x.IsDeleted == false).Select(p => new PageDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    LayoutName = p.Layout.Name,
                }).ToList();
            }
        }

        public PageDto GetPageByName(string Name)
        {
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {
                return _repo.SetContext<Page>().Where(x => x.Name == Name).Select(p => new PageDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    LayoutName = p.Layout.Name,
                    PageContents = p.PageContents.Select(x => new PageContentDto
                    {
                        Id = x.Id,
                        Class = x.Class,
                        PageId = x.PageId,
                        Content = x.Content
                    })
                }).FirstOrDefault();
            }
        }

        public List<PageContentDto> GetPageById(int id)
        {
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {
                var bilgiler = _repo.SetContext<Page>().Where(c => c.Id == id).FirstOrDefault();

                using (BaseRepository<PageContent> repository = new BaseRepository<PageContent>())
                {
                    List<PageContentDto> icerik = new List<PageContentDto>();
                    var veriler = repository.SetContext<PageContent>().Where(x => x.PageId == bilgiler.Id).ToList();

                    foreach (var pageContent in veriler)
                    {
                        PageContentDto dto = new PageContentDto
                        {
                            Id = pageContent.Id,
                            Class = pageContent.Class,
                            Content = pageContent.Content,
                            PageId = pageContent.Id
                        };

                        icerik.Add(dto);
                    }

                    return icerik;
                }
            }
        }

        public void InsertNewPage(string Name, Array Contents, int LayoutID, int MenuId, string[] Classes)
        {
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {
                Page pl = new Page
                {
                    Name = Name,
                    CreatedAt = DateTime.Now,
                    LayoutId = LayoutID,
                    IsDeleted = false,
                    Slug = GenerateSlug(Name),
                    MenuId = MenuId
                };
                _repo.Add(pl);
            }

            using (BaseRepository<PageContent> _repo = new BaseRepository<PageContent>())
            {
                var sonEklenen = _repo.SetContext<Page>().Where(x => x.Name == Name).FirstOrDefault();

                var i = 0;
                foreach (var icerik in Contents)
                {
                    var con = new PageContent
                    {
                        Content = icerik.ToString(),
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                        Class = Classes[i],
                        PageId = sonEklenen.Id
                    };

                    _repo.Add(con);
                    i++;
                }
            }
        }



        public string GenerateSlug(string phrase, int maxLength = 50)
        {
            string str = phrase.ToLower();
            str = str.Replace('ı', 'i');
            str = str.Replace('ü', 'u');
            str = str.Replace('ö', 'o');
            str = str.Replace('ç', 'c');
            str = str.Replace('ğ', 'g');
            str = str.Replace('ş', 's');
            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces/hyphens into one space       
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();
            // cut and trim it
            str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();
            // hyphens
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }

        public void UpdatePage(string Name, List<string> Contents, int LayoutID, string oldPageName)
            {
            Page page2 = new Page();
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {

                var page = _repo.SetContext<Page>().FirstOrDefault(c => c.Name == oldPageName);
                page.Name = Name;
                page.LayoutId = LayoutID;
                page.UpdatedAt = DateTime.Now;
                _repo.Update(page);

                using (BaseRepository<PageContent> _repos = new BaseRepository<PageContent>())
                {
                    int i = 0;
                    var pagecontents = _repos.SetContext<PageContent>().Where(x => x.PageId == page.Id).ToList();
                    foreach (var oldcontent in pagecontents)
                    {
                        _repos.DeleteLayout(oldcontent);
                    }

                    foreach (var icerik in Contents)
                    {
                        var con = new PageContent
                        {
                            Content = icerik.ToString(),
                            CreatedAt = DateTime.Now,
                            IsDeleted = false,
                            PageId = page.Id
                        };

                        _repos.Add(con);
                        i++;
                    }
                }
            }

        }
        public int getMenuId(int pageId)
        {
            using (BaseRepository<Page> _repos = new BaseRepository<Page>())
            {
                Page page=_repos.SetContext<Page>().Where(x => x.Id == pageId).FirstOrDefault();
                return (int)page.MenuId;
            }
            
        }
        public void DeletePage(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
