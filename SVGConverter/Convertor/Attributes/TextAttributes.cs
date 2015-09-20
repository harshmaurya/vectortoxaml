using System.Windows.Media;
using VectorToXamlConvertor.Convertor.Elements;

namespace VectorToXamlConvertor.Convertor.Attributes
{
    class FontFamilyAttribute : SvgAttributeBase<SvgTextAdaptor>
    {
        public FontFamilyAttribute(string value)
            : base(value)
        {
        }

        protected override SvgTextAdaptor ApplyAttribute(SvgTextAdaptor ownerElement)
        {
            ownerElement.Font = new Typeface(Value);
            return ownerElement;
        }
    }

    class FontSizeAttribute : SvgAttributeBase<SvgTextAdaptor>
    {
        public FontSizeAttribute(string value)
            : base(value)
        {
        }

        protected override SvgTextAdaptor ApplyAttribute(SvgTextAdaptor ownerElement)
        {
            ownerElement.FontSize = double.Parse(Value);
            return ownerElement;
        }
    }
}
