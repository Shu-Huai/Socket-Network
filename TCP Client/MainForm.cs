using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace TCP_Client
{
    public partial class MainForm : Form
    {
        private static Socket m_sendSocket;
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
            m_sendSocket.Send(list.ToArray());
            _ = m_sendSocket.Receive(buffer);
            if (buffer[1] == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void ShowLog(string log)
        {
            logEditor.AppendText(log + Environment.NewLine);
        }
        private void ChangeStatus()
        {
            IsConnected = !IsConnected;
            IPEditor.ReadOnly = !IPEditor.ReadOnly;
            portEditor.ReadOnly = !portEditor.ReadOnly;
            messageEditor.Enabled = !messageEditor.Enabled;
            sendButton.Enabled = !sendButton.Enabled;
            fileButton.Enabled = !fileButton.Enabled;
            selectButton.Enabled = !selectButton.Enabled;
            directoryEditor.Enabled = !directoryEditor.Enabled;
        }
        private void Receive()
        {
            int size;
            do
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024];
                    size = m_sendSocket.Receive(buffer);
                    switch (buffer[0])
                    {
                        case 0:
                            ShowLog(m_sendSocket.RemoteEndPoint + "：" + (size == 0 ? "断开连接。" : Encoding.Default.GetString(buffer, 1, size - 1)));
                            break;
                        case 3:
                            IsLogin = false;
                            connectButton.Text = "连接";
                            ChangeStatus();
                            ShowLog(m_sendSocket.RemoteEndPoint + "：断开连接。");
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    size = 1;
                }
            } while (size != 1 && IsConnected && m_sendSocket.Connected);
        }
        private void ChangeConnectStatus(object sender, EventArgs e)
        {
            if (!IsConnected)
            {
                m_sendSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint port;
                try
                {
                    IPAddress ip = IPAddress.Parse(IPEditor.Text);
                    port = new(ip, int.Parse(portEditor.Text));
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
                    m_sendSocket.Connect(port);
                    LoginForm loginForm = new();
                    loginForm.ShowDialog();
                    if (!IsLogin)
                    {
                        byte[] buffer = new byte[1];
                        buffer[0] = 3;
                        m_sendSocket.Send(buffer);
                        return;
                    }
                    Thread thread = new(Receive);
                    thread.IsBackground = true;
                    thread.Start();
                    connectButton.Text = "断开连接";
                    ShowLog($"连接成功：{port}。");
                }
                catch (Exception error)
                {
                    ShowLog($"出现异常：{error}。");
                    IsConnected = true;
                    IPEditor.ReadOnly = true;
                    portEditor.ReadOnly = true;
                    messageEditor.Enabled = true;
                    sendButton.Enabled = true;
                    fileButton.Enabled = true;
                    selectButton.Enabled = true;
                    directoryEditor.Enabled = true;
                }
                AcceptButton = null;
                ChangeStatus();
                _ = messageEditor.Focus();
            }
            else
            {
                IsLogin = false;
                byte[] buffer = new byte[1];
                buffer[0] = 3;
                m_sendSocket.Send(buffer);
                ShowLog(m_sendSocket.LocalEndPoint + "：断开连接。");
                connectButton.Text = "连接";
                m_sendSocket.Close();
                ChangeStatus();
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
                m_sendSocket.Send(list.ToArray());
                ShowLog(m_sendSocket.LocalEndPoint + "：" + messageEditor.Text);
                messageEditor.Clear();
            }
        }
        private void ClosingForm(object sender, FormClosingEventArgs e)
        {
            if (IsConnected)
            {
                byte[] buffer = new byte[1];
                buffer[0] = 3;
                m_sendSocket.Send(buffer);
                IsConnected = false;
                ShowLog(m_sendSocket.LocalEndPoint + "：断开连接。");
                m_sendSocket.Close();
            }
        }
        private void SelectFile(object sender, EventArgs e)
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
            m_sendSocket.Send(list.ToArray(), 0, size + 1, SocketFlags.None);
            ShowLog(m_sendSocket.LocalEndPoint + "：发送文件“" + directoryEditor.Text + "”。");
            directoryEditor.Clear();
        }
    }
}