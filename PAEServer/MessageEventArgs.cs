using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace PAEServer
{
    //用户消息事件
    class MessageEventArgs : EventArgs
    {
        public string UserName { get; set; }
        public string Msg { get; set; }

        //public DateTime Time { get; set; }
        //public string Font { get; set; }
        //public string FontColor { get; set; }
        //public string FontSize { get; set; }
    }
}
