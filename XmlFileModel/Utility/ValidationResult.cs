using System.Collections.Generic;

namespace XmlFileModel.Utility
{
    public class ValidationResult
    {

        public bool IsValid { get; }
        public List<string> Errors { get; }


        public ValidationResult(bool isValid, List<string> errors)
        {
            IsValid = isValid;
            Errors = errors;
        }
    }
}
