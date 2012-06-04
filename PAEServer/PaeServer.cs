using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PAEServer
{
    public partial class PaeServer : Form
    {
        /**
         * 在load事件中加入动态效果
         * */
        public const Int32 AW_HOR_POSITIVE = 0x00000001;    //自左向右显示窗体
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;    //自右向左显示窗体
        public const Int32 AW_VER_POSITIVE = 0x00000004;    //自上而下显示窗体
        public const Int32 AW_VER_NEGATIVE = 0x00000008;    //自下而上显示窗体
        public const Int32 AW_CENTER = 0x00000010;          //窗体向外扩展
        public const Int32 AW_HIDE = 0x00010000;            //隐藏窗体
        public const Int32 AW_ACTIVATE = 0x00020000;        //激活窗体
        public const Int32 AW_SLIDE = 0x00040000;           //使用滚动动画类型
        public const Int32 AW_BLEND = 0x00080000;           //使用淡入效果
        //AW_HIDE: 隐藏窗口
        //AW_ACTIVE: 激活窗口, 在使用了 AW_HIDE 效果时不可使用此效果
        //声明AnimateWindow函数
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        /**end 动态效果*/
        /*使窗体可拖动代码*/
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        /*end 使窗体可拖动代码*/

        //核心变量声明
        //局域网服务器
        private WifiServer wifiServer = null;
        //串口服务器
        private BluetoothServer bluetoothServer = null;
        //键盘控制器
        private KeyboardController controller;
        //当前连接数
        private int ConnectedCount = 0;

        public PaeServer()
        {
            InitializeComponent();

            initWifiServerEvents();//注册服务器事件
            initController();//初始化键盘控制器

            initComInfo(); // 初始化串口信息
            initNotifyIcon();//初始化托盘程序
        }

        //注册局域网服务器事件
        private void initWifiServerEvents()
        {
            wifiServer = new WifiServer(Config.SERVER_IP, Config.SERVER_PORT);
            wifiServer.serverStartEvent += new EventHandler(wifiServer_serverStartEvent);
            wifiServer.serverCloseEvent += new EventHandler(wifiServer_serverCloseEvent);
            wifiServer.msgReceivedEvent += new EventHandler<MessageEventArgs>(wifiServer_msgReceivedEvent);
            wifiServer.clientConnectEvent += new EventHandler<ClientConnectEventArgs>(wifiServer_clientConnectEvent);
        }
        //注册串口服务器事件
        private void initComServerEvents(BluetoothServer comServer)
        {
            comServer.serverStartEvent +=new EventHandler<ComEventArgs>(comServer_serverStartEvent);
            comServer.clientConnectEvent += new EventHandler<ClientConnectEventArgs>(comServer_clientConnectEvent);
            comServer.msgReceivedEvent += new EventHandler<MessageEventArgs>(comServer_msgReceivedEvent);
        }
        //注册键盘控制器
        private void initController()
        {
            controller = new KeyboardController();
            controller.initKeyboardController();
        }

        /**-----------------------下面是其他线程的事件处理函数-------------------**/
        //局域网服务器成功启动事件
        private void wifiServer_serverStartEvent(object sender, EventArgs e)
        {
            isWifiListening = true;
            this.wifiBt.Image = global::PAEServer.Properties.Resources.startedWifi;
            this.toolTip.SetToolTip(this.wifiBt, "点击关闭局域网服务器");
        }
        //局域网服务器关闭启动事件
        private void wifiServer_serverCloseEvent(object sender, EventArgs e)
        {
            isWifiListening = false;
            this.wifiBt.Image = global::PAEServer.Properties.Resources.startWifi;
        }
        //局域网客户连接事件
        private void wifiServer_clientConnectEvent(object sender, ClientConnectEventArgs e)
        {
            //添加一个panel显示该用户的用户名
            //每一次有用户连接则先清空面板，再刷新面板
            connectedPanel.Controls.Clear();
            //向主界面发送消息
            this.Invoke((EventHandler)(delegate
                 {
                     foreach (string username in e.UserList)
                     {
                         UserPanel userPanel = new UserPanel(username, wifiServer);
                         userPanel.Location = new System.Drawing.Point(0, ConnectedCount * 40);
                         this.connectedPanel.Controls.Add(userPanel);
                     }
                 }));
            ConnectedCount++;
        }
        //接收到局域网客户消息事件
        private void wifiServer_msgReceivedEvent(object sender, MessageEventArgs e)
        {
            string msg = e.Msg;
            controller.handleMessage(msg);
        }
        //下面是串口消息的处理函数
        //监听某个串口成功
        private void comServer_serverStartEvent(object sender, ComEventArgs e)
        {
           MessageBox.Show( e.PortName+"正在监听中...");
        }
        //处理串口用户连接事件
        private void comServer_clientConnectEvent(object sender, ClientConnectEventArgs e)
        {
            //向主界面发送消息
            this.Invoke((EventHandler)(delegate
            {
                UserPanel userPanel = new UserPanel(e.UserName,bluetoothServer);
                userPanel.Location = new System.Drawing.Point(0, ConnectedCount * 40);
                this.connectedPanel.Controls.Add(userPanel);
            }
                ));
        }
        //处理串口消息事件
        private void comServer_msgReceivedEvent(object sender, MessageEventArgs e)
        {

        }

        /*---------------UI按钮处理函数----------------------*/
        private void commBt_Click(object sender, EventArgs e)
        {
            if (ConnectedCount <= Config.MAXCONNECTION)
            {
                inputComInfoPanel.Visible = true;
            }
            else
            {
                MessageBox.Show("已达到最大连接数，请断开一些连接后再进行操作.");
            }
        }
        //开启局域网服务器
        private bool isWifiListening;
        private void wifiBt_Click(object sender, EventArgs e)
        {
            if (!isWifiListening)
            {
                wifiServer.startUdp();
            }
            else
            {
                wifiServer.closeServer();
            }
        }

        private void closeBt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comReturnBt_Click(object sender, EventArgs e)
        {
            returnToConnectionPanel();
        }
        private void returnToConnectionPanel()
        {
            inputComInfoPanel.Visible = false;
        }

        //添加串口监听
        private void comAddBt_Click(object sender, EventArgs e)
        {

            if (comPort.SelectedItem != null)
            {
                string port = comPort.SelectedItem.ToString();
                bluetoothServer = new BluetoothServer(port);
                //为这个串口注册事件
                initComServerEvents(bluetoothServer);
                bluetoothServer.start();
                returnToConnectionPanel();
            }
            else
                MessageBox.Show("请选择一个端口");
        }
        private void tools_Click(object sender, EventArgs e)
        {
            showToolsDialog();
        }

        //初始化串口需要填写的信息
        public void initComInfo()
        {
            //1，端口信息
            string[] portList = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i < portList.Length; ++i)
            {
                string port = portList[i];
                comPort.Items.Add(port);
            }
            //2，波特率信息
            for (int i = 0; i < Config.ComBaudRate.Length; ++i)
            {
                comBaudRate.Items.Add(Config.ComBaudRate[i]);
            }
            //3，比特位信息
            for (int i = 0; i < Config.ComBit.Length; ++i)
            {
                comBit.Items.Add(Config.ComBit[i]);
            }
            //设置默认选项
            //comPort.SelectedIndex = 0;
            //comBaudRate.SelectedIndex = 0;
            //comBit.SelectedIndex = 0;
        }

        //-----------------下面是托盘程序----------------//

        /// <summary>
        /// 隐藏窗体，显示托盘图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hideBt_Click(object sender, EventArgs e)
        {
            this.Hide();
            //然后显示托盘程序
            notifyIcon.BalloonTipText = "正在后台运行中...";
            notifyIcon.ShowBalloonTip(1000);
        }
        /**
         * 初始化托盘图标
         * */
        private void initNotifyIcon()
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem wifiCreatorItem = new MenuItem("创建Wifi虚拟热点");
            wifiCreatorItem.Click += new EventHandler(wifiCreatorItem_Click);
            MenuItem exitItem = new MenuItem("退出");
            exitItem.Click += new EventHandler(exitItem_Click);

            contextMenu.MenuItems.Add(wifiCreatorItem);
            contextMenu.MenuItems.Add(exitItem);
            notifyIcon.BalloonTipTitle = "无线控制平台";
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.Visible = true;

        }
        //托盘点击关闭事件
        private void exitItem_Click(object sender, EventArgs e)
        {
            exitPae();
        }
        private void wifiCreatorItem_Click(object sender, EventArgs e)
        {
            showToolsDialog();
        }
        private void showToolsDialog()
        {
            HotSpotCreator creator = new HotSpotCreator();
            creator.StartPosition = FormStartPosition.CenterScreen;
            creator.Show();
        }
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //显示主窗体
            this.Visible = true;
            //显示在系统任务栏 
            this.ShowInTaskbar = true;
        }

        /// <summary>
        /// 关闭窗体时释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaeServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            exitPae();
        }
        
        private void exitPae()
        {
            //先将托盘图标设置为不可见，再关闭
            notifyIcon.Visible = false;
            if (wifiServer != null) wifiServer.closeServer();
            //如果有n个串口，则要遍历每个串口来关闭
            if (bluetoothServer != null) bluetoothServer.closeComm();
            //关闭键盘控制器
            if (controller != null) controller.closeKeyboardController();
            this.Close();
        }

        private void PaeServer_Load(object sender, EventArgs e)
        {
            //淡入窗体动画效果，第二个参数为淡入时间，效果可以叠加
            AnimateWindow(this.Handle, 500, AW_BLEND);
            //这行可以强行与主线程的UI控件进行通信
            //CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 使窗口可拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaeServer_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);

        }

    }
}
