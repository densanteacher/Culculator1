﻿using System.ComponentModel;
using System;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Calclator1
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        
        
        
        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.Rev = new System.Windows.Forms.Button();
            this.Dot = new System.Windows.Forms.Button();
            this.button0 = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.Divide = new System.Windows.Forms.Button();
            this.Multiply = new System.Windows.Forms.Button();
            this.Subtract = new System.Windows.Forms.Button();
            this.Percent = new System.Windows.Forms.Button();
            this.CE = new System.Windows.Forms.Button();
            this.C = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.Div = new System.Windows.Forms.Button();
            this.Sq = new System.Windows.Forms.Button();
            this.Sqrt = new System.Windows.Forms.Button();
            this.Equal = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.M = new System.Windows.Forms.Button();
            this.MS = new System.Windows.Forms.Button();
            this.Mminus = new System.Windows.Forms.Button();
            this.Mplus = new System.Windows.Forms.Button();
            this.MC = new System.Windows.Forms.Button();
            this.MR = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(74, 457);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 80);
            this.button1.TabIndex = 0;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnNum_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(160, 457);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 80);
            this.button2.TabIndex = 1;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnNum_Click);
            this.button2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button3.Location = new System.Drawing.Point(246, 457);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 80);
            this.button3.TabIndex = 2;
            this.button3.Text = "3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnNum_Click);
            this.button3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button4.Location = new System.Drawing.Point(246, 371);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 80);
            this.button4.TabIndex = 5;
            this.button4.Text = "6";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnNum_Click);
            this.button4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button5.Location = new System.Drawing.Point(160, 371);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(80, 80);
            this.button5.TabIndex = 4;
            this.button5.Text = "5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnNum_Click);
            this.button5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button6.Location = new System.Drawing.Point(74, 371);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(80, 80);
            this.button6.TabIndex = 3;
            this.button6.Text = "4";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btnNum_Click);
            this.button6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button7.Location = new System.Drawing.Point(246, 285);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(80, 80);
            this.button7.TabIndex = 8;
            this.button7.Text = "9";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.btnNum_Click);
            this.button7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button8.Location = new System.Drawing.Point(160, 285);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(80, 80);
            this.button8.TabIndex = 7;
            this.button8.Text = "8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.btnNum_Click);
            this.button8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button9.Location = new System.Drawing.Point(74, 285);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(80, 80);
            this.button9.TabIndex = 6;
            this.button9.Text = "7";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.btnNum_Click);
            this.button9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Rev
            // 
            this.Rev.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Rev.Location = new System.Drawing.Point(74, 543);
            this.Rev.Name = "Rev";
            this.Rev.Size = new System.Drawing.Size(80, 80);
            this.Rev.TabIndex = 11;
            this.Rev.Text = "+/-";
            this.Rev.UseVisualStyleBackColor = true;
            this.Rev.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Dot
            // 
            this.Dot.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Dot.Location = new System.Drawing.Point(246, 543);
            this.Dot.Name = "Dot";
            this.Dot.Size = new System.Drawing.Size(80, 80);
            this.Dot.TabIndex = 10;
            this.Dot.Text = ".";
            this.Dot.UseVisualStyleBackColor = true;
            this.Dot.Click += new System.EventHandler(this.btnDot_Click);
            this.Dot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // button0
            // 
            this.button0.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button0.Location = new System.Drawing.Point(160, 543);
            this.button0.Name = "button0";
            this.button0.Size = new System.Drawing.Size(80, 80);
            this.button0.TabIndex = 9;
            this.button0.Text = "0";
            this.button0.UseVisualStyleBackColor = true;
            this.button0.Click += new System.EventHandler(this.btnNum_Click);
            this.button0.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Add
            // 
            this.Add.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Add.Location = new System.Drawing.Point(332, 457);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(80, 80);
            this.Add.TabIndex = 15;
            this.Add.Text = "+";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.btnOpe_Click);
            this.Add.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Divide
            // 
            this.Divide.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Divide.Location = new System.Drawing.Point(332, 199);
            this.Divide.Name = "Divide";
            this.Divide.Size = new System.Drawing.Size(80, 80);
            this.Divide.TabIndex = 14;
            this.Divide.Text = "÷";
            this.Divide.UseVisualStyleBackColor = true;
            this.Divide.Click += new System.EventHandler(this.btnOpe_Click);
            this.Divide.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Multiply
            // 
            this.Multiply.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Multiply.Location = new System.Drawing.Point(332, 285);
            this.Multiply.Name = "Multiply";
            this.Multiply.Size = new System.Drawing.Size(80, 80);
            this.Multiply.TabIndex = 13;
            this.Multiply.Text = "×";
            this.Multiply.UseVisualStyleBackColor = true;
            this.Multiply.Click += new System.EventHandler(this.btnOpe_Click);
            this.Multiply.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Subtract
            // 
            this.Subtract.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Subtract.Location = new System.Drawing.Point(332, 371);
            this.Subtract.Name = "Subtract";
            this.Subtract.Size = new System.Drawing.Size(80, 80);
            this.Subtract.TabIndex = 12;
            this.Subtract.Text = "-";
            this.Subtract.UseVisualStyleBackColor = true;
            this.Subtract.Click += new System.EventHandler(this.btnOpe_Click);
            this.Subtract.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Percent
            // 
            this.Percent.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Percent.Location = new System.Drawing.Point(74, 113);
            this.Percent.Name = "Percent";
            this.Percent.Size = new System.Drawing.Size(80, 80);
            this.Percent.TabIndex = 19;
            this.Percent.Text = "％";
            this.Percent.UseVisualStyleBackColor = true;
            this.Percent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // CE
            // 
            this.CE.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CE.Location = new System.Drawing.Point(160, 113);
            this.CE.Name = "CE";
            this.CE.Size = new System.Drawing.Size(80, 80);
            this.CE.TabIndex = 18;
            this.CE.Text = "CE";
            this.CE.UseVisualStyleBackColor = true;
            this.CE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // C
            // 
            this.C.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.C.Location = new System.Drawing.Point(246, 113);
            this.C.Name = "C";
            this.C.Size = new System.Drawing.Size(80, 80);
            this.C.TabIndex = 17;
            this.C.Text = "C";
            this.C.UseVisualStyleBackColor = true;
            this.C.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Back
            // 
            this.Back.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Back.Location = new System.Drawing.Point(332, 113);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(80, 80);
            this.Back.TabIndex = 16;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.btnBack_Click);
            this.Back.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Div
            // 
            this.Div.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Div.Location = new System.Drawing.Point(74, 199);
            this.Div.Name = "Div";
            this.Div.Size = new System.Drawing.Size(80, 80);
            this.Div.TabIndex = 24;
            this.Div.Text = "1/x";
            this.Div.UseVisualStyleBackColor = true;
            this.Div.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Sq
            // 
            this.Sq.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Sq.Location = new System.Drawing.Point(160, 199);
            this.Sq.Name = "Sq";
            this.Sq.Size = new System.Drawing.Size(80, 80);
            this.Sq.TabIndex = 23;
            this.Sq.Text = "x^2";
            this.Sq.UseVisualStyleBackColor = true;
            this.Sq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Sqrt
            // 
            this.Sqrt.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Sqrt.Location = new System.Drawing.Point(246, 199);
            this.Sqrt.Name = "Sqrt";
            this.Sqrt.Size = new System.Drawing.Size(80, 80);
            this.Sqrt.TabIndex = 22;
            this.Sqrt.Text = "√x";
            this.Sqrt.UseVisualStyleBackColor = true;
            this.Sqrt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Equal
            // 
            this.Equal.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Equal.Location = new System.Drawing.Point(332, 543);
            this.Equal.Name = "Equal";
            this.Equal.Size = new System.Drawing.Size(80, 80);
            this.Equal.TabIndex = 25;
            this.Equal.Text = "=";
            this.Equal.UseVisualStyleBackColor = true;
            this.Equal.Click += new System.EventHandler(this.btnEq_Click);
            this.Equal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox1.Location = new System.Drawing.Point(74, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(338, 34);
            this.textBox1.TabIndex = 20;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numOnly_keyPress);
            // 
            // textBox2
            // 
            this.textBox2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox2.Location = new System.Drawing.Point(160, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(252, 19);
            this.textBox2.TabIndex = 21;
            // 
            // M
            // 
            this.M.Location = new System.Drawing.Point(373, 89);
            this.M.Margin = new System.Windows.Forms.Padding(2);
            this.M.Name = "M";
            this.M.Size = new System.Drawing.Size(38, 18);
            this.M.TabIndex = 26;
            this.M.Text = "M";
            this.M.UseVisualStyleBackColor = true;
            this.M.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // MS
            // 
            this.MS.Location = new System.Drawing.Point(330, 89);
            this.MS.Margin = new System.Windows.Forms.Padding(2);
            this.MS.Name = "MS";
            this.MS.Size = new System.Drawing.Size(38, 18);
            this.MS.TabIndex = 27;
            this.MS.Text = "MS";
            this.MS.UseVisualStyleBackColor = true;
            this.MS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Mminus
            // 
            this.Mminus.Location = new System.Drawing.Point(287, 89);
            this.Mminus.Margin = new System.Windows.Forms.Padding(2);
            this.Mminus.Name = "Mminus";
            this.Mminus.Size = new System.Drawing.Size(38, 18);
            this.Mminus.TabIndex = 28;
            this.Mminus.Text = "M-";
            this.Mminus.UseVisualStyleBackColor = true;
            this.Mminus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Mplus
            // 
            this.Mplus.Location = new System.Drawing.Point(244, 89);
            this.Mplus.Margin = new System.Windows.Forms.Padding(2);
            this.Mplus.Name = "Mplus";
            this.Mplus.Size = new System.Drawing.Size(38, 18);
            this.Mplus.TabIndex = 29;
            this.Mplus.Text = "M+";
            this.Mplus.UseVisualStyleBackColor = true;
            this.Mplus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // MC
            // 
            this.MC.Location = new System.Drawing.Point(160, 89);
            this.MC.Margin = new System.Windows.Forms.Padding(2);
            this.MC.Name = "MC";
            this.MC.Size = new System.Drawing.Size(38, 18);
            this.MC.TabIndex = 30;
            this.MC.Text = "MC";
            this.MC.UseVisualStyleBackColor = true;
            this.MC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // MR
            // 
            this.MR.Location = new System.Drawing.Point(202, 89);
            this.MR.Margin = new System.Windows.Forms.Padding(2);
            this.MR.Name = "MR";
            this.MR.Size = new System.Drawing.Size(38, 18);
            this.MR.TabIndex = 31;
            this.MR.Text = "MR";
            this.MR.UseVisualStyleBackColor = true;
            this.MR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(74, 87);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 32;
            this.button10.Text = "Output";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.btnOutput_Click);
            this.button10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 661);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.MR);
            this.Controls.Add(this.MC);
            this.Controls.Add(this.Mplus);
            this.Controls.Add(this.Mminus);
            this.Controls.Add(this.MS);
            this.Controls.Add(this.M);
            this.Controls.Add(this.Equal);
            this.Controls.Add(this.Div);
            this.Controls.Add(this.Sq);
            this.Controls.Add(this.Sqrt);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Percent);
            this.Controls.Add(this.CE);
            this.Controls.Add(this.C);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.Divide);
            this.Controls.Add(this.Multiply);
            this.Controls.Add(this.Subtract);
            this.Controls.Add(this.Rev);
            this.Controls.Add(this.Dot);
            this.Controls.Add(this.button0);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "電卓";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button Rev;
        private System.Windows.Forms.Button Dot;
        private System.Windows.Forms.Button button0;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Divide;
        private System.Windows.Forms.Button Multiply;
        private System.Windows.Forms.Button Subtract;
        private System.Windows.Forms.Button Percent;
        private System.Windows.Forms.Button CE;
        private System.Windows.Forms.Button C;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button Div;
        private System.Windows.Forms.Button Sq;
        private System.Windows.Forms.Button Sqrt;
        private System.Windows.Forms.Button Equal;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private Button M;
        private Button MS;
        private Button Mminus;
        private Button Mplus;
        private Button MC;
        private Button MR;
        private Button button10;
    }
}

