namespace ForwardSecrecy
{
    partial class EmailHomeForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNewMail = new System.Windows.Forms.Button();
            this.btnMailRcv = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNewMail);
            this.panel1.Controls.Add(this.btnMailRcv);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 55);
            this.panel1.TabIndex = 0;
            // 
            // btnNewMail
            // 
            this.btnNewMail.Location = new System.Drawing.Point(114, 0);
            this.btnNewMail.Name = "btnNewMail";
            this.btnNewMail.Size = new System.Drawing.Size(116, 55);
            this.btnNewMail.TabIndex = 1;
            this.btnNewMail.Text = "New Mail";
            this.btnNewMail.UseVisualStyleBackColor = true;
            this.btnNewMail.Click += new System.EventHandler(this.btnNewMail_Click);
            // 
            // btnMailRcv
            // 
            this.btnMailRcv.Location = new System.Drawing.Point(0, 0);
            this.btnMailRcv.Name = "btnMailRcv";
            this.btnMailRcv.Size = new System.Drawing.Size(116, 55);
            this.btnMailRcv.TabIndex = 0;
            this.btnMailRcv.Text = "Received Mail";
            this.btnMailRcv.UseVisualStyleBackColor = true;
            this.btnMailRcv.Click += new System.EventHandler(this.btnMailRcv_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 55);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(845, 395);
            this.panelContainer.TabIndex = 1;
            // 
            // EmailHomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 450);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panel1);
            this.Name = "EmailHomeForm";
            this.Text = "EmailHomeForm";
            this.Load += new System.EventHandler(this.EmailHomeForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button btnNewMail;
        private Button btnMailRcv;
        private Panel panelContainer;
    }
}