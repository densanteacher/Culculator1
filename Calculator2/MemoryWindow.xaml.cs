using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calculator2
{
    /// <summary>
    /// MemoryWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MemoryWindow : Window
    {
        private List<string> _memory = new List<string>();
        public decimal _result;
        public int counter = 0;
        public MemoryWindow(List<string> memory, decimal result)
        {
            InitializeComponent();
            _memory = memory;
            _result = result;

            foreach (var item in _memory)
            {
                addControl(item);
            }
        }
        public void addControl(string m)
        {
            /*var grid = new Grid();
            grid.Name = "grid" + counter;
            grid.Width = 600;
            grid.Height = 50;
            var setGrid = new TextBlock();
            setGrid.Text = m;
            mainPanel.Children.Add(grid);
            mainPanel.RegisterName(grid.Name, grid);

            var lbl = new Label();
            lbl.Name = "lbl" + counter;
            lbl.Content = m;
            grid.Children.Add(lbl);
            grid.RegisterName(lbl.Name, lbl);

            var subPanel = new StackPanel();
            subPanel.Orientation = Orientation.Horizontal;
            grid.Children.Add(subPanel);

            var MC = new Button();
            MC.Content = "MC";
            MC.Height = 20;
            subPanel.Children.Add(MC);

            var Mplus = new Button();
            Mplus.Content = "M+";
            subPanel.Children.Add(Mplus);

            var Mminus = new Button();
            Mminus.Content = "M-";
            subPanel.Children.Add(Mminus);

            counter++;*/
        }
    }
}
