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
        // TODO: private のほうがよいでしょう。
        // TODO: new List<string>() → new() と省略できます。
        public List<string> memory = new List<string>();

        public MainWindow()
        {
            this.InitializeComponent();

            // TODO: 初期化用のメソッドを作るのがよさそうです。
            this.mainText.Text = "0";

            // TODO: ソースコードをコメントアウトするとスペースなしのコメントになります。そのため、通常のコメントにはスペースを入れたほうが読みやすくなります。
            // クリア（mainTextとsubText両方消す）ボタン
            this.C.Click += (s, e) =>
            {
                this.mainText.Text = "0";
                this.subText.Text = null;
            };

            //CE（mainTextのみ消す）ボタン
            this.CE.Click += (s, e) => { mainText.Text = "0"; };
            //+/-(プラスマイナス反転）ボタン
            this.Rev.Click += (s, e) =>
            {
                try
                {
                    string txt1 = mainText.Text;
                    decimal inputRev = -decimal.Parse(txt1);
                    mainText.Text = inputRev.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            };

            //Backボタン
            this.Back.Click += (s, e) =>
            {
                string txt1 = mainText.Text;
                string mem = txt1.Remove(txt1.Length - 1);
                if (mem.Length == 0 || mem == "-")
                    mainText.Text = "0";
                else
                    mainText.Text = mem;
            };

            //x^2(二乗)ボタン
            this.Sq.Click += (s, e) =>
            {
                try
                {
                    string txt1 = mainText.Text;
                    decimal inputSq = decimal.Parse(txt1) * decimal.Parse(txt1);
                    mainText.Text = inputSq.ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            };

            //√x（平方根）ボタン
            this.Sqrt.Click += (s, e) =>
            {
                try
                {
                    string txt1 = mainText.Text;
                    double inputSqrt = Math.Sqrt(double.Parse(txt1));
                    mainText.Text = inputSqrt.ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            };

            //1/xボタン
            this.Div.Click += (s, e) =>
            {
                try
                {
                    string txt1 = mainText.Text;
                    decimal inputDiv = 1 / decimal.Parse(txt1);
                    mainText.Text = inputDiv.ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            };

            //％ボタン
            this.Percent.Click += (s, e) =>
            {
                try
                {
                    string txt1 = mainText.Text;
                    decimal inputPer = decimal.Parse(txt1) / 100;
                    mainText.Text = inputPer.ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            };

            //小数点ボタン
            this.Dot.Click += (s, e) =>
            {
                Button btn = (Button)s;
                if (!mainText.Text.Contains("."))
                    mainText.Text += ".";
            };

            this.Memory.Click += (s, e) =>
            {
                decimal result = decimal.Parse(mainText.Text);
                MemoryWindow mw = new MemoryWindow(memory, result);
                mw.Show();
            };

            //Ms（メモリ記録）機能
            this.MS.Click += (s, e) => { memory.Add(mainText.Text); };

            //M-ボタン
            this.Mminus.Click += (s, e) =>
            {
                try
                {
                    if (memory.Count > 0)
                    {
                        string txt1 = mainText.Text;
                        decimal memoryMinus = decimal.Parse(memory[0]) - decimal.Parse(txt1);
                        memory[0] = memoryMinus.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            };
            //M+ボタン
            this.Mplus.Click += (s, e) =>
            {
                try
                {
                    if (memory.Count > 0)
                    {
                        string txt1 = mainText.Text;
                        decimal memoryPlus = decimal.Parse(memory[0]) + decimal.Parse(txt1);
                        memory[0] = memoryPlus.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            };


            //MR(メモリ呼び出し）ボタン
            this.MR.Click += (s, e) => { if (memory.Count > 0) mainText.Text = memory[0]; };


            // TODO: 処理のそばにコメントをつけるよりは、メソッド化するのがよいでしょう。
            // メソッド化することで処理に名前が付きます。またメソッドにドキュメンテーションコメントを付けることができます。
            this.MC.Click += (s, e) => { this.ClearMemory(); };
        }

        /// <summary>
        /// メモリをクリアします。
        /// </summary>
        private void ClearMemory()
        {
            memory.Clear();
        }

        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            // TODO: (Button)のキャストではなく、as によるキャストを推奨します。
            Button btn = (Button)sender;
            // TODO: decimal.Parse() の decimal は C# の型で、実際のクラスは Decimal になります。そのため、Decimal.Parse() とするほうが意味的には正確です。
            // TODO: imput→input
            // TODO: inputという英語は他動詞の意味もあるので、変数名は名詞となる方がよいです。またここでは mainText と Content の計算結果を格納しているので、resultText あたりのほうがよさそうです。
            decimal imputNum = decimal.Parse(mainText.Text + btn.Content);
            // TODO: mainText も this 参照をつけて揃えましょう。
            mainText.Text = imputNum.ToString();
        }

        private void btnOpe_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            // TODO: イベントメソッドを直接呼ぶのではなく、中身の処理をメソッド化して、それを呼び出しましょう。
            // sender と e に渡される内容が、このメソッドのものになるので、実際のイベントで呼び出すコントロールではなくなるのはバグの元です。
            btnEq_Click(sender, e);
            subText.Text = mainText.Text + btn.Content;
            mainText.Text = "0";
        }

        private void btnEq_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // TODO: 早期リターンに書き直してみましょう。
                if ((subText.Text != null) && (subText.Text.Trim().Length != 0))
                {
                    string txt2 = subText.Text;
                    string mem = txt2.Remove(txt2.Length - 1);
                    decimal inputEq = 0;
                    // TODO: if の中括弧は省略しないほうがよいです。稀ですが、マージしてインデントがズレた時に混乱の元となります。
                    if (txt2.Contains("÷"))
                        inputEq = decimal.Parse(mem) / decimal.Parse(mainText.Text);
                    else if (txt2.Contains("×"))
                        inputEq = decimal.Parse(mem) * decimal.Parse(mainText.Text);
                    else if (txt2.Contains("-"))
                        inputEq = decimal.Parse(mem) - decimal.Parse(mainText.Text);
                    else if (txt2.Contains("+"))
                        inputEq = decimal.Parse(mem) + decimal.Parse(mainText.Text);
                    mainText.Text = inputEq.ToString();
                    subText.Text = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
