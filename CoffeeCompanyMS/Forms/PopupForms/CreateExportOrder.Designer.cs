namespace CoffeeCompanyMS.UI.Export
{
    partial class CreateExportOrder
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DTPDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.dataGridViewIngredients = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.storeBranchSelector1 = new CoffeeCompanyMS.UC.StoreBranchSelector();
            this.warehouseSelector1 = new CoffeeCompanyMS.UC.WarehouseSelector();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIngredients)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(20, 137);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(178, 26);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Enable Recurrence";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Estimated Delivery Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Recurrence Period (days)";
            // 
            // DTPDeliveryDate
            // 
            this.DTPDeliveryDate.CustomFormat = "yyyy-MM-dd";
            this.DTPDeliveryDate.Location = new System.Drawing.Point(266, 188);
            this.DTPDeliveryDate.Name = "DTPDeliveryDate";
            this.DTPDeliveryDate.Size = new System.Drawing.Size(251, 22);
            this.DTPDeliveryDate.TabIndex = 7;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(336, 228);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(103, 22);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dataGridViewIngredients
            // 
            this.dataGridViewIngredients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewIngredients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIngredients.Location = new System.Drawing.Point(12, 267);
            this.dataGridViewIngredients.Name = "dataGridViewIngredients";
            this.dataGridViewIngredients.RowHeadersWidth = 51;
            this.dataGridViewIngredients.RowTemplate.Height = 24;
            this.dataGridViewIngredients.Size = new System.Drawing.Size(756, 150);
            this.dataGridViewIngredients.TabIndex = 11;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(361, 436);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(67, 35);
            this.btnSubmit.TabIndex = 14;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // storeBranchSelector1
            // 
            this.storeBranchSelector1.Location = new System.Drawing.Point(20, 66);
            this.storeBranchSelector1.Margin = new System.Windows.Forms.Padding(0);
            this.storeBranchSelector1.Name = "storeBranchSelector1";
            this.storeBranchSelector1.Size = new System.Drawing.Size(600, 35);
            this.storeBranchSelector1.TabIndex = 13;
            // 
            // warehouseSelector1
            // 
            this.warehouseSelector1.Location = new System.Drawing.Point(20, 9);
            this.warehouseSelector1.Margin = new System.Windows.Forms.Padding(0);
            this.warehouseSelector1.Name = "warehouseSelector1";
            this.warehouseSelector1.Size = new System.Drawing.Size(600, 35);
            this.warehouseSelector1.TabIndex = 12;
            // 
            // CreateExportOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 482);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.storeBranchSelector1);
            this.Controls.Add(this.warehouseSelector1);
            this.Controls.Add(this.dataGridViewIngredients);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.DTPDeliveryDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox1);
            this.Name = "CreateExportOrder";
            this.Text = "CreateExportOrder";
            this.Load += new System.EventHandler(this.CreateExportOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIngredients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker DTPDeliveryDate;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.DataGridView dataGridViewIngredients;
        private UC.WarehouseSelector warehouseSelector1;
        private UC.StoreBranchSelector storeBranchSelector1;
        private System.Windows.Forms.Button btnSubmit;
    }
}