using System.Collections.Generic;
using System.Windows.Shapes;
using SVGConverter.Convertor.Elements;
using VectorToXamlConvertor.Convertor.Elements;

namespace VectorToXamlConvertor.Convertor
{
    class SvgElementHost
    {
        private ICollection<Path> _path;
        private readonly ISvgElement _svgElement;

        public SvgElementHost(ISvgElement svgElement)
        {
            _svgElement = svgElement;
        }
        
        public ICollection<Path> GetPathCollection()
        {
            return _svgElement.GeneratePaths();
        }
    }
}
