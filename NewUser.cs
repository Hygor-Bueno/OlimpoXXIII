using System;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using userData;
using System.Collections.Generic;

namespace Olimpo_XIII
{
    public partial class NewUser : Form
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void NewUser_Load(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e){
            User user = new User(TxtLogin.Text,TextPass.Text,TextConfirmPass.Text);
            dynamic passChecked = user.CheckPass(user.Pass,"A senha");
            dynamic passConfirmChecked = user.CheckPass(user.Pass, "Confimar senha");
            dynamic passEquals = user.passEquals();
            dynamic loginCheck = user.CheckLogin();
            Console.WriteLine(loginCheck);
            string message = $" {passChecked.message}{passConfirmChecked.message}{passEquals.message}{loginCheck.message}";
            bool error = (Convert.ToBoolean(passChecked.error) || Convert.ToBoolean(passConfirmChecked.error) || Convert.ToBoolean(passEquals.error) || Convert.ToBoolean(loginCheck.error));
            if (!error){
                if (!File.Exists($"User.txt")) File.AppendAllText("User.txt", "{List:[]}");
                var data = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("User.txt"));
                var h = JsonConvert.DeserializeObject<List<User>>(JsonConvert.SerializeObject(data.List));
                h.Add(user);
                data.List = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(h));           
                File.WriteAllText("User.txt", JsonConvert.SerializeObject(data));
                this.Close();
            }
            else
            {
                string messageBoxText = message;
                string caption = "Atenção";
                System.Windows.MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
