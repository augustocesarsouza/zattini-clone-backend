using System.ComponentModel;
using System.Reflection;

namespace Zattini.Domain.EnumHelper
{
    public class EnumHelper
    {
        public static T GetEnumValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null && attribute.Description == description)
                    return (T)field.GetValue(null);
            }

            throw new ArgumentException($"'{description}' is not a valid description for enum '{typeof(T).Name}'.");
        }

        public static List<string> GetEnumDescriptions<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e =>
                {
                    var field = typeof(T).GetField(e.ToString());
                    var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                         .FirstOrDefault() as DescriptionAttribute;
                    return attribute?.Description ?? e.ToString();
                })
                .ToList();
        }
    }
}
