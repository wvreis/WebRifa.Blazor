using System.ComponentModel.DataAnnotations;

namespace WebRifa.Blazor.Core.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
public sealed class CustomEmailAddressAttribute : DataTypeAttribute {
    public CustomEmailAddressAttribute()
        : base(DataType.EmailAddress)
    {}

    public override bool IsValid(object? value)
    {
        if (value == null) {
            return true;
        }

        if (!(value is string valueAsString)) {
            return false;
        }

        if (string.IsNullOrEmpty((string)value)) {
            return true;
        }

        // only return true if there is only 1 '@' character
        // and it is neither the first nor the last character
        int index = valueAsString.IndexOf('@');

        return
            index > 0 &&
            index != valueAsString.Length - 1 &&
            index == valueAsString.LastIndexOf('@');
    }
}