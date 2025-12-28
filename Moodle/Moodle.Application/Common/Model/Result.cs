using Moodle.Domain.Common.Validation;

namespace Moodle.Application.Common.Model
{
    public class Result<TValue> where TValue : class
    {
        private List<ValidationResultItem> _info = new List<ValidationResultItem>();
        private List<ValidationResultItem> _warnings = new List<ValidationResultItem>();
        private List<ValidationResultItem> _errors = new List<ValidationResultItem>();

        public TValue Value { get; set; }
        public Guid ReqId { get; init; } = Guid.NewGuid();

        public IReadOnlyList<ValidationResultItem> Infos
        {
            get => _info.AsReadOnly();
            init => _info.AddRange(value);
        }
        public IReadOnlyList<ValidationResultItem> Warnings
        {
            get => _warnings.AsReadOnly();
            init => _warnings.AddRange(value);
        }
        public IReadOnlyList<ValidationResultItem> Errors
        {
            get => _errors.AsReadOnly();
            init => _errors.AddRange(value);

        }
        public bool hasErrors => _errors.Any();
        public bool hasWarnings => _warnings.Any();
        public bool hasInfos => _info.Any();

        public void setValue(TValue value)
        {
            Value = value;
        }
        public void setValidationResult(ValidationResult validationResult)
        {
            _info.AddRange(validationResult.ValidationItems
                .Where(vi => vi.Severity == ValidationSeverity.Info)
                .Select(ValidationResultItem.FromValidationItem));
            _warnings.AddRange(validationResult.ValidationItems
                .Where(vi => vi.Severity == ValidationSeverity.Warning)
                .Select(ValidationResultItem.FromValidationItem));
            _errors.AddRange(validationResult.ValidationItems
                .Where(vi => vi.Severity == ValidationSeverity.Error)
                .Select(ValidationResultItem.FromValidationItem));
        }
    }
}
