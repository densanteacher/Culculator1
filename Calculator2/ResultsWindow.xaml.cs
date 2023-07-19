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
        // DONE: _results と ResultsList で二重にデータを管理する状況に見えます。
        // 二重管理はバグの元です。どちらかだけで管理できないか考えてみましょう。

        // _resultsで管理し、ResultsListでの表示はResetListBox（）メソッド（旧ClearWithAddListBox）を実行することで解決しました。

        /// <summary>
        /// メインウィンドウから渡されたresultsリストの参照を格納します。
        /// </summary>
        private readonly List<string> _results;

        // DONE: 計算された数字と読めますが、この値を plus/minus しているので、計算前の値では？
        // 何を主とするかで、受け取り方が変わるので、迷いそうなときはコメントを残します。
        // private はドキュメンテーションコメントは必須ではないと言われていますが、できるだけコメントしておいた方がよいでしょう。

        // 確かに紛らわしいので、フィールド名自体を変更しました。それとは別にコメントも残しておきます。

        /// <summary>
        /// メインウィンドウから渡されたメインテキストの値を格納します。
        /// </summary>
        private readonly decimal _mainTextValue;

        public ResultsWindow(List<string> results, decimal mainTextValue)
        {
            this.InitializeComponent();
            this._results = results;
            this._mainTextValue = mainTextValue;

            this.ResetListBox();

        }

        // DONE: With でつながず、メソッドを分割した方がよいでしょう。
        // この場合は、ひとつのメソッドでなんとかしたいと思われますので、メソッド名を工夫した方がよいでしょう。
        // WithやAndで複数の動作をつなげることは、よくありません。
        /// <summary>
        /// リストをクリアした後、<see cref="ResultsWindow._results"/> 内の要素を追加し表示します。
        /// </summary>
        public void ResetListBox()
        {
            this.ResultsList.Items.Clear();

            foreach (var item in this._results)
            {
                this.ResultsList.Items.Add(item);
            }

            if (this._results.Count > 0)
            {
                this.ResultsList.SelectedIndex = 0;
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
                var selectedItem = this.ResultsList.SelectedItem.ToString() ?? "";
                this._results.Remove(selectedItem);

                this.ResetListBox();
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
            try
            {
                if (this._results.Count == 0)
                {
                    return;
                }

                var selectedItem = this.ResultsList.SelectedItem.ToString() ?? "";
                var index = this.ResultsList.SelectedIndex;
                // DONE: ParseよりTryParseを使いましょう。
                if (!(Decimal.TryParse(selectedItem, out var resultsListValue)))
                {
                    return;
                }

                var plusResult = resultsListValue + this._mainTextValue;
                this._results[index] = plusResult.ToString();

                this.ResetListBox();
                this.ResultsList.SelectedIndex = index;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }

        }

        /// <summary>
        /// リストボックスで選択されている値から、メインウィンドウのメインテキストに表示されている値を引きます。
        /// </summary>
        private void InputMemoryMinus()
        {
            try
            {
                if (this._results.Count == 0)
                {
                    return;
                }

                var selectedItem = this.ResultsList.SelectedItem.ToString() ?? "";
                var index = this.ResultsList.SelectedIndex;
                // DONE: this
                if(!(Decimal.TryParse(selectedItem, out var resultsListValue)))
                {
                    return;
                }

                var minusResult = resultsListValue - this._mainTextValue;
                this._results[index] = minusResult.ToString();

                this.ResetListBox();
                this.ResultsList.SelectedIndex = index;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }

        }

        // DONE: see の使い方は ClearWithAddListBox のコメントを参照してみてください。
        // DONE: 下記のようにClearMemory"も" 見てほしい場合は、seealso を使います。
        /// <summary>
        /// <seealso cref="ResultsWindow.ClearMemory"/>
        /// MCボタンを押したとき、リストボックスで選択している値を削除します。
        /// </summary>

        private void MemoryClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ClearMemory();
        }

        /// <summary>
        /// <seealso cref="ResultsWindow.InputMemoryPlus"/>
        /// M+ボタンを押したとき、リストボックスで選択している値に、メインウィンドウのメインテキストに表示されている値を足します。
        /// </summary>
        private void MemoryPlusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputMemoryPlus();
        }

        /// <summary>
        /// <seealso cref="ResultsWindow.InputMemoryMinus"/>
        /// M-ボタンを押したとき、リストボックスで選択している値から、メインウィンドウのメインテキストに表示されている値を引きます。
        /// </summary>
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
                    MessageBox.Show(this,"0で除算することはできません。実行を中止します。");
                    break;
                case OverflowException:
                    MessageBox.Show(this,"オーバーフローが発生しました。実行を中止します。");
                    break;
                case ArithmeticException:
                    MessageBox.Show(this,"計算中にエラーが発生しました。実行を中止します。");
                    break;
                case NullReferenceException:
                    MessageBox.Show(this,"参照がNullです。実行を中止します。");
                    break;
                default:
                    MessageBox.Show(this,"予期せぬエラーが発生しました。実行を中止します。");
                    break;
            }

            // DONE: 上へ
        }
    }
}
