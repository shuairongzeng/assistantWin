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
                // �����ͻ��� Socket
                using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    // ���ӵ�������
                    clientSocket.Connect(IPAddress.Parse(ipAddress), port);

                    // ������Ϣ
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    clientSocket.Send(data);

                    // ���շ�������Ӧ
                    byte[] buffer = new byte[1024];
                    int bytesRead = clientSocket.Receive(buffer);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // ��ʾ��������Ӧ��Ϣ
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
                // ����������Ϣ
                ConnectToServer(ipAddr, 12306, "00 Heartbeat");
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������ʱ����: " + ex.Message);
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

            // ��ȡ����������
            string localHostName = Dns.GetHostName();

            // ��ȡ���������� IP ��ַ�б�
            IPAddress[] localIPAddresses = Dns.GetHostAddresses(localHostName);

            // �����ź������Ʋ����߳���
            int maxThreads = 50;
            SemaphoreSlim semaphore = new SemaphoreSlim(maxThreads);

            // ���������б�
            var tasks = new List<Task>();

            // �������������� IP ��ַ�б�
            foreach (IPAddress ipAddress in localIPAddresses)
            {
                // ��� IPv4 ��ַ
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    // ��ȡ IP ��ַ��ǰ�����ֽ�
                    byte[] ipBytes = ipAddress.GetAddressBytes();
                    byte[] networkBytes = new byte[3];
                    Array.Copy(ipBytes, networkBytes, 3);

                    // ��������� IP ��ַ
                    string networkAddress = $"{networkBytes[0]}.{networkBytes[1]}.{networkBytes[2]}.";

                    // ���������� IP ��ַ��Χ���� 1 �� 254��
                    for (int i = 2; i <= 254; i++)
                    {
                        string targetAddress = networkAddress + i.ToString();

                        // ʹ���ź������Ʋ����߳���
                        semaphore.Wait();

                        // ������������� IP ��ַ
                        Task task = Task.Run(() =>
                        {
                            try
                            {
                                // ʹ�� Ping �෢�� ICMP ���󣬼���Ƿ��ܹ�������Ӧ
                                Ping ping = new Ping();
                                PingReply reply = ping.Send(targetAddress, 200);

                                if (reply.Status == IPStatus.Success)
                                {
                                    // �ɷ��ʵ� IP ��ַ
                                    Console.WriteLine("�ɷ��ʵ� IP ��ַ: " + targetAddress);
                                    list.Add(targetAddress);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("��� IP ��ַʱ����: " + ex.Message);
                            }
                            finally
                            {
                                // �ͷ��ź���
                                semaphore.Release();
                            }
                        });

                        // ��������ӵ������б�
                        tasks.Add(task);
                    }
                }
            }

            // �ȴ������������
            Task.WaitAll(tasks.ToArray());
            return list;
        }
    }
}