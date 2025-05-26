using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CoffeeCompanyMS.Forms.Authentication;
using CoffeeCompanyMS.Patterns;
using CoffeeCompanyMS.Models;
using System.Data.SqlClient;

namespace CoffeeCompanyMS.UC
{
    public partial class StoreBranchSelector : UserControl
    {
        public event EventHandler<Guid> SelectedItemChanged;

        private List<Guid> locationIds;

        public Guid SelectedValue { get; private set; }

        public StoreBranchSelector()
        {
            InitializeComponent();
        }

        private void StoreBranchSelector_Load(object sender, EventArgs e)
        {
            LoadStoreBranches();
            SelectedValue = Guid.Empty;
        }

        private void LoadStoreBranches()
        {
            try
            {
                var locations = DAOManager.Instance.LocationDAO.GetAllLocations()
                    .Where(l => l.LocationIndex > 0)
                    .ToList();

                if (locations == null || locations.Count == 0)
                {
                    MessageBox.Show("No store branches found.");
                    return;
                }

                List<string> locationNames = new List<string>();
                locationIds = new List<Guid>();

                foreach (var location in locations)
                {
                    locationNames.Add(location.Name);
                    locationIds.Add(location.Id);
                }

                SetLocations(locationNames, locationIds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting Store Branches: " + ex.Message);
            }
        }

        public void SetLocations(IEnumerable<string> locations, IEnumerable<Guid> locationids)
        {
            cbbLocation.Items.Clear();
            cbbLocation.Items.AddRange(locations.ToArray());
            this.locationIds = locationids.ToList();
        }

        public void SetSelectedLocationId(Guid locationId)
        {
            if (locationIds != null)
            {
                int index = locationIds.IndexOf(locationId);
                if (index >= 0 && index < cbbLocation.Items.Count)
                {
                    cbbLocation.SelectedIndex = index;
                    SelectedValue = locationId;
                }
            }
        }

        private void cbbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbLocation.SelectedIndex >= 0 && locationIds != null && cbbLocation.SelectedIndex < locationIds.Count)
            {
                var selectedLocationId = locationIds[cbbLocation.SelectedIndex];
                SelectedValue = selectedLocationId;
                SelectedItemChanged?.Invoke(this, selectedLocationId);
            }
        }

        public void Disable()
        {
            cbbLocation.Enabled = false;
        }
    }
} 