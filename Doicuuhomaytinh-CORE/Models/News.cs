using System;
using System.Collections.Generic;

namespace Doicuuhomaytinh_CORE.Models
{
    public partial class News
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Contents { get; set; }
        public string? Thumb { get; set; }
        public bool Acive { get; set; }
        public string? Metadesc { get; set; }
        public string? MetaKey { get; set; }
        public string? Alias { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? ShortContent { get; set; }
        public string? Author { get; set; }
        public string? Tags { get; set; }
        public bool Ishot { get; set; }
        public bool IsNewfeed { get; set; }
        public int? CateId { get; set; }
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Category? Cate { get; set; }
    }
}
