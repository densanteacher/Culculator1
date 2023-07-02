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
        }
        private void btnNum_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            decimal inputNum = decimal.Parse(textBox1.Text + btn.Text);

            textBox1.Text = inputNum.ToString();
        }
        private void btnDot_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (!textBox1.Text.Contains(".")){
                String inputDot = textBox1.Text + btn.Text;
                textBox1.Text = inputDot.ToString();
            }
        }
        private void btnOpe_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if((textBox2.Text != null) && (textBox2.Text.Trim().Length != 0))
            {
                btnEq_Click(sender, new EventArgs());
            }
            String inputOpe = textBox2.Text + textBox1.Text + btn.Text;
            textBox2.Text = inputOpe.ToString();
            textBox1.Text = "0";
        }
        private void btnEq_Click(object sender, EventArgs e)
        {
            String txt2 = textBox2.Text;
            String mem = txt2.Remove(txt2.Length - 1);
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
        private void btnBack_Click(object sender, EventArgs e)
        {
            String txt1 = textBox1.Text;
            String mem = txt1.Remove(txt1.Length - 1);
            if (mem.Length == 0)
                textBox1.Text = "0";
            else
                textBox1.Text = mem;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = null;
        }
        private void btnCE_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }
        private void btnReverse_Click(object sender, EventArgs e)
        {
            String txt1 = textBox1.Text;
            decimal inputRev = -decimal.Parse(txt1);
            textBox1.Text = inputRev.ToString();
        }
        private void btnSq_Click(object sender, EventArgs e)
        {
            String txt1 = textBox1.Text;
            decimal inputSq = decimal.Parse(txt1) * decimal.Parse(txt1);
            textBox1.Text = inputSq.ToString();
        }
        private void btnSqrt_Click(object sender, EventArgs e)
        {
            String txt1 = textBox1.Text;
            double inputSqrt = Math.Sqrt(double.Parse(txt1));
            textBox1.Text = inputSqrt.ToString();
        }
        private void btnDiv_Click(object sender, EventArgs e)
        {
            String txt1 = textBox1.Text;
            decimal inputDiv = 1 / decimal.Parse(txt1);
            textBox1.Text = inputDiv.ToString();
        }
    }
}
