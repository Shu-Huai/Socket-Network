﻿
namespace TCP_Server
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
            this.IPCombo = new System.Windows.Forms.ComboBox();
            this.beginButton = new System.Windows.Forms.Button();
            this.logEditor = new System.Windows.Forms.TextBox();
            this.portEditor = new System.Windows.Forms.TextBox();
            this.IPEditor = new System.Windows.Forms.TextBox();
            this.IPLabel = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IPCombo
            // 
            this.IPCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IPCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IPCombo.Enabled = false;
            this.IPCombo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IPCombo.FormattingEnabled = true;
            this.IPCombo.Location = new System.Drawing.Point(655, 12);
            this.IPCombo.Name = "IPCombo";
            this.IPCombo.Size = new System.Drawing.Size(187, 29);
            this.IPCombo.TabIndex = 3;
            // 
            // beginButton
            // 
            this.beginButton.AutoSize = true;
            this.beginButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.beginButton.Location = new System.Drawing.Point(228, 10);
            this.beginButton.Name = "beginButton";
            this.beginButton.Size = new System.Drawing.Size(91, 31);
            this.beginButton.TabIndex = 2;
            this.beginButton.Text = "开始监听";
            this.beginButton.UseVisualStyleBackColor = true;
            this.beginButton.Click += new System.EventHandler(this.BeginWatch);
            // 
            // logEditor
            // 
            this.logEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logEditor.Enabled = false;
            this.logEditor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.logEditor.Location = new System.Drawing.Point(12, 47);
            this.logEditor.Multiline = true;
            this.logEditor.Name = "logEditor";
            this.logEditor.ReadOnly = true;
            this.logEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logEditor.Size = new System.Drawing.Size(920, 442);
            this.logEditor.TabIndex = 5;
            this.logEditor.WordWrap = false;
            // 
            // portEditor
            // 
            this.portEditor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.portEditor.Location = new System.Drawing.Point(159, 12);
            this.portEditor.Name = "portEditor";
            this.portEditor.Size = new System.Drawing.Size(63, 29);
            this.portEditor.TabIndex = 1;
            this.portEditor.Text = "50000";
            // 
            // IPEditor
            // 
            this.IPEditor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IPEditor.Location = new System.Drawing.Point(12, 12);
            this.IPEditor.Name = "IPEditor";
            this.IPEditor.Size = new System.Drawing.Size(141, 29);
            this.IPEditor.TabIndex = 0;
            this.IPEditor.Text = "0.0.0.0";
            // 
            // IPLabel
            // 
            this.IPLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IPLabel.AutoSize = true;
            this.IPLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IPLabel.Location = new System.Drawing.Point(543, 15);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(106, 21);
            this.IPLabel.TabIndex = 24;
            this.IPLabel.Text = "连接的地址：";
            // 
            // disconnectButton
            // 
            this.disconnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.disconnectButton.AutoSize = true;
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.disconnectButton.Location = new System.Drawing.Point(848, 10);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(84, 31);
            this.disconnectButton.TabIndex = 4;
            this.disconnectButton.Text = "断开连接";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.Disconnect);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.IPCombo);
            this.Controls.Add(this.beginButton);
            this.Controls.Add(this.logEditor);
            this.Controls.Add(this.portEditor);
            this.Controls.Add(this.IPEditor);
            this.MinimumSize = new System.Drawing.Size(960, 540);
            this.Name = "MainForm";
            this.Text = "服务端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox IPCombo;
        private System.Windows.Forms.Button beginButton;
        private System.Windows.Forms.TextBox logEditor;
        private System.Windows.Forms.TextBox portEditor;
        private System.Windows.Forms.TextBox IPEditor;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Button disconnectButton;
    }
}

