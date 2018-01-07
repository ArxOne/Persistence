#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence.Reflection
{
    using System;
    using System.Text;
    using Utility;

    /// <summary>
    /// Transtyper
    /// </summary>
    public static class Transtyper
    {
        /// <summary>
        /// Converts the value to a persistend value.
        /// </summary>
        /// <param name="clrValue">The color value.</param>
        /// <param name="mappings">The mappings.</param>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static object ToPersistent(object clrValue, TypeMappings mappings, out object tag)
        {
            if (clrValue == null)
            {
                tag = null;
                return null;
            }

            var clrType = clrValue.GetType();
            var persistedType = mappings.GetPersisted(clrType);
            if (persistedType == null)
                throw new ArgumentException($"CLR type {clrType.FullName} is not supported");
            tag = persistedType.Tag;
            return Transtype(clrValue, persistedType.Persisted);
        }

        /// <summary>
        /// Converts the value to a persistend value.
        /// </summary>
        /// <param name="clrValue">The color value.</param>
        /// <param name="mappings">The mappings.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static object ToPersistent(object clrValue, TypeMappings mappings) => ToPersistent(clrValue, mappings, out var _);

        /// <summary>
        /// Converts a persistent value to CLR value.
        /// </summary>
        /// <param name="persistentValue">The persistent value.</param>
        /// <param name="clrType">Type of the color.</param>
        /// <returns></returns>
        public static object FromPersistent(object persistentValue, Type clrType)
        {
            return Transtype(persistentValue, clrType);
        }

        /// <summary>
        /// Transtypes the specified object.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static object Transtype(object o, Type targetType)
        {
            // classes
            if (targetType == typeof(string))
                return ToString(o);

            if (targetType == typeof(Version))
                return ToVersion(o);

            if (targetType == typeof(Uri))
                return ToUri(o);

            if (targetType == typeof(byte[]))
                return ToBytes(o);

            // values
            if (targetType.IsNullable())
            {
                if (o == null)
                    return null;
                return Transtype(o, targetType.GetNullabled());
            }

            if (o == null)
                throw new ArgumentNullException($"Can not convert null to {targetType.FullName}");

            if (targetType == typeof(bool))
                return ToBool(o);

            if (targetType == typeof(int))
                return (int)ToLong(o);

            if (targetType == typeof(uint))
                return (uint)ToLong(o);

            if (targetType == typeof(long))
                return ToLong(o);

            if (targetType.IsEnum)
                return ToEnum(o, targetType);

            throw new ArgumentException($"Unsupported target type {targetType.FullName}");
        }

        /// <summary>
        /// Transtypes the specified o.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static T Transtype<T>(object o) => (T)Transtype(o, typeof(T));

        private static bool ToBool(object o)
        {
            if (o is bool bo)
                return bo;
            if (o is byte b)
                return b != 0;
            if (o is sbyte sb)
                return sb != 0;
            if (o is int i)
                return i != 0;
            if (o is uint ui)
                return ui != 0;
            if (o is long l)
                return l != 0;
            if (o is ulong ul)
                return ul != 0;
            if (o is Enum e)
                return Convert.ToInt64(e) != 0;
            if (o is string s)
                return bool.Parse(s);
            return o != null;
        }

        private static object ToEnum(object o, Type targetType)
        {
            if (o is byte || o is sbyte || o is short || o is ushort || o is int || o is uint || o is long || o is ulong)
                return Enum.ToObject(targetType, o);
            return Enum.Parse(targetType, ToString(o));
        }

        private static long ToLong(object o)
        {
            if (o is bool bo)
                return bo ? 1 : 0;
            if (o is byte b)
                return b;
            if (o is sbyte sb)
                return sb;
            if (o is int i)
                return i;
            if (o is uint ui)
                return ui;
            if (o is long l)
                return l;
            if (o is ulong ul)
                return (long)ul; // bof
            if (o is Enum e)
                return Convert.ToInt64(e);
            if (long.TryParse(ToString(o), out var pl))
                return pl;
            throw new FormatException($"Can not convert value to long");
        }

        private static T ToT<T>(object o, Func<string, T> ctor)
            where T : class
        {
            if (o is T t)
                return t;
            if (o == null)
                return null;
            return ctor(ToString(o));
        }

        private static Version ToVersion(object o) => ToT(o, s => new Version(s));

        private static Uri ToUri(object o) => ToT(o, s => new Uri(s));

        private static byte[] ToBytes(object o)
        {
            if (o is byte[] bytes)
                return bytes;
            if (o == null)
                return null;
            if (o is string s)
                return Encoding.UTF8.GetBytes(s);
            throw new ArgumentException($"Can not convert {o.GetType()} to byte[]");
        }

        private static string ToString(object o)
        {
            if (o is string s)
                return s;
            if (o is byte[] bytes)
                return Encoding.UTF8.GetString(bytes);
            return o?.ToString();
        }
    }
}
