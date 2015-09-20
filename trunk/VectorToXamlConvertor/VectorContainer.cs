using System.Windows;
using System.Windows.Controls;

namespace VectorToXamlConvertor
{
    class VectorContainer
    {
        private Grid _grid;

        public UIElement Container
        {
            get { return _grid; }
        }

        public VectorContainer()
        {
            CreateBaseContainer();
        }

        private void CreateBaseContainer()
        {
            _grid = new Grid();
        }

        public void AddToContainer(UIElement element)
        {
            _grid.Children.Add(element);
        }
    }
}
