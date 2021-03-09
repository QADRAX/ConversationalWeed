using System.Collections.Generic;

namespace ConversationalWeed.Models
{
    public class ValidationResult
    {
        public IList<string> InvalidReasons { get; set; }
        public bool Success
        {
            get
            {
                if (InvalidReasons.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }

    public class ValidationResult<T> : ValidationResult where T : class
    {
        public T Result { get; set; }
    }
}
