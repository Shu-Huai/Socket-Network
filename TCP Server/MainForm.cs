using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace TCP_Server
{
    public partial class MainForm : Form
    {
        public bool IsBegin { get; set; }
        private bool m_isFromLocal;
        private readonly List<Socket> sendSockets_;
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            IsBegin = false;
            sendSockets_ = new();
        }
        private void ShowLog(string log)
        {
            logEditor.AppendText(log + Environment.NewLine);
        }
        private void Receive(object o)
        {
            int index = (int)o;
            Socket temp = sendSockets_[index];
            int size;
            do
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024];
                    size = temp.Receive(buffer);
                    switch (buffer[0])
                    {
                        case 0:
                            if (!m_isFromLocal)
                            {
                                ShowLog(temp.RemoteEndPoint + "：" + (size == 0 ? "断开连接。" : Encoding.Default.GetString(buffer, 1, size - 1)));
                            }
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
                            }
                            else
                            {
                                information[1] = 0;
                            }
                            sendSockets_[index].Send(information);
                            ShowLog(temp.RemoteEndPoint + "：登陆成功。");
                            break;
                        case 3:
                            ShowLog(temp.RemoteEndPoint + "：断开连接。");
                            sendSockets_.RemoveAt(index);
                            if (IPCombo.SelectedItem.ToString() == temp.RemoteEndPoint.ToString())
                            {
                                IPCombo.SelectedIndex--;
                            }
                            IPCombo.Items.Remove(temp.RemoteEndPoint.ToString());
                            temp.Close();
                            return;
                        default:
                            break;
                    }
                }
                catch
                {
                    size = 0;
                }
            } while (size != 0);
        }
        void Watch(object o)
        {
            Socket watchSocket = o as Socket;
            while (IsBegin)
            {
                Socket receive = watchSocket.Accept();
                if (!m_isFromLocal)
                {
                    sendSockets_.Add(receive);
                    IPCombo.Items.Add(sendSockets_[^1].RemoteEndPoint.ToString());
                    if (IPCombo.SelectedIndex == -1)
                    {
                        IPCombo.SelectedIndex = 0;
                    }
                    ShowLog($"收到连接：{sendSockets_[^1].RemoteEndPoint}。");
                    Thread thread = new(Receive);
                    thread.IsBackground = true;
                    thread.Start(sendSockets_.Count - 1);
                }
            }
            m_isFromLocal = false;
            watchSocket.Close();
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
                    Socket watchSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    watchSocket.Bind(port);
                    IsBegin = true;
                    watchSocket.Listen(10);
                    Thread thread = new(Watch);
                    thread.IsBackground = true;
                    thread.Start(watchSocket);
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
                Socket m_sendSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint tempPort = new(IPAddress.Parse("127.0.0.1"), int.Parse(portEditor.Text));
                m_sendSocket.Connect(tempPort);
                m_isFromLocal = true;
                foreach (Socket item in sendSockets_)
                {
                    byte[] buffer = new byte[1];
                    buffer[0] = 3;
                    item.Send(buffer);
                    item.Close();
                }
                sendSockets_.Clear();
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
            if (sendSockets_.Count == 0)
            {
                return;
            }
            byte[] buffer = new byte[1];
            buffer[0] = 3;
            int index = IPCombo.SelectedIndex;
            sendSockets_[index].Send(buffer);
            IPCombo.Items.RemoveAt(index);
            ShowLog(sendSockets_[index].LocalEndPoint + "：断开连接");
            sendSockets_[index].Close();
            sendSockets_.RemoveAt(index);
            IPCombo.SelectedIndex = index - 1;
            if (IPCombo.SelectedIndex == -1 && IPCombo.Items.Count != 0)
            {
                IPCombo.SelectedIndex = 0;
            }
        }
    }
}