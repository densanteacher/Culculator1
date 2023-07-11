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

            this.ListInitialize();

            this.MemoryClear.Click += (s, e) => { this.ClearMemory(); };
            this.MemoryPlus.Click += (s, e) => { this.PlusMemory(); };
            this.MemoryMinus.Click += (s, e) => { this.MinusMemory(); };
            this.OK.Click += (s, e) => { this.Close(); };
        }

        /// <summary>
        /// リストを初期化した後、_memory内の要素を追加し表示します。
        /// </summary>
        public void ListInitialize()
        {
            this.memoryList.Items.Clear();
            foreach (var item in _memory)
            {
                this.memoryList.Items.Add(item);
            }
            if(_memory.Count > 0)
            {
                this.memoryList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// リストボックスで選択されている値を削除します
        /// </summary>
        private void ClearMemory()
        {
            if (_memory.Count == 0)
            {
                return;
            }
            string? selected = this.memoryList.SelectedItem.ToString();
            _memory.Remove(selected);
            this.memoryList.Items.Remove(selected);
            this.memoryList.SelectedIndex = 0;
        }

        /// <summary>
        /// リストボックスで選択されている値に、メインウィンドウのメインテキストに表示されている値を足します
        /// </summary>
        private void PlusMemory()
        {
            try
            {
                if (_memory.Count == 0)
                {
                    return;
                }
                string? selected = this.memoryList.SelectedItem.ToString();
                int index = this.memoryList.SelectedIndex;
                Decimal plusResult = Decimal.Parse(selected) + _result;
                _memory[index] = plusResult.ToString();
                this.ListInitialize();
                this.memoryList.SelectedIndex = index;
            } catch (Exception ex)
            {
                ErrorMessage();
                global::System.Console.WriteLine(ex.Message);
            }
            
        }

        /// <summary>
        /// リストボックスで選択されている値から、メインウィンドウのメインテキストに表示されている値を引きます
        /// </summary>
        private void MinusMemory()
        {
            try
            {
                if (_memory.Count == 0)
                {
                    return;
                }
                string? selected = this.memoryList.SelectedItem.ToString();
                int index = this.memoryList.SelectedIndex;
                Decimal minusResult = Decimal.Parse(selected) - _result;
                _memory[index] = minusResult.ToString();
                this.ListInitialize();
                this.memoryList.SelectedIndex = index;
            } catch(Exception ex)
            {
                ErrorMessage();
                global::System.Console.WriteLine(ex.Message);
            }
            
        }

        private void ErrorMessage()
        {
            MessageBox.Show("不可能な処理が実行されました。");
        }

    }
}
