namespace Reader_CE208_S7
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
            this.components = new System.ComponentModel.Container();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.serialPort_CE208_S7 = new System.IO.Ports.SerialPort(this.components);
            this.Connect_CE208 = new System.Windows.Forms.Button();
            this.LogText = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.ReadingOutOfSession = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Enabled = false;
            this.buttonConnect.Location = new System.Drawing.Point(664, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(124, 52);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Log in to the session";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(664, 70);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(124, 52);
            this.buttonUpdate.TabIndex = 1;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(520, 12);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(138, 21);
            this.comboBoxPorts.TabIndex = 2;
            // 
            // serialPort_CE208_S7
            // 
            this.serialPort_CE208_S7.DtrEnable = true;
            this.serialPort_CE208_S7.PortName = "COM4";
            this.serialPort_CE208_S7.ReadTimeout = 2000;
            // 
            // Connect_CE208
            // 
            this.Connect_CE208.Location = new System.Drawing.Point(664, 70);
            this.Connect_CE208.Name = "Connect_CE208";
            this.Connect_CE208.Size = new System.Drawing.Size(124, 52);
            this.Connect_CE208.TabIndex = 3;
            this.Connect_CE208.Text = "Reading in a Session";
            this.Connect_CE208.UseVisualStyleBackColor = true;
            this.Connect_CE208.Visible = false;
            this.Connect_CE208.Click += new System.EventHandler(this.Connect_CE208_Click);
            // 
            // LogText
            // 
            this.LogText.Location = new System.Drawing.Point(12, 12);
            this.LogText.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.LogText.Multiline = true;
            this.LogText.Name = "LogText";
            this.LogText.ReadOnly = true;
            this.LogText.Size = new System.Drawing.Size(398, 200);
            this.LogText.TabIndex = 4;
            this.LogText.Visible = false;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(12, 218);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(102, 46);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Visible = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // ReadingOutOfSession
            // 
            this.ReadingOutOfSession.Enabled = false;
            this.ReadingOutOfSession.Location = new System.Drawing.Point(664, 128);
            this.ReadingOutOfSession.Name = "ReadingOutOfSession";
            this.ReadingOutOfSession.Size = new System.Drawing.Size(124, 52);
            this.ReadingOutOfSession.TabIndex = 7;
            this.ReadingOutOfSession.Text = "Reading out of session";
            this.ReadingOutOfSession.UseVisualStyleBackColor = true;
            this.ReadingOutOfSession.Click += new System.EventHandler(this.ReadingOutOfSession_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 415);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(776, 23);
            this.progressBar1.TabIndex = 8;
            this.progressBar1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.ReadingOutOfSession);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.LogText);
            this.Controls.Add(this.Connect_CE208);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.IO.Ports.SerialPort serialPort_CE208_S7;
        private System.Windows.Forms.Button Connect_CE208;
        private System.Windows.Forms.TextBox LogText;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button ReadingOutOfSession;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

