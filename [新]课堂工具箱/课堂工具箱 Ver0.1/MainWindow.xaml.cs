using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Panuon.UI.Silver;


namespace 课堂工具箱_Ver0._1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowX
    {
        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        public bool ChildWindowsProcess(IntPtr hwd, IntPtr lparam)
        {
            StringBuilder title = new StringBuilder(256);
            API.GetWindowText(hwd, title, title.Capacity);
            StringBuilder buff = new StringBuilder(256);
            GetClassName(hwd, buff, buff.Capacity);
            IntPtr EdithWnd = new IntPtr(0);
            EdithWnd = API.FindWindowEx(ACCESS.屏幕广播句柄, EdithWnd, buff.ToString(), title.ToString());
            API.EnableWindow(hwd, true);
            return true;
        }
        public MainWindow()
        {
            InitializeComponent();
            this.Title = ACCESS.版本标题;
            HOOK A = new HOOK();
            A.Show();
            void TimerMain()
            {
                //====================================================
                DispatcherTimer 解锁屏幕广播工具 = new DispatcherTimer();
                解锁屏幕广播工具.Tick += new EventHandler(解锁屏幕广播工具_Tick);
                解锁屏幕广播工具.Interval = TimeSpan.FromSeconds(1);
                解锁屏幕广播工具.Start();
                void 解锁屏幕广播工具_Tick(object sender, EventArgs e)
                {
                    if (解锁屏幕广播工具CB.IsChecked == true)
                    {
                        if (ACCESS.屏幕广播状态 == true)
                        {
                            API.EnumChildWindows(ACCESS.屏幕广播句柄, ChildWindowsProcess, IntPtr.Zero);
                        }
                    }
                }
                //====================================================
                DispatcherTimer 获取进程运行状态 = new DispatcherTimer();
                获取进程运行状态.Tick += new EventHandler(获取进程运行状态_Tick);
                获取进程运行状态.Interval = TimeSpan.FromSeconds(1);
                获取进程运行状态.Start();
                void 获取进程运行状态_Tick(object sender, EventArgs e)
                {
                    Process[] ProssesName = Process.GetProcessesByName("StudentMain");
                    if (ProssesName.Length == 0) 
                    { 
                        ACCESS.进程运行状态 = false;
                        进程运行状态LB.Content = "未运行";
                        var color = (Color)ColorConverter.ConvertFromString("#FF00FF00");
                        var brush = new SolidColorBrush(color);
                        进程运行状态LB.Foreground = brush;
                    }
                    else 
                    { 
                        ACCESS.进程运行状态 = true;
                        进程运行状态LB.Content = "已运行";
                        var color = (Color)ColorConverter.ConvertFromString("#E81123");
                        var brush = new SolidColorBrush(color);
                        进程运行状态LB.Foreground = brush;
                    }
                }
                //====================================================
                DispatcherTimer 获取屏幕广播状态 = new DispatcherTimer();
                获取屏幕广播状态.Tick += new EventHandler(获取屏幕广播状态_Tick);
                获取屏幕广播状态.Interval = TimeSpan.FromSeconds(1);
                获取屏幕广播状态.Start();
                void 获取屏幕广播状态_Tick(object sender, EventArgs e)
                {
                    ACCESS.屏幕广播句柄=API.FindWindow(null,"屏幕广播");
                    if (ACCESS.屏幕广播句柄 == (IntPtr)0)
                    {
                        ACCESS.屏幕广播状态 = false;
                        屏幕广播状态LB.Content = "未运行";
                        var color = (Color)ColorConverter.ConvertFromString("#FF00FF00");
                        var brush = new SolidColorBrush(color);
                        屏幕广播状态LB.Foreground = brush;
                    }
                    else
                    {
                        ACCESS.屏幕广播状态 = true;
                        屏幕广播状态LB.Content = "已运行";
                        var color = (Color)ColorConverter.ConvertFromString("#E81123");
                        var brush = new SolidColorBrush(color);
                        屏幕广播状态LB.Foreground = brush;
                    }
                }
                //====================================================
                DispatcherTimer 获取保持安静状态 = new DispatcherTimer();
                获取保持安静状态.Tick += new EventHandler(获取保持安静状态_Tick);
                获取保持安静状态.Interval = TimeSpan.FromSeconds(1);
                获取保持安静状态.Start();
                void 获取保持安静状态_Tick(object sender, EventArgs e)
                {
                    ACCESS.保持安静句柄 = API.FindWindow(null, "BlackScreen Window");
                    if (ACCESS.保持安静句柄 == (IntPtr)0)
                    {
                        ACCESS.保持安静状态 = false;
                        保持安静状态LB.Content = "未运行";
                        var color = (Color)ColorConverter.ConvertFromString("#FF00FF00");
                        var brush = new SolidColorBrush(color);
                        保持安静状态LB.Foreground = brush;
                    }
                    else
                    {
                        ACCESS.保持安静状态 = true;
                        保持安静状态LB.Content = "已运行";
                        var color = (Color)ColorConverter.ConvertFromString("#E81123");
                        var brush = new SolidColorBrush(color);
                        保持安静状态LB.Foreground = brush;
                    }
                }
                //====================================================
                DispatcherTimer 窗口置顶 = new DispatcherTimer();
                窗口置顶.Tick += new EventHandler(窗口置顶_Tick);
                窗口置顶.Interval = TimeSpan.FromSeconds(0.01);
                窗口置顶.Start();
                void 窗口置顶_Tick(object sender, EventArgs e)
                {
                    if (窗口置顶CB.IsChecked == true)
                    {
                        API.SetWindowPos(API.FindWindow(null, ACCESS.版本标题), -1, 0, 0, 0, 0, 1 | 2);
                    }
                    else
                    {
                        API.SetWindowPos(API.FindWindow(null, ACCESS.版本标题), -2, 0, 0, 0, 0, 1 | 2);
                    }
                }
                //====================================================
                DispatcherTimer 获取进程路径 = new DispatcherTimer();
                获取进程路径.Tick += new EventHandler(获取进程路径_Tick);
                获取进程路径.Interval = TimeSpan.FromSeconds(0.5);
                获取进程路径.Start();
                void 获取进程路径_Tick(object sender, EventArgs e)
                {
                    if (ACCESS.进程运行状态 == true)
                    {
                        Process[] process = Process.GetProcessesByName("StudentMain");
                        foreach (Process p in process)
                        {
                            ACCESS.进程路径 = p.MainModule.FileName.ToString();
                        }
                    }
                    else { }
                }
                //====================================================
                //SetWindowDisplayAffinity
                //https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-setwindowdisplayaffinity
            }
            TimerMain();
        }
        private void WindowX_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        void 结束进程()
        {
            Process[] process = Process.GetProcessesByName("StudentMain");
            foreach (Process killprocess in process)
            {
                killprocess.Kill();
            }
        }
        void 加载进程()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = ACCESS.进程路径;
            info.Arguments = "";
            Process.Start(info);
        }
        //===============================================================================================================
        private void 结束进程BT_Click(object sender, RoutedEventArgs e)
        {
            结束进程();
        }

        private void 加载进程BT_Click(object sender, RoutedEventArgs e)
        {
            if (ACCESS.进程运行状态 == false)
            {
                try
                {
                    加载进程();
                    //加载成功
                }
                catch (Exception)
                {
                    throw;//没有路径加载不了
                }
            }
            else
            {
                //已经运行了
            }
        }

        private void 重启进程BT_Click(object sender, RoutedEventArgs e)
        {
            if (ACCESS.进程运行状态 == false)
            {
               //进程未在运行
            }
            else
            {
                try
                {
                    结束进程();
                    加载进程();
                    //加载成功
                }
                catch (Exception)
                {
                    throw;//没有路径加载不了
                }
            }
            
        }

        private void 最小化屏幕广播BT_Click(object sender, RoutedEventArgs e)
        {
            API.ShowWindow(ACCESS.屏幕广播句柄,6);
        }

        private void 最小化保持安静BT_Click(object sender, RoutedEventArgs e)
        {
            API.ShowWindow(ACCESS.保持安静句柄, 6);
        }
    }
}

