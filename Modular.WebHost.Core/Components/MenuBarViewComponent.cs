using Microsoft.AspNetCore.Mvc;

namespace Modular.WebHost.Core.Components
{
    public class MenuBarViewComponent : ViewComponent
    {
        public MenuBarViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
