using System;
using System.ComponentModel.DataAnnotations;

public class DateRangeAttribute : ValidationAttribute
{
    private readonly string _endDatePropertyName;

    public DateRangeAttribute(string endDatePropertyName)
    {
        _endDatePropertyName = endDatePropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var startDate = (DateOnly?)value;

        var endDateProperty = validationContext.ObjectType.GetProperty(_endDatePropertyName);
        if (endDateProperty == null)
        {
            return new ValidationResult($"Unknown property: {_endDatePropertyName}");
        }

        var endDate = (DateOnly?)endDateProperty.GetValue(validationContext.ObjectInstance);

        if (startDate != null && endDate != null && startDate > endDate)
        {
            return new ValidationResult("Start date must be before the end date.");
        }

        return ValidationResult.Success;
    }
}
