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

        private void LoadIngredients()
        {
            try
            {
                if (sourceLocationID == Guid.Empty) return;

                var batchDAO = DAOManager.Instance.BatchDAO;
                var ingredients = batchDAO.GetIngredientSummariesByLocation(sourceLocationID);

                ingredientTable = new DataTable();
                ingredientTable.Columns.Add("Name", typeof(string));
                ingredientTable.Columns.Add("Unit", typeof(string));
                ingredientTable.Columns.Add("Quantity", typeof(int));

                foreach (var ingredient in ingredients)
                {
                    ingredientTable.Rows.Add(
                        ingredient.IngredientName,
                        ingredient.Unit,
                        ingredient.TotalQuantity
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
                    MessageBox.Show("Please select a destination store branch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Add items to transfer order
                foreach (DataRow row in ingredientTable.Rows)
                {
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    if (quantity > 0)
                    {
                        var ingredient = DAOManager.Instance.IngredientDAO.GetIngredientById((Guid)row["ID"]);
                        var item = new TransferOrderItem(
                            id: Guid.NewGuid(),
                            quantity: quantity,
                            expirationDate: Convert.ToDateTime(row["ExpirationDate"]),
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

                // Save transfer order
                var transferOrderDAO = DAOManager.Instance.TransferOrderDAO;
                bool success = transferOrderDAO.InsertTransferOrder(transferOrder);

                if (success)
                {
                    MessageBox.Show("Export order created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create export order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating export order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
