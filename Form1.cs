//using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using userData;


namespace Olimpo_XIII
{
    public partial class LblLinkRegister : Form
    {
        Thread thr;
        public LblLinkRegister()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            User user = new User(TxtLogin.Text, TxtPass.Text);
            string json = JsonConvert.SerializeObject(user);
            if (!FileExist("User.txt"))
            {
                File.AppendAllText("User.txt", "{List:[]}");
            }
            Login(json);
        }

        private void Login(string user)
        {
            var data = File.ReadAllText("User.txt");
            if (FilterItemInArrayJson(data, user, "Login") && FilterItemInArrayJson(data, user, "Pass"))
            {
                Lbl_message.Text = "Sucesso no Login";
            }
            else
            {
                Lbl_message.Text = "Não encontrado, clique em 'Novo usuário'";
            }
        }

        private bool FileExist(string path)
        {
            return File.Exists(path);
        }


        private bool FilterItemInArrayJson(string list, string user, string key)
        {
            dynamic json = JsonConvert.DeserializeObject(list);
            dynamic jsonUser = JsonConvert.DeserializeObject(user);
            bool response = false;
            foreach (dynamic item in json.List)
            {
                if (item[key] == jsonUser[key])
                {
                    response = true;
                }
            }
            return response;
        }

        private void TxtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NewUser user = new NewUser();
            thr = new Thread(OpenWindow);
            thr.SetApartmentState(ApartmentState.STA);
            thr.Start();
        }
        private void OpenWindow(object obj)
        {
            Application.Run(new NewUser());
        }

        private void LblLinkRegister_Load(object sender, EventArgs e)
        {
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
