using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompanyMS.DTOs
{
    internal class BatchSummaryDTO
    {
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int NumberOfBatches { get; set; }
        public int TotalQuantity { get; set; }
        public string Unit { get; set; }
        public DateTime ClosestExpirationDate { get; set; }

        public BatchSummaryDTO(SqlDataReader reader)
        {
            IngredientId = Guid.Parse(reader["IngredientID"].ToString());
            IngredientName = reader["IngredientName"].ToString();
            NumberOfBatches = Convert.ToInt32(reader["NumberOfBatches"]);
            TotalQuantity = Convert.ToInt32(reader["TotalQuantity"]);
            Unit = reader["Unit"].ToString();
            ClosestExpirationDate = Convert.ToDateTime(reader["ClosestExpirationDate"]);
        }
    }
}
