using Culculator1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator1
{
    public partial class Form2 : Form
    {
        private List<string> _memory = new List<string>();
        public decimal _result;
        public int counter = 0;
        public Form2(List<string> memory, decimal result)
        {
            InitializeComponent();
            _memory = memory;
            _result = result;

            foreach(string s in _memory)
            {
                addControl();
            }
        }

        //ラベル、ボタンを動的に生成
        public void addControl()
        {
            Label label = new Label();
            label.Location = new Point(12, 20 + counter * 50);
            label.Size = new Size(50, 19);
            label.TabIndex = counter;
            label.AutoSize = true;
            label.Text = _memory[counter];
            label.Name = $"lbl{counter}";

            //パネルにラベルリンクを追加
            this.panel1.Controls.Add(label);

            Button btnMC = new Button();
            btnMC.Location = new Point(12, 40 + counter * 50);
            btnMC.Text = "MC";
            btnMC.Name = $"MC{counter}";
            btnMC.Click += btnMC_Click;
            btnMC.Size = new System.Drawing.Size(75, 23);

            Button btnMplus = new Button();
            btnMplus.Location = new Point(87, 40 + counter * 50);
            btnMplus.Text = "M+";
            btnMplus.Name = $"Mplus{counter}";
            btnMplus.Click += btnMplus_Click;
            btnMplus.Size = new System.Drawing.Size(75, 23);

            Button btnMminus = new Button();
            btnMminus.Location = new Point(162, 40 + counter * 50);
            btnMminus.Text = "M-";
            btnMminus.Name = $"Mminus{counter}";
            btnMminus.Click += btnMminus_Click;
            btnMminus.Size = new System.Drawing.Size(75, 23);

            //パネルにボタンを追加
            this.panel1.Controls.Add(btnMC);
            this.panel1.Controls.Add(btnMplus);
            this.panel1.Controls.Add(btnMminus);

            counter++;

        }

        //MCボタン（メモリクリア）
        private void btnMC_Click (object sender, EventArgs e)
        {
                Button btn = (Button)sender;
                string btnNum = btn.Name.Substring(2);
                _memory.RemoveAt(Int32.Parse(btnNum));
                panel1.Controls.Clear();
                counter = 0;
                foreach (string s in _memory)
                {
                    addControl();
                }
         
            
        }

        //M+ボタン
        private void btnMplus_Click(object sender, EventArgs e) 
        {
            try
            {
                Button btn = (Button)sender;
                int btnNum = Int32.Parse(btn.Name.Substring(5));
                Control[] cs = this.Controls.Find("lbl" + btnNum, true);
                decimal memoryPlus = decimal.Parse(_memory[btnNum]) + _result;
                _memory[btnNum] = Convert.ToString(memoryPlus);
                cs[0].Text = Convert.ToString(memoryPlus);
            } catch(Exception ex)
            {
                Form3 f = new Form3();
                f.ShowDialog(this);
                f.Dispose();
                Console.WriteLine(ex.Message);
            }
        }
        
        //M-ボタン
        private void btnMminus_Click(object sender,EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int btnNum = Int32.Parse(btn.Name.Substring(6));
                Control[] cs = this.Controls.Find("lbl" + btnNum, true);
                decimal memoryMinus = decimal.Parse(_memory[btnNum]) - _result;
                _memory[btnNum] = Convert.ToString(memoryMinus);
                cs[0].Text = Convert.ToString(memoryMinus);
            } catch (Exception ex)
            {
                Form3 f = new Form3();
                f.ShowDialog(this);
                f.Dispose();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
