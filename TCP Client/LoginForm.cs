using System;
using System.Windows.Forms;
namespace TCP_Client
{
    public partial class LoginForm : Form
    {
        public delegate bool DelLogin(string userName, string password);
        public LoginForm()
        {
            InitializeComponent();
        }
        public bool Test(DelLogin delLogin)
        {
            return delLogin(userText.Text, passwordText.Text);
        }
        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }
        private void Login(object sender, EventArgs e)
        {
            if (userText.Text == "" || passwordText.Text == "")
            {
                _ = MessageBox.Show("用户名或密码错误", "失败", MessageBoxButtons.OK);
                return;
            }
            DelLogin delLogin = new(MainForm.Login);
            if (Test(delLogin))
            {
                MainForm.IsLogin = true;
                _ = MessageBox.Show("登录成功", "成功", MessageBoxButtons.OK);
                Close();
            }
            else
            {
                _ = MessageBox.Show("用户名或密码错误", "失败", MessageBoxButtons.OK);
                userText.Clear();
                passwordText.Clear();
            }
        }
    }
}