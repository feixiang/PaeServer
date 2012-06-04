namespace PAEServer
{
    partial class HotSpotCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotSpotCreator));
            this.stopWifi = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.wifiPass = new System.Windows.Forms.TextBox();
            this.wifiName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btCreateWifi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stopWifi
            // 
            this.stopWifi.Location = new System.Drawing.Point(24, 93);
            this.stopWifi.Name = "stopWifi";
            this.stopWifi.Size = new System.Drawing.Size(94, 30);
            this.stopWifi.TabIndex = 15;
            this.stopWifi.Text = "停用虚拟热点";
            this.stopWifi.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(82, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "密码至少8位";
            // 
            // wifiPass
            // 
            this.wifiPass.Location = new System.Drawing.Point(84, 46);
            this.wifiPass.Name = "wifiPass";
            this.wifiPass.Size = new System.Drawing.Size(143, 21);
            this.wifiPass.TabIndex = 13;
            this.wifiPass.Text = "PAESERVER";
            // 
            // wifiName
            // 
            this.wifiName.Location = new System.Drawing.Point(84, 12);
            this.wifiName.Name = "wifiName";
            this.wifiName.Size = new System.Drawing.Size(143, 21);
            this.wifiName.TabIndex = 12;
            this.wifiName.Text = "PAESERVER";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "热点名称：";
            // 
            // btCreateWifi
            // 
            this.btCreateWifi.Location = new System.Drawing.Point(137, 93);
            this.btCreateWifi.Name = "btCreateWifi";
            this.btCreateWifi.Size = new System.Drawing.Size(90, 30);
            this.btCreateWifi.TabIndex = 9;
            this.btCreateWifi.Text = "创建Wifi热点";
            this.btCreateWifi.UseVisualStyleBackColor = true;
            this.btCreateWifi.Click += new System.EventHandler(this.btCreateWifi_Click);
            // 
            // HotSpotCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 135);
            this.Controls.Add(this.stopWifi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.wifiPass);
            this.Controls.Add(this.wifiName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCreateWifi);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HotSpotCreator";
            this.Text = "虚拟热点创建器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button stopWifi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox wifiPass;
        private System.Windows.Forms.TextBox wifiName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCreateWifi;
    }
}