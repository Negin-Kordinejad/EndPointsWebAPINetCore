using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EndPointsWebAPINetCore.Dtos
{
    public class RegisterUserDto
    {
      //  public string Id { get; set; }
      [Required]
      [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
      //  [Display(Name ="Confirm Password")]
      [Compare("Password",ErrorMessage = "Is notMatch")]
        public string ConfirmPassword { get; set; }
        //public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();
    }
}
