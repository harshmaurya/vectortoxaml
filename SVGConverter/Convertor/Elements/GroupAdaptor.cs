using System.Collections.Generic;
using System.Xml.Linq;

namespace SVGConverter.Convertor.Elements
{
    class GroupAdaptor
    {
        public List<XElement> SubElements { get; private set; }

        public GroupAdaptor(List<XElement> subElements)
        {
            SubElements = subElements;
        }
    }
}
