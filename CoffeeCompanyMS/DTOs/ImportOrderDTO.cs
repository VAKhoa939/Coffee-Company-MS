using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompanyMS.DTOs
{
    internal class ImportOrderDTO
    {
        public Guid OrderID { get; }
        public string RecurrenceID { get; }
        public string SupplierName { get; }
        public DateTime OrderDate { get; }
        public DateTime EstimatedDeliveryDate { get; }
        public string ActualDeliveryDate { get; }
        public string Status { get; }

        public ImportOrderDTO(SqlDataReader reader, Func<Guid, string> getSupplierName)
        {
            OrderID = reader.GetGuid(reader.GetOrdinal("OrderID"));
            RecurrenceID = reader["RecurrenceID"].ToString();
            SupplierName = getSupplierName(OrderID);
            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate"));
            EstimatedDeliveryDate = reader.GetDateTime(reader.GetOrdinal("EstimatedDeliveryDate"));
            ActualDeliveryDate = reader["ActualDeliveryDate"].ToString();
            Status = reader["Status"].ToString();
        }
    }

}
