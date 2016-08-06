using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zap
{
    public partial class Password : Form
    {
        string saltString = "ngocvuduc@gmail.com";
        public Password()
        {
            InitializeComponent();
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                this.Submit.PerformClick();
            }

        }

        private void Submit_Click(object sender, EventArgs e)
        {
            

            if (CreateSHAHash(passwordTextBox.Text, saltString) == Properties.Settings.Default.Cript)
            {
                SaBan settingsForm = new SaBan();

                // Show the settings form
                settingsForm.Show();
                this.Visible = false;
                this.Hide();
                this.Close();
                this.Dispose();

            }
        }
        public static string CreateSHAHash(string Password, string Salt)
        {
            System.Security.Cryptography.SHA512Managed HashTool = new System.Security.Cryptography.SHA512Managed();
            Byte[] PasswordAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(Password, Salt));
            Byte[] EncryptedBytes = HashTool.ComputeHash(PasswordAsByte);
            HashTool.Clear();
            return Convert.ToBase64String(EncryptedBytes).ToLower();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            passEncript.Text = CreateSHAHash(passwordTextBox.Text, saltString);
        }

        private void Password_Load(object sender, EventArgs e)
        {

        }
    }
}
