using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorToXamlConvertor.Convertor.Elements
{
    class SvgGenericShapeAdaptor : Shape
    {
        protected override Geometry DefiningGeometry
        {
            get { throw new NotImplementedException(); }
        }

        public new Brush Fill
        {
            get { return base.Fill; }
            set { base.Fill = value; }
        }

        public new Brush Stroke
        {
            get { return base.Stroke; }
            set { base.Stroke = value; }
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double RadiusX { get; set; }

        public double RadiusY { get; set; }

        public double ShapeHeight { get; set; }

        public double ShapeWidth { get; set; }

    }
}
