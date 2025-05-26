using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompanyMS.Models
{
    internal class TransferOrder
    {
        private Guid id;
        private DateTime orderDate;
        private DateTime estimatedDeliveryDate;
        private DateTime? actualDeliveryDate;
        private string status;
        private Guid recurrenceID;
        private int recurrencePeriod;
        private List<TransferOrderItem> items;
        private Guid destinationID;

        // Constructor to create a TransferOrder object with specified values
        public TransferOrder(Guid id, DateTime orderDate, DateTime estimatedDeliveryDate, DateTime? actualDeliveryDate, string status, Guid recurrenceID, int recurrencePeriod, List<TransferOrderItem> items, Guid destinationID)
        {
            this.id = id;
            this.orderDate = orderDate;
            this.estimatedDeliveryDate = estimatedDeliveryDate;
            this.actualDeliveryDate = actualDeliveryDate;
            this.status = status;
            this.recurrenceID = recurrenceID;
            this.recurrencePeriod = recurrencePeriod;
            this.items = items ?? new List<TransferOrderItem>();
            this.destinationID = destinationID;
        }

        // Updated constructor to accept a loader function for Items
        public TransferOrder(SqlDataReader reader, Func<Guid, List<TransferOrderItem>> loadItems)
        {
            id = Guid.Parse(reader["ID"].ToString());
            orderDate = Convert.ToDateTime(reader["OrderDate"]);
            estimatedDeliveryDate = Convert.ToDateTime(reader["EstimatedDeliveryDate"]);
            actualDeliveryDate = reader["ActualDeliveryDate"] == DBNull.Value
                ? (DateTime?)null
                : Convert.ToDateTime(reader["ActualDeliveryDate"]);
            status = reader["Status"].ToString();
            if (!Guid.TryParse(reader["RecurrenceID"].ToString(), out Guid recurrenceID))
            {
                recurrenceID = Guid.Empty; // Handle case where RecurrenceID is not present or invalid
            }
            recurrencePeriod = Convert.ToInt32(reader["RecurrencePeriod"]);

            // Load the list of TransferOrderItem objects for this TransferOrder
            items = loadItems(id) ?? new List<TransferOrderItem>();
        }

        public Guid Id { get => id; set => id = value; }
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public DateTime EstimatedDeliveryDate { get => estimatedDeliveryDate; set => estimatedDeliveryDate = value; }
        public DateTime? ActualDeliveryDate { get => actualDeliveryDate; set => actualDeliveryDate = value; }
        public string Status { get => status; set => status = value; }
        public Guid RecurrenceID { get => recurrenceID; set => recurrenceID = value; }
        public int RecurrencePeriod { get => recurrencePeriod; set => recurrencePeriod = value; }
        public List<TransferOrderItem> Items { get => items; set => items = value; }
        public Guid DestinationID
        {
            get => destinationID;
            set => destinationID = value;
        }

        // Method to add an item to the TransferOrder
        public void AddItem(TransferOrderItem item)
        {
            if (item != null && !items.Any(i => i.Id == item.Id))
            {
                items.Add(item);
            }
        }

        // Method to remove an item from the TransferOrder
        public void RemoveItem(Guid itemId)
        {
            var itemToRemove = items.FirstOrDefault(i => i.Id == itemId);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }
    }
}
