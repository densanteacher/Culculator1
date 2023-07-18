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
        private readonly List<string> _results;

        // DONE: readonlyをつけてもよい条件を調べてみましょう。

        // 宣言時、あるいはコンストラクタの中でだけ値を設定できるのがreadonlyでの定義であるとわかりました。
        // また、参照型の場合はreadonlyとなるのはオブジェクトへの参照情報のみであり、オブジェクトのメンバーなどは変更できます。
        // つまり、_resultsのようなリストの場合、新たに違うリストへの参照は設定できませんが、リスト内の要素は変更することができます。
        // この様な理解でよろしいでしょうか？
        private readonly decimal _calculatedNumber;


        public ResultsWindow(List<string> results, decimal calculatedNumber)
        {
            this.InitializeComponent();
            this._results = results;
            this._calculatedNumber = calculatedNumber;

            this.ClearWithAddListBox();

        }

        /// <summary>
        /// リストをクリアした後、<see cref="ResultsWindow._results"/> 内の要素を追加し表示します。
        /// </summary>
        public void ClearWithAddListBox()
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
                this.ResultsList.Items.Remove(selectedItem);
                this.ResultsList.SelectedIndex = 0;
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
                // DONE: this
                // DONE: Parseと足し算を分けましょう。
                var mainValue = Decimal.Parse(selectedItem);
                var plusResult = mainValue + this._calculatedNumber;
                this._results[index] = plusResult.ToString();
                this.ClearWithAddListBox();
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
                var minusResult = Decimal.Parse(selectedItem) - _calculatedNumber;
                this._results[index] = minusResult.ToString();
                this.ClearWithAddListBox();
                this.ResultsList.SelectedIndex = index;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }

        }

        // DONE: see と seealso の違いについて調べてみましょう。

        // seeはリンク、seealsoは参照を記述するものです。

        // DONE: sender, e のコメントは不要です。
        /// <summary>
        /// <see cref="ResultsWindow.ClearMemory"/>
        /// MCボタンを押したとき、リストボックスで選択している値を削除します。
        /// </summary>

        private void MemoryClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ClearMemory();
        }

        /// <summary>
        /// <see cref="ResultsWindow.InputMemoryPlus"/>
        /// M+ボタンを押したとき、リストボックスで選択している値に、メインウィンドウのメインテキストに表示されている値を足します。
        /// </summary>
        private void MemoryPlusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputMemoryPlus();
        }

        /// <summary>
        /// <see cref="ResultsWindow.InputMemoryMinus"/>
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
            switch (ex)
            {
                case DivideByZeroException:
                    MessageBox.Show(this,"0で除算することはできません。");
                    break;
                case OverflowException:
                    MessageBox.Show(this,"オーバーフローが発生しました。この計算は行えません。");
                    break;
                case ArithmeticException:
                    MessageBox.Show(this,"計算中にエラーが発生しました。実行を中止します。");
                    break;
                case NullReferenceException:
                    MessageBox.Show(this,"参照がNullです。実行できません。");
                    break;
                default:
                    MessageBox.Show(this,"予期せぬエラーが発生しました。実行を中止します。");
                    break;
            }
            Console.WriteLine(ex.Message);
        }


    }
}
