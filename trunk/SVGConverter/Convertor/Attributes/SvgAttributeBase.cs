using System;

namespace VectorToXamlConvertor.Convertor.Attributes
{
    abstract class SvgAttributeBase<T> : ISvgAttribute<T> where T : class
    {
        protected SvgAttributeBase(string value)
        {
            Value = value;
        }

        public string Value { get; set; }


        public TElemType TryApplyAttribute<TElemType>(T ownerElement) where TElemType : T
        {
            try
            {
                return (TElemType)ApplyAttribute(ownerElement);
            }
            catch (Exception)
            {
                return (TElemType)ownerElement;
            }
        }

        protected abstract T ApplyAttribute(T ownerElement);

    }
}
