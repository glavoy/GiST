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
            this.uniqueIDSComboBox = new System.Windows.Forms.ComboBox();
            this.modifySurveyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // newSurveyButton
            // 
            this.newSurveyButton.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newSurveyButton.Location = new System.Drawing.Point(116, 237);
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
            this.subjIDTextBox.Location = new System.Drawing.Point(116, 176);
            this.subjIDTextBox.MaxLength = 10;
            this.subjIDTextBox.Name = "subjIDTextBox";
            this.subjIDTextBox.Size = new System.Drawing.Size(165, 35);
            this.subjIDTextBox.TabIndex = 0;
            // 
            // subjIDLabel1
            // 
            this.subjIDLabel1.AutoSize = true;
            this.subjIDLabel1.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjIDLabel1.Location = new System.Drawing.Point(133, 126);
            this.subjIDLabel1.Name = "subjIDLabel1";
            this.subjIDLabel1.Size = new System.Drawing.Size(133, 31);
            this.subjIDLabel1.TabIndex = 3;
            this.subjIDLabel1.Text = "Subject ID";
            // 
            // uniqueIDSComboBox
            // 
            this.uniqueIDSComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uniqueIDSComboBox.FormattingEnabled = true;
            this.uniqueIDSComboBox.Location = new System.Drawing.Point(374, 176);
            this.uniqueIDSComboBox.Name = "uniqueIDSComboBox";
            this.uniqueIDSComboBox.Size = new System.Drawing.Size(165, 37);
            this.uniqueIDSComboBox.TabIndex = 5;
            // 
            // modifySurveyButton
            // 
            this.modifySurveyButton.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifySurveyButton.Location = new System.Drawing.Point(374, 237);
            this.modifySurveyButton.Name = "modifySurveyButton";
            this.modifySurveyButton.Size = new System.Drawing.Size(165, 89);
            this.modifySurveyButton.TabIndex = 6;
            this.modifySurveyButton.Text = "Modify Survey";
            this.modifySurveyButton.UseVisualStyleBackColor = true;
            this.modifySurveyButton.Click += new System.EventHandler(this.modifySurveyButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(133, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 31);
            this.label1.TabIndex = 7;
            this.label1.Text = "Enter New";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(368, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "Select Existing";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(389, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 31);
            this.label3.TabIndex = 9;
            this.label3.Text = "Subject ID";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modifySurveyButton);
            this.Controls.Add(this.uniqueIDSComboBox);
            this.Controls.Add(this.subjIDLabel1);
            this.Controls.Add(this.subjIDTextBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.newSurveyButton);
            this.Name = "MainMenu";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button newSurveyButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox subjIDTextBox;
        private System.Windows.Forms.Label subjIDLabel1;
        private System.Windows.Forms.ComboBox uniqueIDSComboBox;
        private System.Windows.Forms.Button modifySurveyButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

