using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEServer
{
    /**
     * 在这里调用控制器
     * 
     */
    class KeySimulator
    {
        public static void initController()
        {
            Controller.getController().initController();
        }
        public static void closeController()
        {
            Controller.getController().closeController();
        }

        public static void pressKey(int key)
        {
            Controller.getController().pressKey(key);
        }

        /**
         * 按下两个键
         * */
        public static void pressKey(int key1, int key2)
        {
            Controller.getController().pressTwoKey(key1, key2);
        }

        public static void pressNKeys(int[] keys)
        {
            Controller.getController().pressNKeys(keys);
        }
    }
}
