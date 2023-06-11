namespace clientWinApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btn_connect = new Button();
            heartbeatTimer = new System.Windows.Forms.Timer(components);
            btn_seachSvc = new Button();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // btn_connect
            // 
            btn_connect.Location = new Point(562, 169);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(112, 34);
            btn_connect.TabIndex = 0;
            btn_connect.Text = "连接服务器";
            btn_connect.UseVisualStyleBackColor = true;
            btn_connect.Click += btn_connect_Click;
            // 
            // heartbeatTimer
            // 
            heartbeatTimer.Interval = 5000;
            heartbeatTimer.Tick += heartbeatTimer_Tick;
            // 
            // btn_seachSvc
            // 
            btn_seachSvc.Location = new Point(150, 99);
            btn_seachSvc.Name = "btn_seachSvc";
            btn_seachSvc.Size = new Size(208, 34);
            btn_seachSvc.TabIndex = 1;
            btn_seachSvc.Text = "检测可用服务";
            btn_seachSvc.UseVisualStyleBackColor = true;
            btn_seachSvc.Click += btn_seachSvc_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(124, 231);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(303, 207);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(richTextBox1);
            Controls.Add(btn_seachSvc);
            Controls.Add(btn_connect);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btn_connect;
        private System.Windows.Forms.Timer heartbeatTimer;
        private Button btn_seachSvc;
        private RichTextBox richTextBox1;
    }
}