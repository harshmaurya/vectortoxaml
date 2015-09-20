using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace VectorToXamlConvertor.Services
{
    static class XmlHelper
    {
        internal static List<string> GetNodeValue(Stream xmlFile, string node)
        {
            var result = new List<string>();
            var doc = new XmlDocument();
            doc.Load(xmlFile);
            var elemList = doc.GetElementsByTagName(node);
            for (var i = 0; i < elemList.Count; i++)
            {
                result.Add(elemList[i].InnerXml);
            }
            return result;
        }

        internal static List<string> GetAttributeValue(Stream xmlFile, string node, string attribute)
        {
            var result = new List<string>();
            var doc = new XmlDocument();
            doc.Load(xmlFile);
            var elemList = doc.GetElementsByTagName(node);
            for (var i = 0; i < elemList.Count; i++)
            {
                var xmlAttributeCollection = elemList[i].Attributes;
                if (xmlAttributeCollection != null) result.Add(xmlAttributeCollection.GetNamedItem(attribute).Value);
            }
            return result;
        }

        internal static string GetNodeTree(Stream xmlFile)
        {
            var doc = new XmlDocument();
            doc.Load(xmlFile);
            return doc.InnerXml;
        }
    }
}
