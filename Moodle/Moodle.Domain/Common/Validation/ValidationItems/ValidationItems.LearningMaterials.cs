namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class LearningMaterials
        {
            public static string CodePrefix { get; set; } = nameof(LearningMaterials);
            public static readonly ValidationItem EmptyTitleErr = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = $"Title cant be empty",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem InvalidURL = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = $"URL invalid",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
        }
    }
}
