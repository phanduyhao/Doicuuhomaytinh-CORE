using System;
using System.Collections.Generic;

namespace Doicuuhomaytinh_CORE.Models
{
    public partial class Member
    {
        public int MemberId { get; set; }
        public string? FullName { get; set; }
        public string? Position { get; set; }
        public string? Thumb { get; set; }
        public string? Description { get; set; }
    }
}
