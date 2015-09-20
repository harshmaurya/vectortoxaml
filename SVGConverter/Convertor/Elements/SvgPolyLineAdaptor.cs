using System.Collections.Generic;
using System.Windows;

namespace VectorToXamlConvertor.Convertor.Elements
{
    class SvgPolyLineAdaptor : SvgGenericShapeAdaptor
    {
        public List<Point> Coordinates { get; set; }

        public SvgPolyLineAdaptor()
        {
            Coordinates = new List<Point>();
        }
    }
}
