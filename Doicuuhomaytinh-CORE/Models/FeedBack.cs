using System;
using System.Collections.Generic;

namespace Doicuuhomaytinh_CORE.Models
{
    public partial class FeedBack
    {
        public int FeedbackId { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Contents { get; set; }
        public bool Active { get; set; }
        public string? Title { get; set; }
    }
}
