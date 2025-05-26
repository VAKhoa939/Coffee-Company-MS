namespace CoffeeCompanyMS.UC
{
    partial class WarehouseSelector
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
            this.cbbLocation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbbLocation
            // 
            this.cbbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLocation.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbLocation.FormattingEnabled = true;
            this.cbbLocation.Location = new System.Drawing.Point(182, 0);
            this.cbbLocation.Name = "cbbLocation";
            this.cbbLocation.Size = new System.Drawing.Size(411, 29);
            this.cbbLocation.TabIndex = 0;
            this.cbbLocation.SelectedIndexChanged += new System.EventHandler(this.cbbLocation_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source Warehouse";
            // 
            // WarehouseSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbLocation);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "WarehouseSelector";
            this.Size = new System.Drawing.Size(600, 35);
            this.Load += new System.EventHandler(this.WarehouseSelector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cbbLocation;
        private System.Windows.Forms.Label label1;
    }
} 