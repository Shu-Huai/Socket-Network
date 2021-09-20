using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace UDP_Client
{
    public partial class MainForm : Form
    {
        private static Socket m_socket;
        private static IPEndPoint m_farPort;
        public bool IsConnected { get; set; }
        public static bool IsLogin { get; set; }
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            IsConnected = false;
        }
        public static bool Login(string userName, string password)
        {
            string package = userName + "\0" + password;
            byte[] buffer = Encoding.Default.GetBytes(package);
            List<byte> list = new();
            list.Add(2);
            list.AddRange(buffer);
            m_socket.SendTo(list.ToArray(), m_farPort);
            EndPoint temp = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                _ = m_socket.ReceiveFrom(buffer, ref temp);
            }
            catch
            {
                return false;
            }
            return buffer[1] != 0;
        }
        private void ShowLog(string log)
        {
            logEditor.AppendText(log + Environment.NewLine);
        }
        private void SetStatus(bool status)
        {
            IsConnected = status;
            IPEditor.ReadOnly = status;
            portEditor.ReadOnly = status;
            messageEditor.Enabled = status;
            sendButton.Enabled = status;
            fileButton.Enabled = status;
            selectButton.Enabled = status;
            directoryEditor.Enabled = status;
        }
        private void Receive()
        {
            int size;
            do
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024];
                    EndPoint port = new IPEndPoint(IPAddress.Any, 0);
                    size = m_socket.ReceiveFrom(buffer, ref port);
                    switch (buffer[0])
                    {
                        case 0:
                            ShowLog(port + "：" + Encoding.Default.GetString(buffer, 1, size - 1));
                            break;
                        case 3:
                            IsLogin = false;
                            connectButton.Text = "连接";
                            SetStatus(false);
                            ShowLog(port + "：断开连接。");
                            return;
                        default:
                            break;
                    }
                }
                catch
                {
                    return;
                }
            } while (IsConnected);
        }
        private void ChangeConnectStatus(object sender, EventArgs e)
        {
            if (!IsConnected)
            {
                m_socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                try
                {
                    IPAddress ip = IPAddress.Parse(IPEditor.Text);
                    m_farPort = new(ip, int.Parse(portEditor.Text));
                }
                catch
                {
                    MessageBox.Show("输入有误，请重新输入。", "错误");
                    IPEditor.Clear();
                    portEditor.Clear();
                    return;
                }
                try
                {
                    LoginForm loginForm = new();
                    loginForm.ShowDialog();
                    if (!IsLogin)
                    {
                        return;
                    }
                    Thread thread = new(Receive);
                    thread.IsBackground = true;
                    thread.Start();
                    SetStatus(true);
                    connectButton.Text = "断开连接";
                    ShowLog($"连接成功：{m_farPort}。");
                }
                catch (Exception error)
                {
                    ShowLog($"出现异常：{error}。");
                }
                AcceptButton = null;
                _ = messageEditor.Focus();
            }
            else
            {
                IsLogin = false;
                connectButton.Text = "连接";
                SetStatus(false);
                byte[] buffer = new byte[1];
                buffer[0] = 3;
                m_socket.SendTo(buffer, m_farPort);
                m_socket.Dispose();
            }
        }
        private void SendMessage(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                byte[] buffer = Encoding.Default.GetBytes(messageEditor.Text);
                List<byte> list = new();
                list.Add(0);
                list.AddRange(buffer);
                m_socket.SendTo(list.ToArray(), m_farPort);
                ShowLog(m_socket.LocalEndPoint + "：" + messageEditor.Text);
                messageEditor.Clear();
            }
        }
        private void SelectFiles(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new();
            dialog.Title = "打开文件";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.Filter = "所有类型|*.*";
            dialog.ShowDialog();
            directoryEditor.Text = dialog.FileName;
        }
        private void SendFile(object sender, EventArgs e)
        {
            using FileStream file = new(directoryEditor.Text, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[1024 * 1024 * 10];
            int size = file.Read(buffer);
            List<byte> list = new();
            list.Add(1);
            list.AddRange(buffer);
            m_socket.SendTo(list.ToArray(), 0, size + 1, SocketFlags.None, m_farPort);
            ShowLog(m_socket.LocalEndPoint + "：发送文件“" + directoryEditor.Text + "”。");
            directoryEditor.Clear();
        }
        private void NewLine(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Enter)
            {
                messageEditor.Text += Environment.NewLine;
            }
        }
        private new void Closing(object sender, FormClosingEventArgs e)
        {
            if (IsConnected)
            {
                byte[] buffer = new byte[1];
                buffer[0] = 3;
                m_socket.SendTo(buffer, m_farPort);
                m_socket.Dispose();
            }
        }
    }
}