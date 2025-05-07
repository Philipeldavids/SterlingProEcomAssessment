using System.ComponentModel.DataAnnotations;

namespace SterlingProEcommerce.Models.ViewModel
{
    
        public class RegisterViewModel
        {
            [Required]
            [Display(Name = "First Name")]
            [StringLength(50)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [StringLength(50)]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            public string ConfirmPassword { get; set; }
        }
  
}
