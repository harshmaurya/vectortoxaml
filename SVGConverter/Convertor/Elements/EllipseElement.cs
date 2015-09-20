using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using SVGConverter.Convertor.Elements;

namespace VectorToXamlConvertor.Convertor.Elements
{
    class EllipseElement : SvgGeometryElement<SvgGenericShapeAdaptor, Shape>
    {
        public EllipseElement(XElement element) : base(element)
        {
        }

        public override SvgGenericShapeAdaptor GetBaseObject(XElement element)
        {
            return new SvgGenericShapeAdaptor();
        }
        
        protected override Geometry GetGeometry(SvgGenericShapeAdaptor objectToConvert)
        {
            return new EllipseGeometry(new Point(objectToConvert.X, objectToConvert.Y),
                objectToConvert.RadiusX, objectToConvert.RadiusY);
        }
    }
}
