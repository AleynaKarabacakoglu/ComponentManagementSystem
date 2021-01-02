using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Abstract
{
    public interface IPageService
    {
        IEnumerable<PageDto> GetPages();
        PageDto GetPageByName(string Name);
        List<PageContentDto> GetPageById(int id);

        void InsertNewPage(string Name, Array Columns, int LayoutID, int MenuId, string[] Class);
        void UpdatePage(string Name, List<string> Contents, int LayoutID, string oldPage);
        void DeletePage(string Name);
        int getMenuId(int pageId);
    }
}
