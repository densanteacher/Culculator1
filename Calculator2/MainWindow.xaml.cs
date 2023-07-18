using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Runtime.ExceptionServices;

namespace Calculator2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private readonly List<string> _results = new();
        bool calculateFlag = false;

        public string PropertyMainText
        {
            get { return this.MainText.Text; }
        }

        public MainWindow()
        {
            this.InitializeComponent();
            this.ClearText(true);

        }

        #region メインテキスト処理関連メソッド

        /// <summary>
        /// テキストボックス初期化メソッドです。
        /// </summary>
        /// <param name="isSub">trueの時はSubTextも初期化し、falseの時はMainTextのみ初期化します。</param>
        private void ClearText(bool isSub)
        {

            this.MainText.Text = "0";
            this.calculateFlag = false;
            if (isSub)
            {
                this.SubText.Clear();
            }
        }

        // DONE: ボタンの処理でinputというメソッド名を多用しましたので、他の名前にした方がよいでしょう。
        // こういう単純な代入だけをするメソッドのことをセッターと呼んだりします。
        /// <summary>
        /// 数値として渡された計算結果を、メインテキストに表示します。
        /// </summary>
        /// <param name="result">計算結果です。</param>
        private void SetMainText(decimal result)
        {
            this.MainText.Text = result.ToString();
        }

        /// <summary>
        /// 文字列として渡された計算結果を、メインテキストに表示します。
        /// </summary>
        /// <param name="resultString">計算結果の文字列です。</param>
        private void SetMainText(string resultString)
        {
            this.MainText.Text = resultString;
        }


        /// <summary>
        /// メインテキストの数値の正負反転を反転します。
        /// </summary>
        private void InputReverseSign()
        {
            try
            {
                var txt = PropertyMainText;

                // DONE: 加減が難しいのですが、整理して短くなってきたので、このくらいなら早期リターンしない方がよいかもしれません。
                // DONE: parsed → mainValue くらいにしておきましょうか。
                if (Decimal.TryParse(txt, out var mainValue))
                {
                    var result = -mainValue;
                    this.SetMainText(result);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        // DONE?: ほかも InputXxx の形に揃えてしまいましょう。

        // BackspaceもInputとしてよいのでしょうか？どちらかといえば削除というニュアンスが強いような気がするのですが…
        /// <summary>
        /// メインテキストの末尾一文字を消去します。
        /// </summary>
        private void InputBackspace()
        {
            try
            {
                // DONE: var はプロジェクト全体にできるだけ適用してみてください。
                var txt = PropertyMainText;
                var bs = txt.Remove(txt.Length - 1);
                if (bs.Length == 0 || bs == "-")
                {
                    this.ClearText(false);
                }
                else
                {
                    this.SetMainText(bs);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メインテキストの数値の2乗を求めます。
        /// </summary>
        private void InputSquareOfX()
        {
            try
            {
                // DONE: MainTextを参照するプロパティを宣言して使ってみましょう。
                var txt = this.PropertyMainText;
                if (!(Decimal.TryParse(txt, out var mainValue)))
                {
                    return;
                }
                var result = mainValue * mainValue;
                this.SetMainText(result);

                this.calculateFlag = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メインテキストの数値の平方根を求めます。
        /// </summary>
        private void InputSquareRootOfX()
        {
            try
            {
                var txt = PropertyMainText;
                if (!(Double.TryParse(txt, out var mainValue)))
                {
                    return;
                }

                var result = Math.Sqrt(mainValue);
                this.SetMainText((decimal)result);
                this.calculateFlag = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メインテキストの数値をxとし、1/xを求めます。
        /// </summary>
        private void InputDivideByX()
        {
            try
            {
                var txt = PropertyMainText;
                if (!(Decimal.TryParse(txt, out var mainValue)))
                {
                    return;
                }
                var result = 1 / mainValue;
                this.SetMainText(result);

                this.calculateFlag = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// メインテキストの数値の百分率パーセンテージを求めます。
        /// </summary>
        private void InputPercentage()
        {
            try
            {
                var txt = PropertyMainText;
                if (!(Decimal.TryParse(txt, out var mainValue)))
                {
                    return;
                }

                var result = mainValue / 100;
                this.SetMainText(result);

                this.calculateFlag = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メインテキストの数値に小数点を追加します。
        /// </summary>
        private void InputDecimalMark()
        {
            if (!this.PropertyMainText.Contains("."))
            {
                this.MainText.Text += ".";
            }
        }

        /// <summary>
        /// SubTextに数値と演算記号が格納されている場合、計算を行います。
        /// </summary>
        private void Calculate()
        {
            if (!(Decimal.TryParse(this.PropertyMainText, out var valueMain)))
            {
                return;
            }

            try
            {
                var sub = this.SubText.Text;
                if ((sub == "") && (sub.Trim().Length == 0))
                {
                    return;
                }

                var sub2 = sub.Remove(sub.Length - 1);
                if (!(Decimal.TryParse(sub2, out var valueSub)))
                {
                    return;
                }

                // DONE: result は外で使わないので、使うスコープだけで宣言しましょう。
                if (sub.Contains("÷"))
                {
                    var result = valueSub / valueMain;
                    this.SetMainText(result);
                }
                else if (sub.Contains("×"))
                {
                    var result = valueSub * valueMain;
                    this.SetMainText(result);
                }
                else if (sub.Contains("+"))
                {
                    var result = valueSub + valueMain;
                    this.SetMainText(result);
                }
                else if (sub.Contains("-"))
                {
                    var result = valueSub - valueMain;
                    this.SetMainText(result);
                }

                this.SubText.Text = "";
                calculateFlag = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }
        #endregion


        #region Memory関連メソッド

        /// <summary>
        /// ResultsWindowを表示します。
        /// ResultsWindowには、現在の_resultsリストの一覧が表示されます。
        /// </summary>
        private void OpenResultsWindow()
        {
            try
            {
                if (!(Decimal.TryParse(this.PropertyMainText, out var result)))
                {
                    return;
                }
                var mw = new ResultsWindow(this._results, result);
                mw.Owner = this;
                mw.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// _resultsリストにメインテキストの数値を記録します。
        /// </summary>
        private void SaveMemory()
        {
            this._results.Add(this.PropertyMainText);
        }

        /// <summary>
        /// _resultsリストに最初に追加された数値から、現在のメインテキストの数値を引きます。
        /// </summary>
        private void InputMemoryMinus()
        {
            try
            {
                if (this._results.Count == 0)
                {
                    return;
                }

                var txt = this.PropertyMainText;

                var isSuccess = Decimal.TryParse(txt, out var mainValue);
                var isSuccess2 = Decimal.TryParse(this._results[0], out var result);
                if (!isSuccess || !isSuccess2)
                {
                    return;
                }

                result -= mainValue;
                this._results[0] = result.ToString();

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        // DONE: InputMemoryPlus がいいのでは？
        /// <summary>
        /// _resultsリストに最初に追加された数値に、現在のメインテキストの数値を足します。
        /// </summary>
        private void InputMemoryPlus()
        {
            try
            {
                if (this._results.Count == 0)
                {
                    return;
                }

                var txt = this.PropertyMainText;
                bool isSuccess = Decimal.TryParse(txt, out var mainValue);
                bool isSuccess2 = Decimal.TryParse(this._results[0], out var result);
                if (!isSuccess || !isSuccess2)
                {
                    return;
                }

                result += mainValue;
                this._results[0] = result.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// _resultsリストに最初に追加された数値を、メインテキストに再表示します。
        /// </summary>
        private void RecallMemory()
        {
            if (this._results.Count == 0)
            {
                return;
            }

            try
            {
                // DONE: SetMainTextのオーバーロードを定義して使ってみましょう。
                this.SetMainText(this._results[0]);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// _resultsリストをクリアします。
        /// </summary>
        private void ClearMemory()
        {
            this._results.Clear();
        }
        #endregion


        #region OnClickイベント

        // DONE: regionディレクティブを使ってイベントメソッドを区切ってみましょう。

        /// <summary>
        ///　_resultsリストに格納されている値をresult.txtに書き込みます。
        /// </summary>
        private void OutputButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                {
                    using var writer = new StreamWriter(Constants.path, false);
                    foreach (var item in this._results)
                    {
                        writer.WriteLine(item);
                    }
                }

                MessageBox.Show(this, "記録した数値をテキストファイルに出力しました。", "出力完了");
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// 押したボタンの数値をメインテキストに追加します。
        /// </summary>
        private void NumberButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn)
            {
                return;
            }

            if (this.calculateFlag) {
                this.ClearText(false);
            }

            if (Decimal.TryParse(this.PropertyMainText + btn.Content.ToString(), out var result))
            {
                this.SetMainText(result);
            }
        }

        /// <summary>
        /// +/-ボタンを押したとき、メインテキストの数値の正負を反転させます。
        /// </summary>
        private void SignReverseButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputReverseSign();
        }

        /// <summary>
        /// .ボタンを押したとき、メインテキストの末尾に小数点を追加します
        /// </summary>
        private void DecimalPointButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputDecimalMark();
        }

        /// <summary>
        /// 四則演算のボタンを押したとき、計算を行い、現在のメインテキストと四則演算の記号をサブテキストに格納します。
        /// </summary>
        private void OperatorButton_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            this.Calculate();

            this.SubText.Text = this.PropertyMainText + btn?.Content;
            this.ClearText(false);
        }

        /// <summary>
        /// 1/xボタンを押したとき、メインテキストの値をxとして1/xを表示します。
        /// </summary>
        private void DivideByButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputDivideByX();
        }

        /// <summary>
        /// x^2ボタンを押したとき、メインテキストの値を二乗した数値を表示します。
        /// </summary>
        private void SquareButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputSquareOfX();
        }

        /// <summary>
        /// √xボタンを押したとき、メインテキストの値の平方根を表示します。
        /// </summary>
        private void SquareRootButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputSquareRootOfX();
        }

        /// <summary>
        /// %ボタンを押したとき、メインテキストの値の100分の1を表示します。
        /// </summary>
        private void PercentButton__OnClick(object sender, RoutedEventArgs e)
        {
            this.InputPercentage();
        }

        /// <summary>
        /// CEボタンを押したとき、メインテキストの値を消去し0にします。
        /// </summary>
        private void ClearEntryButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ClearText(false);
        }

        /// <summary>
        /// Cボタンを押したとき、メインテキストとサブテキストの値を消去します。
        /// </summary>
        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ClearText(true);
        }

        /// <summary>
        /// Backボタンを押したとき、メインテキストの末尾一文字を消去します。
        /// </summary>
        private void BackSpaceButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputBackspace();
        }

        /// <summary>
        /// =ボタンを押したとき、計算を行い結果をメインテキストに表示します。
        /// </summary>
        private void EqualButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Calculate();
        }

        /// <summary>
        /// Mボタンを押したとき、resultsWindowを開きます。
        /// </summary>
        private void MemoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.OpenResultsWindow();
        }

        /// <summary>
        /// MSボタンを押したとき、_resultsリストにメインテキストの計算結果を記録します。
        /// </summary>
        private void MemorySaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.SaveMemory();
        }

        /// <summary>
        /// M-ボタンを押したとき、_resultsリストに保存された計算結果から現在のメインテキストの数値を引きます。
        /// </summary>
        private void MemoryMinusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputMemoryMinus();
        }

        /// <summary>
        /// M+ボタンを押したとき、_resultsリストに保存された計算結果に現在のメインテキストの数値を足します。
        /// </summary>
        private void MemoryPlusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputMemoryPlus();
        }

        /// <summary>
        /// MRボタンを押したとき、_resultsリストに保存された計算結果をメインテキストに表示します。
        /// </summary>
        private void MemoryRecallButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.RecallMemory();
        }

        /// <summary>
        /// MCボタンを押したとき、_resultsリストを全て消去します。
        /// </summary>
        private void MemoryClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ClearMemory();
        }
        #endregion


        #region Keydownイベント関連メソッド

        /// <summary>
        /// キー押下時、対応した数値の入力や四則演算を行います。
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            switch (e.Key)
            {
                case Key.Enter:
                    this.Calculate();
                    break;

                case Key.Back:
                    this.InputBackspace();
                    break;

                case Key.Decimal:
                    this.InputDecimalMark();
                    break;

                case Key.Divide:
                case Key.Multiply:
                case Key.Subtract:
                case Key.Add:
                    this.PressOperatorKey(e.Key);
                    break;

                case Key.D1:
                case Key.NumPad1:
                case Key.D2:
                case Key.NumPad2:
                case Key.D3:
                case Key.NumPad3:
                case Key.D4:
                case Key.NumPad4:
                case Key.D5:
                case Key.NumPad5:
                case Key.D6:
                case Key.NumPad6:
                case Key.D7:
                case Key.NumPad7:
                case Key.D8:
                case Key.NumPad8:
                case Key.D9:
                case Key.NumPad9:
                case Key.D0:
                case Key.NumPad0:
                    this.PressNumberKey(e.Key);
                    break;

                default:
                    break;
            }
            this.EqualButton.Focus();
        }

        /// <summary>
        /// キーボードで数値キーを押下した際、対応する数値をメインテキストの末尾に表示する処理です。
        /// </summary>
        /// <param name="key">Keyはキーボードで押されたキー情報です。</param>
        private void PressNumberKey(Key key)
        {
            decimal result;
            if(this.calculateFlag)
            {
                this.ClearText(false);
            }
            switch ((int)key)
            {
                case >= 34 and <= 43:
                    if (Decimal.TryParse(this.PropertyMainText + ((int)key - 34), out result))
                    {
                        this.SetMainText(result);
                    }
                    break;

                case >= 74 and <= 83:
                    if (Decimal.TryParse(this.PropertyMainText + ((int)key - 74), out result))
                    {
                        this.SetMainText(result);
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 現在のメインテキストと四則演算の記号をサブテキストに格納します。
        /// </summary>
        /// <param name="key">Keyはキーボードで押されたキー情報です。</param>
        private void PressOperatorKey(Key key)
        {
            this.Calculate();

            var result = this.PropertyMainText;
            switch (key)
            {
                case Key.Divide:
                    result += "÷";
                    break;
                case Key.Multiply:
                    result += "×";
                    break;
                case Key.Subtract:
                    result += "-";
                    break;
                case Key.Add:
                    result += "+";
                    break;
            }
            this.SubText.Text = result;

            this.ClearText(false);
        }
        #endregion


        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        private void ShowErrorMessage(Exception ex)
        {
            Console.WriteLine(ex.Message);

            switch (ex)
            {
                case DivideByZeroException:
                    MessageBox.Show(this, "0で除算することはできません。");
                    break;

                case OverflowException:
                    MessageBox.Show(this, "オーバーフローが発生しました。実行できません。");
                    break;

                case ArithmeticException:
                    MessageBox.Show(this, "計算中にエラーが発生しました。実行できません。");
                    break;

                case NullReferenceException:
                    MessageBox.Show(this, "参照がNullです。実行できません。");
                    break;

                default:
                    MessageBox.Show(this, "予期せぬエラーが発生しました。実行できません。");
                    break;
            }
        }
    }
}
