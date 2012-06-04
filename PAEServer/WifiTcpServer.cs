using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Windows.Forms;

namespace PAEServer
{
    /// <summary>
    /// TCP服务器，基于有连接的通信
    /// </summary>
    class WifiTcpServer
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
        //键盘控制器
        private KeyboardController controller;
        //服务器监听线程
        private Thread listenerThread = null;
        private TcpListener tcpListener;
        //将用户的用户名与Socket对应
        private Dictionary<string, Socket> userlist = new Dictionary<string, Socket>();

        //线程一直接收消息的标记
        private volatile bool keepReading;
        //服务器线程一直监听的标记
        private volatile bool keepListening;

        public WifiTcpServer(string ipAddress, int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            initController();
        }
        
        //开启服务
        //注册键盘控制器
        private void initController()
        {
            controller = new KeyboardController();
            controller.initKeyboardController();
        }

        public void start()
        {
            keepReading = true;
            keepListening = true;
            listenerThread = new Thread(new ThreadStart(Work));
            tcpListener = new TcpListener(ipAddress, port);
            try
            {
                tcpListener.Start();
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

        public void Work()
        {
            while (keepListening)
            {
                Socket soket = tcpListener.AcceptSocket();
                //对每个新连接的用户开一个线程
                keepReading = true;
                Thread userThread = new Thread(
                         delegate()
                         {
                             ResponseClient(soket);
                         }
                    );
                userThread.Start();
            }

        }

        private void ResponseClient(Socket socket)
        {
            while (keepReading)
            {
                try
                {
                    int count = socket.Available;
                    //有数据才读
                    if (count > 0)
                    {
                        byte[] buffer = new byte[count];
                        //接收消息
                        socket.Receive(buffer);
                        //对TCP的数据，一定要加上结束符判断数据结束
                        string msg = String.Empty;
                        msg = Encoding.ASCII.GetString(buffer).TrimEnd('\0');
                        //与客户端协议好，处理用 | 分开的消息
                        // 前缀为client表示是新客户连接
                        // 前缀为message表示是数据消息
                        string[] tokens = msg.Split('|');
                        //连接成功后，先发客户端的用户名过来，再接收下一条信息
                        //如果是用户连接消息
                        if (tokens[0] == Config.CLIENT_PRE)
                        {
                            Console.WriteLine(tokens[1]);
                            //如果有用户已经存在，则不处理
                            if (this.userlist.Keys.Contains(tokens[0]))
                            {
                                break;
                            }
                            //否则将用户加入到用户列表中
                            else
                            {
                                //取后面的作为用户名
                                this.userlist.Add(tokens[1], socket);
                            }
                            //触发新用户加入事件，先设置参数
                            ClientConnectEventArgs e1 = new ClientConnectEventArgs();
                            e1.UserName = tokens[1];
                            e1.UserList = this.GetUserList();
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
                            MessageEventArgs e = new MessageEventArgs();
                            e.Msg = msg;
                            controller.handleMessage(msg);
                            //向主线程发送消息事件，让键盘控制器进行相应处理，也可以在这里直接处理
                            //if (this.msgReceivedEvent != null)
                            //{
                            //    this.msgReceivedEvent(this, e);
                            //}
                        }
                    }
                }
                catch (Exception)
                {
                    keepReading = false;
                }

            }
        }
        //获取用户列表，存储在事件参数中，传回给主界面
        private List<string> GetUserList()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, Socket> item in userlist)
            {
                list.Add(item.Key);
            }
            return list;
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {
            foreach (KeyValuePair<string, Socket> item in userlist)
            {
                byte[] buffer = Encoding.Unicode.GetBytes(message.ToCharArray());
                item.Value.Send(buffer);
            }
        }
        /// <summary>
        /// 通过用户名，关闭某一个socket
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Boolean closeASocket(String username)
        {
            Socket socket = null;
            bool isClose = false;
            if (userlist.TryGetValue(username, out socket))
            {
                try
                {
                    keepReading = false;
                    socket.Close();
                    isClose = true;
                    //从用户列表中删除此用户
                    userlist.Remove(username);
                }
                catch (Exception)
                {
                }
            }
            return isClose;
        }

        /// <summary>
        /// 遍历用户列表，关闭所有用户的Socket
        /// </summary>
        public void closeAllSockets()
        {
            keepReading = false;
            foreach (KeyValuePair<string, Socket> item in userlist)
            {
                item.Value.Close();
            }
        }

        public void updateUserList()
        {

        }

        public void closeServer()
        {
            //利用标志位优雅地关闭线程
            keepReading = false;
            keepListening = false;
            controller.closeKeyboardController();
            closeAllSockets();
            tcpListener.Stop();
            if (this.serverCloseEvent != null)
            {
                //向主线程发送事件触发消息
                this.serverCloseEvent(this, new EventArgs());
            }
        }
    }
}
