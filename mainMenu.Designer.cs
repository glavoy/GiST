namespace gist
{
    partial class mainMenu
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
            this.SuspendLayout();
            // 
            // newSurveyButton
            // 
            this.newSurveyButton.Location = new System.Drawing.Point(455, 216);
            this.newSurveyButton.Name = "newSurveyButton";
            this.newSurveyButton.Size = new System.Drawing.Size(75, 23);
            this.newSurveyButton.TabIndex = 0;
            this.newSurveyButton.Text = "New survey";
            this.newSurveyButton.UseVisualStyleBackColor = true;
            this.newSurveyButton.Click += new System.EventHandler(this.newSurveyButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(41, 27);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "button2";
            this.exitButton.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.newSurveyButton);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newSurveyButton;
        private System.Windows.Forms.Button exitButton;
    }
}

