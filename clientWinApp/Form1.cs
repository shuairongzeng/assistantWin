using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Timers;
using System.Net.NetworkInformation;
using System.Collections.Generic;

namespace clientWinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string ipAddr = "";

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (ipAddr == "")
            {
                var list = CheckLocalNetwork();
                foreach (var item in list)
                {
                    richTextBox1.AppendText(item.ToString() + Environment.NewLine);
                    try
                    {
                        if (ConnectToServer(item.ToString(), 12306, "hello"))
                        {
                            ipAddr = item.ToString();
                            break;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            if (ipAddr != "")
            {
                heartbeatTimer.Enabled = true;
            }


        }

        private bool ConnectToServer(string ipAddress, int port, string message)
        {
            try
            {
                // 创建客户端 Socket
                using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    // 连接到服务器
                    clientSocket.Connect(IPAddress.Parse(ipAddress), port);

                    // 发送消息
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    clientSocket.Send(data);

                    // 接收服务器响应
                    byte[] buffer = new byte[1024];
                    int bytesRead = clientSocket.Receive(buffer);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // 显示服务器响应消息
                    return true;
                }
            }
            catch (Exception ex)
            {
              return false;
            }
        }

        private void heartbeatTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // 发送心跳消息
                ConnectToServer(ipAddr, 12306, "00 Heartbeat");
            }
            catch (Exception ex)
            {
                MessageBox.Show("心跳检测时出错: " + ex.Message);
            }
        }

        private void btn_seachSvc_Click(object sender, EventArgs e)
        {
            var list = CheckLocalNetwork();
            foreach (var item in list)
            {
                richTextBox1.AppendText(item.ToString() + Environment.NewLine);
            }
        }



        private List<string> CheckLocalNetwork()
        {
            List<string> list = new List<string>();

            // 获取本地主机名
            string localHostName = Dns.GetHostName();

            // 获取本地主机的 IP 地址列表
            IPAddress[] localIPAddresses = Dns.GetHostAddresses(localHostName);

            // 创建信号量控制并发线程数
            int maxThreads = 50;
            SemaphoreSlim semaphore = new SemaphoreSlim(maxThreads);

            // 创建任务列表
            var tasks = new List<Task>();

            // 遍历本地主机的 IP 地址列表
            foreach (IPAddress ipAddress in localIPAddresses)
            {
                // 检查 IPv4 地址
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    // 获取 IP 地址的前三个字节
                    byte[] ipBytes = ipAddress.GetAddressBytes();
                    byte[] networkBytes = new byte[3];
                    Array.Copy(ipBytes, networkBytes, 3);

                    // 构造局域网 IP 地址
                    string networkAddress = $"{networkBytes[0]}.{networkBytes[1]}.{networkBytes[2]}.";

                    // 遍历局域网 IP 地址范围（从 1 到 254）
                    for (int i = 2; i <= 254; i++)
                    {
                        string targetAddress = networkAddress + i.ToString();

                        // 使用信号量控制并发线程数
                        semaphore.Wait();

                        // 启动任务来检测 IP 地址
                        Task task = Task.Run(() =>
                        {
                            try
                            {
                                // 使用 Ping 类发送 ICMP 请求，检测是否能够接收响应
                                Ping ping = new Ping();
                                PingReply reply = ping.Send(targetAddress, 200);

                                if (reply.Status == IPStatus.Success)
                                {
                                    // 可访问的 IP 地址
                                    Console.WriteLine("可访问的 IP 地址: " + targetAddress);
                                    list.Add(targetAddress);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("检测 IP 地址时出错: " + ex.Message);
                            }
                            finally
                            {
                                // 释放信号量
                                semaphore.Release();
                            }
                        });

                        // 将任务添加到任务列表
                        tasks.Add(task);
                    }
                }
            }

            // 等待所有任务完成
            Task.WaitAll(tasks.ToArray());
            return list;
        }
    }
}