using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using VectorToXamlConvertor.Convertor.Elements;

namespace SVGConverter.Convertor.Elements
{
    class PolyLineElement : SvgGeometryElement<SvgPolyLineAdaptor, Shape>
    {
        private readonly bool _isPolygon;

        public PolyLineElement(XElement element, bool isPolygon)
            : base(element)
        {
            _isPolygon = isPolygon;
        }

        public override SvgPolyLineAdaptor GetBaseObject(XElement element)
        {
            return new SvgPolyLineAdaptor();
        }
        
        protected override Geometry GetGeometry(SvgPolyLineAdaptor objectToConvert)
        {
            if (objectToConvert.Coordinates.Count == 0) return null;

            var pathFigure = new PathFigure
            {
                StartPoint = objectToConvert.Coordinates[0],
                IsClosed = true,
                IsFilled = _isPolygon
            };

            for (var i = 1; i < objectToConvert.Coordinates.Count; ++i)
                pathFigure.Segments.Add(new LineSegment(objectToConvert.Coordinates[i], true));

            var pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            return pathGeometry;
        }
    }
}
