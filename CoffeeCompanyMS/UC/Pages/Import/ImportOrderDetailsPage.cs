using CoffeeCompanyMS.Models;
using CoffeeCompanyMS.Patterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace CoffeeCompanyMS.UC.Pages.Import
{
    public partial class ImportOrderDetailsPage : UserControl
    {
        private Guid selectedOrderID;
        private TransferOrder order;

        public ImportOrderDetailsPage(Guid orderID)
        {
            InitializeComponent();
            this.selectedOrderID = orderID;
        }

        private void ImportOrderDetailsPage_Load(object sender, EventArgs e)
        {
            var orderDAO = DAOManager.Instance.TransferOrderDAO;
            order = orderDAO.GetTransferOrderById(selectedOrderID);
            LoadOrderSummary();
            LoadOrderItems();
        }

        private void LoadOrderSummary()
        {
            try
            {
                var supplierDAO = DAOManager.Instance.SupplierDAO;

                lblOrderID.Text = selectedOrderID.ToString();
                lblSupplierName.Text = supplierDAO.GetImportSupplierName(selectedOrderID);
                lblItemCount.Text = order.Items.Count.ToString();
                lblTotalCost.Text = order.CalculateTotalCost().ToString("C2"); // Assuming UnitPrice is in decimal format
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting order summary: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrderItems()
        {
            try
            {
                var table = new DataTable();
                table.Columns.Add("IngredientName", typeof(string));
                table.Columns.Add("Quantity", typeof(int));
                table.Columns.Add("Unit", typeof(string));
                table.Columns.Add("UnitPrice", typeof(decimal));
                table.Columns.Add("ExpirationDate", typeof(DateTime));

                foreach (var item in order.Items)
                {
                    var ing = item.Ingredient;
                    table.Rows.Add(ing.Name, item.Quantity, ing.Unit, ing.UnitPrice, item.ExpirationDate);
                }

                dataGridViewItems.DataSource = table;

                if (dataGridViewItems.Columns.Count > 0)
                {
                    dataGridViewItems.Columns["IngredientName"].HeaderText = "Ingredient Name";
                    dataGridViewItems.Columns["UnitPrice"].HeaderText = "Unit Price ($)";
                    dataGridViewItems.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting order items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
