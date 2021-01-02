using Business.Services.Abstract;
using Common.Dtos;
using Domain.Concrete;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services.Concrete
{
    public class LayoutService : ILayoutService
    {
        public IEnumerable<LayoutDto> GetLayouts()
        {
            IEnumerable<LayoutDto> list;
            using (BaseRepository<Layout> _repo = new BaseRepository<Layout>())
            {
                list = _repo.SetContext<Layout>().Where(x => x.IsDeleted == false).Select(p => new LayoutDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsDeleted = p.IsDeleted,
                    Items = p.LayoutItems.Select(x => new LItemDto()
                    {
                        Id = x.Id,
                        Class = x.Class,
                    })
                }).ToList();
            }

            return list;
        }
        public IEnumerable<LayoutDto> GetLayoutsForUpdate(string Name)
        {
            IEnumerable<LayoutDto> list;
            using (BaseRepository<Layout> _repo = new BaseRepository<Layout>())
            {
                list = _repo.SetContext<Layout>().Where(x => x.IsDeleted == false && x.Name == Name).Include("LayoutItems").Select(p => new LayoutDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsDeleted = p.IsDeleted,
                    UpdatedAt = p.UpdatedAt,
                    CreatedAt = p.CreatedAt,

                    Items = p.LayoutItems.Where(x => !x.IsDeleted).Select(x => new LItemDto
                    {
                        Id = x.Id,
                        Class = x.Class,
                        LayoutId = x.LayoutId,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,


                    })
                }).ToList();
            }

            return list;
        }


        public LayoutDto GetLayoutByName(string Name)
        {
            using (BaseRepository<Layout> _repo = new BaseRepository<Layout>())
            {
                return _repo.SetContext<Layout>().Where(p => p.Name == Name).Select(p => new LayoutDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Items = p.LayoutItems.Where(x => !x.IsDeleted).Select(x => new LItemDto
                    {
                        Id = x.Id,
                        Class = x.Class

                    })
                }).FirstOrDefault();
            }
        }

        public void UpdateLayout(string oldName, string Name, List<string> columns)
        {
            Layout layout = new Layout();
            
            using (BaseRepository<Layout> _repo = new BaseRepository<Layout>())
            {
                //Layout Güncelleme İşlemi
                var layoutBilgiler = _repo.SetContext<Layout>().FirstOrDefault(c => c.Name == oldName);
                if (Name != oldName)
                {
                    layoutBilgiler.Name = Name;
                    _repo.Update(layoutBilgiler);
                }

                using (BaseRepository<LayoutItem> _repository = new BaseRepository<LayoutItem>())
                {
                    //Layout İtemlerini silme işlemi
                    var eskilayoutKolonlar = _repository.SetContext<LayoutItem>().Where(x => x.LayoutId == layoutBilgiler.Id)
                        .ToList();
                    foreach (var oldColumn in eskilayoutKolonlar)
                    {
                        _repository.DeleteLayout(oldColumn);
                    }

                    foreach (var kolonBilgisi in columns)
                    {
                        var post = new LayoutItem
                        {
                            Class = kolonBilgisi.ToString(),
                            LayoutId = layoutBilgiler.Id,
                            UpdatedAt = DateTime.Now,
                            IsDeleted = false
                        };
                        _repository.Add(post);
                    }

                }

            }
        }

        public void DeleteLayout(string Name)
        {
            using (BaseRepository<Layout> _repo = new BaseRepository<Layout>())
            {
                var layout = _repo.SetContext<Layout>().Where(c => c.Name == Name).FirstOrDefault();
                _repo.Delete(layout);
            }
        }

        public void InsertNewLayout(string Name, List<string> Kolonlar)
        {
            Layout layout = new Layout();
            using (BaseRepository<Layout> _repo = new BaseRepository<Layout>())
            {
                Layout pl = new Layout
                {
                    Name = Name,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                };
                _repo.Add(pl);
            }

            using (BaseRepository<LayoutItem> _repo = new BaseRepository<LayoutItem>())
            {
                layout = _repo.SetContext<Layout>().Where(c => c.Name == Name).First();


                foreach (var kolonBilgisi in Kolonlar)
                {
                    var post = new LayoutItem { Class = kolonBilgisi.ToString(), LayoutId = layout.Id, };
                    _repo.Add(post);
                }
            }
        }
    }
}
