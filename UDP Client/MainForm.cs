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
            _ = m_socket.ReceiveFrom(buffer, ref temp);
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
                    EndPoint point = new IPEndPoint(IPAddress.Any, 0);
                    size = m_socket.ReceiveFrom(buffer, ref point);
                    switch (buffer[0])
                    {
                        case 0:
                            ShowLog(point + "：" + (size == 0 ? "断开连接。" : Encoding.Default.GetString(buffer, 1, size - 1)));
                            break;
                        case 3:
                            IsLogin = false;
                            connectButton.Text = "连接";
                            IsConnected = false;
                            IPEditor.ReadOnly = false;
                            portEditor.ReadOnly = false;
                            messageEditor.Enabled = false;
                            sendButton.Enabled = false;
                            fileButton.Enabled = false;
                            selectButton.Enabled = false;
                            directoryEditor.Enabled = false;
                            ShowLog(point + "：断开连接。");
                            return;
                        default:
                            break;
                    }
                }
                catch
                {
                    size = 1;
                }
            } while (size != 1 && IsConnected);
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
                    connectButton.Text = "断开连接";
                    ShowLog($"连接成功：{m_farPort}。");
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
                Close();
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
    }
}