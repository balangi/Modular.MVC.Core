#nullable disable

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Modular.Modules.Core.Models.ManageViewModels;

public class ConfigureTwoFactorViewModel
{
    public string SelectedProvider { get; set; }

    public ICollection<SelectListItem> Providers { get; set; }
}
