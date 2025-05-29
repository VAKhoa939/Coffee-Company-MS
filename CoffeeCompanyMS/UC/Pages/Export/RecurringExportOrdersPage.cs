using CoffeeCompanyMS.DTOs;
using CoffeeCompanyMS.Forms.Authentication;
using CoffeeCompanyMS.Models;
using CoffeeCompanyMS.Patterns;
using CoffeeCompanyMS.UI;
using CoffeeCompanyMS.UI.Export;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CoffeeCompanyMS.UC.Pages.Export
{
    public partial class RecurringExportOrdersPage : UserControl
    {
        private Guid selectedLocationID;
        public Action<Guid> MoveToDetaisPage { get; set; }

        public RecurringExportOrdersPage()
        {
            InitializeComponent();

            locationSelector1.SelectedItemChanged += (s, value) =>
            {
                selectedLocationID = value;
                LoadRecurringExportOrders();
            };
        }

        private void RecurringExportOrdersPage_Load(object sender, EventArgs e)
        {
            selectedLocationID = locationSelector1.SelectedValue;
            if (selectedLocationID != Guid.Empty)
            {
                LoadRecurringExportOrders();
            }
        }

        private void LoadRecurringExportOrders()
        {
            try
            {
                var transferOrderDAO = DAOManager.Instance.TransferOrderDAO;
                List<RecurringExportOrderDTO> exportOrders = transferOrderDAO.GetActiveRecurringExportOrders(selectedLocationID);

                // Create a DataTable for display
                DataTable dt = new DataTable();
                dt.Columns.Add("RecurrenceID", typeof(Guid));
                dt.Columns.Add("DestinationName", typeof(string));
                dt.Columns.Add("RecurrencePeriod", typeof(int));
                dt.Columns.Add("LatestOrderID", typeof(Guid));
                dt.Columns.Add("LatestOrderDate", typeof(DateTime));
                dt.Columns.Add("EstimatedNextOrderDate", typeof(DateTime));

                foreach (var order in exportOrders)
                {
                    dt.Rows.Add(
                        order.RecurrenceID,
                        order.DestinationName,
                        order.RecurrencePeriod,
                        order.LatestOrderID,
                        order.LatestOrderDate,
                        order.EstimatedNextOrderDate
                    );
                }

                // Bind the data to the DataGridView
                dataGridViewRecurring.DataSource = dt;

                if (dataGridViewRecurring.Columns.Count > 0)
                {
                    dataGridViewRecurring.Columns["RecurrenceID"].HeaderText = "Recurrence ID";
                    dataGridViewRecurring.Columns["DestinationName"].HeaderText = "Destination";
                    dataGridViewRecurring.Columns["RecurrencePeriod"].HeaderText = "Recurrence Period (days)";
                    dataGridViewRecurring.Columns["LatestOrderID"].HeaderText = "Latest Order ID";
                    dataGridViewRecurring.Columns["LatestOrderDate"].HeaderText = "Latest Order Date";
                    dataGridViewRecurring.Columns["EstimatedNextOrderDate"].HeaderText = "Estimated Next Order Date";
                    AddCancelBtnColumn();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading recurring export orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AddCancelBtnColumn()
        {
            if (!dataGridViewRecurring.Columns.Contains("Cancel"))
            {
                DataGridViewButtonColumn cancelButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Cancel",
                    HeaderText = "",
                    Text = "Cancel",
                    UseColumnTextForButtonValue = true
                };
                dataGridViewRecurring.Columns.Add(cancelButtonColumn);
            }
        }

        private void dataGridViewRecurring_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewRecurring.Columns["Cancel"].Index)
            {
                var selectedRow = dataGridViewRecurring.Rows[e.RowIndex];
                Guid orderID = Guid.Parse(selectedRow.Cells["LatestOrderID"].Value.ToString());
                DialogResult result = MessageBox.Show("Are you sure you want to cancel this recurring order?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    CancelRecurringOrder(orderID);
                    LoadRecurringExportOrders(); // Refresh the list after cancellation
                }
            }
        }

        private void CancelRecurringOrder(Guid orderId)
        {
            try
            {
                var transferOrderDAO = DAOManager.Instance.TransferOrderDAO;

                TransferOrder order = transferOrderDAO.GetTransferOrderById(orderId);

                if (order == null)
                    throw new Exception("Transfer order not found.");

                if (order.Status == "RecurringStopped" || order.Status == "Canceled")
                    throw new Exception("This transfer order is already canceled or recurrence already stopped.");

                order.Status = "RecurringStopped";
                transferOrderDAO.UpdateTransferOrder(order);

                MessageBox.Show("Recurring order cancelled successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error canceling recurring order: " + ex.Message, "Cancel Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateExportOrder createExportOrder = new CreateExportOrder();
            createExportOrder.ShowDialog();
        }

        private void dataGridViewRecurring_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridViewRecurring.Rows[e.RowIndex];
                Guid orderID = Guid.Parse(selectedRow.Cells["LatestOrderID"].Value.ToString());
                MoveToDetaisPage(orderID);
            }
        }
    }
}
