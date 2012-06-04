using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace PAEServer
{
    class WifiServer
    {
        //三个事件句柄，要定义成public，用来向主界面发送消息
        //定义事件，将其与代理绑定
        public event EventHandler serverStartEvent; //普通触发事件，没有传递参数
        public event EventHandler serverCloseEvent;
        public event EventHandler<ClientConnectEventArgs> clientConnectEvent; //传递参数的事件
        public event EventHandler<MessageEventArgs> msgReceivedEvent;
        public event EventHandler clientLostEvent; //用户连接丢失

        //定义成员变量
        private IPAddress ipAddress { get; set; }
        private int port { get; set; }
        public WifiServer(string ipAddress, int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            initController();
        }

        //键盘控制器
        private KeyboardController controller;
        //服务器监听线程
        private Thread listenerThread = null;
        //UDP连接用户列表
        private List<string> udpUserList = new List<string>();

        //服务器线程一直监听的标记
        private volatile bool keepListening;
        //开启服务
        //注册键盘控制器
        private void initController()
        {
            controller = new KeyboardController();
            controller.initKeyboardController();
        }


        //使用UDP进行连接，试图解决粘包问题
        //UDP已经没有了客户端与服务端的区别了
        private UdpClient udpServer;
        private IPEndPoint ipEndPoint;
        public void startUdp()
        {
            keepListening = true;
            try
            {
                udpServer = new UdpClient(port);
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(Config.SERVER_IP), 0);
                listenerThread = new Thread(udpWork);
                listenerThread.IsBackground = true;
                listenerThread.Start();
                if (this.serverStartEvent != null)
                {
                    //向主线程发送事件触发消息
                    this.serverStartEvent(this, new EventArgs());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("开启服务器失败");
            }
        }
        //UDP处理接收消息的函数
        public void udpWork()
        {
            while (keepListening)
            {
                try
                {
                    int count = udpServer.Available;
                    //有数据才读
                    if (count > 0)
                    {
                        byte[] buffer = new byte[count];
                        //接收消息
                        buffer = udpServer.Receive(ref ipEndPoint);
                        string msg = Encoding.ASCII.GetString(buffer);
                        //与客户端协议好，处理用 | 分开的消息
                        // 前缀为client表示是新客户连接
                        // 无前缀表示是数据消息
                        string[] tokens = msg.Split('|');
                        //连接成功后，先发客户端的用户名过来，再接收下一条信息
                        //如果是用户连接消息
                        if (tokens[0] == Config.CLIENT_PRE)
                        {
                            Console.WriteLine(tokens[1]);
                            //如果有用户已经存在，则不处理
                            if (this.udpUserList.Contains(tokens[0]))
                            {
                                break;
                            }
                            //否则将用户加入到用户列表中
                            else
                            {
                                //取后面的作为用户名
                                this.udpUserList.Add(tokens[1]);
                            }
                            //触发新用户加入事件，先设置参数
                            ClientConnectEventArgs e1 = new ClientConnectEventArgs();
                            e1.UserName = tokens[1];
                            e1.UserList = udpUserList;
                            //通知服务器界面
                            if (this.clientConnectEvent != null)
                            {
                                //参数含义：第一个是sender消息发送者，第二个是消息的参数信息（包含要传递的信息：任何复杂对象！），可以无参数
                                this.clientConnectEvent(this, e1);
                            }
                        }
                        //否则认为是命令消息
                        //if (tokens[0] == Config.CMD_PRE)
                        else
                        {
                            controller.handleMessage(msg);
                            //MessageEventArgs e = new MessageEventArgs();
                            //e.Msg = msg;
                            ////向主线程发送消息事件，让键盘控制器进行相应处理，也可以在这里直接处理
                            //if (this.msgReceivedEvent != null)
                            //{
                            //    this.msgReceivedEvent(this, e);
                            //}
                        }
                    }
                }
                catch (Exception)
                {
                    keepListening = false;
                }

            }
        }

        public void closeServer()
        {
            //利用标志位优雅地关闭线程
            keepListening = false;
            controller.closeKeyboardController();
            try
            {
                if (udpServer != null)
                    udpServer.Close();
                if (this.serverCloseEvent != null)
                {
                    //向主线程发送事件触发消息
                    this.serverCloseEvent(this, new EventArgs());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("关闭服务器失败");
            }

        }
    }
}
