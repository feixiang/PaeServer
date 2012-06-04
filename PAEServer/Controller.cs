using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 使用单件模式获得控制器的唯一对象
 * */
namespace PAEServer
{
    sealed class Controller : ControlBase
    {
        private static Controller controller;

        public static Controller getController()
        {
            if (controller == null)
            {
                try
                {
                    controller = new Controller();
                    //controller.initController();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return controller;
        }
    }
}
