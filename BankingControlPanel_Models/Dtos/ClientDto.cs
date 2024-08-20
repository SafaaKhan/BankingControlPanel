using BankingControlPanel_Models.ValidateModelAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingControlPanel_Models.Dtos
{
    public class ClientDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(60,ErrorMessage = "First name should be less than 60 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Last name should be less than 60 characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(11,MinimumLength =11, ErrorMessage = "Personal Id should be exactly 11 characters.")]
       // [RegularExpression(@"^\d{11}$", ErrorMessage = "Personal Id should be exactly 11 digits.")]
        public string PersonalId { get; set; }

        public string ProfilePhoto { get; set; }
        //https://www.nuget.org/packages/libphonenumber-csharp
        [Required]
        [PhoneNumber("KSA")]
        public string MobileNumber { get; set; }
        [Required]
        public string Sex { get; set; }
        public AddressDto Address { get; set; }
        [Required]
        
        public ICollection<AccountDto> Accounts { get; set; }
    }

    public class AddressDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }

    public class AccountDto
    {
        public string AccountNumber { get; set; }
    }
}
