using System.Collections.Generic;
using System.Windows.Media;
using VectorToXamlConvertor.Convertor.Elements;

namespace SVGConverter.Convertor.Elements
{
    class SvgDefinitions
    {
        private static SvgDefinitions _sInstance;

        private SvgDefinitions()
        {
            Gradients = new Dictionary<string, GradientBrush>();
            Elements = new Dictionary<string, ISvgElement>();
        }

        public static SvgDefinitions Instance
        {
            get {return  _sInstance ?? (_sInstance = new SvgDefinitions()); }
        }

        public Dictionary<string, GradientBrush> Gradients { get; private set; }

        public Dictionary<string, ISvgElement> Elements { get; private set; } 
    }
}
