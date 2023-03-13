using System;
using System.Collections.Generic;

namespace Doicuuhomaytinh_CORE.Models
{
    public partial class Category
    {
        public Category()
        {
            News = new HashSet<News>();
        }

        public int CateId { get; set; }
        public string? CateName { get; set; }
        public string? Alias { get; set; }
        public string? Thumb { get; set; }
        public bool Active { get; set; }
        public int? Ordering { get; set; }
        public int? Parent { get; set; }
        public int? Levels { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
