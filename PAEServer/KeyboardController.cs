using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PAEServer
{
    /// <summary>
    /// 这个是用来处理消息的接口
    /// </summary>
    class KeyboardController
    {
        public void initKeyboardController()
        {
            KeySimulator.initController();
        }
        public void closeKeyboardController()
        {
            KeySimulator.closeController();
        }

        //下面处理消息
        public void handleMessage(String msg)
        {
            Console.WriteLine("消息是"+msg);
            //直接将消息转成Int执行
            try
            {
                //判断是否是组合键
                string[] tokens = msg.Split('+');
                switch (tokens.Length)
                {
                    case 1 :
                        KeySimulator.pressKey(int.Parse(tokens[0]));
                        break; 
                    case 2:
                        KeySimulator.pressKey(int.Parse(tokens[0]), int.Parse(tokens[1]));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(msg + ":error" + e.StackTrace);
            }
        }
    }
}
