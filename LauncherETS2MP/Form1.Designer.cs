namespace LauncherETS2MP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnPlay = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.backgroundDownload = new System.ComponentModel.BackgroundWorker();
            this.resultLabel = new System.Windows.Forms.Label();
            this.backgroundExtract = new System.ComponentModel.BackgroundWorker();
            this.backgroundInnoUP = new System.ComponentModel.BackgroundWorker();
            this.backgroundCopy = new System.ComponentModel.BackgroundWorker();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurVersion = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundDelete = new System.ComponentModel.BackgroundWorker();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.backgroundGame = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Black;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPlay.ForeColor = System.Drawing.Color.Black;
            this.btnPlay.Image = global::LauncherETS2MP.Properties.Resources.wheel;
            this.btnPlay.Location = new System.Drawing.Point(530, 182);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(60, 60);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Go";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(27, 203);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(486, 23);
            this.progressBar.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.MediumBlue;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExit.Image = global::LauncherETS2MP.Properties.Resources.btnClose;
            this.btnExit.Location = new System.Drawing.Point(582, 1);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(14, 14);
            this.btnExit.TabIndex = 3;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(251, 2);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(57, 13);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "Need auth";
            this.lblUser.Click += new System.EventHandler(this.lblUser_Click);
            this.lblUser.MouseLeave += new System.EventHandler(this.lblUser_MouseLeave);
            this.lblUser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblUser_MouseMove);
            // 
            // backgroundDownload
            // 
            this.backgroundDownload.WorkerReportsProgress = true;
            this.backgroundDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundDownload_DoWork);
            this.backgroundDownload.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundDownload_ProgressChanged);
            this.backgroundDownload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundDownload_RunWorkerCompleted);
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.BackColor = System.Drawing.Color.Transparent;
            this.resultLabel.ForeColor = System.Drawing.Color.SeaGreen;
            this.resultLabel.Location = new System.Drawing.Point(44, 179);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(64, 13);
            this.resultLabel.TabIndex = 6;
            this.resultLabel.Text = "Game ready";
            this.resultLabel.DoubleClick += new System.EventHandler(this.resultLabel_DoubleClick);
            // 
            // backgroundExtract
            // 
            this.backgroundExtract.WorkerReportsProgress = true;
            this.backgroundExtract.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundExtract_DoWork);
            this.backgroundExtract.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundExtract_ProgressChanged);
            this.backgroundExtract.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundExtract_RunWorkerCompleted);
            // 
            // backgroundInnoUP
            // 
            this.backgroundInnoUP.WorkerReportsProgress = true;
            this.backgroundInnoUP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundInnoUP_DoWork);
            this.backgroundInnoUP.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundInnoUP_ProgressChanged);
            this.backgroundInnoUP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundInnoUP_RunWorkerCompleted);
            // 
            // backgroundCopy
            // 
            this.backgroundCopy.WorkerReportsProgress = true;
            this.backgroundCopy.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundCopy_DoWork);
            this.backgroundCopy.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundCopy_ProgressChanged);
            this.backgroundCopy.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundCopy_RunWorkerCompleted);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.MediumBlue;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMinimize.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnMinimize.Image = global::LauncherETS2MP.Properties.Resources.btnHide;
            this.btnMinimize.Location = new System.Drawing.Point(566, 1);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(14, 14);
            this.btnMinimize.TabIndex = 7;
            this.btnMinimize.Text = "-";
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(171, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Logged as: ";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.DarkCyan;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Current version:";
            // 
            // lblCurVersion
            // 
            this.lblCurVersion.AutoSize = true;
            this.lblCurVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblCurVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurVersion.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblCurVersion.Location = new System.Drawing.Point(115, 32);
            this.lblCurVersion.Name = "lblCurVersion";
            this.lblCurVersion.Size = new System.Drawing.Size(73, 13);
            this.lblCurVersion.TabIndex = 10;
            this.lblCurVersion.Text = "1.0.0 Release";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVersion.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblVersion.Location = new System.Drawing.Point(115, 45);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(73, 13);
            this.lblVersion.TabIndex = 12;
            this.lblVersion.Text = "1.0.0 Release";
            this.lblVersion.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.DarkCyan;
            this.label4.Location = new System.Drawing.Point(12, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Your version:";
            this.label4.Visible = false;
            // 
            // backgroundDelete
            // 
            this.backgroundDelete.WorkerReportsProgress = true;
            this.backgroundDelete.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundDelete_DoWork);
            this.backgroundDelete.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundDelete_ProgressChanged);
            this.backgroundDelete.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundDelete_RunWorkerCompleted);
            // 
            // trayIcon
            // 
            this.trayIcon.Text = "notifyIcon1";
            this.trayIcon.Visible = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LauncherETS2MP.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(600, 250);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblCurVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnPlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblUser;
        private System.ComponentModel.BackgroundWorker backgroundDownload;
        private System.Windows.Forms.Label resultLabel;
        private System.ComponentModel.BackgroundWorker backgroundExtract;
        private System.ComponentModel.BackgroundWorker backgroundInnoUP;
        private System.ComponentModel.BackgroundWorker backgroundCopy;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCurVersion;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundDelete;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.Button btnPlay;
        private System.ComponentModel.BackgroundWorker backgroundGame;
    }
}

