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
        private readonly List<string> _memories = new();

        public MainWindow()
        {
            this.InitializeComponent();
            this.Cleartext(true);
        }

        // DONE: Initializeという名前は、最初に一回初期化するときだけ使いましょう。
        // 今回はテキストをクリアする目的なのでClearという単語を使うのがよいでしょう。
        /// <summary>
        /// テキストボックス初期化メソッドです。
        /// </summary>
        /// <param name="isSub">trueの時はSubTextも初期化し、falseの時はMainTextのみ初期化します。</param>
        private void Cleartext(bool isSub)
        {
            this.MainText.Text = "0";
            if (isSub == true)
            {
                this.SubText.Clear();
            }
        }

        // DONE: 反転という意味でのReverseはよいと思います。Reverseというメソッド名はStringクラスにもあります。なので符号の反転とわかるようにしてみましょう。
        // String.Reverse() についても調べてみましょう。
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
            string txt = this.MainText.Text;
            string bs = txt.Remove(txt.Length - 1);
            if (bs.Length == 0 || bs == "-")
            {
                // DONE: this
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
                // DONE: 変数宣言の型のDecimalはdecimalでよいです。
                decimal parsed = Decimal.Parse(txt);
                parsed *= parsed;
                this.MainText.Text = parsed.ToString();
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
                double parsed = Double.Parse(txt);
                parsed = Math.Sqrt(parsed);
                this.MainText.Text = parsed.ToString();
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
                decimal parsed = Decimal.Parse(txt);
                parsed = 1 / parsed;
                this.MainText.Text = parsed.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メインテキストの数値の百分率パーセンテージを求めます。
        /// </summary>
        private void GetPercentage()
        {
            try
            {
                string txt = this.MainText.Text;
                decimal parsed = Decimal.Parse(txt);
                parsed /= 100;
                this.MainText.Text = parsed.ToString();
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
            // DONE?: 例外が起こりそうなところはtry-catchがあるとよいでしょう。
            // 他にも該当箇所がありそうです。
            try
            {
                decimal result = Decimal.Parse(this.MainText.Text);

                // DONE: 今まで直した内容を MemoryWindow にも適用してみましょう。
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

        /// <summary>
        /// memoryリストに最初に追加された数値に、現在のメインテキストの数値を足します。
        /// </summary>
        private void AddMemory()
        {
            try
            {
                // DONE?: 早期リターン
                // 他にも早期リターンができる箇所があります。
                // 場合によるのですが、たいていはインデントが深くないほうが喜ばれます。
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
            if (this._memories.Count > 0)
            {
                this.MainText.Text = this._memories[0];
            };
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
                using (FileStream fs = File.Create(path)) { };
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
            // DONE: 変数の型として var を使ってみましょう。
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
            // DONE: var と is を組み合わせて使ってみましょう。

            if (sender is Button btn)
            {
                this.Calculate();
                this.SubText.Text = MainText.Text + btn.Content;
                this.Cleartext(false);
            }
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
            string sub = this.SubText.Text;
            bool isSuccess = Decimal.TryParse(this.MainText.Text, out var valueMain);
            if (!isSuccess)
            {
                return;
            }

            try
            {
                // DONE: C#のNullableについて調べてみましょう。
                // またC#8.0からnull参照許容型がデフォルトでオフになっています。
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
                Console.WriteLine(ex.Message);
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
                    MessageBox.Show("0で除算することはできません。");
                    break;
                case OverflowException:
                    MessageBox.Show("オーバーフローが発生しました。この計算は行えません。");
                    break;
                case ArithmeticException:
                    MessageBox.Show("計算中にエラーが発生しました。実行を中止します。");
                    break;
                case NullReferenceException:
                    MessageBox.Show("参照がNullです。実行できません。");
                    break;
                case Exception:
                    MessageBox.Show("予期せぬエラーが発生しました。実行を中止します。");
                    break;
            }
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
            }
        }

        // DONE: 押した際の処理というコメントを具体的にどういった処理をしているかを要約した内容に変更してみましょう。
        // summary(要約) 以外の情報を記載したい場合は、remarks(備考) というドキュメンテーションコメントが使えます。
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
            // DONE？: 「既に格納されている場合は、計算も行います。」ということですが、先に計算をしている？
            // 言いたいことは推測できますが、コメントが正しい状況を表現できていない状態になっているようです。
            // 格納済みが何をあらわすかはわかりませんが、素直にコメントとを読むと以下の処理のように読めます。
            //if (is格納済み)
            //{
            //    Calculate();
            //}

            // ANSWER：実際に格納されているか否かを判定するのはCalculate（）メソッド内の処理なので、そちらのSummaryに説明を移しました。

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
            Cleartext(false);

        }
    }
}
