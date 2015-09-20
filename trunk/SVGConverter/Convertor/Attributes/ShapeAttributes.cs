using VectorToXamlConvertor.Convertor.Elements;

namespace VectorToXamlConvertor.Convertor.Attributes
{
    class XAttribute : SvgAttributeBase<SvgGenericShapeAdaptor>
    {
        public XAttribute(string value)
            : base(value)
        {
        }

        protected override SvgGenericShapeAdaptor ApplyAttribute(SvgGenericShapeAdaptor ownerElement)
        {
            ownerElement.X = double.Parse(Value);
            return ownerElement;
        }
    }

    class YAttribute : SvgAttributeBase<SvgGenericShapeAdaptor>
    {
        public YAttribute(string value)
            : base(value)
        {
        }

        protected override SvgGenericShapeAdaptor ApplyAttribute(SvgGenericShapeAdaptor ownerElement)
        {
            ownerElement.Y = double.Parse(Value);
            return ownerElement;
        }
    }

    class RadiusAttribute : SvgAttributeBase<SvgGenericShapeAdaptor>
    {
        public RadiusAttribute(string value)
            : base(value)
        {
        }

        protected override SvgGenericShapeAdaptor ApplyAttribute(SvgGenericShapeAdaptor ownerElement)
        {
            ownerElement.RadiusX = double.Parse(Value);
            ownerElement.RadiusY = double.Parse(Value);
            return ownerElement;
        }
    }

    class RadiusXAttribute : SvgAttributeBase<SvgGenericShapeAdaptor>
    {
        public RadiusXAttribute(string value)
            : base(value)
        {
        }

        protected override SvgGenericShapeAdaptor ApplyAttribute(SvgGenericShapeAdaptor ownerElement)
        {
            ownerElement.RadiusX = double.Parse(Value);
            return ownerElement;
        }
    }

    class RadiusYAttribute : SvgAttributeBase<SvgGenericShapeAdaptor>
    {
        public RadiusYAttribute(string value)
            : base(value)
        {
        }

        protected override SvgGenericShapeAdaptor ApplyAttribute(SvgGenericShapeAdaptor ownerElement)
        {
            ownerElement.RadiusY = double.Parse(Value);
            return ownerElement;
        }
    }


    class HeightAttribute : SvgAttributeBase<SvgGenericShapeAdaptor>
    {
        public HeightAttribute(string value)
            : base(value)
        {
        }

        protected override SvgGenericShapeAdaptor ApplyAttribute(SvgGenericShapeAdaptor ownerElement)
        {
            ownerElement.ShapeHeight = double.Parse(Value);
            return ownerElement;
        }
    }

    class WidthAttribute : SvgAttributeBase<SvgGenericShapeAdaptor>
    {
        public WidthAttribute(string value)
            : base(value)
        {
        }

        protected override SvgGenericShapeAdaptor ApplyAttribute(SvgGenericShapeAdaptor ownerElement)
        {
            ownerElement.ShapeWidth = double.Parse(Value);
            return ownerElement;
        }
    }
}
