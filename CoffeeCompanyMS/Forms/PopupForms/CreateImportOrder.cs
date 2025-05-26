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
using System.Data.SqlClient;

namespace CoffeeCompanyMS.UI.Import
{
    public partial class CreateImportOrder : Form
    {
        private Guid selectedLocationID;
        private List<TransferOrderItem> orderItems;
        private DataTable ingredientTable;

        public CreateImportOrder()
        {
            InitializeComponent();
            orderItems = new List<TransferOrderItem>();
            InitializeDataGridView();
            LoadIngredients();
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

            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ExpirationDate",
                HeaderText = "Expiration Date",
                DataPropertyName = "ExpirationDate"
            });
        }

        private void LoadIngredients()
        {
            try
            {
                var ingredientDAO = DAOManager.Instance.IngredientDAO;
                var ingredients = ingredientDAO.GetAllIngredients();

                ingredientTable = new DataTable();
                ingredientTable.Columns.Add("ID", typeof(Guid));
                ingredientTable.Columns.Add("Name", typeof(string));
                ingredientTable.Columns.Add("Unit", typeof(string));
                ingredientTable.Columns.Add("Quantity", typeof(int));
                ingredientTable.Columns.Add("ExpirationDate", typeof(DateTime));

                foreach (var ingredient in ingredients)
                {
                    ingredientTable.Rows.Add(
                        ingredient.Id,
                        ingredient.Name,
                        ingredient.Unit,
                        0,
                        DateTime.Now.AddMonths(1)
                    );
                }

                dataGridViewIngredients.DataSource = ingredientTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ingredients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DTPDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            labeldate.Text = DTPDeliveryDate.Value.ToString("yyyy-MM-dd");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedLocationID == Guid.Empty)
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
                    destinationID: selectedLocationID
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
                    MessageBox.Show("Import order created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create import order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating import order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void locationSelector1_SelectedItemChanged(object sender, Guid value)
        {
            selectedLocationID = value;
        }
    }
}
