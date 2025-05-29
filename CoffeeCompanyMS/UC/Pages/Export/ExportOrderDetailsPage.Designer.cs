namespace CoffeeCompanyMS.UC.Pages.Export
{
    partial class ExportOrderDetailsPage
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
            this.lblDestinationName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvExportItems = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOrderID = new System.Windows.Forms.Label();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalCost = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExportItems)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDestinationName
            // 
            this.lblDestinationName.AutoSize = true;
            this.lblDestinationName.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestinationName.Location = new System.Drawing.Point(229, 57);
            this.lblDestinationName.Name = "lblDestinationName";
            this.lblDestinationName.Size = new System.Drawing.Size(65, 25);
            this.lblDestinationName.TabIndex = 0;
            this.lblDestinationName.Text = "label2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTotalCost);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblItemCount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblOrderID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblDestinationName);
            this.panel1.Location = new System.Drawing.Point(67, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(656, 136);
            this.panel1.TabIndex = 2;
            // 
            // dgvExportItems
            // 
            this.dgvExportItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExportItems.Location = new System.Drawing.Point(67, 179);
            this.dgvExportItems.Name = "dgvExportItems";
            this.dgvExportItems.RowHeadersWidth = 51;
            this.dgvExportItems.RowTemplate.Height = 24;
            this.dgvExportItems.Size = new System.Drawing.Size(656, 313);
            this.dgvExportItems.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Destination Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "Order ID";
            // 
            // lblOrderID
            // 
            this.lblOrderID.AutoSize = true;
            this.lblOrderID.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderID.Location = new System.Drawing.Point(229, 15);
            this.lblOrderID.Name = "lblOrderID";
            this.lblOrderID.Size = new System.Drawing.Size(65, 25);
            this.lblOrderID.TabIndex = 5;
            this.lblOrderID.Text = "label1";
            // 
            // lblItemCount
            // 
            this.lblItemCount.AutoSize = true;
            this.lblItemCount.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCount.Location = new System.Drawing.Point(568, 15);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(65, 25);
            this.lblItemCount.TabIndex = 7;
            this.lblItemCount.Text = "label3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(360, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = "Item Count";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(360, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 26);
            this.label4.TabIndex = 8;
            this.label4.Text = "Total Cost";
            // 
            // lblTotalCost
            // 
            this.lblTotalCost.AutoSize = true;
            this.lblTotalCost.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCost.Location = new System.Drawing.Point(568, 58);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(65, 25);
            this.lblTotalCost.TabIndex = 9;
            this.lblTotalCost.Text = "label4";
            // 
            // ExportOrderDetailsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvExportItems);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ExportOrderDetailsPage";
            this.Size = new System.Drawing.Size(791, 529);
            this.Load += new System.EventHandler(this.ExportOrderDetailsPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExportItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDestinationName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvExportItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOrderID;
        private System.Windows.Forms.Label lblItemCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalCost;
        private System.Windows.Forms.Label label4;
    }
}
