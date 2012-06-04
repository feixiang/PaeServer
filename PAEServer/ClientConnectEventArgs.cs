using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEServer
{
    /// <summary>
    /// 用户连接事件
    /// 用来向主线程传递参数
    /// </summary>
    class ClientConnectEventArgs : EventArgs
    {
        public string UserName { get; set; }
        public List<string> UserList { get; set; }
    }
}
