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
            this.SuspendLayout();
            // 
            // NextButton
            // 
            this.NextButton.BackgroundImage = global::gist.Properties.Resources.next;
            this.NextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NextButton.Location = new System.Drawing.Point(200, 332);
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
            this.PrevButton.Location = new System.Drawing.Point(32, 332);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(116, 99);
            this.PrevButton.TabIndex = 1;
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // responsePanel
            // 
            this.responsePanel.Location = new System.Drawing.Point(32, 130);
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
            // Survey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}