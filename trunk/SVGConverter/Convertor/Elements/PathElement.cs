using System.Collections.Generic;
using System.Windows.Shapes;
using System.Xml.Linq;
using VectorToXamlConvertor.Convertor.Attributes;

namespace VectorToXamlConvertor.Convertor.Elements
{
    class PathElement : SvgElementBase<Path, Shape>
    {
        public PathElement(XElement element)
            : base(element)
        {

        }

        public override Path GetBaseObject(XElement element)
        {
            var pathObject = new Path();
            var pathGeometryCollection = element.Elements("PathGeometry");
            var result = pathObject;
            foreach (var xElement in pathGeometryCollection)
            {
                var attribute = xElement.Attribute("Figures");
                var figuresAttribute = new FiguresAttribute(attribute.Value);
                result = figuresAttribute.TryApplyAttribute<Path>(result);
            }
            return result;
        }

        protected override ICollection<Path> ConvertObjectToPaths(Path objectToConvert)
        {
            return new List<Path> {objectToConvert};
        }
    }
}
