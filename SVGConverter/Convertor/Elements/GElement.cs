using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Xml.Linq;
using VectorToXamlConvertor.Convertor;
using VectorToXamlConvertor.Convertor.Elements;

namespace SVGConverter.Convertor.Elements
{
    class GElement : SvgElementBase<GroupAdaptor, object>
    {
        public GElement(XElement element)
            : base(element)
        {

        }

        public override GroupAdaptor GetBaseObject(XElement element)
        {
            var subElements = new List<XElement>();

            foreach (var subElement in element.Elements())
            {
                foreach (var attribute in element.Attributes())
                {
                    try
                    {
                        subElement.Add(attribute);
                    }
                    catch (Exception)
                    {
                        //Do not apply the attribute if already present. Throws duplicate attribute exception
                    }

                }
                subElements.Add(subElement);
            }
            return new GroupAdaptor(subElements);
        }

        protected override ICollection<Path> ConvertObjectToPaths(GroupAdaptor objectToConvert)
        {
            var pathCollection = new List<Path>();
            foreach (var subElement in objectToConvert.SubElements)
            {
                var svgElement = ElementFactory.GetSvgElement(subElement);
                if (svgElement != null)
                    pathCollection.AddRange(svgElement.GeneratePaths());
            }
            return pathCollection;
        }
    }
}
