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
                // �������� Socket
                Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // �󶨶˿ں� IP ��ַ
                listenerSocket.Bind(new IPEndPoint(IPAddress.Any, port));

                // ��ʼ�����ͻ�������
                listenerSocket.Listen(10);

                // �ں�̨�߳��н��տͻ������Ӻ���Ϣ
                Thread acceptThread = new Thread(() =>
                {
                    while (true)
                    {
                        // ���ܿͻ�������
                        Socket clientSocket = listenerSocket.Accept();

                        // �ں�̨�߳��д���ͻ�����Ϣ
                        Thread receiveThread = new Thread(() =>
                        {
                            // ���տͻ�����Ϣ
                            byte[] buffer = new byte[1024];
                            int bytesRead = clientSocket.Receive(buffer);
                            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                            // ����ͻ�����Ϣ
                            string response = ProcessMessage(message);

                            // ������Ӧ��Ϣ���ͻ���
                            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                            clientSocket.Send(responseBytes);

                            // �رտͻ�������
                            clientSocket.Close();
                        });

                        receiveThread.Start();
                    }
                });

                acceptThread.IsBackground = true;
                acceptThread.Start();

                lb_state.Text = "���������ɹ�";
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������ʱ����: " + ex.Message);
            }

        }

        string ProcessMessage(string message)
        {

            AppendToLog(message);

            // �����ﴦ��ͻ��˷��͵���Ϣ����������Ӧ��Ϣ
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
            // ����־����׷����Ϣ��������
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