using Microsoft.AspNetCore.Mvc;
using Doicuuhomaytinh_CORE.Models;
namespace Doicuuhomaytinh_CORE.Components
{
    [ViewComponent(Name = "CategoryView")]
    public class CategoryViewComponents : ViewComponent
    {
        private QuanLyDCHMTContext _context;
        public CategoryViewComponents(QuanLyDCHMTContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofCategory = (from m in _context.Categories
                              select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofCategory));
        }
    }
}
