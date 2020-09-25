namespace gist
{
    partial class newSurvey
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
            this.nextButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.responsePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // nextButton
            // 
            this.nextButton.BackgroundImage = global::gist.Properties.Resources.next;
            this.nextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.nextButton.Location = new System.Drawing.Point(200, 332);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(116, 99);
            this.nextButton.TabIndex = 0;
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // prevButton
            // 
            this.prevButton.BackgroundImage = global::gist.Properties.Resources.previous;
            this.prevButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.prevButton.Location = new System.Drawing.Point(32, 332);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(116, 99);
            this.prevButton.TabIndex = 1;
            this.prevButton.UseVisualStyleBackColor = true;
            // 
            // responsePanel
            // 
            this.responsePanel.Location = new System.Drawing.Point(32, 113);
            this.responsePanel.Name = "responsePanel";
            this.responsePanel.Size = new System.Drawing.Size(361, 213);
            this.responsePanel.TabIndex = 2;
            // 
            // newSurvey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.responsePanel);
            this.Controls.Add(this.prevButton);
            this.Controls.Add(this.nextButton);
            this.Name = "newSurvey";
            this.Text = "newSurvey";
            this.Load += new System.EventHandler(this.newSurvey_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Panel responsePanel;
    }
}