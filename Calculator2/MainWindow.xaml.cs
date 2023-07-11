using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace Calculator2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // DONE: private のほうがよいでしょう。
        // DONE: new List<string>() → new() と省略できます。
        private List<string> memory = new();

        public MainWindow()
        {
            this.InitializeComponent();

            // DONE: 初期化用のメソッドを作るのがよさそうです。
            this.TextInitialize(true);

            // DONE: ソースコードをコメントアウトするとスペースなしのコメントになります。そのため、通常のコメントにはスペースを入れたほうが読みやすくなります。
            // クリア（MainTextとSubText両方消す）ボタン
            this.C.Click += (s, e) => { this.TextInitialize(true); };
            // CE（MainTextのみ消す）ボタン
            this.CE.Click += (s, e) => { this.TextInitialize(false); };
            // +/-(プラスマイナス反転）ボタン
            this.Inv.Click += (s, e) => { this.Inversion(); };
            this.Back.Click += (s, e) =>{ this.BackSpace(); };

            // x^2(二乗)ボタン
            this.Sq.Click += (s, e) => { this.Square(); };

            // √x（平方根）ボタン
            this.Sqrt.Click += (s, e) => { this.SquareRoot(); };

            // 1/xボタン
            this.Div.Click += (s, e) => { this.DivideByX(); };

            // ％ボタン
            this.Perc.Click += (s, e) => { this.Percent(); };

            // 小数点ボタン
            this.Dot.Click += (s, e) => { this.DecimalPoint(s); };

            this.Memory.Click += (s, e) =>　{　this.MemoryWindowOpen(); };

            // Ms（メモリ記録）機能
            this.MemorySave.Click += (s, e) => { this.SaveMemory(); };

            // M-ボタン
            this.MemoryMinus.Click += (s, e) => { this.MinusMemory(); };
            // M+ボタン
            this.MemoryPlus.Click += (s, e) => { this.PlusMemory(); };


            // MR(メモリ呼び出し）ボタン
            this.MemoryRecall.Click += (s, e) => { this.RecallMemory(); };


            // DONE: 処理のそばにコメントをつけるよりは、メソッド化するのがよいでしょう。
            // メソッド化することで処理に名前が付きます。またメソッドにドキュメンテーションコメントを付けることができます。
            this.MemoryClear.Click += (s, e) => { this.ClearMemory(); };
        }

        /// <summary>
        /// テキストボックス初期化メソッド
        /// trueの時はメインサブ両方、falseの時はメインのみ
        /// </summary>
        /// <param name="sub"></param>
        private void TextInitialize(bool sub)
        {
            this.MainText.Text = "0";
            if (sub == true)
            {
                this.SubText.Text = null;
            }
        }

        /// <summary>
        /// メインテキストの数値の正負反転を反転します
        /// </summary>
        private void Inversion()
        {
            try{
                string txt1 = this.MainText.Text;
                decimal invResult = -decimal.Parse(txt1);
                this.MainText.Text = invResult.ToString();
            } catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        /// <summary>
        /// メインテキストの末尾一文字消去します
        /// </summary>
        private void BackSpace()
        {
            string txt1 = this.MainText.Text;
            string mem = txt1.Remove(txt1.Length - 1);
            if (mem.Length == 0 || mem == "-")
                TextInitialize(false);
            else
                this.MainText.Text = mem;
        }

        /// <summary>
        /// メインテキストの数値の2乗を求めます
        /// </summary>
        private void Square()
        {
            try
            {
                string txt1 = this.MainText.Text;
                Decimal sqResult = Decimal.Parse(txt1) * Decimal.Parse(txt1);
                this.MainText.Text = sqResult.ToString();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// メインテキストの数値の平方根を求めます
        /// </summary>
        private void SquareRoot()
        {
            try
            {
                string txt1 = this.MainText.Text;
                double sqrtResult = Math.Sqrt(double.Parse(txt1));
                this.MainText.Text = sqrtResult.ToString();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// メインテキストの数値をxとし、1/xを求めます
        /// </summary>
        private void DivideByX()
        {
            try
            {
                string txt1 = this.MainText.Text;
                Decimal divResult = 1 / Decimal.Parse(txt1);
                this.MainText.Text = divResult.ToString();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// メインテキストの数値の百分率パーセンテージを求めます
        /// </summary>
        private void Percent()
        {
            try
            {
                string txt1 = this.MainText.Text;
                Decimal perResult = Decimal.Parse(txt1) / 100;
                this.MainText.Text = perResult.ToString();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// メインテキストの数値に小数点を追加します
        /// </summary>
        /// <param name="sender"></param>
        private void DecimalPoint(object sender)
        {
            Button? btn = sender as Button;
            if (!this.MainText.Text.Contains("."))
                this.MainText.Text += ".";
        }

        /// <summary>
        /// MemoryWindowを表示します
        /// MemoryWindowには、現在のmemoryリストの一覧が表示されます
        /// </summary>
        private void MemoryWindowOpen()
        {
            Decimal result = Decimal.Parse(this.MainText.Text);
            MemoryWindow mw = new MemoryWindow(memory, result);
            mw.Owner = this;
            mw.Show();
        }

        /// <summary>
        /// memoryリストにメインテキストの数値を記録します。
        /// </summary>
        private void SaveMemory()
        {
            memory.Add(this.MainText.Text);
        }

        /// <summary>
        /// memoryリストに最初に追加された数値から、現在のメインテキストの数値を引きます
        /// </summary>
        private void MinusMemory()
        {
            try
            {
                if (memory.Count > 0)
                {
                    string txt1 = this.MainText.Text;
                    Decimal memoryMinus = Decimal.Parse(memory[0]) - Decimal.Parse(txt1);
                    memory[0] = memoryMinus.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// memoryリストに最初に追加された数値に、現在のメインテキストの数値を足します
        /// </summary>
        private void PlusMemory()
        {
            try
            {
                if (memory.Count > 0)
                {
                    string txt1 = this.MainText.Text;
                    Decimal memoryPlus = Decimal.Parse(memory[0]) + Decimal.Parse(txt1);
                    memory[0] = memoryPlus.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// memoryリストに最初に追加された数値を、メインテキストに再表示します
        /// </summary>
        private void RecallMemory()
        {
            if (memory.Count > 0) this.MainText.Text = memory[0];
        }

        /// <summary>
        /// メモリをクリアします。
        /// </summary>
        private void ClearMemory()
        {
            memory.Clear();
        }

        /// <summary>
        /// 押したボタンの数値をメインテキストに追加します
        /// </summary>
        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            // DONE: (Button)のキャストではなく、as によるキャストを推奨します。
            Button? btn = sender as Button;
            // DONE: Decimal.Parse() の Decimal は C# の型で、実際のクラスは Decimal になります。そのため、Decimal.Parse() とするほうが意味的には正確です。
            // DONE: imput→input
            // DONE: inputという英語は他動詞の意味もあるので、変数名は名詞となる方がよいです。またここでは MainText と Content の計算結果を格納しているので、resultText あたりのほうがよさそうです。
            Decimal resultText = Decimal.Parse(this.MainText.Text + btn.Content.ToString());
            // DONE: MainText も this 参照をつけて揃えましょう。
            this.MainText.Text = resultText.ToString();
        }

        /// <summary>
        /// 現在のメインテキストと四則演算の記号をサブテキストに格納します。
        /// 既に格納されている場合は、計算も行います。
        /// </summary>
        private void btnOpe_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;
            // DONE: イベントメソッドを直接呼ぶのではなく、中身の処理をメソッド化して、それを呼び出しましょう。
            // sender と e に渡される内容が、このメソッドのものになるので、実際のイベントで呼び出すコントロールではなくなるのはバグの元です。
            Decimal resultText = this.Calculation();
            this.SubText.Text = resultText.ToString() + btn.Content;
            TextInitialize(false);
        }

        /// <summary>
        /// 計算を行い、結果をメインテキストに表示します。
        /// </summary>
        private void btnEq_Click(object sender, RoutedEventArgs e)
        {
            /*         try
                     {
                         // DONE: 早期リターンに書き直してみましょう。
                         if ((SubText.Text != null) && (SubText.Text.Trim().Length != 0))
                         {
                             string subTxt = SubText.Text;
                             string mem = subTxt.Remove(subTxt.Length - 1);
                             Decimal inputEq = 0;
                             // DONE: if の中括弧は省略しないほうがよいです。稀ですが、マージしてインデントがズレた時に混乱の元となります。
                             if (subTxt.Contains("÷"))
                             {
                                 inputEq = Decimal.Parse(mem) / Decimal.Parse(MainText.Text);
                             } 
                             else if (subTxt.Contains("×"))
                             {
                                 inputEq = Decimal.Parse(mem) * Decimal.Parse(MainText.Text);
                             }
                             else if (subTxt.Contains("-"))
                             {
                                 inputEq = Decimal.Parse(mem) - Decimal.Parse(MainText.Text);
                             }
                             else if (subTxt.Contains("+"))
                             {

                                 inputEq = Decimal.Parse(mem) + Decimal.Parse(MainText.Text);
                             }
                             MainText.Text = inputEq.ToString();
                             SubText.Text = null;
                         }
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex.Message);
                     }*/
            Decimal resultText = this.Calculation();
            this.MainText.Text = resultText.ToString();
            this.SubText.Text = null;
        }

        /// <summary>
        /// 計算を行います
        /// </summary>
        /// <returns></returns>
        private Decimal Calculation()
        {

            Decimal dmain = Decimal.Parse(this.MainText.Text);
            try
            {
                string sub = this.SubText.Text;
                if ((sub == null) || (sub.Trim().Length == 0))
                {
                    return dmain;
                }
                Decimal dsub = Decimal.Parse(sub.Remove(sub.Length - 1));
                if (sub.Contains("÷"))
                {
                    return dsub / dmain;
                }
                else if (sub.Contains("×"))
                {
                    return dsub * dmain;
                }
                else if (sub.Contains("+"))
                {

                    return dsub + dmain;
                }
                else if (sub.Contains("-"))
                {
                    return dsub - dmain;
                }
                
                return dmain;
            } 
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                return dmain;
            }
        }
    }
}
