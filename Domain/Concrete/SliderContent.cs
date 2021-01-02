using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Concrete
{
    public class SliderContent:BaseClass
    {
        public string imgWay { get; set; }
        public int Slider_Id { get; set; }
        public virtual Slider Slider { get; set; }

    }
}
