using Moodle.Domain.Common.Validation;

namespace Moodle.Domain.Common.Model
{
    public class Resault<TValue>
    {
        public TValue Value { get; private set; }
        public ValidationResult ValidationResault { get; private set; }

        public Resault(TValue value, ValidationResult validationResault)
        {
            Value = value;
            ValidationResault = validationResault;
        }
    }
}
