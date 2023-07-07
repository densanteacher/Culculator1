using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Permissions;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Serialization;
using Calculator1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;


namespace Calclator1
{
    public partial class Form1 : Form
    {
        public List<string> memory = new List<string>();
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            textBox1.Text = "0";
            textBox2.ReadOnly = true;


            //クリア（textBox1とtextBox2両方消す）ボタン
            this.C.Click += (s, e) =>
            {
                textBox1.Text = "0";
                textBox2.Text = null;
            };

            //CE（textBox1のみ消す）ボタン
            this.CE.Click += (s, e) => { textBox1.Text = "0"; };

            //+/-(プラスマイナス反転）ボタン
            this.Rev.Click += (s, e) =>
            {
                string txt1 = textBox1.Text;
                decimal inputRev = -decimal.Parse(txt1);
                textBox1.Text = inputRev.ToString();
            };

            //x^2(二乗)ボタン
            this.Sq.Click += (s, e) =>
            {
                string txt1 = textBox1.Text;
                decimal inputSq = decimal.Parse(txt1) * decimal.Parse(txt1);
                textBox1.Text = inputSq.ToString();
            };

            //√x（平方根）ボタン
            this.Sqrt.Click += (s, e) =>
            {
                string txt1 = textBox1.Text;
                double inputSqrt = Math.Sqrt(double.Parse(txt1));
                textBox1.Text = inputSqrt.ToString();
            };

            //1/xボタン
            this.Div.Click += (s, e) =>
            {
                string txt1 = textBox1.Text;
                decimal inputDiv = 1 / decimal.Parse(txt1);
                textBox1.Text = inputDiv.ToString();
            };

            //％ボタン
            this.Percent.Click += (s, e) =>
            {
                string txt1 = textBox1.Text;
                decimal inputRev = decimal.Parse(txt1) / 100;
                textBox1.Text = inputRev.ToString();
            };
            //M(メモリ）機能
            this.M.Click += (s, e) => {
                Form2 f = new Form2(memory, decimal.Parse(textBox1.Text));
                //Form2を表示する
                f.ShowDialog(this);
                //フォームが必要なくなったところで、Disposeを呼び出す
                f.Dispose();
            };
            //Ms（メモリ記録）機能
            this.MS.Click += (s, e) => {memory.Add(textBox1.Text);};

            //M-ボタン
            this.Mminus.Click += (s, e) =>
            {
                if (memory.Count > 0)
                {
                    string txt1 = textBox1.Text;
                    decimal memoryMinus = decimal.Parse(memory[0]) - decimal.Parse(txt1);
                    memory[0] = memoryMinus.ToString();
                }
            };
            //M+ボタン
            this.Mplus.Click += (s, e) =>
            {
                if(memory.Count > 0)
                {
                    string txt1 = textBox1.Text;
                    decimal memoryPlus = decimal.Parse(memory[0]) + decimal.Parse(txt1);
                    memory[0] = memoryPlus.ToString();
                }
            };


            //MR(メモリ呼び出し）ボタン
            this.MR.Click += (s, e) =>{if(memory.Count> 0) textBox1.Text = memory[0];};


            //MC(メモリクリア)ボタン
            this.MC.Click += (s, e) =>{ memory.Clear();};

        }

        //キー押下時数字と一部のキー以外は弾く処理
        private void numOnly_keyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
                return;
            if (e.KeyChar == '.' && !textBox1.Text.Contains('.'))
                return;

            //数値0～9以外が押された時はイベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar))
                e.Handled = true;


        }
        private void num_keyPress(Object sender, KeyPressEventArgs e)
        {
            Control[] cs = this.Controls.Find("button" + e.KeyChar, true);
            cs[0].Focus();
            
        }
        //数字ボタン
        private void btnNum_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            decimal inputNum = decimal.Parse(textBox1.Text + btn.Text);

            textBox1.Text = inputNum.ToString();
        }


        //.(小数点）ボタン
        private void btnDot_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            if (!textBox1.Text.Contains("."))
            {
                string inputDot = textBox1.Text + btn.Text;
                textBox1.Text = inputDot.ToString();
            }
        }

        //四則演算ボタン
        private void btnOpe_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            btnEq_Click(sender, new EventArgs());
            string inputOpe = textBox2.Text + textBox1.Text + btn.Text;
            textBox2.Text = inputOpe.ToString();
            textBox1.Text = "0";
        }

        //＝（イコール）ボタン
        private void btnEq_Click(object sender, EventArgs e)
        {
            if ((textBox2.Text != null) && (textBox2.Text.Trim().Length != 0))
            {
                string txt2 = textBox2.Text;
                string mem = txt2.Remove(txt2.Length - 1);
                decimal inputEq = 0;
                if (txt2.Contains("÷"))
                    inputEq = decimal.Parse(mem) / decimal.Parse(textBox1.Text);
                else if (txt2.Contains("×"))
                    inputEq = decimal.Parse(mem) * decimal.Parse(textBox1.Text);
                else if (txt2.Contains("-"))
                    inputEq = decimal.Parse(mem) - decimal.Parse(textBox1.Text);
                else if (txt2.Contains("+"))
                    inputEq = decimal.Parse(mem) + decimal.Parse(textBox1.Text);
                textBox1.Text = inputEq.ToString();
                textBox2.Text = null;
            }
        }

        //Back(一文字消す）ボタン
        private void btnBack_Click(object sender, EventArgs e)
        {
            string txt1 = textBox1.Text;
            string mem = txt1.Remove(txt1.Length - 1);
            if (mem.Length == 0 || mem == "-")
                textBox1.Text = "0";
            else
                textBox1.Text = mem;
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            string path = @"..\..\result.txt";
            /*if (!System.IO.File.Exists(path))
            { 
                File.Create(path);
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                path,
                false,
                System.Text.Encoding.GetEncoding("shift_jis"));*/
            using (FileStream fs = File.Create(path)) ;
            //TextBox1.Textの内容を1行ずつ書き込む
            Encoding enc = Encoding.GetEncoding("Shift_JIS");
            using (StreamWriter writer = new StreamWriter(path, false, enc))
            {
                foreach(string item in memory)
                {
                    writer.WriteLine(item);
                }
            }

        }

    }
}
