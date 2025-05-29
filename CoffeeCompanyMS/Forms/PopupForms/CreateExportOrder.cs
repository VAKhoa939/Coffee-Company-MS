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

namespace CoffeeCompanyMS.UI.Export
{
    public partial class CreateExportOrder : Form
    {
        private Guid sourceLocationID;
        private Guid destinationLocationID;
        private DataTable ingredientTable;
        private Dictionary<Guid, List<Batch>> ingredientBatches;

        public CreateExportOrder()
        {
            InitializeComponent();
            ingredientBatches = new Dictionary<Guid, List<Batch>>();
        }

        private void CreateExportOrder_Load(object sender, EventArgs e)
        {
            SetupEventHandlers();
            InitializeDataGridView();
        }

        private void SetupEventHandlers()
        {
            warehouseSelector1.SelectedItemChanged += (s, value) =>
            {
                sourceLocationID = value;
                if (sourceLocationID != Guid.Empty)
                {
                    LoadIngredients();
                }
            };

            storeBranchSelector1.SelectedItemChanged += (s, value) =>
            {
                destinationLocationID = value;
            };

            checkBox1.CheckedChanged += (s, e) =>
            {
                numericUpDown1.Enabled = checkBox1.Checked;
            };

            dataGridViewIngredients.EditingControlShowing += DataGridViewIngredients_EditingControlShowing;
        }

        private void InitializeDataGridView()
        {
            dataGridViewIngredients.Columns.Clear();
            dataGridViewIngredients.AutoGenerateColumns = false;

            // Checkbox column
            var checkCol = new DataGridViewCheckBoxColumn
            {
                Name = "Selected",
                HeaderText = "",
                DataPropertyName = "Selected",
                Width = 30
            };
            dataGridViewIngredients.Columns.Add(checkCol);

            // IngredientID
            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IngredientID",
                HeaderText = "IngredientID",
                DataPropertyName = "IngredientID",
                ReadOnly = true,
                Visible = false // Hide this column as it's not needed for display
            });

