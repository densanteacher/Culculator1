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

namespace Culculator1
{
    public partial class Form2 : Form
    {
        private List<string> _memory = new List<string>();
        public int counter = 0;
        public Form2(List<string> memory)
        {
            InitializeComponent();
            _memory = memory;

            foreach(string s in _memory)
            {
                addControl();
            }
        }
        public void addControl()
        {
            Label label = new Label();
            label.Location = new Point(12, 20 + counter * 50);
            label.Size = new Size(50, 19);
            label.TabIndex = counter;
            label.AutoSize = true;
            label.Text = _memory[counter];

            //パネルにラベルリンクを追加
            this.panel1.Controls.Add(label);

            Button btnMC = new Button();
            btnMC.Location = new Point(12, 40 + counter * 50);
            btnMC.Text = "MC";
            btnMC.Name = "MC" + counter.ToString();
            btnMC.Click += btnMC_Click;
            btnMC.Size = new System.Drawing.Size(75, 23);

            Button btnMplus = new Button();
            btnMplus.Location = new Point(87, 40 + counter * 50);
            btnMplus.Text = "M+";
            btnMplus.Name = "Mplus" + counter.ToString();
            btnMplus.Click += btnMplus_Click;
            btnMplus.Size = new System.Drawing.Size(75, 23);

            Button btnMminus = new Button();
            btnMminus.Location = new Point(162, 40 + counter * 50);
            btnMminus.Text = "M-";
            btnMminus.Name = "Mminus" + counter.ToString();
            btnMminus.Click += btnMminus_Click;
            btnMminus.Size = new System.Drawing.Size(75, 23);

            //パネルにボタンを追加
            this.panel1.Controls.Add(btnMC);
            this.panel1.Controls.Add(btnMplus);
            this.panel1.Controls.Add(btnMminus);

            counter++;

        }

        private void btnMC_Click (object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string btnNum = btn.Name.Substring(2);
            Control[] cs = this.Controls.Find("Label" + btnNum, true);
            _memory.RemoveAt(Int32.Parse(btnNum));
        }

        private void btnMplus_Click(object sender, EventArgs e) 
        { 
        
        }
        
        private void btnMminus_Click(object sender,EventArgs e)
        {

        }
    }
}
