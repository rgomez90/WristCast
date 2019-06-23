using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WristCast.Core.Shared
{
    public abstract class SmartEnum<TEnum, TValue> : IEquatable<SmartEnum<TEnum, TValue>>
        where TEnum : SmartEnum<TEnum, TValue>
    {
        protected SmartEnum(string name, TValue value)
        {
            Name = name;
            Value = value;
        }

        private static readonly Lazy<List<TEnum>> _list = new Lazy<List<TEnum>>(ListAllOptions);

        public bool Equals(SmartEnum<TEnum, TValue> other)
        {
            if (other is null)
            {
                return false;
            }

            // If the objects share the same reference, return true
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // If the runtime types are not the same, return false
            if (GetType() != other.GetType())
            {
                return false;
            }

            // Return true if both name and value match
            return Name == other.Name && EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj) => Equals(obj as SmartEnum<TEnum, TValue>);

        public static TEnum FromName(string name)
        {
            if (name is null) throw new ArgumentNullException(nameof(name));
            var result =
                List.FirstOrDefault(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
            if (result == null)
            {
                throw new ArgumentException($"No {typeof(TEnum).Name} with Name \"{name}\" found.");
            }

            return result;
        }

        public static TEnum FromValue(TValue value)
        {
            // Can't use == to compare generics unless we constrain TValue to "class",
            // which we don't want because then we couldn't use int.
            var result = List.FirstOrDefault(item => EqualityComparer<TValue>.Default.Equals(item.Value, value));
            if (result == null)
            {
                throw new ArgumentException($"No {typeof(TEnum).Name} with Value {value} found.");
            }

            return result;
        }

        public static TEnum FromValue(TValue value, TEnum defaultValue)
        {
            // Can't use == to compare generics unless we constrain TValue to "class",
            // which we don't want because then we couldn't use int.
            var result = List.FirstOrDefault(item => EqualityComparer<TValue>.Default.Equals(item.Value, value));
            if (result == null)
            {
                result = defaultValue;
            }

            return result;
        }

        public override int GetHashCode() => new { Name, Value }.GetHashCode();

        public static List<TEnum> List => _list.Value;

        public string Name { get; protected set; }

        public static bool operator ==(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right)
        {
            // Handle null on left side
            if (left is null)
            {
                if (right is null)
                {
                    // null == null = true
                    return true;
                }

                return false;
            }

            // Equals handles null on right side
            return left.Equals(right);
        }

        public static explicit operator SmartEnum<TEnum, TValue>(TValue value) => FromValue(value);
        public static implicit operator TValue(SmartEnum<TEnum, TValue> smartEnum) => smartEnum.Value;

        public static bool operator !=(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) =>
            !(left == right);

        public override string ToString() => $"{Name} ({Value})";
        public TValue Value { get; protected set; }

        private static List<TEnum> ListAllOptions()
        {
            Type t = typeof(TEnum);
            return t.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(p => t.IsAssignableFrom(p.FieldType))
                .Select(pi => (TEnum)pi.GetValue(null))
                .OrderBy(p => p.Name)
                .ToList();
        }
    }
}
