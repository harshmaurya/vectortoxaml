using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using SVGConverter.Convertor.Elements;

namespace VectorToXamlConvertor.Convertor.Elements
{
    class RectElement : SvgGeometryElement<SvgGenericShapeAdaptor, Shape>
    {
        public RectElement(XElement element)
            : base(element)
        {
        }

        public override SvgGenericShapeAdaptor GetBaseObject(XElement element)
        {
            return new SvgGenericShapeAdaptor();
        }
        
        protected override Geometry GetGeometry(SvgGenericShapeAdaptor objectToConvert)
        {
            return new RectangleGeometry(new Rect(new Point(objectToConvert.X, objectToConvert.Y),
                                   new Size(objectToConvert.ShapeWidth, objectToConvert.ShapeWidth)),
                                   objectToConvert.RadiusX, objectToConvert.RadiusY);
        }
    }
}
