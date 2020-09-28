namespace gist
{
    partial class MainMenu
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
            this.newSurveyButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.subjIDTextBox = new System.Windows.Forms.TextBox();
            this.subjIDLabel1 = new System.Windows.Forms.Label();
            this.subjIDLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // newSurveyButton
            // 
            this.newSurveyButton.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newSurveyButton.Location = new System.Drawing.Point(257, 237);
            this.newSurveyButton.Name = "newSurveyButton";
            this.newSurveyButton.Size = new System.Drawing.Size(165, 89);
            this.newSurveyButton.TabIndex = 0;
            this.newSurveyButton.Text = "New survey";
            this.newSurveyButton.UseVisualStyleBackColor = true;
            this.newSurveyButton.Click += new System.EventHandler(this.newSurveyButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(12, 12);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(109, 65);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "Quit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // subjIDTextBox
            // 
            this.subjIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjIDTextBox.Location = new System.Drawing.Point(257, 176);
            this.subjIDTextBox.MaxLength = 10;
            this.subjIDTextBox.Name = "subjIDTextBox";
            this.subjIDTextBox.Size = new System.Drawing.Size(165, 35);
            this.subjIDTextBox.TabIndex = 0;
            // 
            // subjIDLabel1
            // 
            this.subjIDLabel1.AutoSize = true;
            this.subjIDLabel1.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjIDLabel1.Location = new System.Drawing.Point(274, 126);
            this.subjIDLabel1.Name = "subjIDLabel1";
            this.subjIDLabel1.Size = new System.Drawing.Size(133, 31);
            this.subjIDLabel1.TabIndex = 3;
            this.subjIDLabel1.Text = "Subject ID";
            // 
            // subjIDLabel
            // 
            this.subjIDLabel.AutoSize = true;
            this.subjIDLabel.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjIDLabel.Location = new System.Drawing.Point(110, 376);
            this.subjIDLabel.Name = "subjIDLabel";
            this.subjIDLabel.Size = new System.Drawing.Size(133, 31);
            this.subjIDLabel.TabIndex = 4;
            this.subjIDLabel.Text = "Subject ID";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 450);
            this.Controls.Add(this.subjIDLabel);
            this.Controls.Add(this.subjIDLabel1);
            this.Controls.Add(this.subjIDTextBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.newSurveyButton);
            this.Name = "MainMenu";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button newSurveyButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox subjIDTextBox;
        private System.Windows.Forms.Label subjIDLabel1;
        private System.Windows.Forms.Label subjIDLabel;
    }
}

