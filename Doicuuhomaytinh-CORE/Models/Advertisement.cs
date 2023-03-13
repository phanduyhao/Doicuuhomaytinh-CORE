using System;
using System.Collections.Generic;

namespace Doicuuhomaytinh_CORE.Models
{
    public partial class Advertisement
    {
        public int AdsId { get; set; }
        public string? Title { get; set; }
        public string? Thumb { get; set; }
        public string? ShortDesc { get; set; }
        public string? Contents { get; set; }
        public bool Active { get; set; }
        public bool Ishot { get; set; }
        public bool IsNewfeed { get; set; }
        public string? MetaKey { get; set; }
        public string? Alias { get; set; }
    }
}
