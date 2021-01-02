﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstract
{
    public interface IBase
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        bool IsDeleted { get; set; }

    }
}
