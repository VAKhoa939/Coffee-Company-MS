using CoffeeCompanyMS.Forms.Authentication;
using CoffeeCompanyMS.Models;
using CoffeeCompanyMS.Navigations;
using CoffeeCompanyMS.Patterns;
using CoffeeCompanyMS.UC.Pages.Import;
using CoffeeCompanyMS.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeCompanyMS.UC.Pages.Export
{
    public partial class ExportOrderDetailsPage : UserControl
    {
        private Guid orderId;
        private TransferOrder order;

        // Constructor with Order ID parameter
        public ExportOrderDetailsPage(Guid orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
        }

        // Event handler for Load event of the UserControl
        private void ExportOrderDetailsPage_Load(object sender, EventArgs e)
        {
            // Debug: Check if orderID is valid
            if (orderId == Guid.Empty)
            {
                MessageBox.Show("Invalid Order ID provided.");
                return;
            }
            // Load the order
            var orderDAO = DAOManager.Instance.TransferOrderDAO;
            order = orderDAO.GetTransferOrderById(orderId);
            if (order == null)
            {
                MessageBox.Show("Order not found.");
                return;
            }

            LoadOrderSummary();
            LoadOrderDetails();
        }

        private void LoadOrderSummary()
        {
            lblOrderID.Text = order.Id.ToString();
            lblDestinationName.Text = order.Destination.Name;
            lblItemCount.Text = order.Items.Count.ToString();
            lblTotalCost.Text = "$ " + order.CalculateTotalCost().ToString("C2"); // Assuming UnitPrice is in decimal format
        }

        // Method to load order details into the DataGridView
        private void LoadOrderDetails()
        {
            try
            {
                // Debug: Check if order is loaded
                if (order == null)
                {
                    MessageBox.Show("Order not loaded.");
                    return;
                }

                // Create a DataTable to hold the item details
                DataTable itemsTable = new DataTable();
                itemsTable.Columns.Add("IngredientName", typeof(string));
                itemsTable.Columns.Add("Quantity", typeof(int));
                itemsTable.Columns.Add("Unit", typeof(string));
                itemsTable.Columns.Add("UnitPrice", typeof(decimal));
                itemsTable.Columns.Add("ExpirationDate", typeof(DateTime));

                foreach (var item in order.Items)
                {
                    if (item.Ingredient == null) continue; // Defensive: skip if ingredient is missing

                    itemsTable.Rows.Add(
                        item.Ingredient.Name,
                        item.Quantity,
                        item.Ingredient.Unit,
                        item.Ingredient.UnitPrice,
                        item.ExpirationDate
                    );
                }

                // Debug: Check the number of items
                if (itemsTable.Rows.Count > 0)
                {
                    MessageBox.Show("Found " + itemsTable.Rows.Count + " items.");
                }
                else
                {
                    MessageBox.Show("No export items found.");
                }

                // Display items in the DataGridView
                dgvExportItems.DataSource = itemsTable;
                dgvExportItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvExportItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvExportItems.ReadOnly = true;

                // Set whitespace-separated column headers
                dgvExportItems.Columns["IngredientName"].HeaderText = "Ingredient Name";
                dgvExportItems.Columns["UnitPrice"].HeaderText = "Unit Price ($)";
                dgvExportItems.Columns["ExpirationDate"].HeaderText = "Expiration Date";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading order details: " + ex.Message);
            }
        }


    }
}
