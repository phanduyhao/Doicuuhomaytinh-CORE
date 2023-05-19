using System;
using System.Collections.Generic;
using Doicuuhomaytinh_CORE.Models;

namespace Doicuuhomaytinh_CORE.ModelViews
{
    public class HomeViewVM
    {
        public List<News> News { get; set; }
        public List<Member> members { get; set; }
        public List<Gallery> gallery { get; set; }
        public virtual Category? Cate { get; set; }

    }
}
