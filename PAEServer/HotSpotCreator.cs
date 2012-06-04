using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PAEServer
{
    public partial class HotSpotCreator : Form
    {
        public HotSpotCreator()
        {
            InitializeComponent();
        }
        /**
         * 利用cmd命令创建wifi热点
         * */
        private void btCreateWifi_Click(object sender, EventArgs e)
        {
            string hotSpotName = wifiName.Text.Trim();
            string hotSpotPass = wifiPass.Text.Trim();
            if (hotSpotName.Equals(""))
            {
                MessageBox.Show("热点名不能为空");
                wifiName.Focus();
            }
            else if (hotSpotPass.Length < 8)
            {
                MessageBox.Show("密码须大于8位，请重新输入...");
                wifiPass.Focus();
            }
            else
            {
                if (createHotSpot(hotSpotName, hotSpotPass))
                    MessageBox.Show("wifi热点创建成功\n热点名为:" + hotSpotName + "\n密码为：" + hotSpotPass);
                else
                    MessageBox.Show("wifi热点创建失败");
            }
        }

        /** 
         * 执行Cmd命令创建wifi热点
         * */
        public Boolean createHotSpot(string hotSpotName, string hotSpotPass)
        {
            //创建wifi热点三部曲，仅适用于win7下
            string cmd1 = "netsh wlan set hostednetwork mode=allow";
            string cmd2 = "netsh wlan set hostednetwork ssid=" + hotSpotName + " key=" + hotSpotPass;
            string cmd3 = "netsh wlan start hostednetwork";
            //这句可以查询网卡是否支持虚拟网络,可以查找结果中的 " 支持的承载网络 ：是 "
            //string isDriverSupport = "netsh wlan show drivers";

            string[] cmd = new string[] { cmd1, cmd2, cmd3 };
            string rs = execMutipleCmd(cmd);
            return regexCheckIfSuccess(rs, "已启动承载网络");
        }
        /**
         * 用正则匹配是否成功
         * */
        public Boolean regexCheckIfSuccess(string msg, string reg)
        {
            Regex r = new Regex(reg);
            Match m = r.Match(msg);
            if (m.Success)
                return true;
            else
                return false;
        }

        private void stopWifi_Click(object sender, EventArgs e)
        {
            if (stopHotSpot())
                MessageBox.Show("禁止wifi热点成功");
            else
                MessageBox.Show("禁止操作失败");
        }

        public Boolean stopHotSpot()
        {
            string cmd = "netsh wlan stop hostednetwork";

            string rs = execSingleCmd(cmd);
            return regexCheckIfSuccess(rs, "已停止承载网络");
        }
        /**
         * 执行单条命令
         * */
        public static string execSingleCmd(string cmd)
        {
            //因为cmd直接在window system32目录下，所以无需加路径
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe");

            //设置不显示cmd窗口
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;

            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;

            Process process = Process.Start(startInfo);
            process.StandardInput.AutoFlush = true;

            process.StandardInput.WriteLine(cmd);
            process.StandardInput.WriteLine("exit");
            //等待程序执行完退出进程
            process.WaitForExit();
            //截获输出流
            string rs = process.StandardOutput.ReadToEnd();
            process.Close();
            return rs;
        }

        /**
         * 执行多条命令
         * */
        public static string execMutipleCmd(string[] cmd)
        {
            //因为cmd直接在window system32目录下，所以无需加路径
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe");

            //设置不显示cmd窗口
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;

            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;

            Process process = Process.Start(startInfo);
            process.StandardInput.AutoFlush = true;
            for (int i = 0; i < cmd.Length; ++i)
                process.StandardInput.WriteLine(cmd[i]);
            process.StandardInput.WriteLine("exit");
            //等待程序执行完退出进程
            process.WaitForExit();
            //截获输出流
            string rs = process.StandardOutput.ReadToEnd();
            process.Close();
            return rs;
        }

        //**********************************************************************************//


    }
}
