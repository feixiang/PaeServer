using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

namespace PAEServer
{
    /**
     *调用WinIO来模拟按键 
     **/
    class ControlBase
    {
        /**
         *先引入WinIo的函数 
         **/
        [DllImport("WinIo32.dll")]
        public static extern bool InitializeWinIo();
        [DllImport("WinIo32.dll")]
        public static extern bool GetPortVal(IntPtr wPortAddr, out int pdwPortVal,
                    byte bSize);
        [DllImport("WinIo32.dll")]
        public static extern bool SetPortVal(uint wPortAddr, IntPtr dwPortVal,
                    byte bSize);
        [DllImport("WinIo32.dll")]
        public static extern byte MapPhysToLin(byte pbPhysAddr, uint dwPhysSize,
                        IntPtr PhysicalMemoryHandle);
        [DllImport("WinIo32.dll")]
        public static extern bool UnmapPhysicalMemory(IntPtr PhysicalMemoryHandle,
                        byte pbLinAddr);
        [DllImport("WinIo32.dll")]
        public static extern bool GetPhysLong(IntPtr pbPhysAddr, byte pdwPhysVal);
        [DllImport("WinIo32.dll")]
        public static extern bool SetPhysLong(IntPtr pbPhysAddr, byte dwPhysVal);
        [DllImport("WinIo32.dll")]
        public static extern void ShutdownWinIo();
        [DllImport("user32.dll")]
        public static extern int MapVirtualKey(uint Ucode, uint uMapType);


        public const int KBC_KEY_CMD = 0x64; //键盘命令端口
        public const int KBC_KEY_DATA = 0x60; //键盘数据端口

        /**
         * 初始化和关闭WinIo
         */
        public Boolean initController()
        {
            if (InitializeWinIo())
                return true;
            else return false;
        }
        public void closeController()
        {
            ShutdownWinIo();
        }

        /**
         * 为什么要等输入缓冲区为空时才能写数据？
         * 因为键盘控制器很慢，代码执行得比它快得多，所以需要等待键盘控制器完成工作再执行我们的代码
         * 读0x64端口可以读出键盘的状态
         * 向控制器发送命令时，命令写到控制器的输入缓冲区。因此当输入缓冲区满的时候就不能执行
         * 通过检测状态寄存器的第1位（从0开始，用 | 0x2 来判断 ），可以检测输入缓冲区状态，为1时满，为0时空
         * 无论向0x60输出缓冲区,还是0x64输入缓冲区写东西前都要等状态寄存器变为0
         * */
        public void waitForKeyBoard()
        {
            int dwVal = 0;
            do
            {
                /**
                 * 读 读状态寄存器 读出键盘控制器的状态
                 * 读出的8位数据，格式如下：
                 *      第0位：输出缓冲区状态，
                 *             0表示输出缓冲区为空，不可读
                 *             1表示输出缓冲区为满，可读
                 *        1  ：输入缓冲区状态
                 *             0表示输入缓冲区为空，可写
                 *             1表示输入缓冲区为满，不可写
                 *        其他位这里用不上...
                 * 
                 **/
                bool flag = GetPortVal((IntPtr)0x64, out dwVal, 1);
            } while ((dwVal & 0x2) > 0);
            //while不断检测输入缓冲区，非0的时候跳转，否则继续等
        }

        public void keyDown(int key)
        {
            int btScancode = 0;
            btScancode = MapVirtualKey((byte)key, 0);
            //等待缓冲区为空
            waitForKeyBoard();
            //发送 写 命令
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            //等待缓冲区为空
            waitForKeyBoard();
            //写入该键
            SetPortVal(KBC_KEY_DATA, (IntPtr)btScancode, 1);
        }
        public void keyUp(int key)
        {
            int btScancode = 0;
            btScancode = MapVirtualKey((byte)key, 0);
            //等待缓冲区为空
            waitForKeyBoard();
            //发送 写 命令
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            //等待缓冲区为空
            waitForKeyBoard();
            //写入该键
            SetPortVal(KBC_KEY_DATA, (IntPtr)(btScancode | 0x80), 1);
        }

