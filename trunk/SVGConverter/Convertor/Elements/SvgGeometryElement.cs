using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using VectorToXamlConvertor.Convertor.Elements;

namespace SVGConverter.Convertor.Elements
{
    abstract class SvgGeometryElement<TElemType, TAttrib> : SvgElementBase<TElemType, TAttrib>, ITransformable
        where TElemType : Shape, TAttrib
        where TAttrib : class
    {
        private Geometry _geometry;

        protected SvgGeometryElement(XElement element) : base(element)
        {
        }

        public void ApplyTransformation(Transform transform)
        {
            _geometry.Transform = transform;
        }

        protected sealed override ICollection<Path> ConvertObjectToPaths(TElemType objectToConvert)
        {
            _geometry = GetGeometry(objectToConvert);
            return new List<Path>
            {
                new Path {Data = _geometry, Fill = objectToConvert.Fill, Stroke = objectToConvert.Stroke}
            };
        }

        protected abstract Geometry GetGeometry(TElemType objectToConvert);
        
    }
}
