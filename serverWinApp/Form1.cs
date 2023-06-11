using System.Net.Sockets;
using System.Net;
using System.Text;

namespace serverWinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartServer(12306);
        }

        void StartServer(int port)
        {
            try
            {
                // 创建监听 Socket
                Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // 绑定端口和 IP 地址
                listenerSocket.Bind(new IPEndPoint(IPAddress.Any, port));

                // 开始监听客户端连接
                listenerSocket.Listen(10);

                // 在后台线程中接收客户端连接和消息
                Thread acceptThread = new Thread(() =>
                {
                    while (true)
                    {
                        // 接受客户端连接
                        Socket clientSocket = listenerSocket.Accept();

                        // 在后台线程中处理客户端消息
                        Thread receiveThread = new Thread(() =>
                        {
                            // 接收客户端消息
                            byte[] buffer = new byte[1024];
                            int bytesRead = clientSocket.Receive(buffer);
                            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                            // 处理客户端消息
                            string response = ProcessMessage(message);

                            // 发送响应消息给客户端
                            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                            clientSocket.Send(responseBytes);

                            // 关闭客户端连接
                            clientSocket.Close();
                        });

                        receiveThread.Start();
                    }
                });

                acceptThread.IsBackground = true;
                acceptThread.Start();

                lb_state.Text = "服务启动成功";
            }
            catch (Exception ex)
            {
                MessageBox.Show("启动服务时出错: " + ex.Message);
            }

        }

        string ProcessMessage(string message)
        {

            AppendToLog(message);

            // 在这里处理客户端发送的消息，并返回响应消息
            // ...
            if (!string.IsNullOrEmpty(message))
            {
                if (message.StartsWith("00 "))
                {
                    return "99 ok";
                }
            }

            return "receive: " + message;
        }

        private void AppendToLog(string message)
        {
            // 在日志框中追加消息，并换行
            if (rtxt_log.InvokeRequired)
            {
                rtxt_log.Invoke(new Action(() => rtxt_log.AppendText(message + Environment.NewLine)));
            }
            else
            {
                rtxt_log.AppendText(message + Environment.NewLine);
            }
        }

    }
}