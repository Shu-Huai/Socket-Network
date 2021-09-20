using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UDP_Server
{
    public partial class MainForm : Form
    {
        public bool IsBegin { get; set; }
        private bool m_isFromLocal;
        private Socket m_socket;
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            IsBegin = false;
        }
        private void ShowLog(string log)
        {
            logEditor.AppendText(log + Environment.NewLine);
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
                        case 1:
                            SaveFileDialog dialog = new();
                            dialog.Title = "保存文件";
                            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            dialog.Filter = "所有类型|*.*";
                            dialog.ShowDialog(this);
                            using (FileStream file = new(dialog.FileName, FileMode.Create, FileAccess.Write))
                            {
                                file.Write(buffer, 1, size - 1);
                            }
                            MessageBox.Show("保存成功。", "成功");
                            break;
                        case 2:
                            string package = Encoding.Default.GetString(buffer, 1, size - 1);
                            byte[] information = new byte[2];
                            information[0] = 2;
                            if (package.Split('\0')[0] == "lvzhi" && package.Split('\0')[1] == "prwq0421")
                            {
                                information[1] = 1;
                                ShowLog(point + "：登录成功。");
                                IPCombo.Items.Add(point.ToString());
                                IPCombo.SelectedIndex = IPCombo.Items.Count - 1;
                            }
                            else
                            {
                                information[1] = 0;
                                ShowLog(point + "：用户名或密码错误。");
                            }
                            m_socket.SendTo(information, point);
                            break;
                        case 3:
                            ShowLog(point + "：断开连接。");
                            if (IPCombo.Items.Count != 0 && IPCombo.SelectedItem.ToString() == point.ToString())
                            {
                                IPCombo.SelectedIndex--;
                            }
                            IPCombo.Items.Remove(point.ToString());
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    size = 0;
                }
            } while (size != 0 && IsBegin);
        }
        private void BeginWatch(object sender, EventArgs e)
        {
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
            if (!IsBegin)
            {
                try
                {
                    m_socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    m_socket.Bind(port);
                    IsBegin = true;
                    Thread thread = new(Receive);
                    thread.IsBackground = true;
                    thread.Start();
                    beginButton.Text = "停止监听";
                    ShowLog($"开始监听：{port}。");
                }
                catch (Exception error)
                {
                    ShowLog($"出现异常：{error}。");
                }
            }
            else
            {
                IsBegin = false;
                foreach (string item in IPCombo.Items)
                {
                    IPEndPoint tempPort = new(IPAddress.Parse(item.Split(":")[0]), int.Parse(item.Split(":")[1]));
                    byte[] buffer = new byte[1];
                    buffer[0] = 3;
                    m_socket.SendTo(buffer, tempPort);
                }
                m_socket.Dispose();
                IPCombo.Items.Clear();
                beginButton.Text = "开始监听";
                ShowLog($"停止监听：{port}。");
            }
            IPEditor.ReadOnly = !IPEditor.ReadOnly;
            portEditor.ReadOnly = !portEditor.ReadOnly;
            IPCombo.Enabled = !IPCombo.Enabled;
            disconnectButton.Enabled = !disconnectButton.Enabled;
            logEditor.Enabled = !logEditor.Enabled;
        }
        private void Disconnect(object sender, EventArgs e)
        {
            if (IPCombo.Items.Count != 0)
            {
                Socket tempSocket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint tempPort = new(IPAddress.Parse(IPCombo.SelectedItem.ToString().Split(":")[0]), int.Parse(IPCombo.SelectedItem.ToString().Split(":")[1]));
                byte[] buffer = new byte[1];
                buffer[0] = 3;
                tempSocket.SendTo(buffer, tempPort);
                IPCombo.Items.RemoveAt(IPCombo.SelectedIndex--);
            }
        }
        private new void Closing(object sender, FormClosingEventArgs e)
        {
            if (IsBegin)
            {
                foreach (string item in IPCombo.Items)
                {
                    IPEndPoint tempPort = new(IPAddress.Parse(item.Split(":")[0]), int.Parse(item.Split(":")[1]));
                    byte[] buffer = new byte[1];
                    buffer[0] = 3;
                    m_socket.SendTo(buffer, tempPort);
                }
                m_socket.Dispose();
            }
        }
    }
}