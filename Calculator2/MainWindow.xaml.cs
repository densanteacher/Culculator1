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
        // DONE: private にしたら変数名に _(アンダースコア) のプレフィックスをつけてみましょう。他のページで _ をつけている物がありました。
        // DONE: 配列を扱う変数名は複数形にすると読みやすくなります。
        private List<string> _memories = new();

        public MainWindow()
        {
            this.InitializeComponent();

            this.TextInitialize(true);

            // クリア（MainTextとSubText両方消す）ボタン
            this.Clear.Click += (s, e) => { this.TextInitialize(true); };
            // CE（MainTextのみ消す）ボタン
            this.ClearEntry.Click += (s, e) => { this.TextInitialize(false); };
            // +/-(プラスマイナス反転）ボタン
            this.Inversion.Click += (s, e) => { this.Inverse(); };
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


            // DONE: 処理のそばにコメントをつけるよりは、メソッド化するのがよいでしょう。
            // メソッド化することで処理に名前が付きます。またメソッドにドキュメンテーションコメントを付けることができます。
            this.MemoryClear.Click += (s, e) => { this.ClearMemory(); };
        }

        // DONE: コメントの文章は 。 で終わらせるようにしましょう。(英語だとピリオド)
        // これは好みが分かれるところですが、きちんとした文章でコメントを書くほうがよいとされることが多いです。
        // DONE: メソッド名は基本は動詞とします。
        // DONE: param sub について、コメントを追加しましょう。
        // DONE: bool の変数名は isXxx としてください。動詞から始めるのはメソッド名ですが、bool 変数は例外です。
        /// <summary>
        /// テキストボックス初期化メソッドです。
        /// </summary>
        /// <param name="isSub">trueの時はSubTextも初期化し、falseの時はMainTextのみ初期化します。</param>
        private void TextInitialize(bool isSub)
        {
            this.MainText.Text = "0";
            if (isSub == true)
            {
                this.SubText.Text = null;
            }
        }

        // DONE: メソッド名は動詞から始めます。重要なのでもう一度指摘しておきます。
        /// <summary>
        /// メインテキストの数値の正負反転を反転します。
        /// </summary>
        private void Inverse()
        {
            // DONE: { は改行後におきましょう。Ctrl + K, D を活用するとよいでしょう。編集 -> 詳細 -> ドキュメントのフォーマット です。
            try
            {
                // DONE: txt → txt でよいでしょう。
                string txt = this.MainText.Text;
                // DONE: Parse と一緒に マイナスの記号をつけるとわかりにくいので、処理を分けたほうが無難です。
                // DONE: decimal.Parse → Decimal.Parse
                Decimal invResult = Decimal.Parse(txt);
                invResult = -invResult;
                this.MainText.Text = invResult.ToString();
            }
            catch (ArithmeticException ex)
            {
                this.ShowErrorMessage(ex);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// メインテキストの末尾一文字消去します。
        /// </summary>
        private void BackSpace()
        {
            string txt = this.MainText.Text;
            // DONE: mem という名前は MemoryStream 関連で使われるので、ここでは別の名前がよいでしょう。
            string bs = txt.Remove(txt.Length - 1);
            if (bs.Length == 0 || bs == "-")
                TextInitialize(false);
            else
                this.MainText.Text = bs;
        }

        /// <summary>
        /// メインテキストの数値の2乗を求めます。
        /// </summary>
        private void SquareOfX()
        {
            string txt = this.MainText.Text;
            try
            {
                Decimal sqResult = Decimal.Parse(txt);
                sqResult *= sqResult;
                this.MainText.Text = sqResult.ToString();

            }
            catch (ArithmeticException ex)
            {
                this.ShowErrorMessage(ex);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
                Console.WriteLine(ex.Message);
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
                double sqrtResult = Math.Sqrt(double.Parse(txt));
                this.MainText.Text = sqrtResult.ToString();
            }
            catch (ArithmeticException ex)
            {
                this.ShowErrorMessage(ex);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// メインテキストの数値をxとし、1/xを求めます。
        /// </summary>
        private void DivideByX()
        {
            string txt = this.MainText.Text;
            try
            {
                Decimal divResult = 1 / Decimal.Parse(txt);
                this.MainText.Text = divResult.ToString();
            }
            catch (ArithmeticException ex)
            {
                this.ShowErrorMessage(ex);
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
        private void GetPercentage()
        {
            try
            {
                string txt = this.MainText.Text;
                Decimal perResult = Decimal.Parse(txt) / 100;
                this.MainText.Text = perResult.ToString();
            }
            catch (ArithmeticException ex)
            {
                this.ShowErrorMessage(ex);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// メインテキストの数値に小数点を追加します。
        /// </summary>
        private void HitTheDecimalPoint()
        {
            // DONE: { } 中括弧は省略しないようにしましょう。
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
            // DONE?: この方法だとMemoryWindowが多重起動できてしまいます。

            MemoryWindow mw = new MemoryWindow(_memories, result);
            mw.Owner = this;
            mw.ShowDialog();
        }

        /// <summary>
        /// memoryリストにメインテキストの数値を記録します。
        /// </summary>
        private void SaveMemory()
        {
            _memories.Add(this.MainText.Text);
        }

        /// <summary>
        /// memoryリストに最初に追加された数値から、現在のメインテキストの数値を引きます。
        /// </summary>
        private void SubtractMemory()
        {
            try
            {
                if (_memories.Count > 0)
                {
                    string txt = this.MainText.Text;
                    Decimal memoryMinus = Decimal.Parse(_memories[0]) - Decimal.Parse(txt);
                    _memories[0] = memoryMinus.ToString();
                }
            }
            catch (ArithmeticException ex)
            {
                this.ShowErrorMessage(ex);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// memoryリストに最初に追加された数値に、現在のメインテキストの数値を足します。
        /// </summary>
        private void AddMemory()
        {
            try
            {
                if (_memories.Count > 0)
                {
                    string txt = this.MainText.Text;
                    Decimal memoryPlus = Decimal.Parse(_memories[0]) + Decimal.Parse(txt);
                    _memories[0] = memoryPlus.ToString();
                }
            }
            catch (ArithmeticException ex)
            {
                this.ShowErrorMessage(ex);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// メモリリストに最初に追加された数値を、メインテキストに再表示します。
        /// </summary>
        private void RecallMemory()
        {
            if (_memories.Count > 0) this.MainText.Text = _memories[0];
        }

        /// <summary>
        /// メモリをクリアします。
        /// </summary>
        private void ClearMemory()
        {
            _memories.Clear();
        }

        /// <summary>
        /// memoryに格納されている値をresult.txtに書き込みます。
        /// </summary>
        private void btnOutput_Click(object sender, EventArgs e)
        {
            string path = @"..\..\..\result.txt";
            using (FileStream fs = File.Create(path)) ;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding enc = Encoding.GetEncoding("Shift_JIS");
            using (StreamWriter writer = new StreamWriter(path, false, enc))
            {
                foreach (string item in _memories)
                {
                    writer.WriteLine(item);
                }
            }
            MessageBox.Show("記録した数値をテキストファイルに出力しました。");
        }

        /// <summary>
        /// 押したボタンの数値をメインテキストに追加します。
        /// </summary>
        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            // DONE: 変数の型として var を使ってみましょう。
            var btn = sender as Button;
            Decimal result = Decimal.Parse(this.MainText.Text + btn.Content.ToString());
            this.MainText.Text = result.ToString();
        }

        /// <summary>
        /// 現在のメインテキストと四則演算の記号をサブテキストに格納します。
        /// 既に格納されている場合は、計算も行います。
        /// </summary>
        private void btnOpe_Click(object sender, RoutedEventArgs e)
        {
            // DONE: 型を判定してかつ変換したい場合は is も使えます。どちらがよか検討してみましょう。
            if (sender is Button btn)
            {
                Decimal result = this.Calculate();
                this.SubText.Text = result.ToString() + btn.Content;
                TextInitialize(false);
            }
        }

        /// <summary>
        /// 計算を行い、結果をメインテキストに表示します。
        /// </summary>
        private void btnEq_Click(object sender, RoutedEventArgs e)
        {
            Decimal result = this.Calculate();
            this.MainText.Text = result.ToString();
            this.SubText.Text = null;
        }

        /// <summary>
        /// 計算を行います。
        /// </summary>
        /// <returns></returns>
        private Decimal Calculate()
        {
            // DONE: Parse() メソッドは例外を発生する可能性があります。try の中に入れましょう。もしくは TryParse() を使いましょう。
            Decimal valueMain;
            bool isSuccess = Decimal.TryParse(this.MainText.Text, out valueMain);
            if (!isSuccess)
            {
                return valueMain;
            }
            try
            {
                string sub = this.SubText.Text;
                if ((sub == null) || (sub.Trim().Length == 0))
                {
                    return valueMain;
                }
                // DONE: 型名+変数名という命名の仕方はハンガリアン記法と呼ばれます。その場合はmSubかdecSubとなります。mはdecimalのリテラルで使われるキーワードです。dだとdoubleの意味になります。
                // ただ、ハンガリアン記法はC#ではあまり使われないので違う命名のほうがよいでしょう。
                Decimal valueSub = Decimal.Parse(sub.Remove(sub.Length - 1));
                if (sub.Contains("÷"))
                {
                    return valueSub / valueMain;
                }
                else if (sub.Contains("×"))
                {
                    return valueSub * valueMain;
                }
                else if (sub.Contains("+"))
                {

                    return valueSub + valueMain;
                }
                else if (sub.Contains("-"))
                {
                    return valueSub - valueMain;
                }

                return valueMain;
            }
            catch (ArithmeticException ex)
            {
                this.ShowErrorMessage(ex);
                return valueMain;
            }
            catch (Exception ex)
            {
                // DONE: 自分のインスタンスのメソッドには this をつけましょう。
                this.ShowErrorMessage(ex);
                Console.WriteLine(ex.Message);
                return valueMain;
            }
        }

        // DONE: ドキュメンテーションコメントを書きましょう。
        // DONE: メソッド名は動詞から始めましょう。
        // DONE: Exception ex を引数として取れるようにして、表示されるメッセージに反映できるようにしてみましょう。
        /// <summary>
        /// エラーメッセージを表示します
        /// </summary>
        private void ShowErrorMessage(Exception ex)
        {
            try
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch(DivideByZeroException)
            {
                MessageBox.Show("0で除算することはできません。");
            }
            catch(OverflowException)
            {
                MessageBox.Show("オーバーフローが発生しました。この計算は行えません。");
            }
            catch(ArithmeticException)
            {
                MessageBox.Show("計算中にエラーが発生しました。実行を中止します。");
            }
            catch(Exception)
            {
                MessageBox.Show("予期せぬエラーが発生しました。実行を中止します。");
            }
            finally
            {
                global::System.Console.WriteLine(ex.Message);
            }
        }

        // DONE: sender, e は既定の引数で説明がなくてもわかりますので、その場合はコメント行を削除してしまいましょう。
        /// <summary>
        /// キー押下時、対応した数値の入力や四則演算を行います。
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            ButtonAutomationPeer peer;
            IInvokeProvider? provider;
            switch (e.Key)
            {
                case Key.Enter:
                    Decimal resultText = this.Calculate();
                    this.MainText.Text = resultText.ToString();
                    this.SubText.Text = null;
                    break;
                case Key.Back:
                    BackSpace();
                    break;
                case Key.Decimal:
                    HitTheDecimalPoint();
                    break;
                case Key.Divide:
                    // TODO: ボタンの押下をInvokeしていますが、BackSpace()やDecimalPoint()みたいにメソッドを呼び出す方がスッキリするでしょう。
                    peer = new ButtonAutomationPeer(this.Divide);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.Multiply:
                    peer = new ButtonAutomationPeer(this.Multiply);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.Subtract:
                    peer = new ButtonAutomationPeer(this.Subtract);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.Add:
                    peer = new ButtonAutomationPeer(this.Add);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.D1:
                case Key.NumPad1:
                    peer = new ButtonAutomationPeer(this.One);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.D2:
                case Key.NumPad2:
                    peer = new ButtonAutomationPeer(this.Two);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.D3:
                case Key.NumPad3:
                    peer = new ButtonAutomationPeer(this.Three);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.D4:
                case Key.NumPad4:
                    peer = new ButtonAutomationPeer(this.Four);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.D5:
                case Key.NumPad5:
                    peer = new ButtonAutomationPeer(this.Five);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.D6:
                case Key.NumPad6:
                    peer = new ButtonAutomationPeer(this.Six);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.D7:
                case Key.NumPad7:
                    peer = new ButtonAutomationPeer(this.Seven);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.D8:
                case Key.NumPad8:
                    peer = new ButtonAutomationPeer(this.Eight);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.D9:
                case Key.NumPad9:
                    peer = new ButtonAutomationPeer(this.Nine);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
                case Key.D0:
                case Key.NumPad0:
                    peer = new ButtonAutomationPeer(this.Zero);
                    provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    provider.Invoke();
                    break;
            }
        }
    }
}
