using CoffeeCompanyMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CoffeeCompanyMS.DAOs
{
    internal class TransferOrderItemDAO : BaseDAO
    {
        private readonly IngredientDAO ingredientDAO;

        public TransferOrderItemDAO(IngredientDAO ingredientDAO)
        {
            this.ingredientDAO = ingredientDAO;
        }

        public List<TransferOrderItem> GetAllTransferOrderItems()
        {
            string query = "SELECT * FROM TransferOrderItem";
            return ExecuteQuery(query, reader => new TransferOrderItem(reader, ingredientDAO.GetIngredientById));
        }

        public TransferOrderItem GetTransferOrderItemById(Guid id)
        {
            string query = "SELECT * FROM TransferOrderItem WHERE ID = @ID";
            var parameters = new Dictionary<string, object> { ["@ID"] = id };

            var result = ExecuteQuery(query, reader => new TransferOrderItem(reader, ingredientDAO.GetIngredientById), parameters);
            return result.Count > 0 ? result[0] : null;
        }

        public List<TransferOrderItem> GetItemsByTransferOrderId(Guid transferOrderId)
        {
            string query = "SELECT * FROM TransferOrderItem WHERE TransferOrderID = @TransferOrderID";
            var parameters = new Dictionary<string, object> { ["@TransferOrderID"] = transferOrderId };

            return ExecuteQuery(query, reader => new TransferOrderItem(reader, ingredientDAO.GetIngredientById), parameters);
        }

        public bool InsertTransferOrderItem(int quantity, DateTime expirationDate, Guid transferOrderId, Guid ingredientId, SqlTransaction transaction = null)
        {
            string query = @"
                INSERT INTO TransferOrderItem (Quantity, ExpirationDate, TransferOrderID, IngredientID)
                VALUES (@Quantity, @ExpirationDate, @TransferOrderID, @IngredientID)";

            var parameters = new Dictionary<string, object>
            {
                ["@Quantity"] = quantity,
                ["@ExpirationDate"] = expirationDate,
                ["@TransferOrderID"] = transferOrderId,
                ["@IngredientID"] = ingredientId
            };

            return ExecuteNonQuery(query, parameters, transaction);
        }

        public bool UpdateTransferOrderItem(TransferOrderItem item)
        {
            string query = @"
        UPDATE TransferOrderItem
        SET Quantity = @Quantity,
            ExpirationDate = @ExpirationDate
        WHERE ID = @ID";

            var parameters = new Dictionary<string, object>
            {
                ["@ID"] = item.Id,
                ["@Quantity"] = item.Quantity,
                ["@ExpirationDate"] = item.ExpirationDate
            };

            return ExecuteNonQuery(query, parameters);
        }

        public bool DeleteTransferOrderItem(Guid id)
        {
            string query = "DELETE FROM TransferOrderItem WHERE ID = @ID";
            var parameters = new Dictionary<string, object> { ["@ID"] = id };

            return ExecuteNonQuery(query, parameters);
        }
    }
}
