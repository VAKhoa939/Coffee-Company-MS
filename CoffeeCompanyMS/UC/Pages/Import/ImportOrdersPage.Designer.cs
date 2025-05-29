namespace CoffeeCompanyMS.UC.Pages.Import
{
    partial class ImportOrdersPage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewImportOrder = new System.Windows.Forms.DataGridView();
            this.locationSelector1 = new CoffeeCompanyMS.UC.LocationSelector();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImportOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewImportOrder
            // 
            this.dataGridViewImportOrder.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImportOrder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewImportOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImportOrder.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewImportOrder.Location = new System.Drawing.Point(27, 87);
            this.dataGridViewImportOrder.Name = "dataGridViewImportOrder";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImportOrder.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewImportOrder.RowHeadersWidth = 51;
            this.dataGridViewImportOrder.RowTemplate.Height = 24;
            this.dataGridViewImportOrder.Size = new System.Drawing.Size(738, 405);
            this.dataGridViewImportOrder.TabIndex = 5;
            this.dataGridViewImportOrder.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewImportOrder_CellDoubleClick);
            this.dataGridViewImportOrder.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewImportOrder_CellValueChanged);
            this.dataGridViewImportOrder.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewImportOrder_EditingControlShowing);
            // 
            // locationSelector1
            // 
            this.locationSelector1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.locationSelector1.Location = new System.Drawing.Point(45, 21);
            this.locationSelector1.Margin = new System.Windows.Forms.Padding(0);
            this.locationSelector1.Name = "locationSelector1";
            this.locationSelector1.Size = new System.Drawing.Size(411, 63);
            this.locationSelector1.TabIndex = 4;
            // 
            // ImportOrdersPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewImportOrder);
            this.Controls.Add(this.locationSelector1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ImportOrdersPage";
            this.Size = new System.Drawing.Size(791, 529);
            this.Load += new System.EventHandler(this.ImportOrdersPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImportOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LocationSelector locationSelector1;
        private System.Windows.Forms.DataGridView dataGridViewImportOrder;
    }
}
