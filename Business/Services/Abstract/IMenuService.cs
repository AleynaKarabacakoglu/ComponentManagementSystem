﻿using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Abstract
{
    public interface IMenuService
    {
        IEnumerable<MenuDto> GetMenus();
        MenuDto GetMenuByName(string Name);
        void InsertNewMenu(string Name, int? ParentId, string icon);
        void UpdateMenu(string Name, int? ParentId, string icon, string oldName);
        void DeleteMenu(string Name);
        string GetMenuRecursion();
    }
}
