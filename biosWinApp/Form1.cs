using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace biosWinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        public Random random = new Random();
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            string str = "";
            for (int i = 0; i < 12; i++)
            {
                str += random.NextInt64(0, 9);
            }
            textBox1.Text = $"'{str}'";
            // 将文本复制到剪贴板
            Clipboard.SetText(textBox1.Text);
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            var str = GenerateRandomString(12);
            textBox2.Text = $"'{str}'";
            // 将文本复制到剪贴板
            Clipboard.SetText(textBox2.Text);
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder sb = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }


    }
}