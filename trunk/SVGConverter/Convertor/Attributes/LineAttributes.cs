using VectorToXamlConvertor.Convertor.Elements;

namespace VectorToXamlConvertor.Convertor.Attributes
{
    class X1Attributes : SvgAttributeBase<SvgLineAdaptor>
    {
        public X1Attributes(string value) : base(value)
        {
        }

        protected override SvgLineAdaptor ApplyAttribute(SvgLineAdaptor ownerElement)
        {
            ownerElement.X1 = double.Parse(Value);
            return ownerElement;
        }
    }

    class X2Attributes : SvgAttributeBase<SvgLineAdaptor>
    {
        public X2Attributes(string value)
            : base(value)
        {
        }

        protected override SvgLineAdaptor ApplyAttribute(SvgLineAdaptor ownerElement)
        {
            ownerElement.X2 = double.Parse(Value);
            return ownerElement;
        }
    }

    class Y1Attributes : SvgAttributeBase<SvgLineAdaptor>
    {
        public Y1Attributes(string value)
            : base(value)
        {
        }

        protected override SvgLineAdaptor ApplyAttribute(SvgLineAdaptor ownerElement)
        {
            ownerElement.Y1 = double.Parse(Value);
            return ownerElement;
        }
    }

    class Y2Attributes : SvgAttributeBase<SvgLineAdaptor>
    {
        public Y2Attributes(string value)
            : base(value)
        {
        }

        protected override SvgLineAdaptor ApplyAttribute(SvgLineAdaptor ownerElement)
        {
            ownerElement.Y2 = double.Parse(Value);
            return ownerElement;
        }
    }
}
