using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElmaIntegration;
using ElmaIntegration.Models;

namespace MailProxyApp
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вход в приложение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var elmaServer = ElmaUrl.Text;
            var integration = new Integration(elmaServer);
            var serverStatus = new ServerStatus();

            while (serverStatus.Status == "0" || string.IsNullOrWhiteSpace(serverStatus.Status))
            {
                System.Threading.Thread.Sleep(1000);
                serverStatus = integration.GetServerStatus();
            }

            var elmaLogin = login.Text;
            var elmaPassword = password.Text;
            var auth = integration.Authorization(elmaLogin, elmaPassword);
            if(auth != null)
            {
                this.Hide();
                var mainForm = new Form1(integration);
                mainForm.Closed += (s, args) => this.Close();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Ошибка авторизации");
            }
        }
    }
}
