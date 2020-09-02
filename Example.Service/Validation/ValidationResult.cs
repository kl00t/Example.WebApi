using System.Collections.Generic;

namespace Example.Service.Validation
{
    public class ValidationResult
    {
        public bool PassedValidation { get; set; }
        public List<string> Errors { get; set; }

        public ValidationResult(bool passedValidation)
        {
            PassedValidation = passedValidation;
            Errors = new List<string>();
        }

        public ValidationResult(bool passedValidation, string error)
        {
            PassedValidation = passedValidation;
            Errors = new List<string> { error };
        }

        public ValidationResult(bool passedValidation, List<string> errors)
        {
            PassedValidation = passedValidation;
            Errors = errors;
        }
    }
}