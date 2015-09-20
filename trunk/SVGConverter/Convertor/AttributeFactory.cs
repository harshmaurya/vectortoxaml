using SVGConverter.Convertor.Attributes;
using VectorToXamlConvertor.Convertor.Attributes;
using VectorToXamlConvertor.Convertor.Elements;
using XAttribute = VectorToXamlConvertor.Convertor.Attributes.XAttribute;

namespace VectorToXamlConvertor.Convertor
{
    class AttributeFactory
    {
        public static object GetAttribute(System.Xml.Linq.XAttribute attribute)
        {
            return GetAttribute(attribute.Name.LocalName, attribute.Value);
        }

        public static object GetAttribute(string attributeName, string attributeValue)
        {
            switch (attributeName)
            {
                case "id":
                    return new IdAttribute(attributeValue);

                case "transform" :
                    return new TransformationAttribute(attributeValue);

                case "d": return new DataAttribute(attributeValue);

                case "fill":
                    return new FillAttribute(attributeValue);

                case "fill-opacity":
                    return new Opacity(attributeValue);

                case "stroke":
                    return new StrokeAttribute(attributeValue);

                case "stroke-width":
                    return new StrokeWidth(attributeValue);

                case "font-family":
                    return new FontFamilyAttribute(attributeValue);

                case "font-size":
                    return new FontSizeAttribute(attributeValue);

                case "cx":
                case "x":
                    return new XAttribute(attributeValue);

                case "cy":
                case "y":
                    return new YAttribute(attributeValue);

                case "x1":
                    return new X1Attributes(attributeValue);

                case "x2":
                    return new X2Attributes(attributeValue);

                case "y1":
                    return new Y1Attributes(attributeValue);
                    
                case "y2":
                    return new Y2Attributes(attributeValue);

                case "rx":
                    return new RadiusXAttribute(attributeValue);

                case "ry":
                    return new RadiusYAttribute(attributeValue);

                case "r":
                    return new RadiusAttribute(attributeValue);

                case "height":
                    return new HeightAttribute(attributeValue);

                case "width":
                    return new WidthAttribute(attributeValue);

                case "points":
                    return new PolyLineAtrribute(attributeValue);

                default:
                    return null;
            }
        }
    }
}
