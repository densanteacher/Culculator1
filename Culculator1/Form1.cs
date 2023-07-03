using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Culculator1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "0";

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
            this.Sqrt.Click += (s, e) => {
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
        }
        
        //数字ボタン
        private void btnNum_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            decimal inputNum = decimal.Parse(textBox1.Text + btn.Text);

            textBox1.Text = inputNum.ToString();
        }

        //.(小数点）ボタン
        private void btnDot_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (!textBox1.Text.Contains(".")){
                string inputDot = textBox1.Text + btn.Text;
                textBox1.Text = inputDot.ToString();
            }
        }

        //四則演算ボタン
        private void btnOpe_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if((textBox2.Text != null) && (textBox2.Text.Trim().Length != 0))
                btnEq_Click(sender,
                            new EventArgs());
            string inputOpe = textBox2.Text + textBox1.Text + btn.Text;
            textBox2.Text = inputOpe.ToString();
            textBox1.Text = "0";
        }

        //＝（イコール）ボタン
        private void btnEq_Click(object sender, EventArgs e)
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

        //Back(一文字消す）ボタン
        private void btnBack_Click(object sender, EventArgs e)
        {
            string txt1 = textBox1.Text;
            string mem = txt1.Remove(txt1.Length - 1);
            if (mem.Length == 0)
                textBox1.Text = "0";
            else
                textBox1.Text = mem;
        }
    }
}
