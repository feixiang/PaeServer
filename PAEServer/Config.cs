using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEServer
{
    class Config
    {
        public static String SERVER_IP = "192.168.2.1";
        public static String SERVER_PASS = "PAESERVER";
        public static int SERVER_PORT = 9090;

        public static int MAXCONNECTION = 4; 

        public static String CLIENT_PRE = "client";
        public static String CMD_PRE = "cmd";

        //串口的一些信息
        public static String[] ComBaudRate = { "115200","57600" ,"56000","9600"};
        public static String[] ComBit = {"8"};
    }
}
