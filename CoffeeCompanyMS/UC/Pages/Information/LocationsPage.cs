using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using CoffeeCompanyMS.Patterns;
using CoffeeCompanyMS.UI.Information;
using CoffeeCompanyMS.Models;

namespace CoffeeCompanyMS.UC.Pages.Information
{
    public partial class LocationsPage : UserControl
    {
        Guid selectedLocationId;

        public LocationsPage()
        {
            InitializeComponent();
        }

        private void LocationsPage_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            selectedLocationId = Guid.Empty;
            LoadDgvLocations();
        }

        private void LoadDgvLocations()
        {
            // Get the DAO instance (adjust as needed for your architecture)
            var locationDAO = DAOManager.Instance.LocationDAO;
            List<Location> locations = locationDAO.GetAllLocations();

            // Create a DataTable for display
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("Location Index", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("Maintenance Cost", typeof(decimal));

            foreach (var loc in locations)
            {
                table.Rows.Add(
                    loc.Id,
                    loc.LocationIndex,
                    loc.Name,
                    loc.Address,
                    loc.MaintenanceCost
                );
            }

            dgvLocations.DataSource = table;
            dgvLocations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLocations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocations.ReadOnly = true;
        }

        private void dgvLocations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLocations.Rows[e.RowIndex];
                selectedLocationId = (Guid)row.Cells["Id"].Value;
                btnDelete.Enabled = true;
            }
            else
            {
                selectedLocationId = Guid.Empty;
                btnDelete.Enabled = false;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateLocation createLocations = new CreateLocation();
            createLocations.ShowDialog();
            if (createLocations.DialogResult == DialogResult.OK)
            {
                LoadDgvLocations();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedLocationId == Guid.Empty)
            {
                MessageBox.Show("No location selected.", "Delete Location", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete the selected location?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var locationDAO = DAOManager.Instance.LocationDAO;
                    bool success = locationDAO.DeleteLocation(selectedLocationId);

                    if (success)
                    {
                        MessageBox.Show("Location deleted successfully.", "Delete Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        selectedLocationId = Guid.Empty;
                        btnDelete.Enabled = false;
                        LoadDgvLocations();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete location.", "Delete Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting location: " + ex.Message, "Delete Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
