using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using SVGConverter.Convertor.Elements;

namespace VectorToXamlConvertor.Convertor.Elements
{
    class LineElement : SvgGeometryElement<SvgLineAdaptor, Shape>
    {
        public LineElement(XElement element)
            : base(element)
        {
        }

        public override SvgLineAdaptor GetBaseObject(XElement element)
        {
            return new SvgLineAdaptor();
        }

        protected override Geometry GetGeometry(SvgLineAdaptor objectToConvert)
        {
            return new LineGeometry(new Point(objectToConvert.X1, objectToConvert.Y1),
                              new Point(objectToConvert.X2, objectToConvert.Y2));
        }
    }
}
