using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices; 
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;

namespace 课堂工具箱_Ver0._1
{
    public partial class HOOK : Form
    {
        
        public HOOK()
        {
            InitializeComponent();
        }
        private KeyboardHook k_hook;
        private void HOOK_Load(object sender, EventArgs e)
        {
            k_hook = new KeyboardHook();
            k_hook.KeyDownEvent += new KeyEventHandler(hook_KeyDown);
            k_hook.Start();
            this.Hide();
        }
        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.End && (int)Control.ModifierKeys == (int)Keys.Control) // 安全退出
            {
                Process.GetCurrentProcess().Kill();
            }
            if (e.KeyValue == (int)Keys.Home && (int)Control.ModifierKeys == (int)Keys.Control) //隐藏
            {
                
                API.ShowWindow(API.FindWindow(null, ACCESS.版本标题), 6);
            }
        }

        private void HOOK_FormClosing(object sender, FormClosingEventArgs e)
        {
            k_hook.Stop();
        }
    }
}
