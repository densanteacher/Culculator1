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
        // DONE: インスタンスは一度だけnewして変わることがないので、readonlyをつけることができます。
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

        // DONE: メソッド名は動詞から始めます。例 ClearMemory()
        /// <summary>
        /// テキストボックス初期化メソッドです。
        /// </summary>
        /// <param name="isSub">trueの時はSubTextも初期化し、falseの時はMainTextのみ初期化します。</param>
        private void InitializeText(bool isSub)
        {
            this.MainText.Text = "0";
            if (isSub == true)
            {
                // DONE: TextBoxの内容を消去したい場合は、Clear()メソッドがあります。
                this.SubText.Clear();
            }
        }

        // DONE: Inverse の意味は、数学英語だと逆数となるようです。
        /// <summary>
        /// メインテキストの数値の正負反転を反転します。
        /// </summary>
        private void Reverse()
        {
            try
            {
                string txt = this.MainText.Text;
                // DONE: Parseした時点では、まだ反転していないのでinvResultと呼ばない方がよいでしょう。
                Decimal reverse = Decimal.Parse(txt);
                reverse = -reverse;
                this.MainText.Text = reverse.ToString();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
                // DONE: ShowErrorMessage()メソッドとセットで使われているので、ShowErrorMessage()メソッドの中にいれてしまいましょう。
            }
        }

        /// <summary>
        /// メインテキストの末尾一文字消去します。
        /// </summary>
        private void BackSpace()
        {
            string txt = this.MainText.Text;
            string bs = txt.Remove(txt.Length - 1);
            // DONE: if の中括弧抜けです。if でドキュメント内を検索し、全体的に見直してみましょう。
            if (bs.Length == 0 || bs == "-")
            {
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
            // DONE: この行がtryの中にあったり外にあったりすることが多いようです。統一しましょう。
            try
            {
                string txt = this.MainText.Text;
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
                // DONE: double.Parse→Double.Parse
                // DONE: ParseとSqrtは分けたほうが無難です。
                // ここは簡単な処理なので混乱しませんが、複雑な処理になると、どこで例外が発生しているかデバッグしにくくなります。
                // またInverseで符号逆転の処理を分けたので、コードの処理の粒度を揃える上でも分けたほうがよいでしょう。
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
            Decimal result = Decimal.Parse(this.MainText.Text);

            // UNDONE: この方法だとMemoryWindowが多重起動できてしまいます。
            MemoryWindow mw = new MemoryWindow(this._memories, result);
            mw.Owner = this;
            mw.ShowDialog();
        }

        /// <summary>
        /// memoryリストにメインテキストの数値を記録します。
        /// </summary>
        private void SaveMemory()
        {
            // DONE: _memoriesはMainWindowの持つインスタンスなので、thisをつけましょう。Shift+F12で参照箇所を検索するとよいでしょう。
            this._memories.Add(this.MainText.Text);
        }

        /// <summary>
        /// memoryリストに最初に追加された数値から、現在のメインテキストの数値を引きます。
        /// </summary>
        private void SubtractMemory()
        {
            try
            {
                // DONE: 早期リターンに変えてみましょう。
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
                // DONE: this抜け。他の箇所も見直してみましょう。
                // インスタンスのメソッドだということを常に意識することが大切です。
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
            // DONE: ここで変数宣言するのではなく、TryParse(xxxx, out var valueMain)とできます。
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
                // DONE: global:: を使ってみたかった？その意味も調べてみましょう。
                // ANSWER:cwにTabキー二回押したらこのように書かれました。単にショートカット使って横着しただけです。紛らわしくてすみません。
                global::System.Console.WriteLine(ex.Message);
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

        // DONE: DOCコメント
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

        // DONE: Downはボタンを押したという動詞にはなりません。
        /// <summary>
        /// 現在のメインテキストと四則演算の記号をサブテキストに格納します。
        /// 既に格納されている場合は、計算も行います。
        /// </summary>
        private void PressOperatorKey(Key key)
        {
            // DONE?: 「既に格納されている場合は、計算も行います。」ということですが、先に計算をしている？
            // 言いたいことは推測できますが、コメントが正しい状況を表現できていない状態になっているようです。
            this.Calculate();
            string result = this.MainText.Text;
            switch (key)
            {
                case Key.Divide:
                    // DONE: += という演算子が使えそうです。
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
