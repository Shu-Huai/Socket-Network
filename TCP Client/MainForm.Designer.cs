
namespace TCP_Client
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.messageEditor = new System.Windows.Forms.TextBox();
            this.logEditor = new System.Windows.Forms.TextBox();
            this.portEditor = new System.Windows.Forms.TextBox();
            this.IPEditor = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.fileButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.directoryEditor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // messageEditor
            // 
            this.messageEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageEditor.Enabled = false;
            this.messageEditor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.messageEditor.Location = new System.Drawing.Point(12, 266);
            this.messageEditor.Multiline = true;
            this.messageEditor.Name = "messageEditor";
            this.messageEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.messageEditor.Size = new System.Drawing.Size(920, 151);
            this.messageEditor.TabIndex = 5;
            this.messageEditor.WordWrap = false;
            // 
            // logEditor
            // 
            this.logEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logEditor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.logEditor.Location = new System.Drawing.Point(12, 47);
            this.logEditor.Multiline = true;
            this.logEditor.Name = "logEditor";
            this.logEditor.ReadOnly = true;
            this.logEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logEditor.Size = new System.Drawing.Size(920, 213);
            this.logEditor.TabIndex = 4;
            this.logEditor.WordWrap = false;
            // 
            // portEditor
            // 
            this.portEditor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.portEditor.Location = new System.Drawing.Point(159, 12);
            this.portEditor.Name = "portEditor";
            this.portEditor.Size = new System.Drawing.Size(63, 29);
            this.portEditor.TabIndex = 1;
            this.portEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EnterToConnect);
            // 
            // IPEditor
            // 
            this.IPEditor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IPEditor.Location = new System.Drawing.Point(12, 12);
            this.IPEditor.Name = "IPEditor";
            this.IPEditor.Size = new System.Drawing.Size(141, 29);
            this.IPEditor.TabIndex = 0;
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.AutoSize = true;
            this.sendButton.Enabled = false;
            this.sendButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sendButton.Location = new System.Drawing.Point(841, 423);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(91, 31);
            this.sendButton.TabIndex = 6;
            this.sendButton.Text = "发送消息";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.SendMessage);
            // 
            // connectButton
            // 
            this.connectButton.AutoSize = true;
            this.connectButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.connectButton.Location = new System.Drawing.Point(228, 10);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(91, 31);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "连接";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ChangeConnectStatus);
            // 
            // fileButton
            // 
            this.fileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fileButton.AutoSize = true;
            this.fileButton.Enabled = false;
            this.fileButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fileButton.Location = new System.Drawing.Point(841, 460);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(91, 31);
            this.fileButton.TabIndex = 9;
            this.fileButton.Text = "发送文件";
            this.fileButton.UseVisualStyleBackColor = true;
            this.fileButton.Click += new System.EventHandler(this.SendFile);
            // 
            // selectButton
            // 
            this.selectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectButton.AutoSize = true;
            this.selectButton.Enabled = false;
            this.selectButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.selectButton.Location = new System.Drawing.Point(745, 460);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(90, 31);
            this.selectButton.TabIndex = 8;
            this.selectButton.Text = "选择";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.SelectFile);
            // 
            // directoryEditor
            // 
            this.directoryEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryEditor.Enabled = false;
            this.directoryEditor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.directoryEditor.Location = new System.Drawing.Point(12, 460);
            this.directoryEditor.Name = "directoryEditor";
            this.directoryEditor.Size = new System.Drawing.Size(727, 29);
            this.directoryEditor.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AcceptButton = this.connectButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.directoryEditor);
            this.Controls.Add(this.messageEditor);
            this.Controls.Add(this.logEditor);
            this.Controls.Add(this.portEditor);
            this.Controls.Add(this.IPEditor);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.connectButton);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(960, 540);
            this.Name = "MainForm";
            this.Text = "客户端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClosingForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox messageEditor;
        private System.Windows.Forms.TextBox logEditor;
        private System.Windows.Forms.TextBox portEditor;
        private System.Windows.Forms.TextBox IPEditor;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.TextBox directoryEditor;
    }
}

