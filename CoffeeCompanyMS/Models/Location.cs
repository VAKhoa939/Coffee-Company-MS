using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompanyMS.Models
{
    internal class Location
    {
        private Guid id;
        private int locationIndex;
        private string address;
        private decimal maintenanceCost;
        private string name;
        private List<Batch> batches;

        // Constructor to create a Location object with specified values
        public Location(Guid id, int locationIndex, string address, decimal maintenanceCost, string name, List<Batch> batches)
        {
            this.id = id;
            this.locationIndex = locationIndex;
            this.address = address;
            this.maintenanceCost = maintenanceCost;
            this.name = name ?? string.Empty;
            this.batches = batches ?? new List<Batch>();

            // Set the name based on the location index
            this.name = locationIndex >= 0 ? $"Warehouse {-1 * locationIndex}" : $"Store Branch {locationIndex}";
        }

        // Constructor to create a Location object from a SqlDataReader and batch loader delegate
        public Location(SqlDataReader reader, Func<Guid, List<Batch>> loadBatches)
        {
            this.id = Guid.Parse(reader["ID"].ToString());
            this.locationIndex = Convert.ToInt32(reader["LocationIndex"]);
            this.address = reader["Address"].ToString() ?? string.Empty;
            this.maintenanceCost = Convert.ToDecimal(reader["MaintenanceCost"]);

            // Set the name based on the location index
            this.name = locationIndex >= 0 ? $"Warehouse {-1 * locationIndex}" : $"Store Branch {locationIndex}";

            // Load batches using the delegate
            this.batches = loadBatches(id) ?? new List<Batch>();
        }

        public Guid Id
        {
            get => id;
            set => id = value;
        }

        public int LocationIndex
        {
            get => locationIndex;
            set => locationIndex = value;
        }

        public string Address
        {
            get => address;
            set => address = value;
        }

        public decimal MaintenanceCost
        {
            get => maintenanceCost;
            set => maintenanceCost = value;
        }

        public string Name
        {
            get => name;
            set => name = value ?? string.Empty;
        }

        public List<Batch> Batches
        {
            get => batches;
            set => batches = value;
        }

        // Method to add a batch to the location
        public void AddBatch(Batch batch)
        {
            if (batch != null && !batches.Any(b => b.Id == batch.Id))
            {
                batches.Add(batch);
            }
        }

        // Method to remove a batch from the location
        public void RemoveBatch(Batch batch)
        {
            if (batch != null)
            {
                batches.RemoveAll(b => b.Id == batch.Id);
            }
        }
    }
}
