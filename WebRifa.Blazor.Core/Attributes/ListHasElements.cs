using System.ComponentModel.DataAnnotations;

namespace WebRifa.Blazor.Core.Attributes;
internal class HashSetHasElements : ValidationAttribute {
    public override bool IsValid(object? value)
    {
        if (value == null) {
            return false;
        }

        if (value is HashSet<int> hashSetInt) {
            return hashSetInt.Any();
        }

        if (value is HashSet<string> hashSetString) {
            return hashSetString.Any();
        }

        if (value is HashSet<object> hashGetObject) {
            return hashGetObject.Any();
        }

        return false;
    }
}
