#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Modular.Modules.Core.Models.ManageViewModels;

public class AddPhoneNumberViewModel
{
    [Required]
    [Phone]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }
}
