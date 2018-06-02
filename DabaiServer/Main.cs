using DabaiServer.Common;
using NewLife.Log;
using NewLife.Net;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DabaiServer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        static TimerX _timer;
        static NetServer _server;
        static Int32 _serverPort;
        

        private void Main_Load(object sender, EventArgs e)
        {
            Runtime.ServerLog = this.ServerLog;
            _serverPort =Convert.ToInt32(textBoxServerPort.Text);
        }

        static void TestServer()
        {
            // 实例化服务端，指定端口，同时在Tcp/Udp/IPv4/IPv6上监听
            var svr = new MyNetServer
            {
                Port = _serverPort,
                Log = XTrace.Log
            };
            svr.Start();

            _server = svr;

            // 定时显示性能数据
            //_timer = new TimerX(ShowStat, svr, 100, 1000);
        }
        static void ShowStat(Object state)
        {
            var msg = "";
            if (state is NetServer ns)
                msg = ns.GetStat();
            else if (state is ISocketRemote ss)
                msg = ss.GetStat();

            Console.WriteLine(msg);
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                //TestServer();
                Thread WorkerThread = new Thread(TestServer);
                WorkerThread.Start();
                Runtime.ShowLog("*** 启动服务成功***  监听端口号：" + _serverPort);

            }
            catch (Exception ex)
            {
                Runtime.ShowLog("！！！ 启动服务失败！！！  详细：" + ex.Message);
                //LogHelper.log.Error("！！！ 启动服务失败！！！  详细：" + ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            Runtime.m_IsRunning = false;
            _server.Stop("手动停止服务...");
            Runtime.ShowLog("*** 停止服务成功***  详细：手动停止服务...");
        }
    }

   

    
}
