using System;
using System.Collections.Generic;

namespace Doicuuhomaytinh_CORE.Models
{
    public partial class Menu
    {
        public int MenuId { get; set; }
        public string? MenuTitle { get; set; }
        public string? Alias { get; set; }
        public bool Active { get; set; }
        public int? Levels { get; set; }
        public int? ParentId { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public int? MenuOrder { get; set; }
        public int? Position { get; set; }
    }
}
