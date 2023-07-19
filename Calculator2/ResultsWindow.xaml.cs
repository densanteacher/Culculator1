using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
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
    public partial class ResultsWindow : Window
    {
        /// <summary>
        /// メインウィンドウから渡されたresultsリストの参照を格納します。
        /// </summary>
        private readonly List<string> _results;

        /// <summary>
        /// メインウィンドウから渡されたメインテキストの値を格納します。
        /// </summary>
        private readonly decimal _mainTextValue;

        public ResultsWindow(List<string> results, decimal mainTextValue)
        {
            this.InitializeComponent();
            this._results = results;
            this._mainTextValue = mainTextValue;

            this.RefreshResultList();
        }

        /// <summary>
        /// リストをクリアした後、<see cref="ResultsWindow._results"/> 内の要素を追加し表示します。
        /// </summary>
        public void RefreshResultList()
        {
            this.ResultList.Items.Clear();

            foreach (var item in this._results)
            {
                this.ResultList.Items.Add(item);
            }

            if (this._results.Count > 0)
            {
                this.ResultList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// リストボックスで選択されている値を削除します。
        /// </summary>
        private void ClearMemory()
        {
            if (this._results.Count == 0)
            {
                return;
            }

            try
            {
                var selectedItem = this.ResultList.SelectedItem.ToString() ?? "";
                this._results.Remove(selectedItem);

                this.RefreshResultList();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void CalculateMemory(Func<decimal, decimal> func)
        {
            try
            {
                if (this._results.Count == 0)
                {
                    return;
                }

                var selectedItem = this.ResultList.SelectedItem.ToString() ?? "";

                if (!(Decimal.TryParse(selectedItem, out var resultListValue)))
                {
                    return;
                }

                var index = this.ResultList.SelectedIndex;
                var result = func(resultListValue);
                this._results[index] = result.ToString();

                this.RefreshResultList();
                this.ResultList.SelectedIndex = index;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// リストボックスで選択されている値に、メインウィンドウのメインテキストに表示されている値を足します。
        /// </summary>
        private void InputMemoryPlus()
        {
            this.CalculateMemory(x => x + this._mainTextValue);
        }

        /// <summary>
        /// リストボックスで選択されている値から、メインウィンドウのメインテキストに表示されている値を引きます。
        /// </summary>
        private void InputMemoryMinus()
        {
            this.CalculateMemory(x => x - this._mainTextValue);

        }

        /// <summary>
        /// MCボタンを押したとき、リストボックスで選択している値を削除します。
        /// </summary>
        /// <seealso cref="ResultsWindow.ClearMemory"/>
        private void MemoryClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ClearMemory();
        }

        /// <summary>
        /// M+ボタンを押したとき、リストボックスで選択している値に、メインウィンドウのメインテキストに表示されている値を足します。
        /// </summary>
        /// <seealso cref="ResultsWindow.InputMemoryPlus"/>
        private void MemoryPlusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputMemoryPlus();
        }

        /// <summary>
        /// M-ボタンを押したとき、リストボックスで選択している値から、メインウィンドウのメインテキストに表示されている値を引きます。
        /// </summary>
        /// <seealso cref="ResultsWindow.InputMemoryMinus"/>
        private void MemoryMinusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputMemoryMinus();
        }

        /// <summary>
        /// OKボタンを押したとき、ウィンドウを閉じます。
        /// </summary>
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        private void ShowErrorMessage(Exception ex)
        {

            Console.WriteLine(ex.Message);

            switch (ex)
            {
                case DivideByZeroException:
                    MessageBox.Show(this, "0で除算することはできません。実行を中止します。");
                    break;
                case OverflowException:
                    MessageBox.Show(this, "オーバーフローが発生しました。実行を中止します。");
                    break;
                case ArithmeticException:
                    MessageBox.Show(this, "計算中にエラーが発生しました。実行を中止します。");
                    break;
                case NullReferenceException:
                    MessageBox.Show(this, "参照がNullです。実行を中止します。");
                    break;
                default:
                    MessageBox.Show(this, "予期せぬエラーが発生しました。実行を中止します。");
                    break;
            }
        }
    }
}
