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
                this.memoryList.Items.Add(item);
            }

            this.MC.Click += (s, e) =>
            {
                string selected = memoryList.SelectedItem.ToString();
                if (selected != null)
                {
                    _memory.Remove(selected);
                    memoryList.Items.Remove(selected);
                }
            };
        }
    }
}
