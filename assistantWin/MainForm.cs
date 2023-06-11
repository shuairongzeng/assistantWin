using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assistantWin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ProcessCommandLineArguments();
        }

        private void ProcessCommandLineArguments()
        {
            string[] commandLineArgs = Environment.GetCommandLineArgs();
           
            if (commandLineArgs.Length > 2 && commandLineArgs[2] == "/copyFile")
            {
                // 当前右键菜单单击后的完整路径，如：K:\cms\111.txt
                string filePath = commandLineArgs[1];
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    // 执行文件复制操作
                    string savePath = @"C:\\file.txt";  // 替换为你希望保存文件的路径
                    File.Copy(filePath, savePath, true);
                    MessageBox.Show("文件复制完成。");
                }
            }
        }

    }
}
