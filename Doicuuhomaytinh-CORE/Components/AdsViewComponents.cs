using Microsoft.AspNetCore.Mvc;
using Doicuuhomaytinh_CORE.Models;
namespace Doicuuhomaytinh_CORE.Components
{
    [ViewComponent(Name = "AdsView")]
    public class adsViewComponents : ViewComponent
    {
        private QuanLyDCHMTContext _context;
        public adsViewComponents(QuanLyDCHMTContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofAds = (from m in _context.Advertisements
                                  select m).OrderBy(m => Guid.NewGuid()).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofAds));
        }
    }
}
