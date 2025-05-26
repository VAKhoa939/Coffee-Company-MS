using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeCompanyMS.Models;
using CoffeeCompanyMS.Patterns;
using CoffeeCompanyMS.DAOs;
using CoffeeCompanyMS.UC;

namespace CoffeeCompanyMS.UI.Export
{
    public partial class CreateExportOrder : Form
    {
        private Guid sourceLocationID;
        private Guid destinationLocationID;
        private List<TransferOrderItem> orderItems;
        private DataTable ingredientTable;

        public CreateExportOrder()
        {
            InitializeComponent();
            orderItems = new List<TransferOrderItem>();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            // Source location (warehouse) selection handler
            warehouseSelector1.SelectedItemChanged += (s, value) =>
            {
                sourceLocationID = value;
                if (sourceLocationID != Guid.Empty)
                {
                    LoadIngredients();
                }
            };

            // Destination location (store branch) selection handler
            storeBranchSelector1.SelectedItemChanged += (s, value) =>
            {
                destinationLocationID = value;
            };

            // Recurrence checkbox handler
            checkBox1.CheckedChanged += (s, e) =>
            {
                numericUpDown1.Enabled = checkBox1.Checked;
            };
        }

        private void InitializeDataGridView()
        {
            dataGridViewIngredients.Columns.Clear();
            dataGridViewIngredients.AutoGenerateColumns = false;

            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IngredientName",
                HeaderText = "Ingredient Name",
                DataPropertyName = "Name",
                ReadOnly = true
            });

            dataGridViewIngredients.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "AvailableBatches",
                HeaderText = "Available Batches",
                DataPropertyName = "AvailableBatches",
                DisplayMember = "DisplayText",
                ValueMember = "BatchId"
            });

            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Quantity",
                DataPropertyName = "Quantity"
            });

            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Unit",
                HeaderText = "Unit",
                DataPropertyName = "Unit",
                ReadOnly = true
            });
        }

        private void LoadIngredients()
        {
            try
            {
                if (sourceLocationID == Guid.Empty) return;

                var ingredientDAO = DAOManager.Instance.IngredientDAO;
                var batchDAO = DAOManager.Instance.BatchDAO;
                var ingredients = ingredientDAO.GetAllIngredients();

                ingredientTable = new DataTable();
                ingredientTable.Columns.Add("ID", typeof(Guid));
                ingredientTable.Columns.Add("Name", typeof(string));
                ingredientTable.Columns.Add("Unit", typeof(string));
                ingredientTable.Columns.Add("Quantity", typeof(int));
                ingredientTable.Columns.Add("AvailableBatches", typeof(List<BatchDisplayInfo>));

                foreach (var ingredient in ingredients)
                {
                    // Get available batches for this ingredient at the warehouse
                    var batches = batchDAO.GetBatchesByIngredientAndLocation(ingredient.Id, sourceLocationID)
                        .Where(b => b.Quantity > 0)
                        .Select(b => new BatchDisplayInfo
                        {
                            BatchId = b.Id,
                            DisplayText = $"{b.Id} {b.Quantity} {b.ExpirationDate:dd/MM/yyyy}"
                        })
                        .ToList();

                    ingredientTable.Rows.Add(
                        ingredient.Id,
                        ingredient.Name,
                        ingredient.Unit,
                        0,
                        batches
                    );
                }

                dataGridViewIngredients.DataSource = ingredientTable;
                dataGridViewIngredients.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ingredients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class BatchDisplayInfo
        {
            public Guid BatchId { get; set; }
            public string DisplayText { get; set; }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (sourceLocationID == Guid.Empty)
                {
                    MessageBox.Show("Please select a source location.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (destinationLocationID == Guid.Empty)
                {
                    MessageBox.Show("Please select a destination location.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create transfer order
                var transferOrder = new TransferOrder(
                    id: Guid.NewGuid(),
                    orderDate: DateTime.Now,
                    estimatedDeliveryDate: DTPDeliveryDate.Value,
                    actualDeliveryDate: null,
                    status: "Pending",
                    recurrenceID: checkBox1.Checked ? Guid.NewGuid() : Guid.Empty,
                    recurrencePeriod: checkBox1.Checked ? (int)numericUpDown1.Value : 0,
                    items: new List<TransferOrderItem>(),
                    destinationID: destinationLocationID
                );

                // Get DAO instances
                var transferOrderDAO = DAOManager.Instance.TransferOrderDAO;
                var transferOrderItemDAO = DAOManager.Instance.TransferOrderItemDAO;
                var batchDAO = DAOManager.Instance.BatchDAO;

                // Add items to transfer order
                foreach (DataGridViewRow row in dataGridViewIngredients.Rows)
                {
                    var quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    if (quantity > 0)
                    {
                        var ingredientId = (Guid)row.Cells["ID"].Value;
                        var selectedBatch = (BatchDisplayInfo)((DataGridViewComboBoxCell)row.Cells["AvailableBatches"]).Value;
                        
                        if (selectedBatch == null)
                        {
                            MessageBox.Show($"Please select a batch for ingredient: {row.Cells["IngredientName"].Value}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Get the batch to check quantity
                        var batch = batchDAO.GetBatchById(selectedBatch.BatchId);
                        if (batch == null || batch.Quantity < quantity)
                        {
                            MessageBox.Show($"Insufficient quantity in selected batch for ingredient: {row.Cells["IngredientName"].Value}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var ingredient = DAOManager.Instance.IngredientDAO.GetIngredientById(ingredientId);
                        var item = new TransferOrderItem(
                            id: Guid.NewGuid(),
                            quantity: quantity,
                            expirationDate: batch.ExpirationDate,
                            ingredient: ingredient
                        );
                        transferOrder.Items.Add(item);
                    }
                }

                if (transferOrder.Items.Count == 0)
                {
                    MessageBox.Show("Please add at least one item to the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert transfer order
                bool orderSuccess = transferOrderDAO.InsertTransferOrder(transferOrder);
                if (!orderSuccess)
                {
                    MessageBox.Show("Failed to create export order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert transfer order items and update batch quantities
                bool allItemsSuccess = true;
                foreach (var item in transferOrder.Items)
                {
                    // Insert transfer order item
                    bool itemSuccess = transferOrderItemDAO.InsertTransferOrderItem(
                        quantity: item.Quantity,
                        expirationDate: item.ExpirationDate,
                        transferOrderId: transferOrder.Id,
                        ingredientId: item.Ingredient.Id
                    );

                    if (!itemSuccess)
                    {
                        allItemsSuccess = false;
                        break;
                    }

                    // Update batch quantity
                    var selectedBatch = ((DataGridViewComboBoxCell)dataGridViewIngredients.Rows
                        .Cast<DataGridViewRow>()
                        .First(r => (Guid)r.Cells["ID"].Value == item.Ingredient.Id)
                        .Cells["AvailableBatches"]).Value as BatchDisplayInfo;

                    if (selectedBatch != null)
                    {
                        var batch = batchDAO.GetBatchById(selectedBatch.BatchId);
                        if (batch != null)
                        {
                            bool updateSuccess = batchDAO.UpdateBatchQuantity(batch.Id, batch.Quantity - item.Quantity);
                            if (!updateSuccess)
                            {
                                allItemsSuccess = false;
                                break;
                            }
                        }
                    }
                }

                if (allItemsSuccess)
                {
                    MessageBox.Show("Export order created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add items to export order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating export order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
