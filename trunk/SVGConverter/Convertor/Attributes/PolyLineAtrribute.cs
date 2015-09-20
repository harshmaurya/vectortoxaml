using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using VectorToXamlConvertor.Convertor.Elements;

namespace VectorToXamlConvertor.Convertor.Attributes
{
    class PolyLineAtrribute : SvgAttributeBase<SvgPolyLineAdaptor>
    {
        public PolyLineAtrribute(string value) : base(value)
        {
        }

        protected override SvgPolyLineAdaptor ApplyAttribute(SvgPolyLineAdaptor ownerElement)
        {
            var points = Value.Split(',', ' ', '\t');
            var coordinateData = (from coordinateValue in points select coordinateValue.Trim() into coordinate where coordinate != "" select Math.Round(Double.Parse(coordinate, CultureInfo.InvariantCulture.NumberFormat), 2)).ToList();

            for (var i = 0; i < coordinateData.Count - 1; i += 2)
                ownerElement.Coordinates.Add(new Point(coordinateData[i], coordinateData[i + 1]));
            return ownerElement;
        }
    }
}
