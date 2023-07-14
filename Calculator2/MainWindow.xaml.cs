﻿using System;
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

        public MainWindow()
        {
            this.InitializeComponent();
            this.ClearText(true);

            // TODO: ???
            using (var fs = File.Create(Constants.path)) { };
        }

        /// <summary>
        /// テキストボックス初期化メソッドです。
        /// </summary>
        /// <param name="isSub">trueの時はSubTextも初期化し、falseの時はMainTextのみ初期化します。</param>
        private void ClearText(bool isSub)
        {
            this.MainText.Text = "0";
            if (isSub)
            {
                this.SubText.Clear();
            }
        }

        /// <summary>
        /// メインテキストの数値の正負反転を反転します。
        /// </summary>
        private void ReverseSign()
        {
            try
            {
                // TODO: var が使える箇所に適用してみましょう。他の箇所もできるだけ使ってみましょう。
                string txt = this.MainText.Text;
                // TODO: isSuccessをなくしてみましょう。
                bool isSuccess = Decimal.TryParse(txt, out var parsed);
                if (!isSuccess)
                {
                    return;
                }

                decimal result = -parsed;

                // TODO: 数値を入れるために毎回 ToString() するのはめんどくさいと思います。
                // MainText.Text に値を設定するためのメソッドを作成しましょう。
                // そうすることで、例えば、ToString() にカルチャを追加する場合も一度で済みます。
                // また文字列を設定していることもあるので、オーバーロードについても調べてみましょう。
                this.MainText.Text = result.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        // TODO: BackSpaceよりはBackspaceの方が一般的です。
        /// <summary>
        /// メインテキストの末尾一文字を消去します。
        /// </summary>
        private void BackSpace()
        {
            try
            {
                string txt = this.MainText.Text;
                string bs = txt.Remove(txt.Length - 1);
                if (bs.Length == 0 || bs == "-")
                {
                    this.ClearText(false);
                }
                else
                {
                    this.MainText.Text = bs;
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
        private void SquareOfX()
        {
            try
            {
                string txt = this.MainText.Text;
                bool isSuccess = Decimal.TryParse(txt, out var parsed);
                if (!isSuccess)
                {
                    return;
                }
                decimal result = parsed * parsed;
                this.MainText.Text = result.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メインテキストの数値の平方根を求めます。
        /// </summary>
        private void SquareRootOfX()
        {
            try
            {
                string txt = this.MainText.Text;
                bool isSuccess = Double.TryParse(txt, out var parsed);
                if (!isSuccess)
                {
                    return;
                }

                double result = Math.Sqrt(parsed);
                this.MainText.Text = result.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メインテキストの数値をxとし、1/xを求めます。
        /// </summary>
        private void DivideByX()
        {
            try
            {
                string txt = this.MainText.Text;
                bool isSuccess = Decimal.TryParse(txt, out var parsed);
                if (!isSuccess)
                {
                    return;
                }

                decimal result = 1 / parsed;
                this.MainText.Text = result.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        // TODO: Askは相手に返事を"求める"ニュアンスだと思います。
        // 間違ってもよいです。いろいろ調べて自分で納得できるものを探してみましょう。
        /// <summary>
        /// メインテキストの数値の百分率パーセンテージを求めます。
        /// </summary>
        private void AskForPercentage()
        {
            try
            {
                string txt = this.MainText.Text;
                bool isSuccess = Decimal.TryParse(txt, out var parsed);
                if (!isSuccess)
                {
                    return;
                }

                decimal result = parsed / 100;
                this.MainText.Text = result.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        // TODO: メソッドの命名について見直してみましょう。
        // プログラミングでtheという冠詞はあまり使いません。
        // 今回は電卓ということで、ボタンがたくさんあります。
        // ボタンを押すと、何かが入力されるという動作は共通です。
        // 入力というのは、Inputです。
        // 小数点の記号のことはDecimalMarkとかDecimalCommaです。
        // つまり・・・？
        /// <summary>
        /// メインテキストの数値に小数点を追加します。
        /// </summary>
        private void HitTheDecimalPoint()
        {
            if (!this.MainText.Text.Contains("."))
            {
                this.MainText.Text += ".";
            }
        }

        // TODO: Memoryが残っています。
        /// <summary>
        /// MemoryWindowを表示します。
        /// MemoryWindowには、現在のmemoryリストの一覧が表示されます。
        /// </summary>
        private void OpenResultsWindow()
        {
            try
            {
                bool isSuccess = Decimal.TryParse(this.MainText.Text, out var result);
                if (!isSuccess)
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
        /// memoryリストにメインテキストの数値を記録します。
        /// </summary>
        private void SaveResult()
        {
            this._results.Add(this.MainText.Text);
        }

        /// <summary>
        /// memoryリストに最初に追加された数値から、現在のメインテキストの数値を引きます。
        /// </summary>
        private void SubtractResult()
        {
            try
            {
                if (this._results.Count == 0)
                {
                    return;
                }

                string txt = this.MainText.Text;
                bool isSuccess = Decimal.TryParse(txt, out var parsed);
                if (!isSuccess)
                {
                    return;
                }
                var result = Decimal.Parse(this._results[0]);
                result -= parsed;
                this._results[0] = result.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// memoryリストに最初に追加された数値に、現在のメインテキストの数値を足します。
        /// </summary>
        private void AddResult()
        {
            try
            {
                if (this._results.Count == 0)
                {
                    return;
                }
                string txt = this.MainText.Text;
                bool isSuccess = Decimal.TryParse(txt, out var parsed);
                bool isSuccess2 = Decimal.TryParse(this._results[0], out var result);
                if (!isSuccess || !isSuccess2)
                {
                    return;
                }
                result += parsed;
                this._results[0] = result.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メモリリストに最初に追加された数値を、メインテキストに再表示します。
        /// </summary>
        private void RecallResult()
        {
            if (this._results.Count == 0)
            {
                return;
            }

            try
            {
                this.MainText.Text = this._results[0];
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メモリをクリアします。
        /// </summary>
        private void ClearResults()
        {
            this._results.Clear();
        }

        /// <summary>
        ///　_memoriesに格納されている値をresult.txtに書き込みます。
        /// </summary>
        private void OutputButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var enc = Encoding.GetEncoding("UTF-8");

                {
                    // TODO: StreamWriterのデフォルトのエンコーディングを調べてみましょう。
                    using var writer = new StreamWriter(Constants.path, false, enc);
                    foreach (string item in this._results)
                    {
                        writer.WriteLine(item);
                    }
                }

                // TODO: ドキュメントのフォーマット(Ctrl + K, D)を活用しましょう。
                MessageBox.Show(this,"記録した数値をテキストファイルに出力しました。", "出力完了", MessageBoxButton.OK);
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
            // TODO: 新しい書き方になりますが、is not を調べてみましょう。
            if (!(sender is Button btn))
            {
                return;
            }

            if (Decimal.TryParse(this.MainText.Text + btn.Content.ToString(), out var result))
            {
                this.MainText.Text = result.ToString();
            }
        }

        /// <summary>
        /// +/-ボタンを押したとき、メインテキストの数値の正負を反転させます。
        /// </summary>
        private void SignReverseButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ReverseSign();
        }

        /// <summary>
        /// .ボタンを押したとき、メインテキストの末尾に小数点を追加します
        /// </summary>
        private void DecimalPointButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.HitTheDecimalPoint();
        }

        /// <summary>
        /// 現在のメインテキストと四則演算の記号をサブテキストに格納します。
        /// 既に格納されている場合は、計算を行ってからサブテキストに格納します。
        /// </summary>
        private void OperatorButton_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            this.Calculate();

            // TODO: this
            this.SubText.Text = MainText.Text + btn?.Content;
            this.ClearText(false);
        }

        /// <summary>
        /// 1/xボタンを押したとき、メインテキストの値をxとして1/xを表示します。
        /// </summary>
        private void DivideByButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DivideByX();
        }

        /// <summary>
        /// x^2ボタンを押したとき、メインテキストの値を二乗した数値を表示します。
        /// </summary>
        private void SquareButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.SquareOfX();
        }

        /// <summary>
        /// √xボタンを押したとき、メインテキストの値の平方根を表示します。
        /// </summary>
        private void SquareRootButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.SquareRootOfX();
        }

        /// <summary>
        /// %ボタンを押したとき、メインテキストの値の100分の1を表示します。
        /// </summary>
        private void PercentButton__OnClick(object sender, RoutedEventArgs e)
        {
            this.AskForPercentage();
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
            this.BackSpace();
        }

        /// <summary>
        /// =ボタンを押したとき、計算を行い結果をメインテキストに表示します。
        /// </summary>
        private void EqualButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Calculate();
        }

        /// <summary>
        /// Mボタンを押したとき、MemoryWindowを開きます。
        /// </summary>
        private void MemoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.OpenResultsWindow();
        }

        /// <summary>
        /// MSボタンを押したとき、メモリリストにメインテキストの計算結果を記録します。
        /// </summary>
        private void MemorySaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.SaveResult();
        }

        /// <summary>
        /// M-ボタンを押したとき、メモリに保存された計算結果から現在のメインテキストの数値を引きます。
        /// </summary>
        private void MemoryMinusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.SubtractResult();
        }

        /// <summary>
        /// M+ボタンを押したとき、メモリに保存された計算結果に現在のメインテキストの数値を足します。
        /// </summary>
        private void MemoryPlusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.AddResult();
        }

        /// <summary>
        /// MRボタンを押したとき、メモリに保存された計算結果をメインテキストに表示します。
        /// </summary>
        private void MemoryRecallButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.RecallResult();
        }

        /// <summary>
        /// MCボタンを押したとき、メモリを全て消去します。
        /// </summary>
        private void MemoryClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ClearResults();
        }

        /// <summary>
        /// SubTextに数値と演算記号が格納されている場合、計算を行います。
        /// </summary>
        private void Calculate()
        {
            bool isSuccess = Decimal.TryParse(this.MainText.Text, out var valueMain);
            if (!isSuccess)
            {
                return;
            }

            try
            {
                string sub = this.SubText.Text;
                if ((sub == "") || (sub.Trim().Length == 0))
                {
                    return;
                }

                sub = sub.Remove(sub.Length - 1);
                isSuccess = Decimal.TryParse(sub, out var valueSub);
                if(!isSuccess)
                {
                    return;
                }

                // TODO: result は外で使わないので、使うスコープだけで宣言しましょう。
                decimal result = 0;
                if (sub.Contains("÷"))
                {
                    result = valueSub / valueMain;
                    this.MainText.Text = result.ToString();
                }
                else if (sub.Contains("×"))
                {
                    result = valueSub * valueMain;
                    this.MainText.Text = result.ToString();
                }
                else if (sub.Contains("+"))
                {

                    result = valueSub + valueMain;
                    this.MainText.Text = result.ToString();
                }
                else if (sub.Contains("-"))
                {
                    result = valueSub - valueMain;
                    this.MainText.Text = result.ToString();
                }

                this.SubText.Text = null;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
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

            // TODO: メッセージボックスよりも先に出たほうがデバッグしやすいでしょう。
            Console.WriteLine(ex.Message);
        }

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
                    // DONE: this
                    this.BackSpace();
                    break;
                case Key.Decimal:
                    this.HitTheDecimalPoint();
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
            decimal result;
            switch ((int)key)
            {
                // TODO: switch だと when と && はパターンマッチングが使えます。たぶんもっと短くできます。
                case int i when i >= 34 && i <= 43:
                    if (Decimal.TryParse(this.MainText.Text + ((int)key - 34), out result))
                    {
                        this.MainText.Text = result.ToString();
                    }
                    break;
                case int i when i >= 74 && i <= 83:
                    if (Decimal.TryParse(this.MainText.Text + ((int)key - 74), out result))
                    {
                        this.MainText.Text = result.ToString();
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
            string result = this.MainText.Text;
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
    }
}
