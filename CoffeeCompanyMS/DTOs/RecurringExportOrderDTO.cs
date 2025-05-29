using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompanyMS.DTOs
{
    internal class RecurringExportOrderDTO
    {
        public Guid RecurrenceID { get; }
        public string DestinationName { get; }
        public int RecurrencePeriod { get; }
        public Guid LatestOrderID { get; }
        public DateTime LatestOrderDate { get; }
        public DateTime EstimatedNextOrderDate { get; }

        public RecurringExportOrderDTO(SqlDataReader reader)
        {
            RecurrenceID = Guid.Parse(reader["RecurrenceID"].ToString());
            DestinationName = reader["DestinationName"].ToString();
            RecurrencePeriod = Convert.ToInt32(reader["RecurrencePeriod"]);
            LatestOrderID = Guid.Parse(reader["LatestOrderID"].ToString());
            LatestOrderDate = Convert.ToDateTime(reader["LatestOrderDate"]);
            EstimatedNextOrderDate = Convert.ToDateTime(reader["EstimatedNextOrderDate"]);
        }
    }
}