        /**
         * 扩展键按下
         * */
        public void keyDownEx(int key)
        {
            int btScancode = 0;
            btScancode = MapVirtualKey((byte)key, 0);
            //等待缓冲区为空
            waitForKeyBoard();
            //发送写 命令
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            //等待缓冲区为空
            waitForKeyBoard();
            //写入 是否为扩展键
            SetPortVal(KBC_KEY_DATA, (IntPtr)0xE0, 1);
            //等待缓冲区为空
            waitForKeyBoard();
            //发送写 命令
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            //等待缓冲区为空
            waitForKeyBoard();
            //写入该键
            SetPortVal(KBC_KEY_DATA, (IntPtr)btScancode, 1);
        }

        /**
         * 按键释放
         * */
        public void keyUpEx(int key)
        {
            int btScancode = 0;
            btScancode = MapVirtualKey((byte)key, 0);
            waitForKeyBoard();
            //发送写 命令
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            //等待缓冲区为空
            waitForKeyBoard();
            //写入 是否为扩展键
            SetPortVal(KBC_KEY_DATA, (IntPtr)0xE0, 1);
            //等待缓冲区为空
            waitForKeyBoard();
            //发送写 命令
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            //等待缓冲区为空
            waitForKeyBoard();
            //与按下键不同这处
            SetPortVal(KBC_KEY_DATA, (IntPtr)(btScancode | 0x80), 1);
        }

        /**
         *核心函数，模拟按键，加入默认延时 
         **/
        public void pressKey(int key)
        {
            keyDown(key);
            System.Threading.Thread.Sleep(100);
            keyUp(key);
        }
        public void pressKeyEx(int key)
        {
            keyDownEx(key);
            System.Threading.Thread.Sleep(100);
            keyUpEx(key);
        }
        /**
         * 重载一个可以加入延时的
         * 参数： time ->  延时时间 - 毫秒
         * */
        public void pressKey(int key, int time)
        {
            keyDown(key);
            System.Threading.Thread.Sleep(time);
            keyUp(key);
        }
        public void pressKeyEx(int key, int time)
        {
            keyDownEx(key);
            System.Threading.Thread.Sleep(time);
            keyUpEx(key);
        }

        /**
         * 按下两个键
         * */
        public void pressTwoKey(int key1, int key2)
        {
            keyDown(key1);
            System.Threading.Thread.Sleep(100);
            keyDown(key2);
            System.Threading.Thread.Sleep(100);
            keyUp(key1);
            System.Threading.Thread.Sleep(100);
            keyUp(key2);
            System.Threading.Thread.Sleep(100);
        }
        public void pressTwoKeyEx(int key1, int key2)
        {
            keyDownEx(key1);
            System.Threading.Thread.Sleep(100);
            keyDownEx(key2);
            System.Threading.Thread.Sleep(100);
            keyUpEx(key1);
            System.Threading.Thread.Sleep(100);
            keyUpEx(key2);
            System.Threading.Thread.Sleep(100);
        }
        /**
         * 按下两个键 , 重载一个可以加入延时的
         * 参数： time -> 延时时间 - 毫秒
         * */
        public void pressTwoKey(int key1, int key2, int time)
        {
            keyDown(key1);
            System.Threading.Thread.Sleep(time);
            keyDown(key2);
            System.Threading.Thread.Sleep(time);
            keyUp(key1);
            System.Threading.Thread.Sleep(time);
            keyUp(key2);
            System.Threading.Thread.Sleep(time);
        }
        public void pressTwoKeyEx(int key1, int key2, int time)
        {
            keyDownEx(key1);
            System.Threading.Thread.Sleep(time);
            keyDownEx(key2);
            System.Threading.Thread.Sleep(time);
            keyUpEx(key1);
            System.Threading.Thread.Sleep(time);
            keyUpEx(key2);
            System.Threading.Thread.Sleep(time);
        }

        /// <summary>
        /// 按下N个键，用循环
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        public void pressNKeys(int[] keys)
        {
            for (int i = 0; i < keys.Length; ++i)
            {
                keyDown(keys[i]);
                System.Threading.Thread.Sleep(100);
            }
            for (int i = 0; i < keys.Length; ++i)
            {
                keyUp(keys[i]);
                System.Threading.Thread.Sleep(100);
            } 
        }
    }
}
