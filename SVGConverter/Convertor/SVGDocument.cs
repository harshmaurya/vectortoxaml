using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using System.Xml.Linq;
using SVGConverter.Convertor.Elements;
using VectorToXamlConvertor.Convertor;
using VectorToXamlConvertor.Convertor.Elements;

namespace SVGConverter.Convertor
{
    class SvgDocument
    {
        private readonly XElement _rootElement;
        private readonly List<ISvgElement> _elements;

        public SvgDocument(XDocument svgXml)
        {
            _rootElement = svgXml.Root;
            _elements = new List<ISvgElement>();
            InitializeDocument();
        }

        private void InitializeDocument()
        {
            foreach (var element in _rootElement.Elements())
            {
                var svgElement = ElementFactory.GetSvgElement(element);
                if (svgElement == null) continue;
                _elements.Add(svgElement);
            }
        }

        /// <summary>
        /// Returns the serialized Paths so that it can be deserialized later when need to render on UI, otherwise threading issue may occur
        /// </summary>
        /// <returns></returns>
        public List<string> GetPathCollection()
        {
            var list = new List<string>();
            foreach (var svgElement in _elements)
            {
                list.AddRange(svgElement.GeneratePaths().Select(XamlWriter.Save));
            }
            return list;
        }
    }
}
