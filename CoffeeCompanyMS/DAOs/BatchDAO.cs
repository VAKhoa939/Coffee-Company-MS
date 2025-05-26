using CoffeeCompanyMS.DTOs;
using CoffeeCompanyMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompanyMS.DAOs
{
    internal class BatchDAO : BaseDAO
    {
        private readonly IngredientDAO ingredientDAO;

        public BatchDAO(IngredientDAO ingredientDAO)
        {
            this.ingredientDAO = ingredientDAO;
        }

        public List<Batch> GetAllBatches()
        {
            string query = "SELECT * FROM Batch";
            return ExecuteQuery(query, reader => new Batch(reader, ingredientDAO.GetIngredientById));
        }

        public List<Batch> GetBatchesByLocationID(Guid locationId)
        {
            string query = "SELECT * FROM Batch WHERE LocationID = @LocationID";
            var parameters = new Dictionary<string, object> { ["@LocationID"] = locationId };

            return ExecuteQuery(query, reader => new Batch(reader, ingredientDAO.GetIngredientById), parameters);
        }

        public Batch GetBatchById(Guid id)
        {
            string query = "SELECT * FROM Batch WHERE ID = @ID";
            var parameters = new Dictionary<string, object> { ["@ID"] = id };

            var result = ExecuteQuery(query, reader => new Batch(reader, ingredientDAO.GetIngredientById), parameters);
            return result.Count > 0 ? result[0] : null;
        }

        public bool InsertBatch(int quantity, DateTime receiptDate, DateTime expirationDate, Guid ingredientId, Guid locationId)
        {
            string query = @"
        INSERT INTO Batch (Quantity, ReceiptDate, ExpirationDate, IngredientID, LocationID)
        VALUES (@Quantity, @ReceiptDate, @ExpirationDate, @IngredientID, @LocationID)";

            var parameters = new Dictionary<string, object>
            {
                ["@Quantity"] = quantity,
                ["@ReceiptDate"] = receiptDate,
                ["@ExpirationDate"] = expirationDate,
                ["@IngredientID"] = ingredientId,
                ["@LocationID"] = locationId
            };

            return ExecuteNonQuery(query, parameters);
        }

        public bool UpdateBatch(Batch batch)
        {
            string query = @"
        UPDATE Batch
        SET Quantity = @Quantity,
            ReceiptDate = @ReceiptDate,
            ExpirationDate = @ExpirationDate
        WHERE ID = @ID";

            var parameters = new Dictionary<string, object>
            {
                ["@ID"] = batch.Id,
                ["@Quantity"] = batch.Quantity,
                ["@ReceiptDate"] = batch.ReceiptDate,
                ["@ExpirationDate"] = batch.ExpirationDate
            };

            return ExecuteNonQuery(query, parameters);
        }

        public bool DeleteBatch(Guid id)
        {
            string query = "DELETE FROM Batch WHERE ID = @ID";
            var parameters = new Dictionary<string, object> { ["@ID"] = id };

            return ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Retrieves a summary of ingredients by location
        /// with details like the number of batches, total quantity, 
        /// unit, and closest expiration date.
        /// </summary>
        public List<BatchSummaryDTO> GetIngredientSummariesByLocation(Guid locationId)
        {
            string query = @"
                SELECT 
                    I.Name AS IngredientName,
                    COUNT(B.ID) AS NumberOfBatches,
                    SUM(B.Quantity) AS TotalQuantity,
                    I.Unit,
                    MIN(B.ExpirationDate) AS ClosestExpirationDate
                FROM Ingredient I
                JOIN Batch B ON I.ID = B.IngredientID
                WHERE 
                    B.LocationID = @LocationID
                    AND B.ExpirationDate >= CAST(GETDATE() AS DATE)
                GROUP BY 
                    I.Name, I.Unit
                HAVING 
                    COUNT(B.ID) > 0";

            var parameters = new Dictionary<string, object>
            {
                ["@LocationID"] = locationId
            };

            return ExecuteQuery(query, reader => new BatchSummaryDTO(reader), parameters);
        }

        /// <summary>
        /// Retrieves a list of batch details for a specific ingredient by its name
        /// with details like quantity, unit, receipt date, and expiration date.
        /// </summary>
        public List<BatchDetailsDTO> GetBatchViewsByIngredientName(string ingredientName)
        {
            string query = @"
                SELECT 
                    b.ID AS BatchID,
                    i.Name AS IngredientName,
                    b.Quantity,
                    i.Unit,
                    b.ReceiptDate,
                    b.ExpirationDate
                FROM Batch b
                JOIN Ingredient i ON b.IngredientID = i.ID
                WHERE 
                    i.Name = @IngredientName
                    AND b.Quantity > 0";

            var parameters = new Dictionary<string, object>
            {
                { "@IngredientName", ingredientName }
            };

            return ExecuteQuery(query, reader => new BatchDetailsDTO(reader), parameters);
        }

        /// <summary>
        /// Gets a list of wastage events for ingredients at a specific location.
        /// with details like expiration date, name, quantity, and unit.
        /// </summary>
        public List<IngredientEventDTO> GetWastageEventsByLocationId(Guid locationId)
        {
            string query = @"
            SELECT B.ExpirationDate, I.Name, B.Quantity, I.Unit
            FROM Batch B
            JOIN Ingredient I ON B.IngredientID = I.ID
            WHERE B.LocationID = @LocationID AND B.Quantity > 0 AND B.ExpirationDate < CAST(GETDATE() AS DATE)";

            var parameters = new Dictionary<string, object>
            {
                ["@LocationID"] = locationId
            };

            return ExecuteQuery(query, reader => new IngredientEventDTO(reader, "Wastage"), parameters);
        }

        public List<Batch> GetBatchesByIngredientAndLocation(Guid ingredientId, Guid locationId)
        {
            string query = @"
                SELECT b.* 
                FROM Batch b
                WHERE b.IngredientID = @IngredientID 
                AND b.LocationID = @LocationID
                AND b.Quantity > 0
                ORDER BY b.ExpirationDate ASC";

            var parameters = new Dictionary<string, object>
            {
                ["@IngredientID"] = ingredientId,
                ["@LocationID"] = locationId
            };

            return ExecuteQuery(query, reader => new Batch(reader));
        }

        public bool UpdateBatchQuantity(Guid batchId, int newQuantity)
        {
            string query = @"
                UPDATE Batch 
                SET Quantity = @Quantity 
                WHERE ID = @ID";

            var parameters = new Dictionary<string, object>
            {
                ["@ID"] = batchId,
                ["@Quantity"] = newQuantity
            };

            return ExecuteNonQuery(query, parameters);
        }
    }
}