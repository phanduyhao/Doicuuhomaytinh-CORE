using Microsoft.AspNetCore.Mvc;
using Doicuuhomaytinh_CORE.Models; 
namespace Doicuuhomaytinh_CORE.Components
{
    [ViewComponent(Name ="MenuView")]
    public class MenuViewComponents : ViewComponent
    {
        private QuanLyDCHMTContext _context;
        public MenuViewComponents(QuanLyDCHMTContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofMenu = (from m in _context.Menus
                              where (m.Active == true) && (m.Position == 1)
                              select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default",listofMenu));
        }
    }
}
