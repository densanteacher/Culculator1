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

        /// <summary>
        /// 数字とバックスペース以外の入力を無効にしたTextBox
        /// </summary>
        public class NumericTextBox : TextBox
        {
            private const int WM_PASTE = 0x302;

            [SecurityPermission(SecurityAction.Demand,
                Flags = SecurityPermissionFlag.UnmanagedCode)]
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_PASTE)
                {
                    IDataObject iData = Clipboard.GetDataObject();
                    //文字列がクリップボードにあるか
                    if (iData != null && iData.GetDataPresent(DataFormats.Text))
                    {
                        string clipStr = (string)iData.GetData(DataFormats.Text);
                        //クリップボードの文字列が数字のみか調べる
                        if (!System.Text.RegularExpressions.Regex.IsMatch(
                            clipStr,
                            @"^[0-9]+$"))
                        {
                            return;
                        }
                    }
                }

                base.WndProc(ref m);
            }

            public NumericTextBox()
                : base()
            {
                //IMEを無効にする
                base.ImeMode = ImeMode.Disable;
                //数字以外で入力が可能な文字
                this.SetAllowKeyChars(new char[] { '\b', '.' });
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
            public new ImeMode ImeMode
            {
                get { return base.ImeMode; }
                set { }
            }

            private char[] _allowKeyChars;
            /// <summary>
            /// 数字以外で入力が可能な文字を設定する
            /// </summary>
            public void SetAllowKeyChars(char[] keyChars)
            {
                this._allowKeyChars = keyChars;
            }
            /// <summary>
            /// 数字以外で入力が可能な文字を取得する
            /// </summary>
            public char[] GetAllowKeyChars()
            {
                return this._allowKeyChars;
            }

            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                base.OnKeyPress(e);
                //数字以外が入力された時はキャンセルする
                if ((e.KeyChar < '0' || '9' < e.KeyChar) &&
                    Array.IndexOf(this._allowKeyChars, e.KeyChar) < 0)
                {
                    e.Handled = true;
                }
            }

            [SecurityPermission(SecurityAction.Demand,
                Flags = SecurityPermissionFlag.UnmanagedCode)]
            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                //Ctrl+VとShift+Insertを無効にする
                if (((keyData & Keys.Control) == Keys.Control &&
                    (keyData & Keys.KeyCode) == Keys.V) ||
                    ((keyData & Keys.Shift) == Keys.Shift &&
                    (keyData & Keys.KeyCode) == Keys.Insert))
                {
                    return true;
                }
                else
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
            }
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
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.Percent = new System.Windows.Forms.Button();
            this.CE = new System.Windows.Forms.Button();
            this.C = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.textBox1 = new Calclator1.Form1.NumericTextBox();
            this.textBox2 = new Calclator1.Form1.NumericTextBox();
            this.Div = new System.Windows.Forms.Button();
            this.Sq = new System.Windows.Forms.Button();
            this.Sqrt = new System.Windows.Forms.Button();
            this.Equal = new System.Windows.Forms.Button();
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
            // 
            // button12
            // 
            this.button12.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button12.Location = new System.Drawing.Point(160, 543);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(80, 80);
            this.button12.TabIndex = 9;
            this.button12.Text = "0";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // button13
            // 
            this.button13.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button13.Location = new System.Drawing.Point(332, 457);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(80, 80);
            this.button13.TabIndex = 15;
            this.button13.Text = "+";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.btnOpe_Click);
            // 
            // button14
            // 
            this.button14.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button14.Location = new System.Drawing.Point(332, 199);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(80, 80);
            this.button14.TabIndex = 14;
            this.button14.Text = "÷";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.btnOpe_Click);
            // 
            // button15
            // 
            this.button15.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button15.Location = new System.Drawing.Point(332, 285);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(80, 80);
            this.button15.TabIndex = 13;
            this.button15.Text = "×";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.btnOpe_Click);
            // 
            // button16
            // 
            this.button16.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button16.Location = new System.Drawing.Point(332, 371);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(80, 80);
            this.button16.TabIndex = 12;
            this.button16.Text = "-";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.btnOpe_Click);
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
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox1.Location = new System.Drawing.Point(74, 65);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(338, 34);
            this.textBox1.TabIndex = 20;
            // 
            // textBox2
            // 
            this.textBox2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox2.Location = new System.Drawing.Point(160, 40);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(252, 19);
            this.textBox2.TabIndex = 21;
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
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 629);
            this.Controls.Add(this.Equal);
            this.Controls.Add(this.Div);
            this.Controls.Add(this.Sq);
            this.Controls.Add(this.Sqrt);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Percent);
            this.Controls.Add(this.CE);
            this.Controls.Add(this.C);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.Rev);
            this.Controls.Add(this.Dot);
            this.Controls.Add(this.button12);
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
            this.Text = "Form1";
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
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button Percent;
        private System.Windows.Forms.Button CE;
        private System.Windows.Forms.Button C;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button Div;
        private System.Windows.Forms.Button Sq;
        private System.Windows.Forms.Button Sqrt;
        private System.Windows.Forms.Button Equal;
        private NumericTextBox textBox1;
        private NumericTextBox textBox2;
    }
}

