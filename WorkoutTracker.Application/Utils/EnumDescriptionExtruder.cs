using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WorkoutTracker.Application.Utils;

public static class EnumDescriptionExtruder
{
    public static string GetEnumDescription(this Enum value)
    {
        var description = value.ToString();
        var fieldInfo = value.GetType().GetField(description)!;
        return fieldInfo.GetCustomAttribute<DisplayAttribute>(false)?.GetName() ?? description;
    }
}