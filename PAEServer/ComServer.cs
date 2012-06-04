using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace PAEServer
{
    /// <summary>
    /// comServer用来管理所有的串口通信
    /// </summary>
    /// 
    class ComServer
    {
        //定义事件处理句柄，用来向主界面发送消息
        //监听串口成功事件
        public event EventHandler serverStartEvent;
        //有用户连接事件
        public event EventHandler<ClientConnectEventArgs> clientConnectEvent;
        //接收到消息事件
        public event EventHandler<MessageEventArgs> msgReceivedEvent;
        //监听串口成功事件
        public event EventHandler clientLostEvent;

        //存放用户的用户名与对应串口，串口中有对应的串口名
        private Dictionary<string, SerialPort> userList = new Dictionary<string, SerialPort>();
        KeyboardController controller; 

        public ComServer()
        {
            controller = new KeyboardController();
            controller.initKeyboardController();
        }
        private SerialPort com; 
        public Boolean addCom(int baudRate, string portName, int dataBits)
        {
            BluetoothServer server = new BluetoothServer(baudRate, portName, dataBits);
            initServerEvent(server);
            server.start();

            return true; 
        }


        private void initServerEvent(BluetoothServer server)
        {
            server.serverStartEvent +=new EventHandler<ComEventArgs>(server_serverStartEvent);
            server.clientConnectEvent +=new EventHandler<ClientConnectEventArgs>(server_clientConnectEvent);
        }
        //对服务器成功开启的消息统一处理
        private void server_serverStartEvent(object sender, EventArgs e)
        {
            if (this.serverStartEvent != null)
            {
                this.serverStartEvent(this, new EventArgs());
            }
        }
        //对用户连接消息则要分开是哪个用户
        private void server_clientConnectEvent(object sender, ClientConnectEventArgs e)
        {
            this.userList.Add(e.UserName, com);
            //触发新用户加入事件，先设置参数
            ClientConnectEventArgs e1 = new ClientConnectEventArgs();
            e1.UserName = e.UserName;
            e1.UserList = this.getUserList();
            //通知服务器界面
            if (this.clientConnectEvent != null)
            {
                //参数含义：第一个是sender消息发送者，第二个是消息的参数信息（包含要传递的信息：任何复杂对象！），可以无参数
                this.clientConnectEvent(this, e1);
            }
        }

        private void closeAcom(string comName)
        {
           
        }
        //所有监听的com列表
        private List<String> getUserList()
        {
            List<String> list = new List<string>();
            foreach (KeyValuePair<string, SerialPort> item in userList)
            {
                list.Add(item.Key);
            }
            return list; 
        }

        private void closeComServer()
        {
            controller.closeKeyboardController();
        }
    }
}
