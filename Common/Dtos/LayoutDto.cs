using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class LayoutDto : BaseDto
    {
        public LayoutDto()
        {
            this.Items = new List<LItemDto>();
        }

        public string Name { get; set; }
       
        public IEnumerable<LItemDto> Items { get; set; }
    }

    public class LItemDto : BaseDto
    {
        public int LayoutId { get; set; }
        public string Class { get; set; }
        

    }

}
