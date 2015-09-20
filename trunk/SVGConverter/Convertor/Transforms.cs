using System.Windows.Media;

namespace SVGConverter.Convertor
{
    interface ITransform
    {
        Transform GetTransformation();
    }
    
    class SvgTranslateTransform : ITransform
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public SvgTranslateTransform(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Transform GetTransformation()
        {
            return new TranslateTransform(X, Y);
        }
    }

    class SVgRotateTransform : ITransform
    {
        public double Angle { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }

        public SVgRotateTransform(double angle, double x, double y)
        {
            Angle = angle;
            X = x;
            Y = y;
        }

        public Transform GetTransformation()
        {
            return new RotateTransform(Angle, X, Y);
        }
    }

    class SvgScaleTransform : ITransform
    {
        public double ScaleX { get; private set; }
        public double ScaleY { get; private set; }

        public SvgScaleTransform(double scaleX, double scaleY)
        {
            ScaleX = scaleX;
            ScaleY = scaleY;
        }

        public Transform GetTransformation()
        {
            return new ScaleTransform(ScaleX, ScaleY);
        }
    }

    class SvgSkewTransform : ITransform
    {
        public double SkewX { get; private set; }
        public double SkewY { get; private set; }

        public SvgSkewTransform(double skewX, double skewY)
        {
            SkewX = skewX;
            SkewY = skewY;
        }

        public Transform GetTransformation()
        {
            return new SkewTransform(SkewX, SkewY);
        }
    }

    class SvgMatrixTransform : ITransform
    {
        public SvgMatrixTransform(double m11, double m12, double m21, double m22, double offsetX, double offsetY)
        {
            M11 = m11;
            M12 = m12;
            M21 = m21;
            M22 = m22;
            OffsetX = offsetX;
            OffsetY = offsetY;
        }

        public double OffsetY { get; private set; }

        public double OffsetX { get; private set; }

        public double M22 { get; private set; }

        public double M21 { get; private set; }

        public double M12 { get; private set; }

        public double M11 { get; private set; }

        public Transform GetTransformation()
        {
            return new MatrixTransform(M11, M12, M21, M22, OffsetX, OffsetY);
        }
    }
}