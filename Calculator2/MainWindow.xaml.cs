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
        private readonly List<string> _memories = new();

        public MainWindow()
        {
            this.InitializeComponent();
            this.Cleartext(true);
        }

        // TODO: メソッド名はキャメルケース
        /// <summary>
        /// テキストボックス初期化メソッドです。
        /// </summary>
        /// <param name="isSub">trueの時はSubTextも初期化し、falseの時はMainTextのみ初期化します。</param>
        private void Cleartext(bool isSub)
        {
            this.MainText.Text = "0";
            // TODO: bool型は true/false と比較することはしません。たまにbool?型だと使うことがあります。
            if (isSub == true)
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
                string txt = this.MainText.Text;
                // DONE: Parseした時点では、まだ反転していないのでreverseと呼ばない方が正確です。
                // 本来はこの程度は指摘しないのですが、なるべく正しい名前をつける習慣をつけておくほうがよいでしょう。
                decimal parsed = Decimal.Parse(txt);
                // TODO: こっちは反転しているので、parsed　のままだとまずいでしょう。
                parsed = -parsed;
                this.MainText.Text = parsed.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メインテキストの末尾一文字を消去します。
        /// </summary>
        private void BackSpace()
        {
            // TODO: try-catch。ここはなくてもセーフですが、Removeするときに例外が発生する可能性があります。
            string txt = this.MainText.Text;
            string bs = txt.Remove(txt.Length - 1);
            if (bs.Length == 0 || bs == "-")
            {
                this.Cleartext(false);
            }
            else
            {
                this.MainText.Text = bs;
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
                decimal square = Decimal.Parse(txt);
                square *= square;
                this.MainText.Text = square.ToString();
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
                double squareRoot = Double.Parse(txt);
                squareRoot = Math.Sqrt(squareRoot);
                this.MainText.Text = squareRoot.ToString();
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
                decimal divResult = 1 / Decimal.Parse(txt);
                this.MainText.Text = divResult.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        // TODO: GetXxx という名前のメソッドは返り値としてXxxを受け取る場合に使います。別の名前を考えてみましょう。
        /// <summary>
        /// メインテキストの数値の百分率パーセンテージを求めます。
        /// </summary>
        private void GetPercentage()
        {
            try
            {
                string txt = this.MainText.Text;
                decimal perResult = Decimal.Parse(txt) / 100;
                this.MainText.Text = perResult.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

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

        /// <summary>
        /// MemoryWindowを表示します。
        /// MemoryWindowには、現在のmemoryリストの一覧が表示されます。
        /// </summary>
        private void OpenMemoryWindow()
        {
            try
            {
                decimal result = Decimal.Parse(this.MainText.Text);

                MemoryWindow mw = new MemoryWindow(this._memories, result);
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
        private void SaveMemory()
        {
            this._memories.Add(this.MainText.Text);
        }

        /// <summary>
        /// memoryリストに最初に追加された数値から、現在のメインテキストの数値を引きます。
        /// </summary>
        private void SubtractMemory()
        {
            try
            {
                if (this._memories.Count == 0)
                {
                    return;
                }

                string txt = this.MainText.Text;
                decimal memoryMinus = Decimal.Parse(this._memories[0]) - Decimal.Parse(txt);
                this._memories[0] = memoryMinus.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        // TODO: Addはここでは加算の意味で使用していますが、this._memoriesがリストなので、Addという用語はリストに追加するような印象をうけます。別のメソッド名を検討しましょう。
        /// <summary>
        /// memoryリストに最初に追加された数値に、現在のメインテキストの数値を足します。
        /// </summary>
        private void AddMemory()
        {
            try
            {
                if (this._memories.Count == 0)
                {
                    return;
                }

                string txt = this.MainText.Text;
                decimal memoryPlus = Decimal.Parse(this._memories[0]) + Decimal.Parse(txt);
                this._memories[0] = memoryPlus.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メモリリストに最初に追加された数値を、メインテキストに再表示します。
        /// </summary>
        private void RecallMemory()
        {
            // TODO: try-catch
            // TODO: 早期リターンにしたほうが、他のメソッドと書き方が揃うでしょう。
            if (this._memories.Count > 0)
            {
                this.MainText.Text = this._memories[0];
            }; // TODO: trim ;
        }

        /// <summary>
        /// メモリをクリアします。
        /// </summary>
        private void ClearMemory()
        {
            this._memories.Clear();
        }

        /// <summary>
        ///　_memoriesに格納されている値をresult.txtに書き込みます。
        /// </summary>
        private void ClickOutputButton(object sender, EventArgs e)
        {
            try
            {
                string path = @"..\..\..\result.txt";
                using (FileStream fs = File.Create(path)) ;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding enc = Encoding.GetEncoding("Shift_JIS");
                using (StreamWriter writer = new StreamWriter(path, false, enc))
                {
                    foreach (string item in this._memories)
                    {
                        writer.WriteLine(item);
                    }
                }
                MessageBox.Show("記録した数値をテキストファイルに出力しました。");
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// 押したボタンの数値をメインテキストに追加します。
        /// </summary>
        private void ClickNumberButton(object sender, RoutedEventArgs e)
        {
            // TODO: try-catch。Parseしている箇所は必要です。

            // DONE: 変数の型として var を使ってみましょう。
            // TODO: 他に var が使える箇所に適用してみましょう。
            var btn = sender as Button;
            decimal result = Decimal.Parse(this.MainText.Text + btn.Content.ToString());
            this.MainText.Text = result.ToString();
        }

        /// <summary>
        /// +/-ボタンを押したとき、メインテキストの数値の正負を反転させます。
        /// </summary>
        private void ClickSignReverseButton(object sender, RoutedEventArgs e)
        {
            this.ReverseSign();
        }

        /// <summary>
        /// .ボタンを押したとき、メインテキストの末尾に小数点を追加します
        /// </summary>
        private void ClickDecimalPointButton(object sender, RoutedEventArgs e)
        {
            this.HitTheDecimalPoint();
        }

        /// <summary>
        /// 現在のメインテキストと四則演算の記号をサブテキストに格納します。
        /// 既に格納されている場合は、計算を行ってからサブテキストに格納します。
        /// </summary>
        private void ClickOperatorButton(object sender, RoutedEventArgs e)
        {
            // TODO: try-catch。nullになる可能性があるところには必要です。

            var btn = sender as Button;
            this.Calculate();
            // TODO: as だと btn が null になる可能性があります。検証方法がいくつかあるので調べて適用してみましょう。
            this.SubText.Text = MainText.Text + btn.Content;
            this.Cleartext(false);
        }

        /// <summary>
        /// 1/xボタンを押したとき、メインテキストの値をxとして1/xを表示します。
        /// </summary>
        private void ClickDivideByButton(object sender, RoutedEventArgs e)
        {
            this.DivideByX();
        }

        /// <summary>
        /// x^2ボタンを押したとき、メインテキストの値を二乗した数値を表示します。
        /// </summary>
        private void ClickSquareButton(object sender, RoutedEventArgs e)
        {
            this.SquareOfX();
        }

        /// <summary>
        /// √xボタンを押したとき、メインテキストの値の平方根を表示します。
        /// </summary>
        private void ClickSquareRootButton(object sender, RoutedEventArgs e)
        {
            this.SquareRootOfX();
        }

        /// <summary>
        /// %ボタンを押したとき、メインテキストの値の100分の1を表示します。
        /// </summary>
        private void ClickPercentButton(object sender, RoutedEventArgs e)
        {
            this.GetPercentage();
        }

        /// <summary>
        /// CEボタンを押したとき、メインテキストの値を消去し0にします。
        /// </summary>
        private void ClickClearEntryButton(object sender, RoutedEventArgs e)
        {
            this.Cleartext(false);
        }

        /// <summary>
        /// Cボタンを押したとき、メインテキストとサブテキストの値を消去します。
        /// </summary>
        private void ClickClearButton(object sender, RoutedEventArgs e)
        {
            this.Cleartext(true);
        }

        /// <summary>
        /// Backボタンを押したとき、メインテキストの末尾一文字を消去します。
        /// </summary>
        private void ClickBackSpaceButton(object sender, RoutedEventArgs e)
        {
            this.BackSpace();
        }

        /// <summary>
        /// =ボタンを押したとき、計算を行い結果をメインテキストに表示します。
        /// </summary>
        private void ClickEqualButton(object sender, RoutedEventArgs e)
        {
            this.Calculate();
        }

        /// <summary>
        /// Mボタンを押したとき、MemoryWindowを開きます。
        /// </summary>
        private void ClickMemoryButton(object sender, RoutedEventArgs e)
        {
            this.OpenMemoryWindow();
        }

        /// <summary>
        /// MSボタンを押したとき、メモリリストにメインテキストの計算結果を記録します。
        /// </summary>
        private void ClickMemorySaveButton(object sender, RoutedEventArgs e)
        {
            this.SaveMemory();
        }

        /// <summary>
        /// M-ボタンを押したとき、メモリに保存された計算結果から現在のメインテキストの数値を引きます。
        /// </summary>
        private void ClickMemoryMinusButton(object sender, RoutedEventArgs e)
        {
            this.SubtractMemory();
        }

        /// <summary>
        /// M+ボタンを押したとき、メモリに保存された計算結果に現在のメインテキストの数値を足します。
        /// </summary>
        private void ClickMemoryPlusButton(object sender, RoutedEventArgs e)
        {
            this.AddMemory();
        }

        /// <summary>
        /// MRボタンを押したとき、メモリに保存された計算結果をメインテキストに表示します。
        /// </summary>
        private void ClickMemoryRecallButton(object sender, RoutedEventArgs e)
        {
            this.RecallMemory();
        }

        /// <summary>
        /// MCボタンを押したとき、メモリを全て消去します。
        /// </summary>
        private void ClickMemoryClearButton(object sender, RoutedEventArgs e)
        {
            this.ClearMemory();
        }

        /// <summary>
        /// SubTextに数値と演算記号が格納されている場合、計算を行います。
        /// </summary>
        private void Calculate()
        {
            // TODO: sub変数のスコープは、使う直前に宣言するほうがよいです。
            string sub = this.SubText.Text;

            bool isSuccess = Decimal.TryParse(this.MainText.Text, out var valueMain);
            if (!isSuccess)
            {
                return;
            }

            try
            {
                if ((sub == "") || (sub.Trim().Length == 0))
                {
                    return;
                }
                decimal valueSub = Decimal.Parse(sub.Remove(sub.Length - 1));
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
                // TODO: delete
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        private void ShowErrorMessage(Exception ex)
        {
            try
            {
                // DONE?: 再スローする必要はありません。
                // また再スローする場合は throw ex; と書けます。
                // 再スローの仕方によってエラー内容が変わることがあります。

                // QUESTION: 再スローしない場合、どうやってエラーをキャッチするのか調べてもよくわかりませんでした。

                throw (ex);
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("0で除算することはできません。");
            }
            catch (OverflowException)
            {
                MessageBox.Show("オーバーフローが発生しました。この計算は行えません。");
            }
            catch (ArithmeticException)
            {
                MessageBox.Show("計算中にエラーが発生しました。実行を中止します。");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("参照がNullです。");
            }
            catch (Exception)
            {
                MessageBox.Show("予期せぬエラーが発生しました。実行を中止します。");
            }
            finally
            {
                Console.WriteLine(ex.Message);
            }
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
            }
        }

        /// <summary>
        /// キーボードで数値キーを押下した際、対応する数値をメインテキストの末尾に表示する処理です。
        /// </summary>
        /// <param name="key">Keyはキーボードで押されたキー情報です。</param>
        private void PressNumberKey(Key key)
        {
            decimal result;
            switch (key)
            {
                case Key.D1:
                case Key.NumPad1:
                    result = Decimal.Parse(this.MainText.Text + 1);
                    this.MainText.Text = result.ToString();
                    break;
                case Key.D2:
                case Key.NumPad2:
                    result = Decimal.Parse(this.MainText.Text + 2);
                    this.MainText.Text = result.ToString();
                    break;
                case Key.D3:
                case Key.NumPad3:
                    result = Decimal.Parse(this.MainText.Text + 3);
                    this.MainText.Text = result.ToString();
                    break;
                case Key.D4:
                case Key.NumPad4:
                    result = Decimal.Parse(this.MainText.Text + 4);
                    this.MainText.Text = result.ToString();
                    break;
                case Key.D5:
                case Key.NumPad5:
                    result = Decimal.Parse(this.MainText.Text + 5);
                    this.MainText.Text = result.ToString();
                    break;
                case Key.D6:
                case Key.NumPad6:
                    result = Decimal.Parse(this.MainText.Text + 6);
                    this.MainText.Text = result.ToString();
                    break;
                case Key.D7:
                case Key.NumPad7:
                    result = Decimal.Parse(this.MainText.Text + 7);
                    this.MainText.Text = result.ToString();
                    break;
                case Key.D8:
                case Key.NumPad8:
                    result = Decimal.Parse(this.MainText.Text + 8);
                    this.MainText.Text = result.ToString();
                    break;
                case Key.D9:
                case Key.NumPad9:
                    result = Decimal.Parse(this.MainText.Text + 9);
                    this.MainText.Text = result.ToString();
                    break;
                case Key.D0:
                case Key.NumPad0:
                    result = Decimal.Parse(this.MainText.Text + 0);
                    this.MainText.Text = result.ToString();
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
            // TODO: this
            Cleartext(false);

        }
    }
}
