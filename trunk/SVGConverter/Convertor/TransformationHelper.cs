using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;

namespace SVGConverter.Convertor
{
    static class TransformationHelper
    {
        private const string TranslateTransformName = "translate";
        private const string RotateTransformName = "rotate";
        private const string ScaleTransformName = "scale";
        private const string SkewXTransformName = "skewX";
        private const string SkewYTransformName = "skewY";
        private const string MatrixTransformName = "matrix";

        public static Transform GetTransform(string transformations)
        {
            var transforms = new List<ITransform>();

            ProcessTranslateTransform(transformations, transforms);
            ProcessRotateTransform(transformations, transforms);
            ProcessScaleTransform(transformations, transforms);
            ProcessSkewTransform(transformations, transforms);
            ProcessMatrixTransform(transformations, transforms);

            var transformGroup = new TransformGroup();
            foreach (var transform in transforms)
            {
                transformGroup.Children.Add(transform.GetTransformation());
            }
            return transformGroup;
        }

        private static void ProcessMatrixTransform(string input, ICollection<ITransform> transforms)
        {
            var parameters = GetTransformParameters(input, MatrixTransformName);
            if (parameters == null) return;
            if (parameters.Length == 6)
            {
                transforms.Add(new SvgMatrixTransform(Double.Parse(parameters[0].Trim(), CultureInfo.InvariantCulture.NumberFormat),
                                      Double.Parse(parameters[1].Trim(), CultureInfo.InvariantCulture.NumberFormat),
                                      Double.Parse(parameters[2].Trim(), CultureInfo.InvariantCulture.NumberFormat),
                                      Double.Parse(parameters[3].Trim(), CultureInfo.InvariantCulture.NumberFormat),
                                      Double.Parse(parameters[4].Trim(), CultureInfo.InvariantCulture.NumberFormat),
                                      Double.Parse(parameters[5].Trim(), CultureInfo.InvariantCulture.NumberFormat)));
            }
        }

        private static void ProcessSkewTransform(string input, ICollection<ITransform> transforms)
        {
            double skewX=0, skewY=0;
            var parameterX = GetTransformParameters(input, SkewXTransformName);
            var parameterY = GetTransformParameters(input, SkewYTransformName);
            if (parameterX == null && parameterY == null) return;
            if (parameterX != null && parameterX.Length == 1)
                skewX = Double.Parse(parameterX[0].Trim(), CultureInfo.InvariantCulture.NumberFormat);
            if (parameterY != null && parameterY.Length == 1)
                skewY = Double.Parse(parameterY[0].Trim(), CultureInfo.InvariantCulture.NumberFormat);
            transforms.Add(new SvgSkewTransform(skewX, skewY));
        }

        private static void ProcessTranslateTransform(string input, ICollection<ITransform> transforms)
        {
            var parameters = GetTransformParameters(input, TranslateTransformName);
            if (parameters == null) return;
            double x = 0, y = 0;
            switch (parameters.Length)
            {
                case 1:
                    {
                        x = Double.Parse(parameters[0].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                    }
                    break;
                case 2:
                    {
                        x = Double.Parse(parameters[0].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                        y = Double.Parse(parameters[1].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                    }
                    break;
            }
            transforms.Add(new SvgTranslateTransform(x, y));
        }

        private static void ProcessRotateTransform(string input, ICollection<ITransform> transforms)
        {
            var parameters = GetTransformParameters(input, RotateTransformName);
            if (parameters == null) return;
            double angle = 0, x = 0, y = 0;
            switch (parameters.Length)
            {
                case 1:
                    {
                        angle = Double.Parse(parameters[0].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                    }
                    break;
                case 2:
                    {
                        angle = Double.Parse(parameters[0].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                        x = Double.Parse(parameters[1].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                    }
                    break;
                case 3:
                    {
                        angle = Double.Parse(parameters[0].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                        x = Double.Parse(parameters[1].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                        y = Double.Parse(parameters[2].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                    }
                    break;
            }

            transforms.Add(new SVgRotateTransform(angle, x, y));
        }

        private static void ProcessScaleTransform(string input, ICollection<ITransform> transforms)
        {
            var parameters = GetTransformParameters(input, ScaleTransformName);
            if (parameters == null) return;
            double scaleX = 0, scaleY = 0;
            switch (parameters.Length)
            {
                case 1:
                    {
                        scaleX = Double.Parse(parameters[0].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                        scaleY = Double.Parse(parameters[0].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                    }
                    break;
                case 2:
                    {
                        scaleX = Double.Parse(parameters[0].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                        scaleY = Double.Parse(parameters[1].Trim(), CultureInfo.InvariantCulture.NumberFormat);
                    }
                    break;
            }
            transforms.Add(new SvgScaleTransform(scaleX, scaleY));
        }

        private static string[] GetTransformParameters(string input, string transformationName)
        {
            if (!input.Contains(transformationName)) return null;
            var firstindex = input.IndexOf(transformationName, StringComparison.InvariantCulture) + transformationName.Length;
            var openingIndex = input.IndexOf('(', firstindex) + 1;
            var closingIndex = input.IndexOf(')', firstindex);
            var parameters = input.Substring(openingIndex, closingIndex - openingIndex);
            var tokens = parameters.Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            return tokens;
        }
    }

}
