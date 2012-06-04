using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PAEServer
{
    public partial class NiceMessageBox : Form
    {
        public NiceMessageBox(String message)
        {
            InitializeComponent();
            this.messageLabel.Text = message; 
        }
        public void show()
        {
            this.Show();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
