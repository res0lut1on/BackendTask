using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Common.Helpers
{
    public static class EnumHelper
    {
        public static string Description<TEnum>(this TEnum value)
            where TEnum : struct, Enum
        {
            if (!value.GetType().IsEnum)
                throw new ArgumentException("Type must be Enum");

            var description = GetAttribute<DescriptionAttribute>(value);
            return description != null ? description.Description : string.Empty;
        }

        public static TAttribute? GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            if (name == null) return null;

            var field = type.GetField(name);

            if (field == null) return null;

            return field
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }
    }
}
