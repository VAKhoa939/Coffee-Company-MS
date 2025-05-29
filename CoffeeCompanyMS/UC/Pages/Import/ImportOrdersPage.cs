using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CoffeeCompanyMS.DAOs;
using CoffeeCompanyMS.DTOs;
using CoffeeCompanyMS.Forms.Authentication;
using CoffeeCompanyMS.Models;
using CoffeeCompanyMS.Patterns;

namespace CoffeeCompanyMS.UC.Pages.Import
{
    public partial class ImportOrdersPage : UserControl
    {
        private Guid selectedLocationID;
        public Action<Guid> MoveToDetaisPage { get; set; }

        public ImportOrdersPage()
        {
            InitializeComponent();

            locationSelector1.SelectedItemChanged += (s, value) =>
            {
                selectedLocationID = value;
                LoadImportOrders();
            };
        }

        private void ImportOrdersPage_Load(object sender, EventArgs e)
        {
            selectedLocationID = locationSelector1.SelectedValue;
            if (selectedLocationID != Guid.Empty)
            {
                LoadImportOrders();
            }
        }

        private void LoadImportOrders()
        {
            try
            {
                TransferOrderDAO dao = DAOManager.Instance.TransferOrderDAO;
                List<ImportOrderDTO> orders = dao.GetImportOrdersByLocation(selectedLocationID);

                dataGridViewImportOrder.DataSource = orders;

                if (dataGridViewImportOrder.Columns.Count > 0)
                {
                    dataGridViewImportOrder.Columns["OrderID"].HeaderText = "Order ID";
                    dataGridViewImportOrder.Columns["RecurrenceID"].HeaderText = "Recurrence ID";
                    dataGridViewImportOrder.Columns["SupplierName"].HeaderText = "Supplier Name";
                    dataGridViewImportOrder.Columns["OrderDate"].HeaderText = "Order Date";
                    dataGridViewImportOrder.Columns["EstimatedDeliveryDate"].HeaderText = "Estimated Delivery";
                    dataGridViewImportOrder.Columns["ActualDeliveryDate"].HeaderText = "Actual Delivery";

                    ReplaceStatusColumnWithComboBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading import orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ReplaceStatusColumnWithComboBox()
        {
            try
            {
                // Remove current Status column with the combobox column
                int statusColumnIndex = dataGridViewImportOrder.Columns["Status"].Index;

                dataGridViewImportOrder.Columns.Remove("Status");

                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
                {
                    Name = "Status",
                    HeaderText = "Status",
                    DataPropertyName = "Status",
                    FlatStyle = FlatStyle.Flat
                };

                comboBoxColumn.Items.AddRange(new object[]
                {
                    "Pending", "Delayed", "Delivered", "Confirmed"
                });

                dataGridViewImportOrder.Columns.Insert(statusColumnIndex, comboBoxColumn);


                // Set the selected values for each comboboxes
                foreach (DataGridViewRow row in dataGridViewImportOrder.Rows)
                {
                    if (row.Cells["Status"] is DataGridViewComboBoxCell cell && row.DataBoundItem is ImportOrderDTO dto)
                    {
                        if (!string.IsNullOrEmpty(dto.Status))
                        {
                            cell.Value = dto.Status;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error setting up Status ComboBox: " + ex.Message);
            }
        }

        private void dataGridViewImportOrder_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridViewImportOrder.CurrentCell.ColumnIndex == dataGridViewImportOrder.Columns["Status"].Index)
            {
                if (e.Control is ComboBox cb)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
        }


        /// <summary>
        /// Handles the CellValueChanged event of the dataGridViewImportOrder control,
        /// in order to change the status of the order when the user selects a new status from the ComboBox.
        /// </summary>
        private void dataGridViewImportOrder_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                // Ensure the sender is a DataGridView
                DataGridView dgv = sender as DataGridView;

                // Check if the column is the Status column
                string columnName = dgv.Columns[e.ColumnIndex].Name;
                if (columnName != "Status") return;

                DataGridViewRow row = dgv.Rows[e.RowIndex];

                object orderIdObj = row.Cells["OrderID"].Value;
                object newStatusObj = row.Cells["Status"].Value;

                // Validate that orderIdObj and newStatusObj are not null
                if (orderIdObj == null || newStatusObj == null)
                {
                    MessageBox.Show("Order ID or new status is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Guid orderId = Guid.Parse(orderIdObj.ToString());
                string newStatus = newStatusObj.ToString();

                // Update the order status
                if (!UpdateOrderStatus(orderId, newStatus))
                {
                    MessageBox.Show("Failed to update order status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Order status updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Update the DataGridView to reflect the change
                LoadImportOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while changing status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool UpdateOrderStatus(Guid transferOrderId, string newStatus)
        {
            var transferOrderDAO = DAOManager.Instance.TransferOrderDAO;
            var batchDAO = DAOManager.Instance.BatchDAO;
            var destinationLocationId = selectedLocationID;

            // Step 1: Retrieve the transfer order
            var transferOrder = transferOrderDAO.GetTransferOrderById(transferOrderId);
            if (transferOrder == null)
                return false;

            // Step 2: Update status
            transferOrder.Status = newStatus;
            bool updateSuccess = transferOrderDAO.UpdateTransferOrder(transferOrder);
            if (!updateSuccess)
                return false;

            // Step 3: If status is "Delivered", insert batches from transfer order items
            if (newStatus == "Delivered")
            {
                DateTime receiptDate = DateTime.Now;
                foreach (var item in transferOrder.Items)
                {
                    bool insertSuccess = batchDAO.InsertBatch(
                        quantity: item.Quantity,
                        receiptDate: receiptDate,
                        expirationDate: item.ExpirationDate,
                        ingredientId: item.Ingredient.Id,
                        locationId: destinationLocationId
                    );

                    if (!insertSuccess)
                        return false; // Exit on first failure
                }
            }

            return true;
        }


        private void dataGridViewImportOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridViewImportOrder.Rows[e.RowIndex];
                Guid orderID = Guid.Parse(selectedRow.Cells["OrderID"].Value.ToString());
                MoveToDetaisPage(orderID);
            }
        }
    }
}
