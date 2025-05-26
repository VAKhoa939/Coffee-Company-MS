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
        private Dictionary<Guid, List<Batch>> ingredientBatches;

        public CreateExportOrder()
        {
            InitializeComponent();
            orderItems = new List<TransferOrderItem>();
            ingredientBatches = new Dictionary<Guid, List<Batch>>();
            InitializeDataGridView();
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

            dataGridViewIngredients.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "AvailableBatches",
                HeaderText = "Available Batches",
                DataPropertyName = "AvailableBatches",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                ReadOnly = false
            });
        }

        private void LoadIngredients()
        {
            try
            {
                var ingredientDAO = DAOManager.Instance.IngredientDAO;
                var batchDAO = DAOManager.Instance.BatchDAO;
                var ingredients = batchDAO.GetIngredientSummariesByLocation(sourceLocationID);

                ingredientTable = new DataTable();
                ingredientTable.Columns.Add("ID", typeof(Guid));
                ingredientTable.Columns.Add("Name", typeof(string));
                ingredientTable.Columns.Add("Unit", typeof(string));
                ingredientTable.Columns.Add("Quantity", typeof(int));
                ingredientTable.Columns.Add("AvailableBatches", typeof(List<string>));

                foreach (var ingredient in ingredients)
                {
                    // Get available batches for this ingredient at the warehouse
                    var batches = batchDAO.GetBatchesByIngredientAndLocation(ingredient.IngredientId, sourceLocationID)
                        .Where(b => b.Quantity > 0)
                        .Select(b => 
                        {
                            return $"{b.Id} {b.Quantity} {b.ExpirationDate:dd/MM/yyyy}";
                        })
                        .ToList();

                    var result = $"Ingredient: {ingredient.IngredientName}, Batches: {string.Join(", ", batches)}";
                    MessageBox.Show(result, "Ingredient Batches", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ingredientTable.Rows.Add(
                        ingredient.IngredientId,
                        ingredient.IngredientName,
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
                    MessageBox.Show("Please select a source warehouse.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (destinationLocationID == Guid.Empty)
                {
                    MessageBox.Show("Please select a destination store.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Add items to transfer order and update batches
                var batchDAO = DAOManager.Instance.BatchDAO;
                foreach (DataRow row in ingredientTable.Rows)
                {
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    if (quantity > 0)
                    {
                        var ingredientId = (Guid)row["ID"];
                        var ingredient = DAOManager.Instance.IngredientDAO.GetIngredientById(ingredientId);

                        // Get selected batch from AvailableBatches column
                        string selectedBatch = row["AvailableBatches"].ToString();
                        if (string.IsNullOrEmpty(selectedBatch))
                        {
                            MessageBox.Show($"Please select a batch for ingredient: {ingredient.Name}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Parse batch ID from the selected batch string
                        Guid batchId = Guid.Parse(selectedBatch.Split(' ')[0]);

                        // Create transfer order item
                        var item = new TransferOrderItem(
                            id: Guid.NewGuid(),
                            quantity: quantity,
                            expirationDate: ingredientBatches[ingredientId].First(b => b.Id == batchId).ExpirationDate,
                            ingredient: ingredient
                        );
                        transferOrder.Items.Add(item);

                        // Update batch quantity
                        var batch = batchDAO.GetBatchById(batchId);
                        batch.Quantity = 0; // Set to 0 since we're transferring all available quantity
                        bool batchUpdated = batchDAO.UpdateBatch(batch);
                        if (!batchUpdated)
                        {
                            MessageBox.Show($"Failed to update batch for ingredient: {ingredient.Name}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                if (transferOrder.Items.Count == 0)
                {
                    MessageBox.Show("Please add at least one item to the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get DAO instances
                var transferOrderDAO = DAOManager.Instance.TransferOrderDAO;
                var transferOrderItemDAO = DAOManager.Instance.TransferOrderItemDAO;

                // Insert transfer order
                bool orderSuccess = transferOrderDAO.InsertTransferOrder(transferOrder);
                if (!orderSuccess)
                {
                    MessageBox.Show("Failed to create export order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert transfer order items
                bool allItemsSuccess = true;
                foreach (var item in transferOrder.Items)
                {
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
