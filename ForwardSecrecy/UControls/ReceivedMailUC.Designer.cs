namespace ForwardSecrecy.UControls
{
    partial class ReceivedMailUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvRcvMail = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.From = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOpenMessage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRcvMail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRcvMail
            // 
            this.dgvRcvMail.AllowUserToAddRows = false;
            this.dgvRcvMail.AllowUserToDeleteRows = false;
            this.dgvRcvMail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRcvMail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.From,
            this.Subject});
            this.dgvRcvMail.Location = new System.Drawing.Point(7, 8);
            this.dgvRcvMail.Name = "dgvRcvMail";
            this.dgvRcvMail.RowTemplate.Height = 25;
            this.dgvRcvMail.Size = new System.Drawing.Size(834, 344);
            this.dgvRcvMail.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Width = 50;
            // 
            // From
            // 
            this.From.HeaderText = "From";
            this.From.Name = "From";
            this.From.Width = 225;
            // 
            // Subject
            // 
            this.Subject.HeaderText = "Subject";
            this.Subject.Name = "Subject";
            this.Subject.Width = 515;
            // 
            // btnOpenMessage
            // 
            this.btnOpenMessage.Location = new System.Drawing.Point(707, 358);
            this.btnOpenMessage.Name = "btnOpenMessage";
            this.btnOpenMessage.Size = new System.Drawing.Size(134, 31);
            this.btnOpenMessage.TabIndex = 1;
            this.btnOpenMessage.Text = "Open message";
            this.btnOpenMessage.UseVisualStyleBackColor = true;
            this.btnOpenMessage.Click += new System.EventHandler(this.btnOpenMessage_Click);
            // 
            // ReceivedMailUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOpenMessage);
            this.Controls.Add(this.dgvRcvMail);
            this.Name = "ReceivedMailUC";
            this.Size = new System.Drawing.Size(845, 395);
            this.Load += new System.EventHandler(this.ReceivedMailUC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRcvMail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgvRcvMail;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn From;
        private DataGridViewTextBoxColumn Subject;
        private Button btnOpenMessage;
    }
}
