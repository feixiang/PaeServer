namespace PAEServer
{
    partial class PaeServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaeServer));
            this.hideBt = new System.Windows.Forms.PictureBox();
            this.closeBt = new System.Windows.Forms.PictureBox();
            this.btPanel = new System.Windows.Forms.Panel();
            this.wifiBt = new System.Windows.Forms.PictureBox();
            this.commBt = new System.Windows.Forms.PictureBox();
            this.connectionInfoPanel = new System.Windows.Forms.Panel();
            this.tools = new System.Windows.Forms.PictureBox();
            this.connectedPanel = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inputComInfoPanel = new System.Windows.Forms.Panel();
            this.comReturnBt = new System.Windows.Forms.Button();
            this.comAddBt = new System.Windows.Forms.Button();
            this.comBit = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comBaudRate = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comPort = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comInfoLabel = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.hideBt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBt)).BeginInit();
            this.btPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wifiBt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commBt)).BeginInit();
            this.connectionInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tools)).BeginInit();
            this.inputComInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // hideBt
            // 
            this.hideBt.BackColor = System.Drawing.Color.Transparent;
            this.hideBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hideBt.Image = global::PAEServer.Properties.Resources.hide;
            this.hideBt.Location = new System.Drawing.Point(391, 0);
            this.hideBt.Name = "hideBt";
            this.hideBt.Size = new System.Drawing.Size(60, 24);
            this.hideBt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.hideBt.TabIndex = 0;
            this.hideBt.TabStop = false;
            this.toolTip.SetToolTip(this.hideBt, "隐藏");
            this.hideBt.Click += new System.EventHandler(this.hideBt_Click);
            // 
            // closeBt
            // 
            this.closeBt.BackColor = System.Drawing.Color.Transparent;
            this.closeBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBt.Image = global::PAEServer.Properties.Resources.close;
            this.closeBt.Location = new System.Drawing.Point(455, 0);
            this.closeBt.Name = "closeBt";
            this.closeBt.Size = new System.Drawing.Size(60, 24);
            this.closeBt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.closeBt.TabIndex = 1;
            this.closeBt.TabStop = false;
            this.toolTip.SetToolTip(this.closeBt, "退出");
            this.closeBt.Click += new System.EventHandler(this.closeBt_Click);
            // 
            // btPanel
            // 
            this.btPanel.BackColor = System.Drawing.Color.Transparent;
            this.btPanel.Controls.Add(this.wifiBt);
            this.btPanel.Controls.Add(this.commBt);
            this.btPanel.Location = new System.Drawing.Point(34, 61);
            this.btPanel.Name = "btPanel";
            this.btPanel.Size = new System.Drawing.Size(306, 42);
            this.btPanel.TabIndex = 2;
            // 
            // wifiBt
            // 
            this.wifiBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.wifiBt.Image = global::PAEServer.Properties.Resources.startWifi;
            this.wifiBt.Location = new System.Drawing.Point(158, 3);
            this.wifiBt.Name = "wifiBt";
            this.wifiBt.Size = new System.Drawing.Size(145, 35);
            this.wifiBt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.wifiBt.TabIndex = 1;
            this.wifiBt.TabStop = false;
            this.toolTip.SetToolTip(this.wifiBt, "开启局域网监听");
            this.wifiBt.Click += new System.EventHandler(this.wifiBt_Click);
            // 
            // commBt
            // 
            this.commBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.commBt.Image = global::PAEServer.Properties.Resources.startComm;
            this.commBt.Location = new System.Drawing.Point(8, 3);
            this.commBt.Name = "commBt";
            this.commBt.Size = new System.Drawing.Size(125, 35);
            this.commBt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.commBt.TabIndex = 0;
            this.commBt.TabStop = false;
            this.toolTip.SetToolTip(this.commBt, "添加串口监听");
            this.commBt.Click += new System.EventHandler(this.commBt_Click);
            // 
            // connectionInfoPanel
            // 
            this.connectionInfoPanel.BackColor = System.Drawing.Color.White;
            this.connectionInfoPanel.Controls.Add(this.tools);
            this.connectionInfoPanel.Controls.Add(this.connectedPanel);
            this.connectionInfoPanel.Controls.Add(this.label9);
            this.connectionInfoPanel.Controls.Add(this.label8);
            this.connectionInfoPanel.Controls.Add(this.label3);
            this.connectionInfoPanel.Controls.Add(this.label2);
            this.connectionInfoPanel.Controls.Add(this.label1);
            this.connectionInfoPanel.Location = new System.Drawing.Point(13, 125);
            this.connectionInfoPanel.Name = "connectionInfoPanel";
            this.connectionInfoPanel.Size = new System.Drawing.Size(500, 261);
            this.connectionInfoPanel.TabIndex = 1;
            // 
            // tools
            // 
            this.tools.BackColor = System.Drawing.Color.Transparent;
            this.tools.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tools.Image = global::PAEServer.Properties.Resources.tools;
            this.tools.Location = new System.Drawing.Point(452, 3);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(45, 45);
            this.tools.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.tools.TabIndex = 6;
            this.tools.TabStop = false;
            this.toolTip.SetToolTip(this.tools, "小工具");
            this.tools.Click += new System.EventHandler(this.tools_Click);
            // 
            // connectedPanel
            // 
            this.connectedPanel.Location = new System.Drawing.Point(19, 82);
            this.connectedPanel.Name = "connectedPanel";
            this.connectedPanel.Size = new System.Drawing.Size(478, 176);
            this.connectedPanel.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Image = global::PAEServer.Properties.Resources._71;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(399, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 20);
            this.label9.TabIndex = 4;
            this.label9.Text = "操作";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Image = global::PAEServer.Properties.Resources._91;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(216, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "连接方式";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Image = global::PAEServer.Properties.Resources._37;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(34, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "客户端名称";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(17, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(425, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "______________________________________________________________________";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "连接列表";
            // 
            // inputComInfoPanel
            // 
            this.inputComInfoPanel.BackColor = System.Drawing.Color.White;
            this.inputComInfoPanel.Controls.Add(this.comReturnBt);
            this.inputComInfoPanel.Controls.Add(this.comAddBt);
            this.inputComInfoPanel.Controls.Add(this.comBit);
            this.inputComInfoPanel.Controls.Add(this.label7);
            this.inputComInfoPanel.Controls.Add(this.comBaudRate);
            this.inputComInfoPanel.Controls.Add(this.label6);
            this.inputComInfoPanel.Controls.Add(this.label5);
            this.inputComInfoPanel.Controls.Add(this.comPort);
            this.inputComInfoPanel.Controls.Add(this.label4);
            this.inputComInfoPanel.Controls.Add(this.comInfoLabel);
            this.inputComInfoPanel.Location = new System.Drawing.Point(13, 125);
            this.inputComInfoPanel.Name = "inputComInfoPanel";
            this.inputComInfoPanel.Size = new System.Drawing.Size(500, 261);
            this.inputComInfoPanel.TabIndex = 2;
            this.inputComInfoPanel.Visible = false;
            // 
            // comReturnBt
            // 
            this.comReturnBt.BackColor = System.Drawing.Color.White;
            this.comReturnBt.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.comReturnBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comReturnBt.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comReturnBt.Location = new System.Drawing.Point(206, 191);
            this.comReturnBt.Name = "comReturnBt";
            this.comReturnBt.Size = new System.Drawing.Size(75, 29);
            this.comReturnBt.TabIndex = 9;
            this.comReturnBt.Text = "返回";
            this.comReturnBt.UseVisualStyleBackColor = false;
            this.comReturnBt.Click += new System.EventHandler(this.comReturnBt_Click);
            // 
            // comAddBt
            // 
            this.comAddBt.BackColor = System.Drawing.Color.White;
            this.comAddBt.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.comAddBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comAddBt.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comAddBt.Location = new System.Drawing.Point(287, 191);
            this.comAddBt.Name = "comAddBt";
            this.comAddBt.Size = new System.Drawing.Size(75, 29);
            this.comAddBt.TabIndex = 8;
            this.comAddBt.Text = "确定";
            this.comAddBt.UseVisualStyleBackColor = false;
            this.comAddBt.Click += new System.EventHandler(this.comAddBt_Click);
            // 
            // comBit
            // 
            this.comBit.FormattingEnabled = true;
            this.comBit.Location = new System.Drawing.Point(210, 144);
            this.comBit.Name = "comBit";
            this.comBit.Size = new System.Drawing.Size(152, 20);
            this.comBit.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(111, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "数据位：";
            // 
            // comBaudRate
            // 
            this.comBaudRate.FormattingEnabled = true;
            this.comBaudRate.Location = new System.Drawing.Point(210, 109);
            this.comBaudRate.Name = "comBaudRate";
            this.comBaudRate.Size = new System.Drawing.Size(152, 20);
            this.comBaudRate.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(111, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "波特率：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label5.Location = new System.Drawing.Point(28, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(347, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "_________________________________________________________";
            // 
            // comPort
            // 
            this.comPort.FormattingEnabled = true;
            this.comPort.Location = new System.Drawing.Point(210, 74);
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(152, 20);
            this.comPort.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(125, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "端口：";
            // 
            // comInfoLabel
            // 
            this.comInfoLabel.AutoSize = true;
            this.comInfoLabel.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comInfoLabel.Location = new System.Drawing.Point(26, 15);
            this.comInfoLabel.Name = "comInfoLabel";
            this.comInfoLabel.Size = new System.Drawing.Size(121, 20);
            this.comInfoLabel.TabIndex = 0;
            this.comInfoLabel.Text = "请填写串口信息：";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "无线控制平台";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // PaeServer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PAEServer.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(525, 398);
            this.Controls.Add(this.inputComInfoPanel);
            this.Controls.Add(this.connectionInfoPanel);
            this.Controls.Add(this.btPanel);
            this.Controls.Add(this.closeBt);
            this.Controls.Add(this.hideBt);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PaeServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "无线控制平台";
            this.Load += new System.EventHandler(this.PaeServer_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PaeServer_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.hideBt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBt)).EndInit();
            this.btPanel.ResumeLayout(false);
            this.btPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wifiBt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commBt)).EndInit();
            this.connectionInfoPanel.ResumeLayout(false);
            this.connectionInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tools)).EndInit();
            this.inputComInfoPanel.ResumeLayout(false);
            this.inputComInfoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox hideBt;
        private System.Windows.Forms.PictureBox closeBt;
        private System.Windows.Forms.Panel btPanel;
        private System.Windows.Forms.PictureBox commBt;
        private System.Windows.Forms.PictureBox wifiBt;
        private System.Windows.Forms.Panel connectionInfoPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel inputComInfoPanel;
        private System.Windows.Forms.Label comInfoLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comBaudRate;
        private System.Windows.Forms.ComboBox comBit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button comAddBt;
        private System.Windows.Forms.Button comReturnBt;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel connectedPanel;
        private System.Windows.Forms.PictureBox tools;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

