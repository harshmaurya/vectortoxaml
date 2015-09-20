using System;
using System.Windows.Media;
using System.Windows.Shapes;
using SVGConverter.Convertor.Elements;

namespace VectorToXamlConvertor.Convertor.Attributes
{
    class FillAttribute : SvgAttributeBase<Shape>
    {
        public FillAttribute(string value)
            : base(value)
        {
        }

        protected override Shape ApplyAttribute(Shape ownerElement)
        {
            var converter = new BrushConverter();
            if (Value.StartsWith("url"))
            {
                int startIndex = Value.IndexOf("#", System.StringComparison.InvariantCultureIgnoreCase) + 1;
                int endIndex = Value.IndexOf(")", System.StringComparison.InvariantCultureIgnoreCase);
                var reference = Value.Substring(startIndex, endIndex - startIndex);
                GradientBrush brush;
                SvgDefinitions.Instance.Gradients.TryGetValue(reference, out brush);
                if (brush != null)
                {
                    ownerElement.Fill = brush;
                }
            }
            else if (Value != null && !Value.Equals("none"))
            {
                ownerElement.Fill = (Brush)converter.ConvertFromString(Value);
            }
            return ownerElement;
        }
    }

    class StrokeAttribute : SvgAttributeBase<Shape>
    {
        public StrokeAttribute(string value)
            : base(value)
        {
        }

        protected override Shape ApplyAttribute(Shape ownerElement)
        {
            var converter = new BrushConverter();
            if (Value != null && !Value.Equals("none"))
            {
                ownerElement.Stroke = (Brush)converter.ConvertFromString(Value);
            }
            return ownerElement;
        }
    }

    class StrokeWidth : SvgAttributeBase<Shape>
    {
        public StrokeWidth(string value)
            : base(value)
        {
        }

        protected override Shape ApplyAttribute(Shape ownerElement)
        {
            ownerElement.StrokeThickness = Double.Parse(Value);
            return ownerElement;
        }
    }

    class Opacity : SvgAttributeBase<Shape>
    {
        public Opacity(string value)
            : base(value)
        {
        }

        protected override Shape ApplyAttribute(Shape ownerElement)
        {
            ownerElement.Opacity = Double.Parse(Value);
            return ownerElement;
        }
    }
}
