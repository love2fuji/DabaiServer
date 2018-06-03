using DabaiServer.Common;
using NewLife.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DabaiServer
{
    /// <summary>定义服务端，用于管理所有网络会话</summary>
    class MyNetServer : NetServer<MyNetSession>
    {
    }

    /// <summary>定义会话。每一个远程连接唯一对应一个网络会话，再次重复收发信息</summary>
    class MyNetSession : NetSession<MyNetServer>
    {
        /// <summary>客户端连接</summary>
        public override void Start()
        {
            base.Start();
            Runtime.ShowLog(string.Format("监听到客户端 ->{0}\r\n", Remote));
            // 欢迎语
            //var str = String.Format("Welcome to visit {1}!  [{0}]\r\n", Remote, Environment.MachineName);
            //Send(str);
        }

        /// <summary>收到客户端数据</summary>
        /// <param name="e"></param>
        protected override void OnReceive(ReceivedEventArgs e)
        {
            if (Runtime.m_IsSaveData)
            {
                WriteLog("收到客户端->{0}:{1} 的数据：{2}", e.Remote.Address, e.Remote.Port, e.Packet.ToStr());
            }
            //WriteLog("收到客户端 ->{0}:{1} 的数据：{2}", e.Remote.Address, e.Remote.Port, e.Packet.ToStr());
            //WriteLog("收到：{0}", e.Packet.ToStr());
            Runtime.ShowLog(string.Format("收到客户端->{0}:{1} 的数据：{2}", e.Remote.Address, e.Remote.Port, e.Packet.ToStr()));
            // 把收到的数据发回去
            Send(e.Packet);
        }
    }
}
