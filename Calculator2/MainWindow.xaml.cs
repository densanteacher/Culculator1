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
        public List<string> memory = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            this.mainText.Text = "0";

            //クリア（mainTextとsubText両方消す）ボタン
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


            //MC(メモリクリア)ボタン
            this.MC.Click += (s, e) => { memory.Clear(); };
        }
        

        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            decimal imputNum = decimal.Parse(mainText.Text + btn.Content);
            mainText.Text = imputNum.ToString();
        }

        private void btnOpe_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btnEq_Click(sender, e);
            subText.Text = mainText.Text + btn.Content;
            mainText.Text = "0";
        }

        private void btnEq_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((subText.Text != null) && (subText.Text.Trim().Length != 0))
                {
                    string txt2 = subText.Text;
                    string mem = txt2.Remove(txt2.Length - 1);
                    decimal inputEq = 0;
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
