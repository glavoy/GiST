namespace gist
{
    partial class Survey
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
            this.NextButton = new System.Windows.Forms.Button();
            this.PrevButton = new System.Windows.Forms.Button();
            this.responsePanel = new System.Windows.Forms.Panel();
            this.questionLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.subjIDLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NextButton
            // 
            this.NextButton.BackgroundImage = global::gist.Properties.Resources.next;
            this.NextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NextButton.Location = new System.Drawing.Point(169, 332);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(116, 99);
            this.NextButton.TabIndex = 0;
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // PrevButton
            // 
            this.PrevButton.BackgroundImage = global::gist.Properties.Resources.previous;
            this.PrevButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PrevButton.Location = new System.Drawing.Point(12, 332);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(116, 99);
            this.PrevButton.TabIndex = 1;
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // responsePanel
            // 
            this.responsePanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.responsePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.responsePanel.Location = new System.Drawing.Point(12, 130);
            this.responsePanel.Name = "responsePanel";
            this.responsePanel.Size = new System.Drawing.Size(565, 196);
            this.responsePanel.TabIndex = 2;
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionLabel.Location = new System.Drawing.Point(29, 9);
            this.questionLabel.MaximumSize = new System.Drawing.Size(750, 0);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(164, 20);
            this.questionLabel.TabIndex = 3;
            this.questionLabel.Text = "Question goes here....";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(588, 41);
            this.label1.MaximumSize = new System.Drawing.Size(750, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 0);
            this.label1.TabIndex = 4;
            this.label1.Text = "Additional text";
            // 
            // subjIDLabel
            // 
            this.subjIDLabel.AutoSize = true;
            this.subjIDLabel.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjIDLabel.Location = new System.Drawing.Point(587, 77);
            this.subjIDLabel.MaximumSize = new System.Drawing.Size(750, 0);
            this.subjIDLabel.Name = "subjIDLabel";
            this.subjIDLabel.Size = new System.Drawing.Size(66, 27);
            this.subjIDLabel.TabIndex = 5;
            this.subjIDLabel.Text = "subjid";
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(624, 17);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 32);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // Survey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 581);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.subjIDLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.questionLabel);
            this.Controls.Add(this.responsePanel);
            this.Controls.Add(this.PrevButton);
            this.Controls.Add(this.NextButton);
            this.Name = "Survey";
            this.Text = "newSurvey";
            this.Load += new System.EventHandler(this.Survey_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.Panel responsePanel;
        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label subjIDLabel;
        private System.Windows.Forms.Button cancelButton;
    }
}