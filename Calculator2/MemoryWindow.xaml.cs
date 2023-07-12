﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calculator2
{
    /// <summary>
    /// MemoryWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MemoryWindow : Window
    {
        private List<string> _memories = new List<string>();
        public decimal _result;
        public int counter = 0;
        public MemoryWindow(List<string> memories, decimal result)
        {
            InitializeComponent();
            _memories = memories;
            _result = result;

            this.InitializeList();

            this.MemoryClear.Click += (s, e) => { this.ClearMemory(); };
            this.MemoryPlus.Click += (s, e) => { this.AddMemory(); };
            this.MemoryMinus.Click += (s, e) => { this.SubtractMemory(); };
            this.OK.Click += (s, e) => { this.Close(); };
        }

        /// <summary>
        /// リストを初期化した後、_memory内の要素を追加し表示します。
        /// </summary>
        public void InitializeList()
        {
            this.memoryList.Items.Clear();
            foreach (var item in _memories)
            {
                this.memoryList.Items.Add(item);
            }
            if(_memories.Count > 0)
            {
                this.memoryList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// リストボックスで選択されている値を削除します。
        /// </summary>
        private void ClearMemory()
        {
            if (_memories.Count == 0)
            {
                return;
            }
            string? selected = this.memoryList.SelectedItem.ToString();
            _memories.Remove(selected);
            this.memoryList.Items.Remove(selected);
            this.memoryList.SelectedIndex = 0;
        }

        /// <summary>
        /// リストボックスで選択されている値に、メインウィンドウのメインテキストに表示されている値を足します。
        /// </summary>
        private void AddMemory()
        {
            try
            {
                if (_memories.Count == 0)
                {
                    return;
                }
                string? selected = this.memoryList.SelectedItem.ToString();
                int index = this.memoryList.SelectedIndex;
                Decimal plusResult = Decimal.Parse(selected) + _result;
                _memories[index] = plusResult.ToString();
                this.InitializeList();
                this.memoryList.SelectedIndex = index;
            } catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
            
        }

        /// <summary>
        /// リストボックスで選択されている値から、メインウィンドウのメインテキストに表示されている値を引きます。
        /// </summary>
        private void SubtractMemory()
        {
            try
            {
                if (_memories.Count == 0)
                {
                    return;
                }
                string? selected = this.memoryList.SelectedItem.ToString();
                int index = this.memoryList.SelectedIndex;
                Decimal minusResult = Decimal.Parse(selected) - _result;
                _memories[index] = minusResult.ToString();
                this.InitializeList();
                this.memoryList.SelectedIndex = index;
            } catch(Exception ex)
            {
                ShowErrorMessage(ex);
            }
            
        }

        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        private void ShowErrorMessage(Exception ex)
        {
            try
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (OverflowException)
            {
                MessageBox.Show("オーバーフローが発生しました。この計算は行えません。");
            }
            catch (ArithmeticException)
            {
                MessageBox.Show("計算中にエラーが発生しました。実行を中止します。");
            }
            catch (Exception)
            {
                MessageBox.Show("予期せぬエラーが発生しました。実行を中止します。");
            }
            finally
            {
                global::System.Console.WriteLine(ex.Message);
            }
        }
    }
}
