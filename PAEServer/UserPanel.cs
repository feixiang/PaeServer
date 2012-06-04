using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PAEServer
{
    class UserPanel : Panel
    {
        private Label ConnectionWayLabel;
        private Button closeConnection;
        private Label userNameLabel;

        private WifiServer wifiServer { get; set; }
        private BluetoothServer bluetoothServer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="way">连接方式，可以是COMX，或WIFI</param>
        public UserPanel(string username,WifiServer wifiServer)
        {
            InitializeComponent();
            this.userName = username;
            this.connnectionWay = "WIFI";
            this.userNameLabel.Text = userName; 
            this.ConnectionWayLabel.Text = connnectionWay;

            this.wifiServer = wifiServer; 
            
        }
        public UserPanel(string username,BluetoothServer server)
        {
            InitializeComponent();
            this.userName = username;
            this.connnectionWay = server.mportName; 
            this.userNameLabel.Text = userName;
            this.ConnectionWayLabel.Text = connnectionWay;

            this.bluetoothServer = server;

        }

        private String userName { get; set; }
        private String connnectionWay { get; set; }
        private int buttonId { get; set;  }

        private void InitializeComponent()
        {
            this.userNameLabel = new System.Windows.Forms.Label();
            this.ConnectionWayLabel = new System.Windows.Forms.Label();
            this.closeConnection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userNameLabel.Location = new System.Drawing.Point(20, 0);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(50, 20);
            this.userNameLabel.TabIndex = 0;
            this.userNameLabel.Text = "用户名";
            // 
            // ConnectionWayLabel
            // 
            this.ConnectionWayLabel.AutoSize = true;
            this.ConnectionWayLabel.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConnectionWayLabel.Location = new System.Drawing.Point(210, 0);
            this.ConnectionWayLabel.Name = "ConnectionWayLabel";
            this.ConnectionWayLabel.Size = new System.Drawing.Size(65, 20);
            this.ConnectionWayLabel.TabIndex = 1;
            this.ConnectionWayLabel.Text = "连接方式";
            // 
            // closeConnection
            // 
            this.closeConnection.BackColor = System.Drawing.Color.White;
            this.closeConnection.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.closeConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeConnection.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.closeConnection.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.closeConnection.Location = new System.Drawing.Point(370, 0);
            this.closeConnection.Name = "closeConnection";
            this.closeConnection.Size = new System.Drawing.Size(75, 30);
            this.closeConnection.TabIndex = 2;
            this.closeConnection.Text = "断开连接";
            this.closeConnection.UseVisualStyleBackColor = false;
            this.closeConnection.Click += new System.EventHandler(this.closeConnection_Click);
            // 
            // UserPanel
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.ConnectionWayLabel);
            this.Controls.Add(this.closeConnection);
            this.Size = new System.Drawing.Size(470, 40);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void closeConnection_Click(object sender, EventArgs e)
        {
            if (wifiServer != null)
            {
                //wifiServer.closeASocket(userName);
            }
            else if(bluetoothServer!=null){
                bluetoothServer.closeComm();
            }
            else
            {
                MessageBox.Show("断开连接失败，请重新尝试.");
            }
        }
    }
}
