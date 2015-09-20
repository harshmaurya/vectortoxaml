using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace VectorToXamlConvertor
{
    public class VectorXaml
    {
        protected bool Equals(VectorXaml other)
        {
            return string.Equals(PathGeometryXaml, other.PathGeometryXaml) && string.Equals(StreamGeometryXaml, other.StreamGeometryXaml) && string.Equals(PathData, other.PathData) && string.Equals(XamlObject.ToString(), other.XamlObject.ToString());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (PathGeometryXaml != null ? PathGeometryXaml.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (StreamGeometryXaml != null ? StreamGeometryXaml.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PathData != null ? PathData.GetHashCode() : 0);
                return hashCode;
            }
        }

        public string PathGeometryXaml { get; set; }

        public string StreamGeometryXaml { get; set; }

        public string PathData { get; set; }

        public object XamlObject { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VectorXaml)obj);
        }

        public VectorXaml Clone()
        {
            return new VectorXaml
            {
                PathGeometryXaml = PathGeometryXaml,
                StreamGeometryXaml = StreamGeometryXaml,
                PathData = PathData,
                XamlObject = new VisualBrush((UIElement)XamlObject)
            };
        }
        public static UIElement CloneElement(UIElement orig)
        {
            if (orig == null)
                return (null);
            var s = XamlWriter.Save(orig);
            var stringReader = new StringReader(s);
            var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings());
            return (UIElement)XamlReader.Load(xmlReader);

        }

    }

}
