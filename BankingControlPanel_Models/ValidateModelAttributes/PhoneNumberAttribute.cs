using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingControlPanel_Models.ValidateModelAttributes
{
    public class PhoneNumberAttribute: ValidationAttribute
    {
        public readonly string _region;

        public PhoneNumberAttribute(string region)
        {
            _region= region;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           

            try
            {
                var phoneNumber = value as string;
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var parsedNumber = phoneNumberUtil.Parse(phoneNumber, _region);

                if (phoneNumberUtil.IsValidNumber(parsedNumber))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Invalid mobile number.");
                }
            }
            catch (NumberParseException)
            {
                return new ValidationResult("Invalid mobile number.");
            }
        }
    }
}
