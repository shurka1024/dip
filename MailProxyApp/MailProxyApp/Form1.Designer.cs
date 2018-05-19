namespace MailProxyApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.log = new System.Windows.Forms.TextBox();
            this.appStatus = new System.Windows.Forms.Label();
            this.currentUserInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(3, 92);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(456, 158);
            this.log.TabIndex = 0;
            // 
            // appStatus
            // 
            this.appStatus.AutoSize = true;
            this.appStatus.Location = new System.Drawing.Point(13, 13);
            this.appStatus.Name = "appStatus";
            this.appStatus.Size = new System.Drawing.Size(35, 13);
            this.appStatus.TabIndex = 1;
            this.appStatus.Text = "label1";
            // 
            // currentUserInfo
            // 
            this.currentUserInfo.AutoSize = true;
            this.currentUserInfo.Location = new System.Drawing.Point(16, 30);
            this.currentUserInfo.Name = "currentUserInfo";
            this.currentUserInfo.Size = new System.Drawing.Size(35, 13);
            this.currentUserInfo.TabIndex = 2;
            this.currentUserInfo.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 262);
            this.Controls.Add(this.currentUserInfo);
            this.Controls.Add(this.appStatus);
            this.Controls.Add(this.log);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MailProxyApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.Label appStatus;
        private System.Windows.Forms.Label currentUserInfo;
    }
}

