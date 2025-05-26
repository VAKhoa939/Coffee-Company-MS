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
using CoffeeCompanyMS.Forms.Authentication;

namespace CoffeeCompanyMS.UI.Import
{
    public partial class CreateImportOrder : Form
    {
        private Guid selectedLocationID;
        private Guid selectedSupplierID;
        private List<TransferOrderItem> orderItems;
        private DataTable ingredientTable;

        public CreateImportOrder()
        {
            InitializeComponent();
            orderItems = new List<TransferOrderItem>();
            InitializeDataGridView();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            // Location selection handler
            locationSelector1.SelectedItemChanged += (s, value) =>
            {
                selectedLocationID = value;
                if (selectedLocationID != Guid.Empty)
                {
                    LoadSuppliers();
                }
            };

            // Supplier selection handler
            comboBoxSuppliers.SelectedIndexChanged += (s, e) =>
            {
                if (comboBoxSuppliers.SelectedValue != null)
                {
                    selectedSupplierID = (Guid)comboBoxSuppliers.SelectedValue;
                    LoadIngredients();
                }
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

            dataGridViewIngredients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ExpirationDate",
                HeaderText = "Expiration Date",
                DataPropertyName = "ExpirationDate"
            });
        }

        private void LoadSuppliers()
        {
            try
            {
                var supplierDAO = DAOManager.Instance.SupplierDAO;
                var suppliers = supplierDAO.GetAllSuppliers();

                comboBoxSuppliers.DisplayMember = "Name";
                comboBoxSuppliers.ValueMember = "Id";
                comboBoxSuppliers.DataSource = suppliers;
                comboBoxSuppliers.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadIngredients()
        {
            try
            {
                if (selectedSupplierID == Guid.Empty) return;

                var ingredientDAO = DAOManager.Instance.IngredientDAO;
                var ingredients = ingredientDAO.GetIngredientsBySupplierId(selectedSupplierID);

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
                dataGridViewIngredients.Enabled = true;
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

                if (selectedSupplierID == Guid.Empty)
                {
                    MessageBox.Show("Please select a supplier.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Get DAO instances
                var transferOrderDAO = DAOManager.Instance.TransferOrderDAO;
                var transferOrderItemDAO = DAOManager.Instance.TransferOrderItemDAO;

                // Insert transfer order
                bool orderSuccess = transferOrderDAO.InsertTransferOrder(transferOrder);
                if (!orderSuccess)
                {
                    MessageBox.Show("Failed to create import order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Import order created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating import order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
