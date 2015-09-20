using System.Collections.Generic;
using System.IO;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using SVGConverter.Convertor;
using Path = System.Windows.Shapes.Path;

namespace VectorToXamlConvertor.Convertor
{
    public class SvgConvertor
    {
        private readonly string _svgFilePath;

        public SvgConvertor(string svgFilePath)
        {
            _svgFilePath = svgFilePath;
        }
        public IEnumerable<string> Convert()
        {
            var stream = new FileStream(_svgFilePath, FileMode.Open);
            var reader = XmlReader.Create(stream, new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore });
            var svgXml = XDocument.Load(reader);
            var svgDoc = new SvgDocument(svgXml);
            stream.Close();
            return svgDoc.GetPathCollection();
        }

        public static VectorXaml GetVectorXaml(IEnumerable<string> pathCollection, Brush overrideFill)
        {
            var pathGeometryContainer = new VectorContainer();
            var streamGeometryContainer = new VectorContainer();
            var pathGeometry = new PathGeometry();
            foreach (var pathString in pathCollection)
            {
                var path = (Path)XamlReader.Parse(pathString);
                if (path.Fill == null || path.Fill.Equals(Brushes.Transparent))
                {
                    path.Fill = overrideFill;
                }
                pathGeometryContainer.AddToContainer(path);
                pathGeometry.AddGeometry(path.Data);
            }
            streamGeometryContainer.AddToContainer(new Path { Data = pathGeometry.Clone() });
            var pathData = pathGeometry.ToString();
            return new VectorXaml
            {
                PathData = pathData,
                PathGeometryXaml = XamlWriter.Save(pathGeometryContainer.Container),
                StreamGeometryXaml = XamlWriter.Save(streamGeometryContainer.Container),
                XamlObject = pathGeometryContainer.Container
            };
        }
    }
}