            // IngredientName
            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IngredientName",
                HeaderText = "Ingredient Name",
                DataPropertyName = "IngredientName",
                ReadOnly = true
            });

            // TotalQuantity
            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalQuantity",
                HeaderText = "Total Quantity",
                DataPropertyName = "TotalQuantity",
                ReadOnly = true
            });

            // Unit
            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Unit",
                HeaderText = "Unit",
                DataPropertyName = "Unit",
                ReadOnly = true
            });

            // AvailableBatches (ComboBox)
            var batchColumn = new DataGridViewComboBoxColumn
            {
                Name = "AvailableBatches",
                HeaderText = "Available Batches",
                DataPropertyName = "AvailableBatches",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                ReadOnly = false
            };
            dataGridViewIngredients.Columns.Add(batchColumn);

            // BatchID
            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BatchID",
                HeaderText = "Batch ID",
                DataPropertyName = "BatchID",
                ReadOnly = true
            });

            // BatchQuantity
            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BatchQuantity",
                HeaderText = "Batch Quantity",
                DataPropertyName = "BatchQuantity",
                ReadOnly = true
            });

            // ExpirationDate
            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ExpirationDate",
                HeaderText = "Expiration Date",
                DataPropertyName = "ExpirationDate",
                ReadOnly = true
            });

            dataGridViewIngredients.Rows.Clear();
        }

        private void LoadIngredients()
        {
            try
            {
                var batchDAO = DAOManager.Instance.BatchDAO;
                var ingredients = batchDAO.GetIngredientSummariesByLocation(sourceLocationID);

                ingredientTable = new DataTable();
                ingredientTable.Columns.Add("Selected", typeof(bool));
                ingredientTable.Columns.Add("IngredientID", typeof(Guid));
                ingredientTable.Columns.Add("IngredientName", typeof(string));
                ingredientTable.Columns.Add("TotalQuantity", typeof(int));
                ingredientTable.Columns.Add("Unit", typeof(string));
                ingredientTable.Columns.Add("AvailableBatches", typeof(string));
                ingredientTable.Columns.Add("BatchID", typeof(Guid));
                ingredientTable.Columns.Add("BatchQuantity", typeof(int));
                ingredientTable.Columns.Add("ExpirationDate", typeof(DateTime));

                foreach (var ingredient in ingredients)
                {
                    var batches = batchDAO.GetBatchesByIngredientAndLocation(ingredient.IngredientId, sourceLocationID)
                        .Where(b => b.Quantity > 0)
                        .ToList();

                    ingredientBatches[ingredient.IngredientId] = batches;

                    // Prepare batch display strings for ComboBox
                    var batchDisplayList = batches.Select(b =>
                        $"{b.Id} {b.Quantity} {b.ExpirationDate:dd/MM/yyyy}").ToList();

                    // Add row with default values
                    ingredientTable.Rows.Add(
                        false,
                        ingredient.IngredientId,
                        ingredient.IngredientName,
                        ingredient.TotalQuantity,
                        ingredient.Unit,
                        "", // ComboBox will be set up below
                        Guid.Empty,
                        0,
                        DBNull.Value
                    );
                }

                dataGridViewIngredients.DataSource = ingredientTable;
                dataGridViewIngredients.Enabled = true;

                // Set up ComboBox items for each row
                for (int i = 0; i < ingredients.Count; i++)
                {
                    var row = dataGridViewIngredients.Rows[i];
                    var ingredientId = (Guid)ingredientTable.Rows[i]["IngredientID"];
                    var batches = ingredientBatches[ingredientId];

                    var comboBox = (DataGridViewComboBoxCell)row.Cells["AvailableBatches"];
                    comboBox.Items.Clear();
                    foreach (var batch in batches)
                    {
                        comboBox.Items.Add($"{batch.Id} {batch.Quantity} {batch.ExpirationDate:dd/MM/yyyy}");
                    }
                    // Set selected index to -1 (no selection)
                    comboBox.Value = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ingredients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridViewIngredients_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridViewIngredients.CurrentCell is DataGridViewComboBoxCell &&
                dataGridViewIngredients.CurrentCell.OwningColumn.Name == "AvailableBatches")
            {
                var comboBox = e.Control as ComboBox;
                if (comboBox != null)
                {
                    comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                    comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                }
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null) return;

            var cell = dataGridViewIngredients.CurrentCell as DataGridViewComboBoxCell;
            if (cell == null) return;

            int rowIndex = cell.RowIndex;
            if (rowIndex < 0) return;

            var row = dataGridViewIngredients.Rows[rowIndex];
            if (comboBox.SelectedItem == null) return;

            // Set checkbox to true
            row.Cells["Selected"].Value = true;

            // Parse batch info
            string selected = comboBox.SelectedItem.ToString();
            var parts = selected.Split(' ');
            if (parts.Length >= 3)
            {
                Guid batchId = Guid.Parse(parts[0]);
                int batchQty = int.Parse(parts[1]);
                DateTime expDate = DateTime.ParseExact(parts[2], "dd/MM/yyyy", null);

                row.Cells["BatchID"].Value = batchId;
                row.Cells["BatchQuantity"].Value = batchQty;
                row.Cells["ExpirationDate"].Value = expDate;
            }
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

                var locationDAO = DAOManager.Instance.LocationDAO;
                var destination = locationDAO.GetLocationById(destinationLocationID);

                var transferOrder = new TransferOrder(
                    id: Guid.NewGuid(),
                    orderDate: DateTime.Now,
                    estimatedDeliveryDate: DTPDeliveryDate.Value,
                    actualDeliveryDate: null,
                    status: "Pending",
                    recurrenceID: checkBox1.Checked ? Guid.NewGuid() : Guid.Empty,
                    recurrencePeriod: checkBox1.Checked ? (int)numericUpDown1.Value : 0,
                    items: new List<TransferOrderItem>(),
                    destination: destination
                );

                var batchDAO = DAOManager.Instance.BatchDAO;
                List<Batch> updatedBatches = new List<Batch>();

                foreach (DataGridViewRow row in dataGridViewIngredients.Rows)
                {
                    if (row.IsNewRow) continue;
                    bool isSelected = row.Cells["Selected"].Value is bool b && b;
                    if (!isSelected) continue;

                    Guid ingredientId = (Guid)row.Cells["IngredientID"].Value;
                    string ingredientName = row.Cells["IngredientName"].Value.ToString();
                    string unit = row.Cells["Unit"].Value.ToString();
                    int totalQty = Convert.ToInt32(row.Cells["TotalQuantity"].Value);

                    Guid batchId = row.Cells["BatchID"].Value is Guid g ? g : Guid.Empty;
                    int batchQty = Convert.ToInt32(row.Cells["BatchQuantity"].Value);
                    DateTime expDate = row.Cells["ExpirationDate"].Value is DateTime dt ? dt : DateTime.MinValue;

                    if (batchId == Guid.Empty)
                    {
                        MessageBox.Show($"Please select a batch for ingredient: {ingredientName}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // For this example, export all available quantity from the batch
                    var ingredient = DAOManager.Instance.IngredientDAO.GetIngredientById(ingredientId);

                    var item = new TransferOrderItem(
                        id: Guid.NewGuid(),
                        quantity: batchQty,
                        expirationDate: expDate,
                        ingredient: ingredient
                    );
                    transferOrder.Items.Add(item);

                    // Update batch quantity to 0 (exported)
                    var batch = batchDAO.GetBatchById(batchId);
                    batch.Quantity = 0;
                    updatedBatches.Add(batch);
                }

                if (transferOrder.Items.Count == 0)
                {
                    MessageBox.Show("Please select at least one batch to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var transferOrderDAO = DAOManager.Instance.TransferOrderDAO;
                bool orderSuccess = transferOrderDAO.InsertTransferOrder(transferOrder);
                if (!orderSuccess)
                {
                    MessageBox.Show("Failed to create export order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update all batches
                foreach (var batch in updatedBatches)
                {
                    bool batchUpdated = batchDAO.UpdateBatch(batch);
                    if (!batchUpdated)
                    {
                        MessageBox.Show($"Failed to update batch: {batch.Id}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Export order created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating export order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
