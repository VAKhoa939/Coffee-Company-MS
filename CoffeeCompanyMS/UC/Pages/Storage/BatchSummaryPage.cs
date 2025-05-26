using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CoffeeCompanyMS.DTOs;
using CoffeeCompanyMS.Patterns;

namespace CoffeeCompanyMS.UC.Pages.Storage
{
    public partial class BatchSummaryPage : UserControl
    {
        private Guid selectedLocationID;
        public Action<Guid, string> MoveToDetaisPage {  get; set; }

        public BatchSummaryPage()
        {
            InitializeComponent();

            // Subscribe to the SelectedItemChanged event of the location selector
            locationSelector1.SelectedItemChanged += (s, value) =>
            {
                selectedLocationID = value;
                LoadIngredients();
            };
        }

        private void BatchSummaryPage_Load(object sender, EventArgs e)
        {
            // Load ingredients for the selected location when the page loads
            selectedLocationID = locationSelector1.SelectedValue;
            if (selectedLocationID != Guid.Empty)
            {
                LoadIngredients();
            }
        }

        private void LoadIngredients()
        {
            try
            {
                var batchDAO = DAOManager.Instance.BatchDAO;
                var summaries = batchDAO.GetIngredientSummariesByLocation(selectedLocationID);

                dataGridViewBatchSummary.DataSource = new BindingList<BatchSummaryDTO>(summaries);

                if (dataGridViewBatchSummary.Columns.Count > 0)
                {
                    dataGridViewBatchSummary.Columns["IngredientId"].Visible = false; // Hide the ID column
                    dataGridViewBatchSummary.Columns["IngredientName"].HeaderText = "Ingredient Name";
                    dataGridViewBatchSummary.Columns["NumberOfBatches"].HeaderText = "Number Of Batches";
                    dataGridViewBatchSummary.Columns["TotalQuantity"].HeaderText = "Total Quantity";
                    dataGridViewBatchSummary.Columns["ClosestExpirationDate"].HeaderText = "Closest Expiration Date";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting Ingredients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the double-click event on the DataGridView 
        /// to navigate to the details page for the selected ingredient.
        /// </summary>
        private void dataGridViewBatchSummary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridViewBatchSummary.Rows[e.RowIndex];
                string ingredientName = selectedRow.Cells["IngredientName"].Value.ToString();
                MoveToDetaisPage(selectedLocationID, ingredientName);
            }
        }
    }
}
