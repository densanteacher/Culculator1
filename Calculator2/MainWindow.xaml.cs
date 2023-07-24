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
        /// <summary>
        /// 計算結果を格納しておくリストです。
        /// </summary>
        private readonly List<string> _results = new();

        /// <summary>
        /// 計算を行ったかどうかを確認します。
        /// </summary>
        private bool _isCalculated = false;

        /// <summary>
        /// メインテキストの値を格納します。
        /// </summary>
        public string MainTextString => this.MainText.Text;

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
            this.SetMainText(0);

            this._isCalculated = false;

            if (isSub)
            {
                this.SubText.Clear();
            }
        }

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

        // TODO: IfParseだと何にParseするかわかりませんでした。IfDecimalがよいですね。
        /// <summary>
        /// 渡された文字列をdecimalの値にパースした後、メインテキストに表示します。
        /// </summary>
        /// <param name="valueText">メインテキストに表示したい値の文字列です。</param>
        private void SetMainTextIfDecimal(string valueText)
        {
            // DONE: たぶんこっちの方が読みやすいと思います。
            if (Decimal.TryParse(valueText, out var value))
            {
                this.SetMainText(value);
            }
        }

        /// <summary>
        /// 計算した結果を <see cref="MainText"/> に設定します。
        /// 使用する計算式は引数で渡します。
        /// </summary>
        /// <param name="func">計算で使用するメソッドを渡せます。<br/>メソッドのシグネチャは <c>decimal Method(decimal x) { ... }</c> となります。</param>
        /// <example>
        /// Usage:
        /// <code>
        /// Calculate((x) => x * x);
        /// </code>
        /// </example>
        private void Calculate(Func<decimal, decimal> func)
        {
            try
            {
                var txt = this.MainTextString;
                if (Decimal.TryParse(txt, out var x))
                {
                    var y = func(x);
                    this.SetMainText(y);
                }

                this._isCalculated = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// SubTextに数値と演算記号が格納されている場合、計算を行います。
        /// </summary>
        private void Calculate()
        {
            try
            {
                var sub = this.SubText.Text;
                if ((sub == "") || (sub.Trim().Length == 0))
                {
                    return;
                }

                var subRemoved = sub.Remove(sub.Length - 1);
                if (!(Decimal.TryParse(subRemoved, out var valueSub)))
                {
                    return;
                }

                if (sub.Contains("÷"))
                {
                    this.Calculate((x) => valueSub / x);
                }
                else if (sub.Contains("×"))
                {
                    this.Calculate((x) => valueSub * x);
                }
                else if (sub.Contains("+"))
                {
                    this.Calculate((x) => valueSub + x);
                }
                else if (sub.Contains("-"))
                {
                    this.Calculate((x) => valueSub - x);
                }

                this.SubText.Text = "";
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メインテキストの数値の正負を反転します。
        /// </summary>
        private void InputReverseSign()
        {
            this.Calculate((x) => x.ReverseSign());
        }

        /// <summary>
        /// メインテキストの数値の2乗を求めます。
        /// </summary>
        private void InputSquareOfX()
        {
            this.Calculate((x) => x.Square());
        }

        /// <summary>
        /// メインテキストの数値の平方根を求めます。
        /// </summary>
        private void InputSquareRootOfX()
        {
            this.Calculate((x) => x.SquareRoot());
        }

        /// <summary>
        /// メインテキストの数値をxとし、1/xを求めます。
        /// </summary>
        private void InputDivideByX()
        {
            this.Calculate((x) => x.DivideBy());
        }

        /// <summary>
        /// メインテキストの数値の百分率パーセンテージを求めます。
        /// </summary>
        private void InputPercentage()
        {
            this.Calculate((x) => x.CalculatePercentage());
        }

        /// <summary>
        /// メインテキストの数値に小数点を追加します。
        /// </summary>
        private void InputDecimalMark()
        {
            if (!this.MainTextString.Contains("."))
            {
                this.MainText.Text += ".";
            }
        }

        /// <summary>
        /// メインテキストの末尾一文字を消去します。
        /// </summary>
        private void InputBackspace()
        {
            try
            {
                var txt = this.MainTextString;
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

        #endregion　メインテキスト処理関連メソッド

        #region Memory関連メソッド

        /// <summary>
        /// <see cref="ResultsWindow"/>を表示します。
        /// </summary>
        private void OpenResultsWindow()
        {
            try
            {
                if (!(Decimal.TryParse(this.MainTextString, out var result)))
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
        /// <see cref="_results"/> にメインテキストの数値を記録します。
        /// </summary>
        private void SaveMemory()
        {
            this._results.Add(this.MainTextString);
        }

        /// <summary>
        /// 計算した結果を <see cref="_results"/> に格納します。
        /// 使用する計算式は引数で渡します。
        /// </summary>
        /// <param name="func">計算で使用するメソッドを渡せます。<br/>メソッドのシグネチャは <c>decimal Method(decimal x, decimal y) { ... }</c> となります。</param>
        /// <example>
        /// Usage:
        /// <code>
        /// Calculate((x, y) => y - x);
        /// </code>
        /// </example>
        private void CalculateMemory(Func<decimal, decimal, decimal> func)
        {
            try
            {
                if (this._results.Count == 0)
                {
                    return;
                }

                var txt = this.MainTextString;

                var isSuccess = Decimal.TryParse(txt, out var x);
                var isSuccess2 = Decimal.TryParse(this._results[0], out var y);
                if (!isSuccess || !isSuccess2)
                {
                    return;
                }
                this._results[0] = func(x, y).ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// <see cref="_results"/> に最初に追加された数値から、現在のメインテキストの数値を引きます。
        /// </summary>
        private void InputMemoryMinus()
        {
            this.CalculateMemory((x, y) => y - x);
        }

        /// <summary>
        /// <see cref="_results"/> に最初に追加された数値に、現在のメインテキストの数値を足します。
        /// </summary>
        private void InputMemoryPlus()
        {
            this.CalculateMemory((x, y) => y + x);
        }

        /// <summary>
        /// <see cref="_results"/> に最初に追加された数値を、メインテキストに再表示します。
        /// </summary>
        private void RecallMemory()
        {
            try
            {
                if (this._results.Count == 0)
                {
                    return;
                }

                this.SetMainText(this._results[0]);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// <see cref="_results"/> をクリアします。
        /// </summary>
        private void ClearMemory()
        {
            this._results.Clear();
        }

        /// <summary>
        /// <see cref="_results"/> に格納されている値を <see cref="Constants.Path"/> に書き込みます。
        /// </summary>
        /// <returns>成功したら true, 失敗したら false を返します。</returns>
        private bool WriteResultFile(string path, List<string> items)
        {
            try
            {
                using var writer = new StreamWriter(path, false);
                foreach (var item in items)
                {
                    writer.WriteLine(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
                return false;
            }
        }

        #endregion　Memory関連イベント


        #region OnClickイベント

        /// <summary>
        ///　Outputボタンを押したとき、<see cref="MainWindow._results"/> に格納されている値を<see cref="Constants.Path"/>にあるテキストファイルに出力します。
        /// </summary>
        private void OutputButton_OnClick(object sender, EventArgs e)
        {
            var isSuccess = this.WriteResultFile(Constants.Path, this._results);
            if (isSuccess)
            {
                MessageBox.Show(this, "記録した数値をテキストファイルに出力しました。", "出力完了");
            }
            else
            {
                MessageBox.Show(this, "出力に失敗しました。", "出力失敗");
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

            if (this._isCalculated)
            {
                this.ClearText(false);
            }

            var str = this.MainTextString + btn.Content.ToString();
            this.SetMainTextIfDecimal(str);
        }

        /// <summary>
        /// +/-ボタンを押したとき、メインテキストの数値の正負を反転させます。
        /// </summary>
        private void SignReverseButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputReverseSign();
        }

        /// <summary>
        /// .(ピリオド)ボタンを押したとき、メインテキストの末尾に小数点を追加します
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
            if (sender is not Button btn)
            {
                return;
            }

            this.Calculate();

            this.SubText.Text = this.MainTextString + btn?.Content;
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
        /// Mボタンを押したとき、<see cref="ResultsWindow"/>を開きます。
        /// </summary>
        private void MemoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.OpenResultsWindow();
        }

        /// <summary>
        /// MSボタンを押したとき、<see cref="_results"/>リストにメインテキストの計算結果を記録します。
        /// </summary>
        private void MemorySaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.SaveMemory();
        }

        /// <summary>
        /// M-ボタンを押したとき、<see cref="_results"/>リストに保存された計算結果から現在のメインテキストの数値を引きます。
        /// </summary>
        private void MemoryMinusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputMemoryMinus();
        }

        /// <summary>
        /// M+ボタンを押したとき、<see cref="_results"/>リストに保存された計算結果に現在のメインテキストの数値を足します。
        /// </summary>
        private void MemoryPlusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputMemoryPlus();
        }

        /// <summary>
        /// MRボタンを押したとき、<see cref="_results"/>リストに保存された計算結果をメインテキストに表示します。
        /// </summary>
        private void MemoryRecallButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.RecallMemory();
        }

        /// <summary>
        /// MCボタンを押したとき、<see cref="_results"/>リストを全て消去します。
        /// </summary>
        private void MemoryClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ClearMemory();
        }

        #endregion OnClickイベント

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
        }

        /// <summary>
        /// キーボードで数値キーを押下した際、対応する数値をメインテキストの末尾に表示する処理です。
        /// </summary>
        /// <param name="key">Keyはキーボードで押されたキー情報です。</param>
        private void PressNumberKey(Key key)
        {
            if (this._isCalculated)
            {
                this.ClearText(false);
            }

            var n = (int)key;
            if (n is >= 34 and <= 43)
            {
                this.SetMainTextIfDecimal(this.MainTextString + (n - 34));
            }
            else if (n is >= 74 and <= 83)
            {
                this.SetMainTextIfDecimal(this.MainTextString + (n - 74));
            }
        }

        /// <summary>
        /// 現在のメインテキストと四則演算の記号をサブテキストに格納します。
        /// </summary>
        /// <param name="key">Keyはキーボードで押されたキー情報です。</param>
        private void PressOperatorKey(Key key)
        {
            this.Calculate();

            var result = this.MainTextString;
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

        #endregion Keydownイベント関連メソッド


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
