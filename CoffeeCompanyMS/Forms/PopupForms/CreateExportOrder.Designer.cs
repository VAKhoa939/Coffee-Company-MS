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
            this.locationSelector1 = new CoffeeCompanyMS.UC.LocationSelector();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DTPDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewIngredients = new System.Windows.Forms.DataGridView();
            this.IngredientColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.UnitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActionColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.locationSelector2 = new CoffeeCompanyMS.UC.LocationSelector();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIngredients)).BeginInit();
            this.SuspendLayout();
            // 
            // locationSelector1
            // 
            this.locationSelector1.Location = new System.Drawing.Point(119, -8);
            this.locationSelector1.Margin = new System.Windows.Forms.Padding(0);
            this.locationSelector1.Name = "locationSelector1";
            this.locationSelector1.Size = new System.Drawing.Size(411, 63);
            this.locationSelector1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(20, 137);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(171, 26);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Enable Recurence";
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
            this.DTPDeliveryDate.Location = new System.Drawing.Point(228, 188);
            this.DTPDeliveryDate.Name = "DTPDeliveryDate";
            this.DTPDeliveryDate.Size = new System.Drawing.Size(200, 22);
            this.DTPDeliveryDate.TabIndex = 7;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(228, 228);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(196, 22);
            this.numericUpDown1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 35);
            this.label4.TabIndex = 9;
            this.label4.Text = "Destination";
            // 
            // dataGridViewIngredients
            // 
            this.dataGridViewIngredients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIngredients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IngredientColumn,
            this.UnitColumn,
            this.UnitPriceColumn,
            this.QuantityColumn,
            this.ActionColumn});
            this.dataGridViewIngredients.Location = new System.Drawing.Point(12, 267);
            this.dataGridViewIngredients.Name = "dataGridViewIngredients";
            this.dataGridViewIngredients.RowHeadersWidth = 51;
            this.dataGridViewIngredients.RowTemplate.Height = 24;
            this.dataGridViewIngredients.Size = new System.Drawing.Size(756, 150);
            this.dataGridViewIngredients.TabIndex = 11;
            // 
            // IngredientColumn
            // 
            this.IngredientColumn.HeaderText = "Ingredient Name";
            this.IngredientColumn.MinimumWidth = 6;
            this.IngredientColumn.Name = "IngredientColumn";
            this.IngredientColumn.Width = 125;
            // 
            // UnitColumn
            // 
            this.UnitColumn.HeaderText = "Unit";
            this.UnitColumn.MinimumWidth = 6;
            this.UnitColumn.Name = "UnitColumn";
            this.UnitColumn.ReadOnly = true;
            this.UnitColumn.Width = 125;
            // 
            // UnitPriceColumn
            // 
            this.UnitPriceColumn.HeaderText = " Unit Price";
            this.UnitPriceColumn.MinimumWidth = 6;
            this.UnitPriceColumn.Name = "UnitPriceColumn";
            this.UnitPriceColumn.ReadOnly = true;
            this.UnitPriceColumn.Width = 125;
            // 
            // QuantityColumn
            // 
            this.QuantityColumn.HeaderText = "Quantity";
            this.QuantityColumn.MinimumWidth = 6;
            this.QuantityColumn.Name = "QuantityColumn";
            this.QuantityColumn.Width = 125;
            // 
            // ActionColumn
            // 
            this.ActionColumn.HeaderText = "Action";
            this.ActionColumn.MinimumWidth = 6;
            this.ActionColumn.Name = "ActionColumn";
            this.ActionColumn.Width = 125;
            // 
            // locationSelector2
            // 
            this.locationSelector2.Location = new System.Drawing.Point(182, 55);
            this.locationSelector2.Margin = new System.Windows.Forms.Padding(0);
            this.locationSelector2.Name = "locationSelector2";
            this.locationSelector2.Size = new System.Drawing.Size(405, 62);
            this.locationSelector2.TabIndex = 12;
            // 
            // CreateExportOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.locationSelector2);
            this.Controls.Add(this.dataGridViewIngredients);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.DTPDeliveryDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.locationSelector1);
            this.Name = "CreateExportOrder";
            this.Text = "CreateExportOrder";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIngredients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UC.LocationSelector locationSelector1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker DTPDeliveryDate;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewIngredients;
        private System.Windows.Forms.DataGridViewComboBoxColumn IngredientColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantityColumn;
        private System.Windows.Forms.DataGridViewButtonColumn ActionColumn;
        private UC.LocationSelector locationSelector2;
    }
}