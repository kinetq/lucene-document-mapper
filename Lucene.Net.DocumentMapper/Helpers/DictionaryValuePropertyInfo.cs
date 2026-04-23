using System;
using System.Reflection;

namespace Lucene.Net.DocumentMapper.Helpers
{
    /// <summary>
    /// A lightweight <see cref="PropertyInfo"/> wrapper that overrides
    /// <see cref="PropertyType"/> so field mappers resolve against the
    /// dictionary's value type rather than the dictionary type itself.
    /// </summary>
    internal sealed class DictionaryValuePropertyInfo : PropertyInfo
    {
        private readonly PropertyInfo _inner;
        private readonly Type _valueType;

        public DictionaryValuePropertyInfo(PropertyInfo inner, Type valueType)
        {
            _inner = inner;
            _valueType = valueType;
        }

        public override Type PropertyType => _valueType;
        public override string Name => _inner.Name;
        public override Type DeclaringType => _inner.DeclaringType;
        public override Type ReflectedType => _inner.ReflectedType;
        public override PropertyAttributes Attributes => _inner.Attributes;
        public override bool CanRead => _inner.CanRead;
        public override bool CanWrite => _inner.CanWrite;

        public override object[] GetCustomAttributes(bool inherit) => _inner.GetCustomAttributes(inherit);
        public override object[] GetCustomAttributes(Type attributeType, bool inherit) => _inner.GetCustomAttributes(attributeType, inherit);
        public override bool IsDefined(Type attributeType, bool inherit) => _inner.IsDefined(attributeType, inherit);
        public override MethodInfo[] GetAccessors(bool nonPublic) => _inner.GetAccessors(nonPublic);
        public override MethodInfo GetGetMethod(bool nonPublic) => _inner.GetGetMethod(nonPublic);
        public override MethodInfo GetSetMethod(bool nonPublic) => _inner.GetSetMethod(nonPublic);
        public override ParameterInfo[] GetIndexParameters() => _inner.GetIndexParameters();
        public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, System.Globalization.CultureInfo culture)
            => _inner.GetValue(obj, invokeAttr, binder, index, culture);
        public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, System.Globalization.CultureInfo culture)
            => _inner.SetValue(obj, value, invokeAttr, binder, index, culture);
    }
}