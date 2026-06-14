namespace DiscordGameSpoofer
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
            gamesList = new ListBox();
            searchBox = new TextBox();
            runningGamesList = new ListBox();
            label1 = new Label();
            stopButton = new Button();
            startButton = new Button();
            detailsBox = new RichTextBox();
            label2 = new Label();
            runningGamesTime = new ListBox();
            logBox = new RichTextBox();
            toggleLogButton = new Button();
            versionLabel = new Label();
            SuspendLayout();
            // 
            // gamesList
            // 
            gamesList.BackColor = Color.FromArgb(17, 24, 39);
            gamesList.BorderStyle = BorderStyle.None;
            gamesList.ForeColor = Color.FromArgb(226, 232, 240);
            gamesList.FormattingEnabled = true;
            gamesList.IntegralHeight = false;
            gamesList.Location = new Point(12, 44);
            gamesList.Name = "gamesList";
            gamesList.Size = new Size(170, 391);
            gamesList.TabIndex = 0;
            gamesList.SelectedIndexChanged += gamesList_SelectedIndexChanged;
            // 
            // searchBox
            // 
            searchBox.BackColor = Color.FromArgb(24, 32, 50);
            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.ForeColor = Color.FromArgb(226, 232, 240);
            searchBox.Location = new Point(12, 12);
            searchBox.Name = "searchBox";
            searchBox.PlaceholderText = "Search...";
            searchBox.Size = new Size(170, 23);
            searchBox.TabIndex = 1;
            searchBox.TextChanged += searchBox_TextChanged;
            // 
            // runningGamesList
            // 
            runningGamesList.BackColor = Color.FromArgb(17, 24, 39);
            runningGamesList.BorderStyle = BorderStyle.None;
            runningGamesList.ForeColor = Color.FromArgb(226, 232, 240);
            runningGamesList.FormattingEnabled = true;
            runningGamesList.Location = new Point(188, 44);
            runningGamesList.Name = "runningGamesList";
            runningGamesList.Size = new Size(519, 180);
            runningGamesList.TabIndex = 2;
            runningGamesList.SelectedIndexChanged += runningGamesList_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(10, 14, 23);
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(56, 189, 248);
            label1.Location = new Point(188, 15);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 3;
            label1.Text = "Running Games";
            // 
            // stopButton
            // 
            stopButton.BackColor = Color.FromArgb(244, 63, 94);
            stopButton.Cursor = Cursors.Hand;
            stopButton.FlatAppearance.BorderSize = 0;
            stopButton.FlatStyle = FlatStyle.Flat;
            stopButton.ForeColor = Color.White;
            stopButton.Location = new Point(713, 12);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(75, 23);
            stopButton.TabIndex = 4;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = false;
            stopButton.Click += stopButton_Click;
            // 
            // startButton
            // 
            startButton.BackColor = Color.FromArgb(14, 165, 233);
            startButton.Cursor = Cursors.Hand;
            startButton.FlatAppearance.BorderSize = 0;
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.ForeColor = Color.White;
            startButton.Location = new Point(632, 12);
            startButton.Name = "startButton";
            startButton.Size = new Size(75, 23);
            startButton.TabIndex = 5;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += startButton_Click;
            // 
            // detailsBox
            // 
            detailsBox.BackColor = Color.FromArgb(17, 24, 39);
            detailsBox.BorderStyle = BorderStyle.None;
            detailsBox.Font = new Font("Consolas", 9F);
            detailsBox.ForeColor = Color.FromArgb(226, 232, 240);
            detailsBox.Location = new Point(188, 260);
            detailsBox.Name = "detailsBox";
            detailsBox.ReadOnly = true;
            detailsBox.Size = new Size(600, 175);
            detailsBox.TabIndex = 6;
            detailsBox.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(10, 14, 23);
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(56, 189, 248);
            label2.Location = new Point(188, 234);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 7;
            label2.Text = "Details";
            // 
            // runningGamesTime
            // 
            runningGamesTime.BackColor = Color.FromArgb(17, 24, 39);
            runningGamesTime.BorderStyle = BorderStyle.None;
            runningGamesTime.ForeColor = Color.FromArgb(226, 232, 240);
            runningGamesTime.FormattingEnabled = true;
            runningGamesTime.Location = new Point(713, 44);
            runningGamesTime.Name = "runningGamesTime";
            runningGamesTime.Size = new Size(75, 180);
            runningGamesTime.TabIndex = 8;
            runningGamesTime.SelectedIndexChanged += runningGamesTime_SelectedIndexChanged;
            // 
            // logBox
            // 
            logBox.BackColor = Color.FromArgb(17, 24, 39);
            logBox.BorderStyle = BorderStyle.None;
            logBox.Font = new Font("Consolas", 9F);
            logBox.ForeColor = Color.FromArgb(226, 232, 240);
            logBox.Location = new Point(188, 260);
            logBox.Name = "logBox";
            logBox.ReadOnly = true;
            logBox.Size = new Size(600, 175);
            logBox.TabIndex = 9;
            logBox.Text = "";
            // 
            // toggleLogButton
            // 
            toggleLogButton.BackColor = Color.FromArgb(0, 192, 0);
            toggleLogButton.Cursor = Cursors.Hand;
            toggleLogButton.FlatAppearance.BorderSize = 0;
            toggleLogButton.FlatStyle = FlatStyle.Flat;
            toggleLogButton.ForeColor = Color.White;
            toggleLogButton.Location = new Point(551, 12);
            toggleLogButton.Name = "toggleLogButton";
            toggleLogButton.Size = new Size(75, 23);
            toggleLogButton.TabIndex = 10;
            toggleLogButton.Text = "Toggle Log";
            toggleLogButton.UseVisualStyleBackColor = false;
            toggleLogButton.Click += toggleLogButton_Click;
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.BackColor = Color.FromArgb(10, 14, 23);
            versionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            versionLabel.ForeColor = Color.FromArgb(56, 189, 248);
            versionLabel.Location = new Point(505, 16);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(40, 15);
            versionLabel.TabIndex = 11;
            versionLabel.Text = "v0.0.0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(10, 14, 23);
            ClientSize = new Size(800, 450);
            Controls.Add(versionLabel);
            Controls.Add(toggleLogButton);
            Controls.Add(logBox);
            Controls.Add(runningGamesTime);
            Controls.Add(label2);
            Controls.Add(detailsBox);
            Controls.Add(startButton);
            Controls.Add(stopButton);
            Controls.Add(label1);
            Controls.Add(runningGamesList);
            Controls.Add(searchBox);
            Controls.Add(gamesList);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Discord Game Spoofer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox gamesList;
        private TextBox searchBox;
        private ListBox runningGamesList;
        private Label label1;
        private Button stopButton;
        private Button startButton;
        private RichTextBox detailsBox;
        private Label label2;
        private ListBox runningGamesTime;
        private RichTextBox logBox;
        private Button toggleLogButton;
        private Label versionLabel;
    }
}
