namespace CoffeeCompanyMS.UC
{
    partial class LocationSelector
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
            this.labelLocation = new System.Windows.Forms.Label();
            this.cbbLocation = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.Font = new System.Drawing.Font("Microsoft Tai Le", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLocation.Location = new System.Drawing.Point(3, 21);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(161, 43);
            this.labelLocation.TabIndex = 0;
            this.labelLocation.Text = "Location:";
            // 
            // cbbLocation
            // 
            this.cbbLocation.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbLocation.FormattingEnabled = true;
            this.cbbLocation.Location = new System.Drawing.Point(162, 21);
            this.cbbLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbbLocation.Name = "cbbLocation";
            this.cbbLocation.Size = new System.Drawing.Size(271, 33);
            this.cbbLocation.TabIndex = 1;
            this.cbbLocation.SelectedIndexChanged += new System.EventHandler(this.cbbLocation_SelectedIndexChanged);
            // 
            // LocationSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbbLocation);
            this.Controls.Add(this.labelLocation);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LocationSelector";
            this.Size = new System.Drawing.Size(462, 79);
            this.Load += new System.EventHandler(this.LocationSelector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.ComboBox cbbLocation;
    }
}
