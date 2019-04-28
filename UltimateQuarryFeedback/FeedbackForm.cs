using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UltimateQuarryFeedback
{
    public class FeedbackForm : Validatable
    {
        public string feedback { get; set; }

        [Range(1, 5)]
        public int starRating { get; set; }

        [EmailAddress]
        [Required]
        public string emailAddress { get; set; }

        [RegularExpression(@"^[a-zA-Z]{1,20}$", 
            ErrorMessage = "The field username must be a string with a minimum length of 1 and a maximum length of 20.")]
        public string username { get; set; }

        public DateTimeOffset submittedDate { get; set; }

        bool Validatable.Validate()
        {
            return Validate(null);
        }

        /// <summary>
        /// Validate a feedbackForm object by their annotations or an injected list of feedbackForms.
        /// </summary>
        /// <param name="feeedbackForms">optional parameter of list to check for uniqueness accross</param>
        public bool Validate(IEnumerable<FeedbackForm> feedbackForms = null)
        {
            // validate using annotations
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(this, context, results, true);
            foreach (var result in results)
            {
                throw new ArgumentException(result.ErrorMessage);
            }

            if ( feedbackForms != null )
            {
                // Validate uniqueness over blah
                var existingEmail = feedbackForms.Where(form => form.emailAddress == this.emailAddress);
                if (existingEmail.Any())
                {
                    throw new ArgumentException("email address has already been used");
                }
            }
            return true;
        }
    }
}
