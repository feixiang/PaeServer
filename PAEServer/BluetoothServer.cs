using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace PAEServer
{
    /// <summary>
    /// 串口监听服务器
    /// </summary>
    class BluetoothServer
    {
        //定义事件处理句柄，用来向主界面发送消息
        //监听串口成功事件
        public event EventHandler<ComEventArgs> serverStartEvent;
        public event EventHandler<ComEventArgs> serverCloseEvent;
        //有用户连接事件
        public event EventHandler<ClientConnectEventArgs> clientConnectEvent;
        //接收到消息事件
        public event EventHandler<MessageEventArgs> msgReceivedEvent;

        private SerialPort comm;
        private Thread readThread;
        //控制线程循环
        private volatile bool keepReading;
        //键盘控制器
        private KeyboardController controller;

        public string mportName { get; set; }
        /**
         *自定义监听Com口 
         */
        public BluetoothServer(int baudRate, string portName, int dataBits)
        {
            initComm();
            this.mportName = portName;
            comm.BaudRate = baudRate;
            comm.PortName = portName;
            comm.DataBits = dataBits;
        }
        //默认com设置，只需传入端口名即可
        public BluetoothServer(string portName)
        {
            initComm();
            comm.BaudRate = 115200;
            comm.PortName = portName;
            comm.DataBits = 8;
        }
        protected void initComm()
        {
            comm = new SerialPort();
            readThread = null;
            keepReading = true;
            initController();
        }
        
        //注册键盘控制器
        private void initController()
        {
            controller = new KeyboardController();
            controller.initKeyboardController();
        }

        /// <summary>
        /// 开始监听该串口
        /// </summary>
        public void start()
        {
            if (!comm.IsOpen)
            {
                try
                {
                    comm.Open();
                    //startReading();
                    //使用消息触发事件
                    comm.DataReceived += new SerialDataReceivedEventHandler(comm_DataReceived);
                    Console.WriteLine(comm.PortName + "正在监听中");
                }
                catch (Exception)
                {
                    throw;
                }
                
                //向主线程发送消息
                if (serverStartEvent != null)
                {
                    ComEventArgs e = new ComEventArgs();
                    e.PortName = comm.PortName; 
                    this.serverStartEvent(this, e);
                }
            }
            else
                MessageBox.Show("打开串口失败");
        }

        private void comm_DataReceived(Object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int count = comm.BytesToRead;
            byte[] readBuffer = new byte[count];
            comm.Read(readBuffer, 0, count);
            String data = Encoding.ASCII.GetString(readBuffer);
            /**
             * 调用游戏手柄处理相应动作
             * */
            handleMessage(data);
        }

        private void startReading()
        {
            //使用keepReading标记位来控制线程的终止
            if (!keepReading)
            {
                keepReading = true;
                readThread = new Thread(new ThreadStart(readPort));
                readThread.Start();
            }
        }

        private void readPort()
        {
            while (keepReading)
            {
                if (comm.IsOpen)
                {
                    int count = comm.BytesToRead;
                    //有数据才接收
                    if (count > 0)
                    {
                        byte[] readBuffer = new byte[count];
                        comm.Read(readBuffer, 0, count);
                        //加上字符串结尾
                        String data = Encoding.ASCII.GetString(readBuffer).TrimEnd('\0');
                        Console.WriteLine(data);
                        //对消息进行分割
                        handleMessage(data);
                    }
                }
            }
        }
        //对上面接收到的消息进行初步处理
        private void handleMessage(string message)
        {
            Console.WriteLine(message);
            string[] tokens = message.Split('|');
            if (tokens[0] == Config.CLIENT_PRE)
            {
                ClientConnectEventArgs e = new ClientConnectEventArgs();
                e.UserName = tokens[0];
                //通知comServer服务器
                if (this.clientConnectEvent != null)
                {
                    //参数含义：第一个是sender消息发送者，第二个是消息的参数信息（包含要传递的信息：任何复杂对象！），可以无参数
                    this.clientConnectEvent(this, e);
                }
            }
            else
            {
                controller.handleMessage(message);
                //向主线程发送消息事件，让键盘控制器进行相应处理，也可以在这里直接处理
            }
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="data">消息</param>
        public void SendData(string data)
        {
            if (comm.IsOpen)
                comm.Write(data);
        }

        private void stopReading()
        {
            //优雅地关闭线程
            keepReading = false;
        }
        public void closeComm()
        {
            //停止线程
            stopReading();
            
            //关闭串口
            try
            {
                controller.closeKeyboardController();
                comm.Close();
                MessageBox.Show(this.mportName + "已经关闭");
            }
            catch (Exception)
            {
                //MessageBox.Show(this.mportName + "关闭失败");
                //关闭失败先不处理
            }
        }

    }
}
