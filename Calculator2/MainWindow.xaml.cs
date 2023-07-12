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

            this.InitializeText(true);


            // クリア（MainTextとSubText両方消す）ボタン
            this.Clear.Click += (s, e) => { this.InitializeText(true); };
            // CE（MainTextのみ消す）ボタン
            this.ClearEntry.Click += (s, e) => { this.InitializeText(false); };
            // +/-(プラスマイナス反転）ボタン
            this.Inversion.Click += (s, e) => { this.Reverse(); };
            this.Back.Click += (s, e) => { this.BackSpace(); };

            // x^2(二乗)ボタン
            this.Square.Click += (s, e) => { this.SquareOfX(); };

            // √x（平方根）ボタン
            this.SquareRoot.Click += (s, e) => { this.SquareRootOfX(); };

            // 1/xボタン
            this.DivideBy.Click += (s, e) => { this.DivideByX(); };

            // ％ボタン
            this.Percent.Click += (s, e) => { this.GetPercentage(); };

            // 小数点ボタン
            this.DecimalPoint.Click += (s, e) => { this.HitTheDecimalPoint(); };

            this.Memory.Click += (s, e) => { this.OpenMemoryWindow(); };

            // Ms（メモリ記録）機能
            this.MemorySave.Click += (s, e) => { this.SaveMemory(); };

            // M-ボタン
            this.MemoryMinus.Click += (s, e) => { this.SubtractMemory(); };
            // M+ボタン
            this.MemoryPlus.Click += (s, e) => { this.AddMemory(); };


            // MR(メモリ呼び出し）ボタン
            this.MemoryRecall.Click += (s, e) => { this.RecallMemory(); };

            this.MemoryClear.Click += (s, e) => { this.ClearMemory(); };
        }

        // TODO: Initializeという名前は、最初に一回初期化するときだけ使いましょう。
        // 今回はテキストをクリアする目的なのでClearという単語を使うのがよいでしょう。
        /// <summary>
        /// テキストボックス初期化メソッドです。
        /// </summary>
        /// <param name="isSub">trueの時はSubTextも初期化し、falseの時はMainTextのみ初期化します。</param>
        private void InitializeText(bool isSub)
        {
            this.MainText.Text = "0";
            if (isSub == true)
            {
                this.SubText.Clear();
            }
        }

        // TODO: 反転という意味でのReverseはよいと思います。Reverseというメソッド名はStringクラスにもあります。なので符号の反転とわかるようにしてみましょう。
        // String.Reverse() についても調べてみましょう。
        /// <summary>
        /// メインテキストの数値の正負反転を反転します。
        /// </summary>
        private void Reverse()
        {
            try
            {
                string txt = this.MainText.Text;
                // TODO: Parseした時点では、まだ反転していないのでreverseと呼ばない方が正確です。
                // 本来はこの程度は指摘しないのですが、なるべく正しい名前をつける習慣をつけておくほうがよいでしょう。
                Decimal reverse = Decimal.Parse(txt);
                reverse = -reverse;
                this.MainText.Text = reverse.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// メインテキストの末尾一文字消去します。
        /// </summary>
        private void BackSpace()
        {
            string txt = this.MainText.Text;
            string bs = txt.Remove(txt.Length - 1);
            if (bs.Length == 0 || bs == "-")
            {
                // TODO: this
                InitializeText(false);
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
                // TODO: 変数宣言の型のDecimalはdecimalでよいです。
                Decimal square = Decimal.Parse(txt);
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
                Double squareRoot = double.Parse(txt);
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
                Decimal divResult = 1 / Decimal.Parse(txt);
                this.MainText.Text = divResult.ToString();
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
                Decimal perResult = Decimal.Parse(txt) / 100;
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
            // TODO: 例外が起こりそうなところはtry-catchがあるとよいでしょう。
            // 他にも該当箇所がありそうです。
            Decimal result = Decimal.Parse(this.MainText.Text);

            // TODO: 今まで直した内容を MemoryWindow にも適用してみましょう。
            MemoryWindow mw = new MemoryWindow(this._memories, result);
            mw.Owner = this;
            mw.ShowDialog();
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
                Decimal memoryMinus = Decimal.Parse(this._memories[0]) - Decimal.Parse(txt);
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
                // TODO: 早期リターン
                // 他にも早期リターンができる箇所があります。
                // 場合によるのですが、たいていはインデントが深くないほうが喜ばれます。
                if (this._memories.Count > 0)
                {
                    string txt = this.MainText.Text;
                    Decimal memoryPlus = Decimal.Parse(this._memories[0]) + Decimal.Parse(txt);
                    this._memories[0] = memoryPlus.ToString();
                }
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
        /// memoryに格納されている値をresult.txtに書き込みます。
        /// </summary>
        private void ClickOutputButton(object sender, EventArgs e)
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

        /// <summary>
        /// 押したボタンの数値をメインテキストに追加します。
        /// </summary>
        private void ClickNumberButton(object sender, RoutedEventArgs e)
        {
            // DONE: 変数の型として var を使ってみましょう。
            var btn = sender as Button;
            Decimal result = Decimal.Parse(this.MainText.Text + btn.Content.ToString());
            this.MainText.Text = result.ToString();
        }

        /// <summary>
        /// 現在のメインテキストと四則演算の記号をサブテキストに格納します。
        /// 既に格納されている場合は、計算を行ってからサブテキストに格納します。
        /// </summary>
        private void ClickOperatorButton(object sender, RoutedEventArgs e)
        {
            // TODO: var と is を組み合わせて使ってみましょう。
            if (sender is Button btn)
            {
                this.Calculate();
                this.SubText.Text = MainText.Text + btn.Content;
                this.InitializeText(false);
            }
        }

        /// <summary>
        /// 計算を行い、結果をメインテキストに表示します。
        /// </summary>
        private void ClickEqualButton(object sender, RoutedEventArgs e)
        {
            this.Calculate();
        }

        /// <summary>
        /// 計算を行います。
        /// </summary>
        /// <returns></returns>
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
                // TODO: C#のNullableについて調べてみましょう。
                // またC#8.0からnull参照許容型がデフォルトでオフになっています。
                if ((sub == null) || (sub.Trim().Length == 0))
                {
                    return;
                }
                Decimal valueSub = Decimal.Parse(sub.Remove(sub.Length - 1));
                Decimal result = 0;
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
            try
            {
                // TODO: 再スローする必要はありません。
                // また再スローする場合は throw ex; と書けます。
                // 再スローの仕方によってエラー内容が変わることがあります。
                throw(ex);
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
                    // TODO: this
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

        // TODO: 押した際の処理というコメントを具体的にどういった処理をしているかを要約した内容に変更してみましょう。
        // summary(要約) 以外の情報を記載したい場合は、remarks(備考) というドキュメンテーションコメントが使えます。
        /// <summary>
        /// キーボードで数値キーを押下した際の処理です。
        /// </summary>
        /// <param name="key">Keyはキーボードで押されたキー情報です。</param>
        private void PressNumberKey(Key key)
        {
            Decimal result;
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
        /// 既に格納されている場合は、計算も行います。
        /// </summary>
        private void PressOperatorKey(Key key)
        {
            // TODO: 「既に格納されている場合は、計算も行います。」ということですが、先に計算をしている？
            // 言いたいことは推測できますが、コメントが正しい状況を表現できていない状態になっているようです。
            // 格納済みが何をあらわすかはわかりませんが、素直にコメントとを読むと以下の処理のように読めます。
            //if (is格納済み)
            //{
            //    Calculate();
            //}

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
            InitializeText(false);

        }
    }
}
