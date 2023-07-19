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

            this.ResetListBox();
        }

        // TODO: この場合はListBoxの表示を更新したいというのが本意だと思いますので、ResetよりRefreshという単語の方がよいでしょう。
        // 更新という意味の英語でUpdateというのもありますが、こちらはもっと意味が広く、DBでも使われるため、画面を更新する系はRefreshを使う事が多いです。
        // あと ListBox がひとつしかないので迷いませんが、RefreshResultList としておいたほうが具体的で、ListBox を探しに行く手間が減ります。
        /// <summary>
        /// リストをクリアした後、<see cref="ResultsWindow._results"/> 内の要素を追加し表示します。
        /// </summary>
        public void ResetListBox()
        {
            // TODO: Result"s"List だと配列が複数あるように受け取れます。単数表現にしましょう。
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

                // TODO: index は TryParse の下へ持っていったほうがよいでしょう。
                var index = this.ResultsList.SelectedIndex;
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

        // TODO: seealso は summary に入れ子にしないほうがよいです。
        // https://stackoverflow.com/questions/3328486/what-is-the-meaning-of-xml-tags-see-and-seealso-in-c-sharp-in-visual-studio
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
        }
    }
}
