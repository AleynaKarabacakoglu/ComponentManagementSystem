using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Abstract
{
    public interface ILayoutService
    {
        IEnumerable<LayoutDto> GetLayouts();

        LayoutDto GetLayoutByName(string Name);

        void InsertNewLayout(string Name, List<string> Kolonlar);
        void UpdateLayout(string oldName, string Name, List<string> Columns);
        void DeleteLayout(string Name);
        IEnumerable<LayoutDto> GetLayoutsForUpdate(string name);
    }
}
