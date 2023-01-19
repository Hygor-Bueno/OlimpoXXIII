using Newtonsoft.Json;
using Olimpo_XIII.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace userData
{
    class User : IUser
    {
        private string login;
        private string pass;
        private string confirmPass;

        public string Login
        {
            get {return this.login;}
            private set {this.login = value;}
        }

        public string Pass
        {
            get { return this.pass; }
            private set { this.pass = value; }
        }

        public User(string login, string pass)
        {
            this.login = login.ToUpper().Trim();
            this.pass = pass.ToUpper();
        }
        public User(string login, string pass, string confirmPass)
        {
            this.login = login.ToUpper().Trim();
            this.pass = pass.ToUpper();
            this.confirmPass = confirmPass.ToUpper();
        }
        public User() { }

        public string getConfirmPass()
        {
            return this.confirmPass;
        }
        public void setConfirmPass(string confirmPass)
        {
            this.confirmPass = confirmPass;
        }
        public void setLogin(string login)
        {
            this.login = login;
        }

        public void setPass(string pass)
        {
            this.pass = pass;
        }
 
        public dynamic passEquals()
        {
            dynamic response = JsonConvert.DeserializeObject("{error:false,message:''}");
            if (this.confirmPass != this.pass)
            {
                response.error = true;
                response.message = "As senhas devem ser iguais. \n";
            }
            return response;
        }
        public bool CheckUser()
        {
            string data = AllUser();
            dynamic jsonData = JsonConvert.DeserializeObject(data);
            bool response = false;
            response = UserExists(jsonData.List, this.login);
            return response;
        }

        public dynamic CheckLogin()
        {
            dynamic response = JsonConvert.DeserializeObject("{'error':false,'message':''}");
            var voidString = VoidString(this.Login,"O login");
            if (!voidString.error && CheckUser())
            {
                response.error = true;
                response.message += "Esse usuário já foi cadastrado.\n";
            }
            if (voidString.error)
            {
                response.error = true;
                response.message += voidString.message;
            }              
            return response;
        }

        public bool UserExists(dynamic list, string user)
        {
            bool response = false;
            foreach (dynamic item in list){
                if (item.Login == user){
                    response = true;
                }
            }
            return response;
        }

        private string AllUser()
        {
            string result = "";
            if(File.Exists("User.txt")) result = File.ReadAllText("User.txt");
            return result;
        }

        public dynamic CheckPass(string value,string title)
        {
            string message = "{error:false,message:''}";
            dynamic json = JsonConvert.DeserializeObject(message);
            var voidString = VoidString(value, title);
            var spaceInString = SpaceInString(value, title);

            if (voidString.error || spaceInString.error)
            {
                json.error = true;
                json.message = $"{voidString.message} {spaceInString.message}";
            }
            Console.WriteLine(json);
            return json;
        }

        public (bool error, string message)  VoidString(string value,string title)
        {
            bool error = false;
            string message = "";
            if (string.IsNullOrWhiteSpace(value))
            {
                error = true;
                message = $"{title}, não deve estar em branco. \n";
            };
            return (error, message);
        }

        public (bool error, string message) SpaceInString(string value,string title)
        {
            bool error = false;
            string message = "";
            var regex = new Regex(@" ");
            if (regex.IsMatch(value))
            {
                error = true;
                message += $"{title}, não deve conter espaço vazio. \n";
            }
            return (error, message);
        }
    }
}