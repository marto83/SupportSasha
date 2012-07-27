using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SupportSasha.Donations.Models
{
    [CustomValidation(typeof(DonationInput), "FinalCheck")]
    public class DonationInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        
        
        public decimal? Amount { get; set; }
        
        public decimal? OtherAmount { get; set; }

        public string Message { get; set; }
        public bool DontShowName { get; set; }
        public string Campaign { get; set; }

        public static ValidationResult FinalCheck(DonationInput input, ValidationContext validationContext)
        {
            if (input.Amount.Value == 0 && input.OtherAmount.HasValue == false)
                return new ValidationResult("Please enter amount.");
            return ValidationResult.Success;
        }
    }
}
