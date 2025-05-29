using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompanyMS.DTOs
{
    public class RecurringImportOrderDTO
    {
        public Guid RecurrenceID { get; }
        public string SupplierName { get; }
        public int RecurrencePeriod { get; }
        public Guid LatestOrderID { get; }
        public DateTime LatestOrderDate { get; }
        public DateTime EstimatedNextOrderDate { get; }

        public RecurringImportOrderDTO(SqlDataReader reader, Func<Guid, string> getSupplierName)
        {
            RecurrenceID = reader.GetGuid(reader.GetOrdinal("RecurrenceID"));
            SupplierName = getSupplierName(LatestOrderID);
            RecurrencePeriod = reader.GetInt32(reader.GetOrdinal("RecurrencePeriod"));
            LatestOrderID = reader.GetGuid(reader.GetOrdinal("LatestOrderID"));
            LatestOrderDate = reader.GetDateTime(reader.GetOrdinal("LatestOrderDate"));
            EstimatedNextOrderDate = reader.GetDateTime(reader.GetOrdinal("EstimatedNextOrderDate"));
        }
    }

}
