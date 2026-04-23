using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lucene.Net.DocumentMapper.Helpers
{
    public static class ReflectionHelpers
    {
        public static bool IsPropertyACollection(this PropertyInfo property)
        {
            return property.PropertyType.GetInterface(typeof(IEnumerable<>).FullName) != null &&
                   property.PropertyType != typeof(String) &&
                   property.PropertyType != typeof(string) &&
                   property.PropertyType != typeof(byte[]);
        }        
        
        public static bool IsACollection(this Type @type)
        {
            return @type.GetInterface(typeof(IEnumerable<>).FullName) != null &&
                   @type != typeof(String) &&
                   @type != typeof(string) &&
                   @type != typeof(byte[]);
        }

        public static bool IsPropertyADictionary(this PropertyInfo property)
        {
            return typeof(IDictionary).IsAssignableFrom(property.PropertyType) ||
                   (property.PropertyType.IsGenericType &&
                    (property.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>) ||
                     property.PropertyType.GetGenericTypeDefinition() == typeof(IDictionary<,>))) ||
                   property.PropertyType.GetInterfaces().Any(i =>
                       i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));
        }

        /// <summary>
        /// Returns the key and value types of a dictionary property as (TKey, TValue).
        /// </summary>
        public static (Type KeyType, Type ValueType) GetDictionaryTypes(this PropertyInfo propertyInfo)
        {
            var args = propertyInfo.PropertyType.GetGenericArguments();
            return (args[0], args[1]);
        }

        public static bool IsPrimitiveType(this PropertyInfo property)
        {
            var @type = property.PropertyType;
            return @type.IsPrimitive || @type.IsValueType || @type == typeof(string);
        }
        
        public static bool IsPrimitiveType(this Type @type)
        {
            return @type.IsPrimitive || @type.IsValueType || @type == typeof(string);
        }

        public static Type GetCollectionElementType(this PropertyInfo propertyInfo)
        {
            var @type = propertyInfo.GetPropertyType();
            if (@type.IsArray)
            {
                return @type.GetElementType();
            }

            return @type.GetGenericArguments().Single();
        }

        public static Type GetPropertyType(this PropertyInfo propertyInfo)
        {
            bool nullable = Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null;
            var propertyType = nullable
                ? Nullable.GetUnderlyingType(propertyInfo.PropertyType)
                : propertyInfo.PropertyType;

            return propertyType;
        }
        
        public static Type GetListType(this PropertyInfo propertyInfo)
        {
            bool nullable = Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null;
            var propertyType = nullable
                ? Nullable.GetUnderlyingType(propertyInfo.PropertyType)
                : propertyInfo.PropertyType;

            return propertyType;
        }
    }
}