using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class BaseDto
    {

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

    }
}
