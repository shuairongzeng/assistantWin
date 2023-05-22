namespace assistantWin
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
            toolStripContainer = new ToolStripContainer();
            statusLabel = new Label();
            outputLabel = new Label();
            toolStrip1 = new ToolStrip();
            backButton = new ToolStripButton();
            forwardButton = new ToolStripButton();
            urlTextBox = new ToolStripTextBox();
            goButton = new ToolStripButton();
            menuStrip1 = new MenuStrip();
            开始ToolStripMenuItem = new ToolStripMenuItem();
            ShowDevToolsMenuItem = new ToolStripMenuItem();
            ExitMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            label2 = new Label();
            toolStripContainer.ContentPanel.SuspendLayout();
            toolStripContainer.TopToolStripPanel.SuspendLayout();
            toolStripContainer.SuspendLayout();
            toolStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            toolStripContainer.ContentPanel.Controls.Add(label2);
            toolStripContainer.ContentPanel.Controls.Add(label1);
            toolStripContainer.ContentPanel.Controls.Add(statusLabel);
            toolStripContainer.ContentPanel.Controls.Add(outputLabel);
            toolStripContainer.ContentPanel.Size = new Size(800, 400);
            toolStripContainer.Dock = DockStyle.Fill;
            toolStripContainer.Location = new Point(0, 25);
            toolStripContainer.Name = "toolStripContainer";
            toolStripContainer.Size = new Size(800, 425);
            toolStripContainer.TabIndex = 0;
            toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            toolStripContainer.TopToolStripPanel.Controls.Add(toolStrip1);
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(-7, 374);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(43, 17);
            statusLabel.TabIndex = 1;
            statusLabel.Text = "label1";
            // 
            // outputLabel
            // 
            outputLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            outputLabel.AutoSize = true;
            outputLabel.Location = new Point(-7, 387);
            outputLabel.Name = "outputLabel";
            outputLabel.Size = new Size(43, 17);
            outputLabel.TabIndex = 0;
            outputLabel.Text = "label1";
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.Items.AddRange(new ToolStripItem[] { backButton, forwardButton, urlTextBox, goButton });
            toolStrip1.Location = new Point(3, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(691, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            toolStrip1.Layout += HandleToolStripLayout;
            // 
            // backButton
            // 
            backButton.Image = Properties.Resources.nav_left_green;
            backButton.ImageTransparentColor = Color.Magenta;
            backButton.Name = "backButton";
            backButton.Size = new Size(56, 22);
            backButton.Text = "Back";
            backButton.Click += backButton_Click;
            // 
            // forwardButton
            // 
            forwardButton.Image = Properties.Resources.nav_right_green;
            forwardButton.ImageTransparentColor = Color.Magenta;
            forwardButton.Name = "forwardButton";
            forwardButton.Size = new Size(76, 22);
            forwardButton.Text = "Forward";
            forwardButton.Click += forwardButton_Click;
            // 
            // urlTextBox
            // 
            urlTextBox.Name = "urlTextBox";
            urlTextBox.Size = new Size(500, 25);
            // 
            // goButton
            // 
            goButton.Image = Properties.Resources.nav_plain_green;
            goButton.ImageTransparentColor = Color.Magenta;
            goButton.Name = "goButton";
            goButton.Size = new Size(45, 22);
            goButton.Text = "Go";
            goButton.Click += goButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { 开始ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 25);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // 开始ToolStripMenuItem
            // 
            开始ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ShowDevToolsMenuItem, ExitMenuItem });
            开始ToolStripMenuItem.Name = "开始ToolStripMenuItem";
            开始ToolStripMenuItem.Size = new Size(44, 21);
            开始ToolStripMenuItem.Text = "开始";
            // 
            // ShowDevToolsMenuItem
            // 
            ShowDevToolsMenuItem.Name = "ShowDevToolsMenuItem";
            ShowDevToolsMenuItem.Size = new Size(160, 22);
            ShowDevToolsMenuItem.Text = "打开开发者工具";
            ShowDevToolsMenuItem.Click += ShowDevToolsMenuItem_Click;
            // 
            // ExitMenuItem
            // 
            ExitMenuItem.Name = "ExitMenuItem";
            ExitMenuItem.Size = new Size(160, 22);
            ExitMenuItem.Text = "退出";
            ExitMenuItem.Click += ExitMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(372, 45);
            label1.Name = "label1";
            label1.Size = new Size(43, 17);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(441, 41);
            label2.Name = "label2";
            label2.Size = new Size(43, 17);
            label2.TabIndex = 3;
            label2.Text = "label2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStripContainer);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            toolStripContainer.ContentPanel.ResumeLayout(false);
            toolStripContainer.ContentPanel.PerformLayout();
            toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.PerformLayout();
            toolStripContainer.ResumeLayout(false);
            toolStripContainer.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStripContainer toolStripContainer;
        private Label outputLabel;
        private ToolStrip toolStrip1;
        private ToolStripButton backButton;
        private ToolStripButton forwardButton;
        private ToolStripTextBox urlTextBox;
        private ToolStripButton goButton;
        private Label statusLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 开始ToolStripMenuItem;
        private ToolStripMenuItem ShowDevToolsMenuItem;
        private ToolStripMenuItem ExitMenuItem;
        private Label label2;
        private Label label1;
    }
}