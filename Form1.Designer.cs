namespace Client
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            progressBar = new ProgressBar();
            statusLabel = new Label();
            percentageLabel = new Label();
            playBtn = new Button();
            updateBtn = new Button();
            settingsBtn = new Button();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 362);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(776, 23);
            progressBar.TabIndex = 0;
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.None;
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Calibri", 20F);
            statusLabel.Location = new Point(12, 408);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(159, 33);
            statusLabel.TabIndex = 1;
            statusLabel.Text = "Downloading";
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // percentageLabel
            // 
            percentageLabel.Font = new Font("Segoe UI", 12F);
            percentageLabel.Location = new Point(12, 336);
            percentageLabel.Name = "percentageLabel";
            percentageLabel.Size = new Size(92, 23);
            percentageLabel.TabIndex = 4;
            percentageLabel.Text = "0%";
            percentageLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // playBtn
            // 
            playBtn.BackColor = Color.Lime;
            playBtn.Font = new Font("Calibri", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            playBtn.Location = new Point(673, 395);
            playBtn.Name = "playBtn";
            playBtn.Size = new Size(115, 46);
            playBtn.TabIndex = 5;
            playBtn.Text = "Play";
            playBtn.UseVisualStyleBackColor = false;
            playBtn.Click += playBtn_Click;
            // 
            // updateBtn
            // 
            updateBtn.BackColor = Color.Transparent;
            updateBtn.BackgroundImage = Properties.Resources.download__2_;
            updateBtn.BackgroundImageLayout = ImageLayout.Stretch;
            updateBtn.FlatStyle = FlatStyle.Popup;
            updateBtn.Location = new Point(738, 309);
            updateBtn.Name = "updateBtn";
            updateBtn.Size = new Size(50, 50);
            updateBtn.TabIndex = 6;
            updateBtn.UseVisualStyleBackColor = false;
            updateBtn.Click += updateBtn_Click;
            // 
            // settingsBtn
            // 
            settingsBtn.BackColor = Color.Transparent;
            settingsBtn.BackgroundImage = (Image)resources.GetObject("settingsBtn.BackgroundImage");
            settingsBtn.BackgroundImageLayout = ImageLayout.Stretch;
            settingsBtn.FlatStyle = FlatStyle.Popup;
            settingsBtn.Location = new Point(12, 12);
            settingsBtn.Name = "settingsBtn";
            settingsBtn.Size = new Size(50, 50);
            settingsBtn.TabIndex = 7;
            settingsBtn.UseVisualStyleBackColor = false;
            settingsBtn.Click += settingsBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Twitch;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(settingsBtn);
            Controls.Add(updateBtn);
            Controls.Add(playBtn);
            Controls.Add(percentageLabel);
            Controls.Add(statusLabel);
            Controls.Add(progressBar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Updater";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBar;
        private Label statusLabel;
        private Label percentageLabel;
        private Button playBtn;
        private Button updateBtn;
        private Button settingsBtn;
    }
}
